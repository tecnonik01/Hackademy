using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackademy.Domain.Entity
{
    public class Badge
    {

        public int BadgeId { get; set; }
        public Guid BadgeIcon { get; set; }
        public string BadgeDescription { get; set; }
        public string BadgeName { get; set;  }
        public bool Obtained { get; set;  }
        public bool Visible { get; set;  }

    }
}
