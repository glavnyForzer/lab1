using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace API_Solution
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDto>().ForMember(c => c.FullAddress, opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));
            CreateMap<Employee, EmployeeDto>();
            CreateMap<Boat, BoatDto>();
            CreateMap<Capitan, CapitanDto>();
            CreateMap<CompanyForCreationDto, Company>();
            CreateMap<EmployeeForCreationDto, Employee>();
            CreateMap<CapitanForCreatonDto, Capitan>();
            CreateMap<BoatForCreationDto, Boat>();
        }
    }
}
