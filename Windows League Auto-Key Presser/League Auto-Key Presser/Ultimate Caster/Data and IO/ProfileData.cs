using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace League_Auto_Key_Presser.Ultimate_Caster
{
    public class ProfileData
    {
        public string ProfileName { get; set; } = "New Profile";
        public SpellData QSpellData { get; set; } = new SpellData('Q', 80);
        public SpellData WSpellData { get; set; } = new SpellData('W', 90);
        public SpellData ESpellData { get; set; } = new SpellData('E', 100);
        public SpellData RSpellData { get; set; } = new SpellData('R', 110);

        //Actives
        public bool ActivesOn { get; set; } = false;
        public int ActivesMillisecondDelay { get; set; } = 200;
        public bool ActivesBoundToQ { get; set; } = false;
        public bool ActivesBoundToW { get; set; } = false;
        public bool ActivesBoundToE { get; set; } = false;
        public bool ActivesBoundToR { get; set; } = false;
        public bool ActivesDo1 { get; set; } = false;
        public bool ActivesDo2 { get; set; } = false;
        public bool ActivesDo3 { get; set; } = false;
        public bool ActivesDo5 { get; set; } = false;
        public bool ActivesDo6 { get; set; } = false;
        public bool ActivesDo7 { get; set; } = false;

        //Ward
        public bool AutoWardOn { get; set; } = false;

        //Ward Hop
        public bool WardHopOn { get; set; } = false;
        public bool WardHopUsingQ { get; set; } = false;
        public bool WardHopUsingW { get; set; } = false;
        public bool WardHopUsingE { get; set; } = false;
        public bool WardHopUsingR { get; set; } = false;

        //Right Click Spam
        public bool RightClickSpamOn { get; set; } = false;
        public int RightClickMillisecondDelay { get; set; } = 50;
        public bool RightClickPreactivateQ { get; set; } = false;
        public bool RightClickPreactivateW { get; set; } = false;
        public bool RightClickPreactivateE { get; set; } = false;
        public bool RightClickPreactivateR { get; set; } = false;
        public bool RightClickPreactivate1 { get; set; } = false;
        public bool RightClickPreactivate2 { get; set; } = false;
        public bool RightClickPreactivate3 { get; set; } = false;
        public bool RightClickPreactivate5 { get; set; } = false;
        public bool RightClickPreactivate6 { get; set; } = false;
        public bool RightClickPreactivate7 { get; set; } = false;
    }
}
