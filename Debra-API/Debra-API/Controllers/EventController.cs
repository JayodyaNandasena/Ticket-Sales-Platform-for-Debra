using AutoMapper;
using Debra_API.DTOs.EventDTOs;
using Debra_API.Repositories.EventRepositories;
using Microsoft.AspNetCore.Mvc;

namespace Debra_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventController : Controller
    {
        private IMapper _mapper;
        private IEventRepository _eventRepository;

        public EventController(IMapper mapper, IEventRepository eventRepository)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
        }

        [HttpPost]
        public IActionResult AddNewEvent(EventDTO newEvent)
        {
            return View();
        }
    }
}
