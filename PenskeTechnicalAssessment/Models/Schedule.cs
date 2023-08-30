using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenskeTechnicalAssessment.Models
{
    public class Schedule
    {
        public string eventName { get; set; }
        public string notes {  get; set; }
        public DateTime? startTimeUtc { get; set; }
        public int runType { get; set; }
    }
}
