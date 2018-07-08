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
        public string ProfileName { get; set; }
        //Q
        public bool QOn { get; set; }
        public int QMillisecondDelay { get; set; }
        public int QPreactivateW { get; set; }
        public int QPreactivateE { get; set; }
        public int QPreactivateR { get; set; }
        //W
        public bool WOn { get; set; }
        public int WMillisecondDelay { get; set; }
        public int WPreactivateQ { get; set; }
        public int WPreactivateE { get; set; }
        public int WPreactivateR { get; set; }
        //E
        public bool EOn { get; set; }
        public int EMillisecondDelay { get; set; }
        public int EPreactivateQ { get; set; }
        public int EPreactivateW { get; set; }
        public int EPreactivateR { get; set; }
        //R
        public bool ROn { get; set; }
        public int RMillisecondDelay { get; set; }
        public int RPreactivateQ { get; set; }
        public int RPreactivateW { get; set; }
        public int RPreactivateE { get; set; }

        //Actives
        public bool ActivesOn { get; set; }
        public int ActivesMillisecondDelay { get; set; }
        public int ActivesBoundToQ { get; set; }
        public int ActivesBoundToW { get; set; }
        public int ActivesBoundToE { get; set; }
        public int ActivesBoundToR { get; set; }
        public int ActivesDo1 { get; set; }
        public int ActivesDo2 { get; set; }
        public int ActivesDo3 { get; set; }
        public int ActivesDo5 { get; set; }
        public int ActivesDo6 { get; set; }
        public int ActivesDo7 { get; set; }

        //Ward
        public bool AutoWardOn { get; set; }

        //Ward Hop
        public bool WardHopOn { get; set; }
        public bool WardHopUsingQ { get; set; }
        public bool WardHopUsingW { get; set; }
        public bool WardHopUsingE { get; set; }
        public bool WardHopUsingR { get; set; }

        //Right Click Spam
        public bool RightClickSpamOn { get; set; }
        public int RightClickMillisecondDelay { get; set; }
        public int RightClickPreactivateQ { get; set; }
        public int RightClickPreactivateW { get; set; }
        public int RightClickPreactivateE { get; set; }
        public int RightClickPreactivateR { get; set; }
        public int RightClickPreactivate1 { get; set; }
        public int RightClickPreactivate2 { get; set; }
        public int RightClickPreactivate3 { get; set; }
        public int RightClickPreactivate5 { get; set; }
        public int RightClickPreactivate6 { get; set; }
        public int RightClickPreactivate7 { get; set; }
    }
}
