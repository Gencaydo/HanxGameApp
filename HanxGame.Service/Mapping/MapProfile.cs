using AutoMapper;
using HanxGame.Core.DTOs;
using HanxGame.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanxGame.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<GameEntity, GameDto>().ReverseMap();
            CreateMap<CustomerEntity, CustomerDto>().ReverseMap();
            CreateMap<SupplierEntity, SupplierDto>().ReverseMap();
        }
    }
}
