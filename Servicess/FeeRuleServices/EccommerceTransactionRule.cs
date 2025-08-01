using Domain.DTOS;
using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicess.FeeRuleServices
{
    public class EccommerceTransactionRule:IFeeRule
    {
        public string RuleName => "E-Commerce Transaction Fee Rule";

        public bool IsApplicable(TransactionDTO transaction)
        {
            return transaction.Type == "ECommerce";
        }

        public decimal CalculateFee(TransactionDTO transaction)
        {
            decimal amount = transaction.Amount ?? 0;
            decimal fee = (amount * 0.018m) + 0.15m;
            return Math.Min(fee, 120m);
        }
    }
}
