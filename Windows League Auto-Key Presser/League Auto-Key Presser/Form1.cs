using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;
using System.Runtime.InteropServices;
using Gma.UserActivityMonitor;
using AutoItX3Lib;

using System.Reflection;
using System.Web;
using System.Runtime;

using System.Threading;

namespace League_Auto_Key_Presser
{
    public partial class Form1 : Form
    {
        static AutoItX3 _autoIT = new AutoItX3();
        ATimer timer = null;

        bool keyQPressed = false;
        bool keyWPressed = false;
        bool keyEPressed = false;
        bool keyRPressed = false;
        bool keyTPressed = false;

        bool pressingSpell1 = false;
        bool pressingSpell2 = false;
        bool pressingSpell3 = false;
        bool pressingSpell4 = false;

        long pressingSpell1LastTime = 0;
        long pressingSpell2LastTime = 0;
        long pressingSpell3LastTime = 0;
        long pressingSpell4LastTime = 0;
        long pressingActiveLastTime = 0;
        long wardHopLastTime = 0;
        long pressingWardLastTime = 0;

        long pressSpell1Interval = (long)10 * 10000;
        long pressSpell2Interval = (long)10 * 10000;
        long pressSpell3Interval = (long)10 * 10000;
        long pressSpell4Interval = (long)100 * 10000;
        long pressActiveInterval = (long)500 * 10000;

        long pressWardInterval = (long)6000 * 10000;

        bool autoKeyOnBool = true;
        bool active1OnBool = false;
        bool active2OnBool = false;
        bool active3OnBool = false;
        bool active5OnBool = false;
        bool active6OnBool = false;
        bool active7OnBool = false;
        bool wardOnBool = false;

        bool wardHopOn = false;
        bool qPreactivateW = false;
        bool qPreactivateE = false;
        bool qPreactivateR = false;

        bool wPreactivateQ = false;
        bool wPreactivateE = false;
        bool wPreactivateR = false;

        bool ePreactivateQ = false;
        bool ePreactivateW = false;
        bool ePreactivateR = false;

        bool rPreactivateQ = false;
        bool rPreactivateW = false;
        bool rPreactivateE = false;

        char wardHopKey = 'Q';

        char activeKey = 'E';

        [DllImport("ntdll.dll", SetLastError = true)]
        static extern int NtSetTimerResolution(int DesiredResolution, bool SetResolution, out int CurrentResolution);

        Thread qThread = new Thread(spell1Thread);
        Thread wThread = new Thread(spell2Thread);
        Thread eThread = new Thread(spell3Thread);
        Thread rThread = new Thread(spell3Thread);

        public Form1()
        {
            InitializeComponent();
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

            pressingSpell1LastTime = DateTime.Now.Ticks;
            pressingSpell2LastTime = DateTime.Now.Ticks;
            pressingSpell3LastTime = DateTime.Now.Ticks;
            pressingSpell4LastTime = DateTime.Now.Ticks;
            pressingActiveLastTime = DateTime.Now.Ticks;
            pressingWardLastTime = DateTime.Now.Ticks;

            activeKeyComboBox.Text = "E";
            wardHopKeyComboBox.Text = "Q";

            qThread.Start();
            wThread.Start();
            eThread.Start();
            rThread.Start();
        }
        //long lastTime;
        //int writeEvery = 200;
        //int currentWrite = 0;
        void timer_Tick()
        {
/*
            if (currentWrite >= writeEvery)
            {
                long elapsedTime2 = DateTime.Now.Ticks - lastTime;
                Console.WriteLine("Elapsed Time: " + elapsedTime2 / 10000.0 + " milliseconds");
                lastTime = DateTime.Now.Ticks;
                currentWrite = 0;
            }
            else
            {
                currentWrite++;
                lastTime = DateTime.Now.Ticks;
            }*/

            if (autoKeyOnBool)
            {
                //Ward hop
                if (keyTPressed && wardHopOn)
                {
                    //Place ward
                    long elapsedTime = DateTime.Now.Ticks - wardHopLastTime;
                    if (elapsedTime >= (long)1000 * 10000)
                    {
                        wardHopLastTime = DateTime.Now.Ticks;
                        tapWard();
                    }
                    //Try to hop
                    if (wardHopKey == 'Q') preactivateQ();
                    if (wardHopKey == 'W') preactivateW();
                    if (wardHopKey == 'E') preactivateE();
                    if (wardHopKey == 'R') preactivateR();
                }
                if (pressingSpell1)
                {
                    if (qPreactivateW) preactivateW();
                    if (qPreactivateE) preactivateE();
                    if (qPreactivateR) preactivateR();
                    preactivateQ();
                    if (activeKey == 'Q')
                    {
                        runActives();
                    }
                }
                if (pressingSpell2)
                {
                    if (wPreactivateQ) preactivateQ();
                    if (wPreactivateE) preactivateE();
                    if (wPreactivateR) preactivateR();
                    preactivateW();
                    if (activeKey == 'W')
                    {
                        runActives();
                    }
                }
                if (pressingSpell3)
                {
                    if (ePreactivateQ) preactivateQ();
                    if (ePreactivateW) preactivateW();
                    if (ePreactivateR) preactivateR();
                    preactivateE();
                    if (activeKey == 'E')
                    {
                        runActives();
                    }
                }
                if (pressingSpell4)
                {
                    if (rPreactivateQ) preactivateQ();
                    if (rPreactivateW) preactivateW();
                    if (rPreactivateE) preactivateE();
                    long elapsedTime = DateTime.Now.Ticks - pressingSpell4LastTime;
                    preactivateR();
                    if (activeKey == 'R')
                    {
                        runActives();
                    }
                }
            }
        }
        void preactivateQ()
        {
            long elapsedTime = DateTime.Now.Ticks - pressingSpell1LastTime;
            if (elapsedTime >= pressSpell1Interval)
            {
                pressingSpell1LastTime = DateTime.Now.Ticks;
                tapSpell1();
            }
        }
        void preactivateW()
        {
            long elapsedTime = DateTime.Now.Ticks - pressingSpell2LastTime;
            if (elapsedTime >= pressSpell2Interval)
            {
                pressingSpell2LastTime = DateTime.Now.Ticks;
                tapSpell2();
            }
        }
        void preactivateE()
        {
            long elapsedTime = DateTime.Now.Ticks - pressingSpell3LastTime;
            if (elapsedTime >= pressSpell3Interval)
            {
                pressingSpell3LastTime = DateTime.Now.Ticks;
                tapSpell3();
            }
        }
        void preactivateR()
        {
            long elapsedTime = DateTime.Now.Ticks - pressingSpell4LastTime;
            if (elapsedTime >= pressSpell4Interval)
            {
                pressingSpell4LastTime = DateTime.Now.Ticks;
                tapSpell4();
            }
        }

