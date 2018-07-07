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
        bool ultimateCasterIsOn = false;
        bool spellCasterIsOn = false;
        int keyCountUp = 0;
        int spellSent = 0;
        bool pressingSpell = false;
        Stopwatch spellStopwatch = Stopwatch.StartNew();
        int pressSpellInterval = 10;
        char spellKey;
        Dictionary<char, SpellController> preactivateSpellControllers = new Dictionary<char, SpellController>();
        HashSet<char> enabledPreactives = new HashSet<char>();
        bool runActivesOnThisSpell = false;
        ActivesController activesController;
        public SpellController(char spellKey)
        {
            this.spellKey = spellKey;
            _autoIT.AutoItSetOption("SendKeyDelay", 0);
            _autoIT.AutoItSetOption("SendKeyDownDelay", 0);
        }

        public void SetActivesController(ActivesController activesController)
        {
            this.activesController = activesController;
        }

        public void AddPreactivateSpellController(char key, SpellController spellController)
        {
            preactivateSpellControllers.Add(key, spellController);
        }

        public void EnablePreactive(char key)
        {
            enabledPreactives.Add(key);
        }

        public void DisablePreactive(char key)
        {
            enabledPreactives.Remove(key);
        }

        public void TimerTick()
        {
            if (pressingSpell)
            {
                if (ultimateCasterIsOn)
                {
                    foreach (char key in enabledPreactives)
                    {
                        SpellController spellController;
                        if (preactivateSpellControllers.TryGetValue(key, out spellController))
                        {
                            spellController.Preactivate();
                        }
                    }
                    Preactivate();
                }
                if (runActivesOnThisSpell)
                {
                    activesController.RunActives();
                }
            }
        }

        public void Preactivate()
        {
            if (spellStopwatch.ElapsedMilliseconds >= pressSpellInterval)
            {
                spellStopwatch.Restart();
                TapSpell();
            }
        }

        public void OnKeyUp()
        {
            //if (e.KeyCode == Keys.Q) // && keyQPressed
            //{ //Q
                if (ultimateCasterIsOn)
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
