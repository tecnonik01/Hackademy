using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackademy.Domain.Entity
{
    public class Career
    {
        public int CareerId { get; set; }
        public int CareerStepNumber { get; set; }
        public int CareerTitle { get; set; }
        public int CareerDescription { get; set; }
        public bool IsDone { get; set; }
        public bool IsDeleted { get; set; }
        public TechLab TechLab { get; set; }
        public int TechLabId { get; set; }
    }
}
