using Domain.DTOS;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
namespace Domain.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
          
            CreateMap<TransactionDTO, Transaction>();
            CreateMap<ClientDto, ClientInfo>();
            CreateMap<FeeLogDto, FeeLog>();

            
            CreateMap<Transaction, TransactionDTO>();
            CreateMap<ClientInfo, ClientDto>();
            CreateMap<FeeLog, FeeLogDto>();
        }
    }
}
