using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenskeTechnicalAssessment.Models
{
    public class JSONLapTimes
    {
        public List<RaceInfo> laps { get; set; }
        public List<RaceFlags> flags { get; set; }
    }
    public class RaceInfo
    {
        //The JSON displays the number as a string. If this was a perminant project, I would have proper JSON objects that get transformed into the C# object I needed
        public string Number { get; set; } 
        public string FullName { get; set; }
        public string Manufacturer { get; set; }
        public int RunningPos { get; set; }
        public int NASCARDriverID { get; set; }

       public List<LapData> Laps { get; set; }


        public int StartingPosition { get; set; }
        public float AverageLapTime { get; set; }
        public string RaceName { get; set; }
        public string TrackName { get; set; }

    }

    public class LapData
    {
        public int Lap {  get; set; }
        public float? LapTime { get; set; }
        public string? LapSpeed { get; set; }
        public int RunningPos { get; set; }
    }

        public class RaceFlags
        {
            public int LapsCompleted { get; set; }
            public int FlagState { get; set; }
        }

    
}
