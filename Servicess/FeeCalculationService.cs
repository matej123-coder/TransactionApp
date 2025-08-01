using AutoMapper;
using Domain.DTOS;
using Domain.Entites;
using Domain.Mapper;
using Domain.Repository;
using FluentValidation;
using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicess
{
    public class FeeCalculationService : IFeeCalculationService
    {
        private readonly IEnumerable<IFeeRule> _rules;
        private readonly IFeeLogRepository _feeLogRepository;
        private readonly ITransaction _transaction;
        private readonly IMapper _mapper;
        private readonly IClientInfoRepository _clientInfoRepository;
        private readonly IValidator<TransactionDTO> _validator;
        public FeeCalculationService(
            IEnumerable<IFeeRule> rules,
            IFeeLogRepository feeLogRepository,
            IMapper mapper,ITransaction transaction, IClientInfoRepository clientInfoRepository,IValidator<TransactionDTO> validator)
        {
            _rules = rules;
            _feeLogRepository = feeLogRepository;
            _mapper = mapper;
            _transaction = transaction;
            _validator = validator;
            _clientInfoRepository = clientInfoRepository;
        }

        public async Task<FeeLogDto> CalculateFeeAsync(TransactionDTO transactionDto)
        {
            var validationResult = await _validator.ValidateAsync(transactionDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }
            decimal totalFee = 0;

            foreach (var rule in _rules)
            {
                if (rule.IsApplicable(transactionDto))
                {
                    totalFee += rule.CalculateFee(transactionDto);
                }
            }
            totalFee = Math.Max(0, totalFee);
            var feeLog = new FeeLog
            {
                Transaction = _mapper.Map<Transaction>(transactionDto),
                FeeAmount = totalFee,
                CreatedAt = DateTime.UtcNow
            };
            var transaction = _mapper.Map<Transaction>(transactionDto);
            await _transaction.SaveTransactionAsync(transaction);
         
            await _feeLogRepository.SaveFeeLogAsync(feeLog);

            return _mapper.Map<FeeLogDto>(feeLog);
        }

        public async Task<IEnumerable<FeeLogDto>> CalculateFeesBatchAsync(IEnumerable<TransactionDTO> transactions)
        {
            var results = new List<FeeLogDto>();

            foreach (var tx in transactions)
            {
                var result = await CalculateFeeAsync(tx);
                results.Add(result);
            }

            return results;
        }

        public async Task<IEnumerable<FeeLogDto>> GetFeeLogs()
        {
            var feeLogs = await _feeLogRepository.GetAllFeeLogs();
            return feeLogs.Select(f => _mapper.Map<FeeLogDto>(f));
        }
    }
}
