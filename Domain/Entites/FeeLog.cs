using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class FeeLog
    {
        public int? Id { get; set; }
        public Transaction? Transaction { get; set; }
        public decimal? FeeAmount { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
