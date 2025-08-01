using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOS
{
    public class FeeLogDto
    { 
        public TransactionDTO? Transaction { get; set; }
        public decimal? FeeAmount { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
