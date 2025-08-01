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
    public class RuleRepository:IRuleRepository
    {
        private readonly AppDbContext _context;

        public RuleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Rule>> GetAllRulesAsync()
        {
            return await _context.Rules.ToListAsync();
        }
    }
}
