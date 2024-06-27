using AutoMapper;
using Debra_API.DTOs.AdminAccountDTOs;
using Debra_API.DTOs.CustomerDTOs;
using Debra_API.DTOs.PartnerDTOs;
using Debra_API.Entities;
namespace Debra_API.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AdminAccount, AdminAccountDTO>();
            CreateMap<AdminAccountDTO, AdminAccount>();

            CreateMap<Customer, CustomerDTO>();
            CreateMap<CustomerDTO, Customer>();

            CreateMap<Partner, PartnerDTO>();
            CreateMap<PartnerDTO, Partner>();

            CreateMap<PartnerAccount, PartnerAccountDTO>();
            CreateMap<PartnerAccountDTO, PartnerAccount>();
        }
    }
}
