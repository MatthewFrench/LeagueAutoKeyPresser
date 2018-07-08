using AutoItX3Lib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace League_Auto_Key_Presser.Ultimate_Caster
{
    class RightClickController
    {
        AutoItX3 _autoIT = new AutoItX3();
        int rightClickCountUp = 0;
        int rightClickSent = 0;
        bool pressingRightClick = false;
        Stopwatch rightClickStopwatch = Stopwatch.StartNew();
        ActivesAndWardController activesController;
        UltimateCasterController ultimateCasterController;
        SpellController qSpellController, wSpellController, eSpellController, rSpellController;

        public RightClickController(UltimateCasterController ultimateCasterController)
        {
            this.ultimateCasterController = ultimateCasterController;
            _autoIT = new AutoItX3();
            _autoIT.AutoItSetOption("MouseClickDelay", 0);
            _autoIT.AutoItSetOption("MouseClickDownDelay", 0);
        }

        public bool IsPressed()
        {
            return pressingRightClick;
        }

        public void SetActivesController(ActivesAndWardController activesController)
        {
            this.activesController = activesController;
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

        public void TimerTick()
        {
            if (pressingRightClick)
            {
                var profile = ultimateCasterController.SelectedProfile;
                if (ultimateCasterController.IsOn() && profile.RightClickSpamOn)
                {
                    if (profile.RightClickPreactivateQ)
                    {
                        qSpellController.Preactivate();
                    }
                    if (profile.RightClickPreactivateW)
                    {
                        wSpellController.Preactivate();
                    }
                    if (profile.RightClickPreactivateE)
                    {
                        eSpellController.Preactivate();
                    }
                    if (profile.RightClickPreactivateR)
                    {
                        rSpellController.Preactivate();
                    }
                    activesController.PreactivateActives();
                    Preactivate();
                }
            }
        }

        public void Preactivate()
        {
            if (rightClickStopwatch.ElapsedMilliseconds >= ultimateCasterController.SelectedProfile.RightClickMillisecondDelay)
            {
                rightClickStopwatch.Restart();
                TapRightClick();
            }
        }

        public void OnMouseUp()
        {
            if (ultimateCasterController.IsOn() && ultimateCasterController.SelectedProfile.RightClickSpamOn)
            {

                Console.WriteLine("Start Right click sent: " + rightClickSent);
                Console.WriteLine("Start Right click count up: " + rightClickCountUp);

                rightClickCountUp++;
                if (rightClickCountUp > rightClickSent && rightClickSent != 0)
                {
                    pressingRightClick = false;
                    rightClickCountUp = 0;
                    rightClickSent = 0;
                }
                Console.WriteLine("End Right click sent: " + rightClickSent);
                Console.WriteLine("End Right click count up: " + rightClickCountUp);
            }
            else
            {
                pressingRightClick = false;
            }
        }

        public bool OnMouseDown()
        {
            if (!pressingRightClick)
            {
                rightClickStopwatch.Restart();
                pressingRightClick = true;
                rightClickCountUp = 0;
                rightClickSent = 0;
                return true;
            }
            return false;
        }

        public void TapRightClick()
        {
            if (rightClickSent <= rightClickCountUp)
            {
                rightClickSent++;
                Console.WriteLine("Sent: " + rightClickSent);
                
                _autoIT.MouseDown("right");

                var delay = ultimateCasterController.SelectedProfile.RightClickMillisecondDelay;
                if (delay <= 0)
                {
                    delay = 1;
                }
                Task.Delay(Math.Min(15, delay / 2)).ContinueWith(t =>
                {
                    _autoIT.MouseUp("right");
                });
            }
        }
    }
}
