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
    public class ClientRepository : IClientInfoRepository
    {
        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveClientAsync(ClientInfo clientInfo)
        {
            _context.Clients.Add(clientInfo);
            await _context.SaveChangesAsync();
        }

        public async Task<ClientInfo?> GetClientByIdAsync(int id)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
