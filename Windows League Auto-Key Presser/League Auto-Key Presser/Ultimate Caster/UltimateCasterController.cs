using System;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Security.Principal;
using System.Collections.Generic;
using Gma.System.MouseKeyHook;

namespace League_Auto_Key_Presser.Ultimate_Caster
{
    public class UltimateCasterController
    {
        LeagueUltimateCasterForm form;
        ProfileController profileController;
        public ProfileData SelectedProfile { get; set; }
        SpellController qSpellController;
        SpellController wSpellController;
        SpellController eSpellController;
        SpellController rSpellController;
        ActivesAndWardController activesAndWardController;
        RightClickController rightClickController;
        ATimer timer = null;
        public bool UltimateCasterOn { get; set; } = true;
        public bool OnlyRunCasterWhenProcessIsOpen { get; set; } = true;
        public bool OnlyRunCasterWhenProcessIsFocused { get; set; } = true;
        public string LeagueProcessName { get; set; } = "League of Legends.exe";
        public bool ElevateProcesses { get; set; } = true;
        Stopwatch checkProcessesStopwatch = new Stopwatch();
        private bool leagueProcessOpen = false;
        private bool leagueProcessIsFocused = false;

        private IKeyboardMouseEvents m_Events;

        [DllImport("ntdll.dll", SetLastError = true)]
        static extern int NtSetTimerResolution(int DesiredResolution, bool SetResolution, out int CurrentResolution);
        [DllImport("ntdll.dll", SetLastError = true)]
        static extern int NtQueryTimerResolution(out ulong MinimumResolution, out ulong MaximumResolution, out ulong CurrentResolution);
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();

        public UltimateCasterController(LeagueUltimateCasterForm form)
        {
            this.form = form;
            profileController = new ProfileController();
            SelectedProfile = profileController.Profiles[0];
            qSpellController = new SpellController(this, 'Q');
            wSpellController = new SpellController(this, 'W');
            eSpellController = new SpellController(this, 'E');
            rSpellController = new SpellController(this, 'R');
            activesAndWardController = new ActivesAndWardController(this);
            rightClickController = new RightClickController(this);
            activesAndWardController.SetRightClickController(rightClickController);
            rightClickController.SetActivesController(activesAndWardController);
            rightClickController.SetQSpellController(qSpellController);
            rightClickController.SetWSpellController(wSpellController);
            rightClickController.SetESpellController(eSpellController);
            rightClickController.SetRSpellController(rSpellController);
            qSpellController.SetActivesController(activesAndWardController);
            wSpellController.SetActivesController(activesAndWardController);
            eSpellController.SetActivesController(activesAndWardController);
            rSpellController.SetActivesController(activesAndWardController);
            activesAndWardController.SetQSpellController(qSpellController);
            activesAndWardController.SetWSpellController(wSpellController);
            activesAndWardController.SetESpellController(eSpellController);
            activesAndWardController.SetRSpellController(rSpellController);
            SpellController[] spellControllerArray = new SpellController[] { qSpellController, wSpellController, eSpellController, rSpellController };
            foreach (SpellController spellController in spellControllerArray)
            {
                spellController.SetSpellController('Q', qSpellController);
                spellController.SetSpellController('W', wSpellController);
                spellController.SetSpellController('E', eSpellController);
                spellController.SetSpellController('R', rSpellController);
            }
            qSpellController.UpdateWithSpellData(SelectedProfile.QSpellData);
            wSpellController.UpdateWithSpellData(SelectedProfile.WSpellData);
            eSpellController.UpdateWithSpellData(SelectedProfile.ESpellData);
            rSpellController.UpdateWithSpellData(SelectedProfile.RSpellData);

            checkProcessesStopwatch.Start();

            m_Events = Hook.GlobalEvents();
            m_Events.KeyDown += HookManager_KeyDown;
            m_Events.KeyUp += HookManager_KeyUp;

            m_Events.MouseDown += HookManager_MouseDown;
            m_Events.MouseUp += HookManager_MouseUp;

            // The requested resolution in 100 ns units:
            int DesiredResolution = 1000;
            // Note: The supported resolutions can be obtained by a call to NtQueryTimerResolution()

            ulong minResolution;
            ulong maxResolution;
            ulong currentResolution;
            var test = NtQueryTimerResolution(out minResolution, out maxResolution, out currentResolution);

            int CurrentResolution = 0;

            DesiredResolution = (int)maxResolution;

            // 1. Requesting a higher resolution
            // Note: This call is similar to timeBeginPeriod.
            // However, it to to specify the resolution in 100 ns units.
            if (NtSetTimerResolution(DesiredResolution, true, out CurrentResolution) != 0)
            {
                Console.WriteLine("Setting resolution failed");
            }
            else
            {
                Console.WriteLine("CurrentResolution [100 ns units]: " + CurrentResolution + " = " + (CurrentResolution * 100.0 / 1000000.0) + "ms");
            }

            ATimer.ElapsedTimerDelegate callback = timer_Tick;
            timer = new ATimer(3, 1, callback);
            timer.Start();
        }

