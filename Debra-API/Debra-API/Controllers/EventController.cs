using AutoMapper;
using Azure.Core;
using Debra_API.DTOs;
using Debra_API.DTOs.CustomerDTOs;
using Debra_API.DTOs.EventDTOs;
using Debra_API.DTOs.EventDTOs.TicketDTOs;
using Debra_API.Entities;
using Debra_API.Repositories.BandRepository;
using Debra_API.Repositories.CategoryRepositories;
using Debra_API.Repositories.EventRepositories;
using Debra_API.Repositories.MusicianRepositories;
using Debra_API.Repositories.PartnerRepositories;
using Debra_API.Repositories.TicketRepositories;
using Microsoft.AspNetCore.Mvc;

namespace Debra_API.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class EventController : Controller
	{
		private IMapper _mapper;
		private IEventRepository _eventRepository;
		private IPartnerRepository _partnerRepository;
		private ICategoryRepository _categoryRepository;
		private ITicketRepository _ticketRepository;
		private IMusicianRepository _musicianRepository;
		private IBandRepository _bandRepository;

		public EventController(IMapper mapper, IEventRepository eventRepository,
			IPartnerRepository partnerRepository, ICategoryRepository categoryRepository,
			ITicketRepository ticketRepository, IMusicianRepository musicianRepository,
			IBandRepository bandRepository)
		{
			_mapper = mapper;
			_eventRepository = eventRepository;
			_partnerRepository = partnerRepository;
			_categoryRepository = categoryRepository;
			_ticketRepository = ticketRepository;
			_musicianRepository = musicianRepository;
			_bandRepository = bandRepository;
		}

		/*[HttpPost]
        public IActionResult AddNewEvent([FromBody] EventCreateDTO request)
        {
			Event createEntity = _mapper.Map<Event>(request);

            createEntity.Partner = _partnerRepository.GetById(request.PartnerId);

			Event? readEntity = _eventRepository.Add(createEntity);

            

			EventReadDTO eventReadDTO = _mapper.Map<EventReadDTO>(readEntity);

			List<Musician> musicians = new List<Musician>();
			foreach (var musician in request.Musicians)
			{
				Musician entity = _mapper.Map<Musician>(musician);
                entity.EventId = eventReadDTO.Id;
				musicians.Add(entity);
			}
			_musicianRepository.Add(musicians);

			List<Band> bands = new List<Band>();
			foreach (var band in request.Bands)
			{
				Band entity = _mapper.Map<Band>(band);
				entity.EventId = eventReadDTO.Id;
				bands.Add(entity);
			}
			_bandRepository.Add(bands);

            List<BandDTO> bandDTOs = new List<BandDTO>();

            foreach (var band in bands)
            {
                bandDTOs.Add(_mapper.Map<BandDTO>(band));
            }

            eventReadDTO.Bands = bandDTOs;

			List<MusicianDTO> musicianDTOs = new List<MusicianDTO>();

			foreach (var musician in musicians)
			{
				musicianDTOs.Add(_mapper.Map<MusicianDTO>(musician));
			}

			eventReadDTO.Musicians = musicianDTOs;

			return Ok(eventReadDTO);
		}*/
		[HttpPost]
		public IActionResult AddNewEvent([FromBody] EventCreateDTO request)
		{
			// Map the EventCreateDTO to the Event entity
			Event createEntity = _mapper.Map<Event>(request);

			Partner? partner = _partnerRepository.GetById(request.PartnerId);

			if (partner != null)
			{
				createEntity.Partner = partner;

				// Add the Event entity to the repository and map the result to EventReadDTO
				Event? readEntity = _eventRepository.Add(createEntity);
				EventReadDTO eventReadDTO = _mapper.Map<EventReadDTO>(readEntity);

				// Map, set EventId, and add musicians to the repository
				var musicians = request.Musicians.Select(musician =>
				{
					var entity = _mapper.Map<Musician>(musician);
					entity.EventId = eventReadDTO.Id;
					return entity;
				}).ToList();
				_musicianRepository.Add(musicians);

				// Map, set EventId, and add bands to the repository
				var bands = request.Bands.Select(band =>
				{
					var entity = _mapper.Map<Band>(band);
					entity.EventId = eventReadDTO.Id;
					return entity;
				}).ToList();
				_bandRepository.Add(bands);

				// Map bands and musicians to their respective DTOs
				eventReadDTO.Bands = bands.Select(band => _mapper.Map<BandDTO>(band)).ToList();
				eventReadDTO.Musicians = musicians.Select(musician => _mapper.Map<MusicianDTO>(musician)).ToList();

				// Return the complete EventReadDTO
				return Ok(
					new OperationResultResponseDTO<EventReadDTO>
						{
							Status = Status.Success,
							Result = eventReadDTO
						}
				);
			}
			
			return Ok(
					new OperationResultResponseDTO<string>
					{
						Status = Status.Failed,
						Result = "Partner Not Found"
					}
				);
		}


		[HttpGet]
		public ActionResult<IEnumerable<EventReadDTO>> GetAll()
		{
			List<Event> allEvents = _eventRepository.GetAll();

			List<EventReadDTO> result = new List<EventReadDTO>();

			foreach (Event item in allEvents)
			{
				Partner? partner = _partnerRepository.GetById(item.PartnerId);

				item.Partner = partner;

				result.Add(
					_mapper.Map<EventReadDTO>(item)
					);
			}

			return Ok(result);
		}

		private void addTicketCategory(CategoryDTO categoryDTO, int eventId, int Quantity)
		{
			addTickets(Quantity, eventId,
				_categoryRepository.add(_mapper.Map<TicketDetails>(categoryDTO))
				);

		}

		private void addTickets(int Quantity, int eventId, int categoryId)
		{
			for (int i = 0; i < Quantity; i++)
			{
				TicketCreateDTO ticketCreateDTO = new TicketCreateDTO(eventId, categoryId);
				_ticketRepository.addTickets(_mapper.Map<Ticket>(ticketCreateDTO));
			}
		}
	}
}
