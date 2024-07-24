using AutoMapper;
using Azure.Core;
using Debra_API.DTOs;
using Debra_API.DTOs.EventDTOs;
using Debra_API.DTOs.EventDTOs.TicketDTOs;
using Debra_API.Entities;
using Debra_API.Repositories.BandRepository;
using Debra_API.Repositories.TicketDetailsRepositories;
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
		private ITicketDetailsRepository _ticketDetailsRepository;
		private ITicketRepository _ticketRepository;
		private IMusicianRepository _musicianRepository;
		private IBandRepository _bandRepository;

		public EventController(IMapper mapper, IEventRepository eventRepository,
			IPartnerRepository partnerRepository, ITicketDetailsRepository ticketDetailsRepository,
			ITicketRepository ticketRepository, IMusicianRepository musicianRepository,
			IBandRepository bandRepository)
		{
			_mapper = mapper;
			_eventRepository = eventRepository;
			_partnerRepository = partnerRepository;
			_ticketDetailsRepository = ticketDetailsRepository;
			_ticketRepository = ticketRepository;
			_musicianRepository = musicianRepository;
			_bandRepository = bandRepository;
		}

		
		[HttpPost]
		public IActionResult AddNewEvent([FromBody] EventCreateDTO request)
		{
			// Map the EventCreateDTO to the Event entity
			Event createEntity = _mapper.Map<Event>(request);

			Partner? partner = _partnerRepository.GetById(request.PartnerId);

			if (partner == null)
			{
				return Ok( new OperationResultResponseDTO<string>
					{
						Status = Status.Failed,
						Result = "Partner Not Found"
					}
				);
			}

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
			eventReadDTO.Bands = bands.Select(_mapper.Map<BandDTO>).ToList();
			eventReadDTO.Musicians = musicians.Select(_mapper.Map<MusicianDTO>).ToList();

			if (request.Tickets != null)
			{
				TicketDetails ticketDetailsEntity = 
					_mapper.Map<TicketDetails>(request.Tickets);

				int detailId = _ticketDetailsRepository.Add(ticketDetailsEntity);

				if (detailId == 0)
				{
					return Ok( new OperationResultResponseDTO<string>
						{
							Status = Status.Failed,
							Result = "Error adding ticket details"
						}
					);
				}

				int numberOfDigits = request.Tickets.Quantity.ToString().Length;
				List<Ticket> ticketList = new List<Ticket>();

				for (int i = 1; i <= request.Tickets.Quantity; i++)
				{
					var ticketCreateDTO = new TicketCreateDTO
					{
						EventId = eventReadDTO.Id,
						DetailsId = detailId,
						Id = $"TKT-{eventReadDTO.Id}-{i.ToString($"D{numberOfDigits}")}"
					};

					ticketList.Add(_mapper.Map<Ticket>(ticketCreateDTO));
				}

				if (!_ticketRepository.AddTickets(ticketList))
				{
					return Ok( new OperationResultResponseDTO<string>
						{
							Status = Status.Failed,
							Result = "Error adding tickets"
						}
					);
				}
			}

			return Ok(new OperationResultResponseDTO<EventReadDTO>
				{
					Status = Status.Success,
					Result = eventReadDTO
				}
			);
		}


		[HttpGet]
		public ActionResult<IEnumerable<EventReadDTO>> GetAll()
		{
			List<Event> allEvents = _eventRepository.GetAll();

			List<EventReadDTO> results = new List<EventReadDTO>();

			foreach (Event item in allEvents)
			{
				Partner? partner = _partnerRepository.GetById(item.PartnerId);

				if (partner == null)
				{
					return Ok("Invalid Partner");
				}

				item.Partner = partner;

				EventReadDTO eventReadDTO = _mapper.Map<EventReadDTO>(item);

				List<Musician> musicians = _musicianRepository.GetByEvent(item.Id);
				List<MusicianDTO> musicianDTOs = [];

                foreach (var musician in musicians)
                {
					musicianDTOs.Add(_mapper.Map<MusicianDTO>(musician));
                }

				List<Band> bands = _bandRepository.GetByEvent(item.Id);
				List<BandDTO> bandDTOs = [];

				foreach (var band in bands)
				{
					bandDTOs.Add(_mapper.Map<BandDTO>(band));
				}


				eventReadDTO.Musicians = musicianDTOs;
				eventReadDTO.Bands = bandDTOs;
				results.Add(eventReadDTO);

			}

			return Ok(results);
		}

        [HttpGet("Upcoming")]
        public ActionResult<IEnumerable<EventReadDTO>> GetUpcomingEvents()
        {
            List<Event> allEvents = _eventRepository.GetUpcomingEvents();

            List<EventReadDTO> results = new List<EventReadDTO>();

            foreach (Event item in allEvents)
            {
                Partner? partner = _partnerRepository.GetById(item.PartnerId);

                if (partner == null)
                {
                    return Ok("Invalid Partner");
                }

                item.Partner = partner;

                EventReadDTO eventReadDTO = _mapper.Map<EventReadDTO>(item);

                List<Musician> musicians = _musicianRepository.GetByEvent(item.Id);
                List<MusicianDTO> musicianDTOs = [];

                foreach (var musician in musicians)
                {
                    musicianDTOs.Add(_mapper.Map<MusicianDTO>(musician));
                }

                List<Band> bands = _bandRepository.GetByEvent(item.Id);
                List<BandDTO> bandDTOs = [];

                foreach (var band in bands)
                {
                    bandDTOs.Add(_mapper.Map<BandDTO>(band));
                }


                eventReadDTO.Musicians = musicianDTOs;
                eventReadDTO.Bands = bandDTOs;
                results.Add(eventReadDTO);

            }

            return Ok(results);
        }

        [HttpGet("Titles")]
        public ActionResult<IEnumerable<string>> GetUpcomingEventTitles()
        {
            List<dynamic> titles = _eventRepository.GetUpcomingEventTitles();
			return Ok(titles);
        }

        [HttpGet("ById")]
		public ActionResult GetById([FromQuery] int Id)
		{
			Event foundEvent = _eventRepository.GetById(Id);

            if (foundEvent is null)
            {
				return Ok(new OperationResultResponseDTO<string>
				{
					Status = Status.Failed,
					Result = "Event not found"
				});
            }


			Partner? partner = _partnerRepository.GetById(foundEvent.PartnerId);

			if (partner == null)
			{
				return Ok(new OperationResultResponseDTO<string>
				{
					Status = Status.Failed,
					Result = "Partner not found"
				});
			}

			foundEvent.Partner = partner;

			EventReadDTO eventReadDTO = _mapper.Map<EventReadDTO>(foundEvent);

			List<Musician> musicians = _musicianRepository.GetByEvent(foundEvent.Id);
			List<MusicianDTO> musicianDTOs = [];

			foreach (var musician in musicians)
			{
				musicianDTOs.Add(_mapper.Map<MusicianDTO>(musician));
			}

			List<Band> bands = _bandRepository.GetByEvent(foundEvent.Id);
			List<BandDTO> bandDTOs = [];

			foreach (var band in bands)
			{
				bandDTOs.Add(_mapper.Map<BandDTO>(band));
			}


			eventReadDTO.Musicians = musicianDTOs;
			eventReadDTO.Bands = bandDTOs;

			return Ok(new OperationResultResponseDTO<EventReadDTO>
			{
				Status = Status.Success,
				Result = eventReadDTO
			});
		}

	}
}
