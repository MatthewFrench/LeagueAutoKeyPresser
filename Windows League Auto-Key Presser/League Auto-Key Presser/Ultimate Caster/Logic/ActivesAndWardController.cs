using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoItX3Lib;
namespace League_Auto_Key_Presser.Ultimate_Caster
{
    class ActivesAndWardController
    {
        static AutoItX3 _autoIT = new AutoItX3();
        bool keyTPressed = false;
        Stopwatch activeStopwatch = Stopwatch.StartNew();
        Stopwatch wardHopStopwatch = Stopwatch.StartNew();
        Stopwatch pressingWardStopwatch = Stopwatch.StartNew();

        const int pressWardInterval = 6000;

        SpellController qSpellController, wSpellController, eSpellController, rSpellController;
        RightClickController rightClickController;
        UltimateCasterController ultimateCasterController;

        public ActivesAndWardController(UltimateCasterController ultimateCasterController)
        {
            this.ultimateCasterController = ultimateCasterController;
            _autoIT.AutoItSetOption("SendKeyDelay", 0);
            _autoIT.AutoItSetOption("SendKeyDownDelay", 0);
        }

        public void SetRightClickController(RightClickController rightClickController)
        {
            this.rightClickController = rightClickController;
        }

        public void SetQSpellController(SpellController spellController)
        {
            qSpellController = spellController;
        }

        public void SetWSpellController(SpellController spellController)
        {
            wSpellController = spellController;
        }

        public void SetESpellController(SpellController spellController)
        {
            eSpellController = spellController;
        }

        public void SetRSpellController(SpellController spellController)
        {
            rSpellController = spellController;
        }

        public void PreactivateActives()
        {
            var profile = ultimateCasterController.SelectedProfile;
            if (activeStopwatch.ElapsedMilliseconds >= profile.ActivesMillisecondDelay)
            {
                var runActives = profile.ActivesOn && ((profile.ActivesBoundToQ && qSpellController.IsPressed()) ||
                (profile.ActivesBoundToW && wSpellController.IsPressed()) ||
                (profile.ActivesBoundToE && eSpellController.IsPressed()) ||
                (profile.ActivesBoundToR && rSpellController.IsPressed()));

                if (runActives || (profile.RightClickSpamOn && rightClickController.IsPressed()))
                {
                    activeStopwatch.Restart();
                }

                if ((profile.ActivesDo1 && runActives) || 
                        (profile.RightClickSpamOn && rightClickController.IsPressed() && profile.RightClickPreactivate1))
                {
                    TapActive1();
                }
                if ((profile.ActivesDo2 && runActives) ||
                        (profile.RightClickSpamOn && rightClickController.IsPressed() && profile.RightClickPreactivate2))
                {
                    TapActive2();
                }
                if ((profile.ActivesDo3 && runActives) ||
                        (profile.RightClickSpamOn && rightClickController.IsPressed() && profile.RightClickPreactivate3))
                {
                    TapActive3();
                }
                if ((profile.ActivesDo5 && runActives) ||
                        (profile.RightClickSpamOn && rightClickController.IsPressed() && profile.RightClickPreactivate5))
                {
                    TapActive5();
                }
                if ((profile.ActivesDo6 && runActives) ||
                        (profile.RightClickSpamOn && rightClickController.IsPressed() && profile.RightClickPreactivate6))
                {
                    TapActive6();
                }
                if ((profile.ActivesDo7 && runActives) ||
                        (profile.RightClickSpamOn && rightClickController.IsPressed() && profile.RightClickPreactivate7))
                {
                    TapActive7();
                }
            }
        }

        public void RunActives()
        {
            var profile = ultimateCasterController.SelectedProfile;
            if ((profile.ActivesBoundToQ && qSpellController.IsPressed()) ||
                (profile.ActivesBoundToW && wSpellController.IsPressed()) ||
                (profile.ActivesBoundToE && eSpellController.IsPressed()) ||
                (profile.ActivesBoundToR && rSpellController.IsPressed()))
            {
                PreactivateActives();
            }
            if (pressingWardStopwatch.ElapsedMilliseconds >= pressWardInterval && profile.AutoWardOn)
            {
                pressingWardStopwatch.Restart();
                TapWard();
            }
        }

        public void OnTimerTick()
        {
            var profile = ultimateCasterController.SelectedProfile;
            if (ultimateCasterController.IsOn())
            {
                //Ward hop
                if (keyTPressed && profile.WardHopOn)
                {
                    //Place ward
                    if (wardHopStopwatch.ElapsedMilliseconds >= 500)
                    {
                        wardHopStopwatch.Restart();
                        TapWard();
                    }
                    //Try to hop
                    Task.Factory.StartNew(() =>
                    {
                        if (profile.WardHopUsingQ)
                        {
                            qSpellController.Preactivate();
                        }
                        if (profile.WardHopUsingW)
                        {
                            wSpellController.Preactivate();
                        }
                        if (profile.WardHopUsingE)
                        {
                            eSpellController.Preactivate();
                        }
                        if (profile.WardHopUsingR)
                        {
                            rSpellController.Preactivate();
                        }
                    });
                }
                RunActives();
            }
        }

        public void WardHopOnKeyUp()
        {
            if (keyTPressed)
            { //T key
                keyTPressed = false;
                wardHopStopwatch.Restart();
            }
        }

        public bool WardHopOnKeyDown()
        {
            if (!keyTPressed)
            { //T key
                keyTPressed = true;
                return true;
            }
            return false;
        }
        public void TapActive1()
        {
            _autoIT.Send("1");
        }
        public void TapActive2()
        {
            _autoIT.Send("2");
        }
        public void TapActive3()
        {
            _autoIT.Send("3");
        }
        public void TapWard()
        {
            _autoIT.Send("4");
        }
        public void TapActive5()
        {
            _autoIT.Send("5");
        }
        public void TapActive6()
        {
            _autoIT.Send("6");
        }
        public void TapActive7()
        {
            _autoIT.Send("7");
        }
    }
}
