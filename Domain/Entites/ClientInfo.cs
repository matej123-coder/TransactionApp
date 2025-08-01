using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class ClientInfo
    {
        public int? Id {  get; set; }
        public int? CreditScore { get; set; }
        public int? Budget { get; set; }
        public string? Segment { get; set; } = "STANDARD";
        public bool? HasPromotion { get; set; } 
    }
}
