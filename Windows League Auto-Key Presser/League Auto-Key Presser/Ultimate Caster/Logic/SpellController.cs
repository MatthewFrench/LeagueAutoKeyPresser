using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoItX3Lib;

namespace League_Auto_Key_Presser.Ultimate_Caster
{
    class SpellController
    {
        AutoItX3 _autoIT = new AutoItX3();
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
            _autoIT = new AutoItX3();
            _autoIT.AutoItSetOption("SendKeyDelay", 0);
            _autoIT.AutoItSetOption("SendKeyDownDelay", 0);
        }

        public bool IsPressed()
        {
            return pressingSpell;
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

        public void TimerTick()
        {
            if (pressingSpell)
            {
                if (ultimateCasterController.IsOn() && spellData.On)
                {
                    foreach (char key in spellData.Preactivate.Reverse())
                    {
                        SpellController spellController;
                        if (spellControllers.TryGetValue(key, out spellController))
                        {
                            spellController.Preactivate();
                        }
                    }
                    Preactivate();
                }
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
            if (ultimateCasterController.IsOn() && spellData.On)
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
        }

        public bool OnKeyDown()
        {
            if (!pressingSpell)
            {
                pressingSpell = true;
                keyCountUp = 0;
                spellSent = 0;
                return true;
            }
            return false;
        }

        public void TapSpell()
        {
            spellSent++;
            _autoIT.Send(Char.ToLower(spellKey).ToString());
        }
    }
}