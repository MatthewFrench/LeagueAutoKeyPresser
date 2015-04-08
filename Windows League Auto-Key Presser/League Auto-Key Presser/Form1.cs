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

namespace League_Auto_Key_Presser
{
    public partial class Form1 : Form
    {
        AutoItX3 _autoIT = new AutoItX3();
        Timer timer = null;

        bool keyQPressed = false;
        bool keyWPressed = false;
        bool keyEPressed = false;
        bool keyRPressed = false;

        bool pressingSpell1 = false;
        bool pressingSpell2 = false;
        bool pressingSpell3 = false;
        bool pressingSpell4 = false;

        long pressingSpell1LastTime = 0;
        long pressingSpell2LastTime = 0;
        long pressingSpell3LastTime = 0;
        long pressingSpell4LastTime = 0;
        long pressingActiveLastTime = 0;

        long pressingWardLastTime = 0;

        long pressSpell1Interval = (long)10 * 1000000 / 100;
        long pressSpell2Interval = (long)10 * 1000000 / 100;
        long pressSpell3Interval = (long)10 * 1000000 / 100;
        long pressSpell4Interval = (long)100 * 1000000 / 100;
        long pressActiveInterval = (long)500 * 1000000 / 100;

        long pressWardInterval = (long)6000 * 1000000 / 100;

        bool autoKeyOnBool = true;
        bool active1OnBool = false;
        bool active2OnBool = false;
        bool active3OnBool = false;
        bool active5OnBool = false;
        bool active6OnBool = false;
        bool active7OnBool = false;
        bool wardOnBool = true;

        char activeKey = 'E';

        public Form1()
        {
            InitializeComponent();
            HookManager.KeyDown += HookManager_KeyDown;
            HookManager.KeyUp += HookManager_KeyUp;

            timer = new Timer();
            timer.Tick += timer_Tick;
            timer.Interval = 8;
            timer.Start();

            pressingSpell1LastTime = DateTime.Now.Ticks;
            pressingSpell2LastTime = DateTime.Now.Ticks;
            pressingSpell3LastTime = DateTime.Now.Ticks;
            pressingSpell4LastTime = DateTime.Now.Ticks;
            pressingActiveLastTime = DateTime.Now.Ticks;
            pressingWardLastTime = DateTime.Now.Ticks;

            activeKeyComboBox.Text = "E";
        }

        void timer_Tick(object sender, System.EventArgs e)
        {
            if (autoKeyOnBool)
            {
                if (pressingSpell1)
                {
                    long elapsedTime = DateTime.Now.Ticks - pressingSpell1LastTime;
                    if (elapsedTime >= pressSpell1Interval)
                    {
                        tapSpell1();
                        pressingSpell1LastTime = DateTime.Now.Ticks;
                    }
                    if (activeKey == 'Q') {
                        runActives();
                    }
                }
                if (pressingSpell2)
                {
                    long elapsedTime = DateTime.Now.Ticks - pressingSpell2LastTime;
                    if (elapsedTime >= pressSpell2Interval)
                    {
                        tapSpell2();
                        pressingSpell2LastTime = DateTime.Now.Ticks;
                    } if (activeKey == 'W')
                    {
                        runActives();
                    }
                }
                if (pressingSpell3)
                {
                    long elapsedTime = DateTime.Now.Ticks - pressingSpell3LastTime;
                    if (elapsedTime >= pressSpell3Interval)
                    {
                        tapSpell3();
                        pressingSpell3LastTime = DateTime.Now.Ticks;
                    }
                    if (activeKey == 'E')
                    {
                        runActives();
                    }
                }
                if (pressingSpell4)
                {
                    long elapsedTime = DateTime.Now.Ticks - pressingSpell4LastTime;
                    if (elapsedTime >= pressSpell4Interval)
                    {
                        tapSpell4();
                        pressingSpell4LastTime = DateTime.Now.Ticks;
                    }
                    if (activeKey == 'R')
                    {
                        runActives();
                    }
                }
            }
        }

        void runActives()
        {
            long elapsedTime = DateTime.Now.Ticks - pressingActiveLastTime;
            if (elapsedTime >= pressActiveInterval)
            {
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
                pressingActiveLastTime = DateTime.Now.Ticks;
            }
            elapsedTime = DateTime.Now.Ticks - pressingWardLastTime;
            if (elapsedTime >= pressWardInterval && wardOnBool) {
                tapWard();
                pressingWardLastTime = DateTime.Now.Ticks;
            }
        }

