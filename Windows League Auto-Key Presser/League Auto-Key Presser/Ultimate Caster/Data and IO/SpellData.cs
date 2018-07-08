using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace League_Auto_Key_Presser.Ultimate_Caster
{
    public class SpellData
    {
        public char SpellKey { get; set; } = 'Q';
        public bool On { get; set; } = false;
        public int MillisecondDelay { get; set; } = 80;
        HashSet<char> Preactivate = new HashSet<char>();

        public SpellData(char spellKey, int milliseconds)
        {
            SpellKey = spellKey;
            MillisecondDelay = milliseconds;
        }
    }
}
