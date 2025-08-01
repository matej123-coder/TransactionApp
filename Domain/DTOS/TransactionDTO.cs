using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOS
{
    public class TransactionDTO
    {
        public string? Type { get; set; } = null!;
        public decimal? Amount { get; set; }
        public string ?Currency { get; set; } = "EUR";
        public bool? IsDomestic { get; set; }
        public ClientDto? Client { get; set; } 
    }
}

