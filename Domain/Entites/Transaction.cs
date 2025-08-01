using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class Transaction
    {
        public int? Id { get; set; } 
        public string? Type { get; set; } = null!;
        public decimal? Amount { get; set; }
        public string? Currency { get; set; } = "EUR";
        public bool? IsDomestic { get; set; }
        public ClientInfo? Client { get; set; }
    }
}
