using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackademy.Domain.Entity
{
    public class TechLab
    {
        public int TechLabId { get; set; }
        public string Name { get; set; }
        public IList<Team> Teams { get; set; }
        public IList<Career> Careers { get; set; }
        public bool IsDeleted { get; set; }
    }
}
