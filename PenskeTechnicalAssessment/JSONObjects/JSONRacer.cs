using PenskeTechnicalAssessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenskeTechnicalAssessment.JSONObjects
{
    public class JSONRacer
    {

    }
    public class JSONLapTimes
    {
        public List<RaceInfo> laps { get; set; }
        public List<RaceFlags> flags { get; set; }
    }
}
