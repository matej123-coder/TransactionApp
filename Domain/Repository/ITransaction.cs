using Domain.DTOS;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface ITransaction
    {
        Task SaveTransactionAsync(Transaction transaction);
        Task<Transaction> GetTransactionById(int id);
    }
}