        void HookManager_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q)
            { //Q
                keyQPressed = false;
            }
            if (e.KeyCode == Keys.W)
            { //W
                keyWPressed = false;
            }
            if (e.KeyCode == Keys.E)
            { //E
                keyEPressed = false;
            }
            if (e.KeyCode == Keys.R)
            { //R
                keyRPressed = false;
            }
            if (e.KeyCode == Keys.Q ||
                e.KeyCode == Keys.W || e.KeyCode == Keys.E || e.KeyCode == Keys.R)
            {
                runLogicPress();
            }
        }

        void HookManager_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q)
            { //Q
                keyQPressed = true;
            }
            if (e.KeyCode == Keys.W)
            { //W
                keyWPressed = true;
            }
            if (e.KeyCode == Keys.E)
            { //E
                keyEPressed = true;
            }
            if (e.KeyCode == Keys.R)
            { //R
                keyRPressed = true;
            }
            if (e.KeyCode == Keys.Q ||
                e.KeyCode == Keys.W || e.KeyCode == Keys.E || e.KeyCode == Keys.R)
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
        }

        void tapSpell1()
        {
            pressSpell1();
            releaseSpell1();
        }
        void tapSpell2()
        {
            pressSpell2();
            releaseSpell2();
        }
        void tapSpell3()
        {
            pressSpell3();
            releaseSpell3();
        }
        void tapSpell4()
        {
            pressSpell4();
            releaseSpell4();
        }
        void tapActive1()
        {
            pressActive1();
            releaseActive1();
        }
        void tapActive2()
        {
            pressActive2();
            releaseActive2();
        }
        void tapActive3()
        {
            pressActive3();
            releaseActive3();
        }
        void tapWard()
        {
            pressWard();
            releaseWard();
        }
        void tapActive5()
        {
            pressActive5();
            releaseActive5();
        }
        void tapActive6()
        {
            pressActive6();
            releaseActive6();
        }
        void tapActive7()
        {
            pressActive7();
            releaseActive7();
        }

        void pressSpell1()
        {
            _autoIT.Send("{z down}");
        }
        void releaseSpell1()
        {
            _autoIT.Send("{z up}");
        }
        void pressSpell2()
        {
            _autoIT.Send("{x down}");
        }
        void releaseSpell2()
        {
            _autoIT.Send("{x up}");
        }
        void pressSpell3()
        {
            _autoIT.Send("{c down}");
        }
        void releaseSpell3()
        {
            _autoIT.Send("{c up}");
        }
        void pressSpell4()
        {
            _autoIT.Send("{v down}");
        }
        void releaseSpell4()
        {
            _autoIT.Send("{v up}");
        }
        void pressActive1()
        {
            _autoIT.Send("{1 down}");
        }
        void releaseActive1()
        {
            _autoIT.Send("{1 up}");
        }
        void pressActive2()
        {
            _autoIT.Send("{2 down}");
        }
        void releaseActive2()
        {
            _autoIT.Send("{2 up}");
        }
        void pressActive3()
        {
            _autoIT.Send("{3 down}");
        }
        void releaseActive3()
        {
            _autoIT.Send("{3 up}");
        }
        void pressWard()
        {
            _autoIT.Send("{4 down}");
        }
        void releaseWard()
        {
            _autoIT.Send("{4 up}");
        }
        void pressActive5()
        {
            _autoIT.Send("{5 down}");
        }
        void releaseActive5()
        {
            _autoIT.Send("{5 up}");
        }
        void pressActive6()
        {
            _autoIT.Send("{6 down}");
        }
        void releaseActive6()
        {
            _autoIT.Send("{6 up}");
        }
        void pressActive7()
        {
            _autoIT.Send("{7 down}");
        }
        void releaseActive7()
        {
            _autoIT.Send("{7 up}");
        }
        void releaseQ()
        {
            _autoIT.Send("{q up}");
        }
        void releaseW()
        {
            _autoIT.Send("{w up}");
        }
        void releaseE()
        {
            _autoIT.Send("{e up}");
        }
        void releaseR()
        {
            _autoIT.Send("{r up}");
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
            pressSpell1Interval = (long)Convert.ToInt32(qValueText.Text) * 10000000 / 100;
        }

        private void wValueText_TextChanged(object sender, EventArgs e)
        {
            pressSpell2Interval = (long)Convert.ToInt32(wValueText.Text) * 10000000 / 100;
        }

        private void eValueText_TextChanged(object sender, EventArgs e)
        {
            pressSpell3Interval = (long)Convert.ToInt32(eValueText.Text) * 10000000 / 100;
        }

        private void rValueText_TextChanged(object sender, EventArgs e)
        {
            pressSpell4Interval = (long)Convert.ToInt32(rValueText.Text) * 10000000 / 100;
        }

        private void activeValueText_TextChanged(object sender, EventArgs e)
        {
            pressActiveInterval = (long)Convert.ToInt32(activeValueText.Text) * 10000000 / 100;
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
    }
}
