using Domain.DTOS;
using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicess.FeeRuleServices
{
    public class CreditScoreRule:IFeeRule
    {
        public string RuleName => "Credit Score Discount Rule";

        public bool IsApplicable(TransactionDTO transaction)
        {
            return transaction.Client?.CreditScore > 400;
        }

        public decimal CalculateFee(TransactionDTO transaction)
        {
            decimal amount = transaction.Amount ?? 0;
            return amount * -0.01m;
        }
    }
}
