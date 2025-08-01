using Domain.DTOS;
using Domain.Entites;
using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicess.FeeRuleServices
{
    public class PosTransactionRule : IFeeRule
    {
        public string RuleName => "POS Transaction Fee Rule";

        public bool IsApplicable(TransactionDTO transaction)
        {
            return transaction.Type == "POS";
        }

        public decimal CalculateFee(TransactionDTO transaction)
        {
            if (transaction.Amount <= 100)
                return 0.20m;

            return (decimal)(transaction.Amount * 0.002m);
        }
    }
}
