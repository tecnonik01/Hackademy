using Hackademy.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackademy.Domain.Entity
{
    public class Prize
    {
        public PrizeEnum PrizeType { get; set; }
        public int PrizeId { get; set; }
        public string PrizeName { get; set; }
        public double PrizePrice { get; set; }
        public string PrizeDescription { get; set; }
        public Guid Image { get; set; }
        public string ImageUrl { get; set; }

    }
}
