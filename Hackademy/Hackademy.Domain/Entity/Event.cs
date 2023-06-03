using Hackademy.Domain.Enum;
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
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public EventTypeEnum EventTypeEnum { get; set; }
        public string EventTitle { get; set; }
        public string? EventStreet { get; set; }
        public string? EventCity { get; set; }
        public string? EventLink { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public bool IsDeleted { get; set; }

    }
}
