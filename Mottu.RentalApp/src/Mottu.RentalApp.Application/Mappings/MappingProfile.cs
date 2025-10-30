using AutoMapper;
using Mottu.RentalApp.Application.DTOs.Requests;
using Mottu.RentalApp.Application.DTOs.Responses;
using Mottu.RentalApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.RentalApp.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Motorcycle, MotorcycleResponse>();
            CreateMap<CreateMotorcycleRequest, Motorcycle>();
            
        }
    }
}
