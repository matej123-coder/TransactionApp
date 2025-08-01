using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOS
{
    public class ClientDto
    {
        public int? CreditScore { get; set; }
        public string? Segment { get; set; } = "STANDARD";
        public bool? HasPromotion { get; set; } 
    }
}
