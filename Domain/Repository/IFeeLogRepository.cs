using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IFeeLogRepository
    {
        Task SaveFeeLogAsync(FeeLog feeLog);
    }
}
