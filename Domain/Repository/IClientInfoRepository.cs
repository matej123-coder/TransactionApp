using Domain.DTOS;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public  interface IClientInfoRepository
    {
        Task SaveClientAsync(ClientInfo clientInfo);
        Task<ClientInfo?> GetClientByIdAsync(int id);
    }
}
