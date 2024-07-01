using AutoMapper;
using Debra_API.DTOs;
using Debra_API.DTOs.EventDTOs;
using Debra_API.DTOs.EventDTOs.TicketDTOs;
using Debra_API.Entities;
using Debra_API.Repositories.CategoryRepositories;
using Debra_API.Repositories.EventRepositories;
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
        private ICategoryRepository _categoryRepository;
        private ITicketRepository _ticketRepository;

        public EventController(IMapper mapper, IEventRepository eventRepository, 
            ICategoryRepository categoryRepository, ITicketRepository ticketRepository)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
            _categoryRepository = categoryRepository;
            _ticketRepository = ticketRepository;
        }

        [HttpPost]
        public IActionResult AddNewEvent([FromBody] EventCreateDTO newEventCreateDTO)
        {
            Event newEvent = _mapper.Map<Event>(newEventCreateDTO);

            Event? addedEvent = _eventRepository.Add(newEvent);

            if (newEventCreateDTO.Tickets != null)
            {
                if (addedEvent != null)
                {
                    addTicketCategory(
                    new CategoryDTO(newEventCreateDTO.Tickets.UnitPrice,newEventCreateDTO.Tickets.Commision)
                    , addedEvent.Id
                    , newEventCreateDTO.Tickets.Quantity);

                    return Ok();
                }
                
            }
            return View();
        }

        private void addTicketCategory(CategoryDTO categoryDTO, int eventId, int Quantity)
        {
            addTickets(Quantity, eventId, 
                _categoryRepository.add(_mapper.Map<Category>(categoryDTO))
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
