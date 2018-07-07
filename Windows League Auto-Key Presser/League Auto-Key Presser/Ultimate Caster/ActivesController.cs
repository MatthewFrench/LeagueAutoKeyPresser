using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace League_Auto_Key_Presser.Ultimate_Caster
{
    class ActivesController
    {
        static AutoItX3 _autoIT = new AutoItX3();
        bool keyTPressed = false;
        Stopwatch activeStopwatch = Stopwatch.StartNew();
        Stopwatch wardHopStopwatch = Stopwatch.StartNew();
        Stopwatch pressingWardStopwatch = Stopwatch.StartNew();
        int pressActiveInterval = 500;

        int pressWardInterval = 6000;
        bool active1OnBool = false;
        bool active2OnBool = false;
        bool active3OnBool = false;
        bool active5OnBool = false;
        bool active6OnBool = false;
        bool active7OnBool = false;
        bool wardOnBool = false;
        bool wardHopOn = false;
        char wardHopKey = 'Q';

        char activeKey = 'E';

        public void RunActives()
        {
            if (activeStopwatch.ElapsedMilliseconds >= pressActiveInterval)
            {
                activeStopwatch.Restart();
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
            if (pressingWardStopwatch.ElapsedMilliseconds >= pressWardInterval && wardOnBool)
            {
                pressingWardStopwatch.Restart();
                tapWard();
            }
        }

        public void OnTimerTick()
        {
            //Ward hop
            if (keyTPressed && wardHopOn)
            {
                //Place ward
                if (wardHopStopwatch.ElapsedMilliseconds >= 1000)
                {
                    wardHopStopwatch.Restart();
                    tapWard();
                }
                //Try to hop
                if (wardHopKey == 'Q') preactivateQ(pressSpell1Interval);
                if (wardHopKey == 'W') preactivateW(pressSpell2Interval);
                if (wardHopKey == 'E') preactivateE(pressSpell3Interval);
                if (wardHopKey == 'R') preactivateR(pressSpell4Interval);
            }
        }

        public void OnKeyUp()
        {

            if (e.KeyCode == Keys.T && keyTPressed)
            { //T key
                keyTPressed = false;
                wardHopStopwatch.Restart();
                //go = true;
            }
        }

        public void OnKeyDown()
        {
            //if (e.KeyCode == Keys.T && !keyTPressed)
            //{ //T key
                keyTPressed = true;
            //    go = true;
            //}
        }
        void tapActive1()
        {
            Task.Factory.StartNew(() =>
            {
                _autoIT.Send("1");
            });
        }
        void tapActive2()
        {
            Task.Factory.StartNew(() =>
            {
                _autoIT.Send("2");
            });
        }
        void tapActive3()
        {
            Task.Factory.StartNew(() =>
            {
                _autoIT.Send("3");
            });
        }
        void tapWard()
        {
            Task.Factory.StartNew(() =>
            {
                _autoIT.Send("4");
            });
        }
        void tapActive5()
        {
            Task.Factory.StartNew(() =>
            {
                _autoIT.Send("5");
            });
        }
        void tapActive6()
        {
            Task.Factory.StartNew(() =>
            {
                _autoIT.Send("6");
            });
        }
        void tapActive7()
        {
            Task.Factory.StartNew(() => {
                _autoIT.Send("7");
            });
        }
    }
}
