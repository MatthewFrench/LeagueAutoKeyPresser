using System;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Gma.UserActivityMonitor;
using AutoItX3Lib;
using System.Windows.Forms;

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
        public bool UltimateCasterOn { get; set; } = false;
        public bool OnlyRunCasterWhenProcessIsOpen { get; set; } = false;
        public string LeagueProcessName { get; set; } = "";
        public bool ElevateProcesses { get; set; } = false;

        [DllImport("ntdll.dll", SetLastError = true)]
        static extern int NtSetTimerResolution(int DesiredResolution, bool SetResolution, out int CurrentResolution);

        public UltimateCasterController(LeagueUltimateCasterForm form)
        {
            this.form = form;
            profileController = new ProfileController();
            /**
             * 
             */





            HookManager.KeyDown += HookManager_KeyDown;
            HookManager.KeyUp += HookManager_KeyUp;

            // The requested resolution in 100 ns units:
            int DesiredResolution = 1000;
            // Note: The supported resolutions can be obtained by a call to NtQueryTimerResolution()

            int CurrentResolution = 0;

            // 1. Requesting a higher resolution
            // Note: This call is similar to timeBeginPeriod.
            // However, it to to specify the resolution in 100 ns units.
            if (NtSetTimerResolution(DesiredResolution, true, out CurrentResolution) != 0)
            {
                Console.WriteLine("Setting resolution failed");
            }
            else
            {
                Console.WriteLine("CurrentResolution [100 ns units]: " + CurrentResolution);
            }

            ATimer.ElapsedTimerDelegate callback = timer_Tick;
            timer = new ATimer(3, 1, callback);
            timer.Start();
        }

        public void SelectedProfileAtIndex(int index)
        {

        }

        void timer_Tick()
        {
            //Run all spell controller and actives timer
        }

        void HookManager_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //Do all spells and actives on key up
        }

        void HookManager_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //Do all spells and actives key down
            //if (go)
            //{
            //    runLogicPress();
            //}
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
