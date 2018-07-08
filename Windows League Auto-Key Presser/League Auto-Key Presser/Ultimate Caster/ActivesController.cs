using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoItX3Lib;
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

        char activeKey = 'E';

        Dictionary<char, SpellController> spellControllers = new Dictionary<char, SpellController>();
        HashSet<char> wardHopPreactives = new HashSet<char>();

        public void SetSpellController(char key, SpellController spellController)
        {
            spellControllers.Add(key, spellController);
        }

        public void EnableWardHopPreactive(char key)
        {
            wardHopPreactives.Add(key);
        }

        public void DisableWardHopPreactive(char key)
        {
            wardHopPreactives.Remove(key);
        }

        public void RunActives()
        {
            if (activeStopwatch.ElapsedMilliseconds >= pressActiveInterval)
            {
                activeStopwatch.Restart();
                if (active1OnBool)
                {
                    TapActive1();
                }
                if (active2OnBool)
                {
                    TapActive2();
                }
                if (active3OnBool)
                {
                    TapActive3();
                }
                if (active5OnBool)
                {
                    TapActive5();
                }
                if (active6OnBool)
                {
                    TapActive6();
                }
                if (active7OnBool)
                {
                    TapActive7();
                }
            }
            if (pressingWardStopwatch.ElapsedMilliseconds >= pressWardInterval && wardOnBool)
            {
                pressingWardStopwatch.Restart();
                TapWard();
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
                    TapWard();
                }
                //Try to hop
                foreach (char key in wardHopPreactives)
                {
                    SpellController spellController;
                    if (spellControllers.TryGetValue(key, out spellController))
                    {
                        spellController.Preactivate();
                    }
                }
            }
        }

        public void OnKeyUp()
        {

            //if (e.KeyCode == Keys.T && keyTPressed)
            //{ //T key
                keyTPressed = false;
                wardHopStopwatch.Restart();
                //go = true;
            //}
        }

        public void OnKeyDown()
        {
            //if (e.KeyCode == Keys.T && !keyTPressed)
            //{ //T key
                keyTPressed = true;
            //    go = true;
            //}
        }
        void TapActive1()
        {
            Task.Factory.StartNew(() =>
            {
                _autoIT.Send("1");
            });
        }
        void TapActive2()
        {
            Task.Factory.StartNew(() =>
            {
                _autoIT.Send("2");
            });
        }
        void TapActive3()
        {
            Task.Factory.StartNew(() =>
            {
                _autoIT.Send("3");
            });
        }
        void TapWard()
        {
            Task.Factory.StartNew(() =>
            {
                _autoIT.Send("4");
            });
        }
        void TapActive5()
        {
            Task.Factory.StartNew(() =>
            {
                _autoIT.Send("5");
            });
        }
        void TapActive6()
        {
            Task.Factory.StartNew(() =>
            {
                _autoIT.Send("6");
            });
        }
        void TapActive7()
        {
            Task.Factory.StartNew(() => {
                _autoIT.Send("7");
            });
        }
    }
}