        void runActives()
        {
            long elapsedTime = DateTime.Now.Ticks - pressingActiveLastTime;
            if (elapsedTime >= pressActiveInterval)
            {
                pressingActiveLastTime = DateTime.Now.Ticks;
                if (active1OnBool)
                {
                    tapActive1();
                }
                if (active2OnBool)
                {
                    tapActive2();
                }
                if (active3OnBool)
                {
                    tapActive3();
                }
                if (active5OnBool)
                {
                    tapActive5();
                }
                if (active6OnBool)
                {
                    tapActive6();
                }
                if (active7OnBool)
                {
                    tapActive7();
                }
            }
            elapsedTime = DateTime.Now.Ticks - pressingWardLastTime;
            if (elapsedTime >= pressWardInterval && wardOnBool)
            {
                pressingWardLastTime = DateTime.Now.Ticks;
                tapWard();
            }
        }

        void HookManager_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            bool go = false;
            if (e.KeyCode == Keys.T && keyTPressed)
            { //T key
                keyTPressed = false;
                wardHopLastTime = 0;
                go = true;
            }
            if (e.KeyCode == Keys.Q && keyQPressed)
            { //Q
                keyQPressed = false;
                go = true;
            }
            if (e.KeyCode == Keys.W && keyWPressed)
            { //W
                keyWPressed = false;
                go = true;
            }
            if (e.KeyCode == Keys.E && keyEPressed)
            { //E
                keyEPressed = false;
                go = true;
            }
            if (e.KeyCode == Keys.R && keyRPressed)
            { //R
                keyRPressed = false;
                go = true;
            }
            if (go)
            {
                runLogicPress();
            }
        }

