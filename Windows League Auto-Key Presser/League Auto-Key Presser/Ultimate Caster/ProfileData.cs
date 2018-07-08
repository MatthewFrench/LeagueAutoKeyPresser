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
        //Q
        public bool QOn { get; set; } = false;
        public int QMillisecondDelay { get; set; } = 80;
        public bool QPreactivateW { get; set; } = false;
        public bool QPreactivateE { get; set; } = false;
        public bool QPreactivateR { get; set; } = false;
        //W
        public bool WOn { get; set; } = false;
        public int WMillisecondDelay { get; set; } = 90;
        public bool WPreactivateQ { get; set; } = false;
        public bool WPreactivateE { get; set; } = false;
        public bool WPreactivateR { get; set; } = false;
        //E
        public bool EOn { get; set; } = false;
        public int EMillisecondDelay { get; set; } = 100;
        public bool EPreactivateQ { get; set; } = false;
        public bool EPreactivateW { get; set; } = false;
        public bool EPreactivateR { get; set; } = false;
        //R
        public bool ROn { get; set; } = false;
        public int RMillisecondDelay { get; set; } = 110;
        public bool RPreactivateQ { get; set; } = false;
        public bool RPreactivateW { get; set; } = false;
        public bool RPreactivateE { get; set; } = false;

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
