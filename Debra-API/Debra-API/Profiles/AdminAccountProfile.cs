using AutoMapper;
using Debra_API.DTO;
using Debra_API.Entities;
namespace Debra_API.Profiles
{
    public class AdminAccountProfile : Profile
    {
        public AdminAccountProfile()
        {
            CreateMap<AdminAccount, AdminAccountDTO>();
        }
    }
}
