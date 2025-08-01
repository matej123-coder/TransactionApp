using Domain.Entites;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.RepositoryLogic
{
    public class FeeLogRepository:IFeeLogRepository
    {
        private readonly AppDbContext _context;

        public FeeLogRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveFeeLogAsync(FeeLog feeLog)
        {
            _context.FeeLogs.Add(feeLog);
            await _context.SaveChangesAsync();
        }
        public async Task<List<FeeLog>> GetAllFeeLogs()
        {
            return await _context.FeeLogs
        .Include(f => f.Transaction)
            .ThenInclude(t => t.Client) 
        .ToListAsync();
        } 
    }
}
