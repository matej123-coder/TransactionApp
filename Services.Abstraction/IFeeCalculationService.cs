using Domain.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IFeeCalculationService
    {
        Task<FeeLogDto> CalculateFeeAsync(TransactionDTO transaction);
        Task<IEnumerable<FeeLogDto>> CalculateFeesBatchAsync(IEnumerable<TransactionDTO> transactions);
        Task<IEnumerable<FeeLogDto>> GetFeeLogs();

    }
}