        void HookManager_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            bool go = false;
            if (e.KeyCode == Keys.T && !keyTPressed)
            { //T key
                keyTPressed = true;
                go = true;
            }
            if (e.KeyCode == Keys.Q && !keyQPressed)
            { //Q
                keyQPressed = true;
                go = true;
            }
            if (e.KeyCode == Keys.W && !keyWPressed)
            { //W
                keyWPressed = true;
                go = true;
            }
            if (e.KeyCode == Keys.E && !keyEPressed)
            { //E
                keyEPressed = true;
                go = true;
            }
            if (e.KeyCode == Keys.R && !keyRPressed)
            { //R
                keyRPressed = true;
                go = true;
            }
            if (go)
            {
                runLogicPress();
            }
        }
        void runLogicPress()
        {
            pressingSpell1 = keyQPressed;
            pressingSpell2 = keyWPressed;
            pressingSpell3 = keyEPressed;
            pressingSpell4 = keyRPressed;
            timer_Tick();
        }

        void tapSpell1()
        {
            spell1Send = true;
        }
        void tapSpell2()
        {
            spell2Send = true;
        }
        void tapSpell3()
        {
            spell3Send = true;
        }
        void tapSpell4()
        {
            spell4Send = true;
        }
        void tapActive1()
        {
                _autoIT.Send("1"); 
        }
        void tapActive2()
        {
                _autoIT.Send("2"); 
        }
        void tapActive3()
        {
                _autoIT.Send("3"); 
        }
        void tapWard()
        {
                _autoIT.Send("4"); 
        }
        void tapActive5()
        {
                _autoIT.Send("5"); 
        }
        void tapActive6()
        {
                _autoIT.Send("6"); 
        }
        void tapActive7()
        {
                _autoIT.Send("7"); 
        }

        private void active1On_CheckedChanged(object sender, EventArgs e)
        {
            active1OnBool = !active1OnBool;
        }

        private void active2On_CheckedChanged(object sender, EventArgs e)
        {
            active2OnBool = !active2OnBool;
        }

        private void active3On_CheckedChanged(object sender, EventArgs e)
        {
            active3OnBool = !active3OnBool;
        }

        private void autoKeyOn_CheckedChanged(object sender, EventArgs e)
        {
            autoKeyOnBool = !autoKeyOnBool;
            keyQPressed = false;
            keyWPressed = false;
            keyEPressed = false;
            keyRPressed = false;

            pressingSpell1 = false;
            pressingSpell2 = false;
            pressingSpell3 = false;
            pressingSpell4 = false;

            pressingSpell1LastTime = 0;
            pressingSpell2LastTime = 0;
            pressingSpell3LastTime = 0;
            pressingSpell4LastTime = 0;

            pressingActiveLastTime = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void qValueText_TextChanged(object sender, EventArgs e)
        {
            pressSpell1Interval = (long)Convert.ToInt32(qValueText.Text) * 10000;
        }

        private void wValueText_TextChanged(object sender, EventArgs e)
        {
            pressSpell2Interval = (long)Convert.ToInt32(wValueText.Text) * 10000;
        }

        private void eValueText_TextChanged(object sender, EventArgs e)
        {
            pressSpell3Interval = (long)Convert.ToInt32(eValueText.Text) * 10000;
        }

        private void rValueText_TextChanged(object sender, EventArgs e)
        {
            pressSpell4Interval = (long)Convert.ToInt32(rValueText.Text) * 10000;
        }

        private void activeValueText_TextChanged(object sender, EventArgs e)
        {
            pressActiveInterval = (long)Convert.ToInt32(activeValueText.Text) * 10000;
        }

        private void active5On_CheckedChanged(object sender, EventArgs e)
        {
            active5OnBool = !active5OnBool;
        }

        private void active6On_CheckedChanged(object sender, EventArgs e)
        {
            active6OnBool = !active6OnBool;
        }

        private void active7On_CheckedChanged(object sender, EventArgs e)
        {
            active7OnBool = !active7OnBool;
        }

        private void wardCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            wardOnBool = !wardOnBool;
        }

        private void activeKeyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            activeKey = activeKeyComboBox.Text.ToCharArray()[0];
        }

        private void qActivateWCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            qPreactivateW = !qPreactivateW;
        }

        private void qActivateECheckBox_CheckedChanged(object sender, EventArgs e)
        {
            qPreactivateE = !qPreactivateE;
        }

        private void qActivateRCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            qPreactivateR = !qPreactivateR;
        }

        private void wActivateQCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            wPreactivateQ = !wPreactivateQ;
        }

        private void wActivateECheckBox_CheckedChanged(object sender, EventArgs e)
        {
            wPreactivateE = !wPreactivateE;
        }

        private void wActivateRCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            wPreactivateR = !wPreactivateR;
        }

        private void eActivateQCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ePreactivateQ = !ePreactivateQ;
        }

        private void eActivateWCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ePreactivateW = !ePreactivateW;
        }

        private void eActivateRCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ePreactivateR = !ePreactivateR;
        }

        private void rActivateQCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            rPreactivateQ = !rPreactivateQ;
        }

        private void rActivateWCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            rPreactivateW = !rPreactivateW;
        }

        private void rActivateECheckBox_CheckedChanged(object sender, EventArgs e)
        {
            rPreactivateE = !rPreactivateE;
        }

        private void wardHopCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            wardHopOn = !wardHopOn;
        }

        private void wardHopKeyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            wardHopKey = wardHopKeyComboBox.Text.ToCharArray()[0];
        }

        static volatile bool spell1Send = false;
        static volatile bool spell2Send = false;
        static volatile bool spell3Send = false;
        static volatile bool spell4Send = false;

        static void spell1Thread()
        {
            while(true) {
                if (spell1Send)
                {
                    _autoIT.Send("z");
                    spell1Send = false;
                }
                Thread.Sleep(1);
            }
        }
        static void spell2Thread()
        {
            while (true)
            {
                if (spell2Send)
                {
                    _autoIT.Send("x");
                    spell2Send = false;
                }
                Thread.Sleep(1);
            }
        }
        static void spell3Thread()
        {
            while (true)
            {
                if (spell3Send)
                {
                    _autoIT.Send("c");
                    spell3Send = false;
                }
                Thread.Sleep(1);
            }
        }
        static void spell4Thread()
        {
            while (true)
            {
                if (spell4Send)
                {
                    _autoIT.Send("z");
                    spell4Send = false;
                }
                Thread.Sleep(1);
            }
        }
    }
    
}
