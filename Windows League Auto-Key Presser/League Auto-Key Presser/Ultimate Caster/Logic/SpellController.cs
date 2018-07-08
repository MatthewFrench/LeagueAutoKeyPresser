using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoItX3Lib;

namespace League_Auto_Key_Presser.Ultimate_Caster
{
    class SpellController
    {
        static AutoItX3 _autoIT = new AutoItX3();
        int keyCountUp = 0;
        int spellSent = 0;
        bool pressingSpell = false;
        Stopwatch spellStopwatch = Stopwatch.StartNew();
        char spellKey;
        Dictionary<char, SpellController> spellControllers = new Dictionary<char, SpellController>();
        ActivesAndWardController activesController;
        UltimateCasterController ultimateCasterController;
        SpellData spellData;

        public SpellController(UltimateCasterController ultimateCasterController, char spellKey)
        {
            this.ultimateCasterController = ultimateCasterController;
            this.spellKey = spellKey;
            _autoIT.AutoItSetOption("SendKeyDelay", 0);
            _autoIT.AutoItSetOption("SendKeyDownDelay", 0);
        }

        public void UpdateWithSpellData(SpellData newSpellData)
        {
            spellData = newSpellData;
        }

        public void SetActivesController(ActivesAndWardController activesController)
        {
            this.activesController = activesController;
        }

        public void SetSpellController(char key, SpellController spellController)
        {
            spellControllers.Add(key, spellController);
        }

        public void EnablePreactive(char key)
        {
            spellData.Preactivate.Add(key);
        }

        public void DisablePreactive(char key)
        {
            spellData.Preactivate.Remove(key);
        }

        public void TimerTick()
        {
            if (pressingSpell)
            {
                if (ultimateCasterController.UltimateCasterOn)
                {
                    foreach (char key in spellData.Preactivate)
                    {
                        SpellController spellController;
                        if (spellControllers.TryGetValue(key, out spellController))
                        {
                            spellController.Preactivate();
                        }
                    }
                    Preactivate();
                }
                /*
                if (runActivesOnThisSpell)
                {
                    activesController.RunActives();
                }
                */
            }
        }

        public void Preactivate()
        {
            if (spellStopwatch.ElapsedMilliseconds >= spellData.MillisecondDelay)
            {
                spellStopwatch.Restart();
                TapSpell();
            }
        }

        public void OnKeyUp()
        {
            //if (e.KeyCode == Keys.Q) // && keyQPressed
            //{ //Q
                if (ultimateCasterController.UltimateCasterOn)
                {
                    keyCountUp++;
                    if (keyCountUp > spellSent && spellSent != 0)
                    {
                        pressingSpell = false;
                        keyCountUp = 0;
                        spellSent = 0;
                    }
                }
                else
                {
                    pressingSpell = false;
                }
            //}
        }

        public void OnKeyDown()
        {
            //if (e.KeyCode == Keys.Q)
            //{ //Q
                if (!pressingSpell)
                {
                    pressingSpell = true;
                    //go = true;
                    keyCountUp = 0;
                    spellSent = 0;
                }
            //}
        }

        public void TapSpell()
        {
            spellSent++;
            Task.Factory.StartNew(() =>
            {
                _autoIT.Send(Char.ToLower(spellKey).ToString());
            });
        }
    }
}
