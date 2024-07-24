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

			CreateMap<TicketDetails, EventTicketCreateDTO>();
			CreateMap<EventTicketCreateDTO, TicketDetails>();

            CreateMap<BandDTO, Band>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.Image) ? Convert.FromBase64String(src.Image) : null));

            CreateMap<Band, BandDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image != null ? Convert.ToBase64String(src.Image) : null));

            CreateMap<MusicianDTO, Musician>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.Image) ? Convert.FromBase64String(src.Image) : null));

            CreateMap<Musician, MusicianDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image != null ? Convert.ToBase64String(src.Image) : null));

            CreateMap<Ticket, TicketCreateDTO>();
			CreateMap<TicketCreateDTO, Ticket>();

			CreateMap<Event, EventCreateDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image != null ? Convert.ToBase64String(src.Image) : null))
                .ForPath(dest => dest.PartnerId, opt => opt.MapFrom(src => src.Partner.Id))
				.ForMember(dest => dest.Tickets, opt => opt.Ignore());

			CreateMap<EventCreateDTO, Event>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.Image) ? Convert.FromBase64String(src.Image) : null))
                .ForMember(dest => dest.Tickets, opt => opt.Ignore())
				.ForMember(dest => dest.Musicians, opt => opt.Ignore())
				.ForMember(dest => dest.Bands, opt => opt.Ignore());

            CreateMap<Event, EventReadDTO>();

			/*CreateMap<Event, EventCreateDTO>()
                .ForMember(dest => dest.ImageBase64, opt => opt.MapFrom(src => Convert.ToBase64String(src.Image)))
                .ForMember(dest => dest.PartnerId, opt => opt.MapFrom(src => src.Partner.Id)) // Assuming Partner has an Id property
                .ForMember(dest => dest.Tickets, opt => opt.Ignore());

            CreateMap<EventCreateDTO, Event>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => Convert.FromBase64String(src.ImageBase64)))
                .ForMember(dest => dest.Partner, opt => opt.Ignore())
                .ForMember(dest => dest.Tickets, opt => opt.Ignore())
                .ForMember(dest => dest.Musicians, opt => opt.Ignore())
                .ForMember(dest => dest.Bands, opt => opt.Ignore());*/
		}
    }
}
