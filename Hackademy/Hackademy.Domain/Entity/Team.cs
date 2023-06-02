using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackademy.Domain.Entity
{
    public class Team
    {
        public int TeamId { get; set; }
        public IList<User> Users { get; set; }
        public int TechLabId { get; set; }
        public TechLab TechLab { get; set; }
        public bool IsDeleted { get; set; }
    }
}
