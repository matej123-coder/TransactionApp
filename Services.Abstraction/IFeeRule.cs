using Domain.DTOS;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services.Abstraction
{
    public interface IFeeRule
    {
        bool IsApplicable(TransactionDTO transaction);
        decimal CalculateFee(TransactionDTO transaction);
        string RuleName { get; }
    }
}
