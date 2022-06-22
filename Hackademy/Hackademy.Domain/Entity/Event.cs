using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackademy.Domain.Entity
{
    public class Event
    {
        public int EventId { get; set; }
        public DateTime EventDateTime { get; set; }
        public string EventDescription { get; set; }
        //public IList<User> Users { get; set; }

    }
}