        public void CheckAndElevateProcesses()
        {
            if (checkProcessesStopwatch.Elapsed.TotalSeconds < 0.5)
            {
                return;
            }
            Process[] processlist = Process.GetProcesses();
            List<Process> foundProcesses = new List<Process>();
            foreach (Process process in processlist)
            {
                if (process.ProcessName.ToLower().Equals(LeagueProcessName.ToLower()) || process.ProcessName.ToLower().Equals(LeagueProcessName.ToLower().Replace(".exe", "")))
                {
                    foundProcesses.Add(process);
                }
            }
            //Console.WriteLine("Process: {0} ID: {1}", theprocess.ProcessName, theprocess.Id);
            leagueProcessOpen = foundProcesses.Count > 0;

            //Now elevate self and League
            var currentProcess = Process.GetCurrentProcess();
            if (currentProcess.PriorityClass != ProcessPriorityClass.AboveNormal)
            {
                currentProcess.PriorityClass = ProcessPriorityClass.AboveNormal;
            }
            var activatedHandle = GetForegroundWindow();
            bool isFocused = false;
            foreach (Process process in foundProcesses)
            {
                try
                {
                    if (process.PriorityBoostEnabled == false)
                    {
                        process.PriorityBoostEnabled = true;
                    }
                    if (process.PriorityClass != ProcessPriorityClass.High)
                    {
                        process.PriorityClass = ProcessPriorityClass.High;
                    }
                    if (activatedHandle == process.MainWindowHandle)
                    {
                        isFocused = true;
                    }
                }
                catch (Exception) { }
            }
            leagueProcessIsFocused = isFocused;

            checkProcessesStopwatch.Restart();
        }

        public bool IsOn()
        {
            return UltimateCasterOn && 
                (!OnlyRunCasterWhenProcessIsOpen || (OnlyRunCasterWhenProcessIsOpen && leagueProcessOpen && 
                    (!OnlyRunCasterWhenProcessIsFocused || (OnlyRunCasterWhenProcessIsFocused && leagueProcessIsFocused))));
        }

        public static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        public void RestartIfNotAdministrator()
        {
            if (!UltimateCasterController.IsAdministrator())
            {
                // Restart program and run as admin
                var exeName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                ProcessStartInfo startInfo = new ProcessStartInfo(exeName);
                startInfo.Verb = "runas";
                Task.Delay(0).ContinueWith(t =>
                {
                    System.Diagnostics.Process.Start(startInfo);
                    Application.Exit();
                });
                form.Hide();
            }
        }

        public void SetSelectedProfileAtIndex(int index)
        {
            SelectedProfile = profileController.Profiles[index];
            //Update the variables in all the controllers
            qSpellController.UpdateWithSpellData(SelectedProfile.QSpellData);
            wSpellController.UpdateWithSpellData(SelectedProfile.WSpellData);
            eSpellController.UpdateWithSpellData(SelectedProfile.ESpellData);
            rSpellController.UpdateWithSpellData(SelectedProfile.RSpellData);
            //Update the user interface with the data
            form.UpdateProfileInterface();
        }

        void timer_Tick()
        {
            CheckAndElevateProcesses();

            //Run all spell controller and actives timer
            qSpellController.TimerTick();
            wSpellController.TimerTick();
            eSpellController.TimerTick();
            rSpellController.TimerTick();
            activesAndWardController.OnTimerTick();
            rightClickController.TimerTick();
        }

        void HookManager_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //Do all spells and actives on key up
            if (e.KeyCode == Keys.Q)
            {
                qSpellController.OnKeyUp();
            }
            else if (e.KeyCode == Keys.W)
            {
                wSpellController.OnKeyUp();
            }
            else if (e.KeyCode == Keys.E)
            {
                eSpellController.OnKeyUp();
            }
            else if (e.KeyCode == Keys.R)
            {
                rSpellController.OnKeyUp();
            } else if (e.KeyCode == Keys.T)
            {
                activesAndWardController.WardHopOnKeyUp();
            }
        }

        void HookManager_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            bool triggeredKeyDown = false;
            if (e.KeyCode == Keys.Q)
            {
                triggeredKeyDown = qSpellController.OnKeyDown();
            }
            else if (e.KeyCode == Keys.W)
            {
                triggeredKeyDown = wSpellController.OnKeyDown();
            }
            else if (e.KeyCode == Keys.E)
            {
                triggeredKeyDown = eSpellController.OnKeyDown();
            }
            else if (e.KeyCode == Keys.R)
            {
                triggeredKeyDown = rSpellController.OnKeyDown();
            }
            else if (e.KeyCode == Keys.T)
            {
                triggeredKeyDown = activesAndWardController.WardHopOnKeyDown();
            }
            //Do a timer tick for immediate responsiveness
            if (triggeredKeyDown)
            {
                runLogicPress();
            }
        }
        
        private void HookManager_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                rightClickController.OnMouseUp();
            }
        }

        private void HookManager_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (rightClickController.OnMouseDown())
                {
                    runLogicPress();
                }
            }
        }

        void runLogicPress()
        {
            timer_Tick();
        }


        public void StopTimer()
        {
            timer.Stop();
        }

        public ProfileController GetProfileController()
        {
            return profileController;
        }
    }
}