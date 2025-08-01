using Domain.DTOS;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validations
{
    public class TransactionValidator : AbstractValidator<TransactionDTO>
    {
        public  TransactionValidator()
        {
            RuleFor(t => t.Amount)
                .NotNull().WithMessage("Amount is required.")
                .GreaterThan(0).WithMessage("Amount must be greater than 0.");

            RuleFor(t => t.Type)
                .NotEmpty().WithMessage("TransactionType is required.");

            RuleFor(t => t.Client)
                .NotNull().WithMessage("Client info is required.");

            RuleFor(t => t.Client!.CreditScore)
                .GreaterThanOrEqualTo(0).WithMessage("Credit score must be positive.");

            RuleFor(t => t.Client!.Segment)
                .NotEmpty().WithMessage("Segment is required.");
        }
    }
}
