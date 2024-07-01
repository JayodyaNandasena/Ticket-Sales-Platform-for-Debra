using AutoMapper;
using Debra_API.DTOs.AdminAccountDTOs;
using Debra_API.DTOs.CustomerDTOs;
using Debra_API.DTOs.EventDTOs;
using Debra_API.DTOs.EventDTOs.TicketDTOs;
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

            CreateMap<CategoryDTO, Category>();
            CreateMap<Category, CategoryDTO>();


            CreateMap<TicketCreateDTO, Ticket>()
                .ForMember(dest => dest.Event, opt => opt.MapFrom(src => new Event { Id = src.EventId }))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => new Category { Id = src.CategoryId }))
                .ForMember(dest => dest.Customer, opt => opt.Ignore());

            CreateMap<Ticket, TicketCreateDTO>()
                .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.Event.Id))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id));

            CreateMap<Event, EventCreateDTO>()
                .ForMember(dest => dest.ImageBase64, opt => opt.MapFrom(src => Convert.ToBase64String(src.Image)))
                .ForMember(dest => dest.PartnerId, opt => opt.MapFrom(src => src.Partner.Id)) // Assuming Partner has an Id property
                .ForMember(dest => dest.Tickets, opt => opt.Ignore());

            CreateMap<EventCreateDTO, Event>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => Convert.FromBase64String(src.ImageBase64)))
                .ForMember(dest => dest.Partner, opt => opt.Ignore())
                .ForMember(dest => dest.Tickets, opt => opt.Ignore())
                .ForMember(dest => dest.Musicians, opt => opt.Ignore())
                .ForMember(dest => dest.Bands, opt => opt.Ignore());
        }
    }
}
