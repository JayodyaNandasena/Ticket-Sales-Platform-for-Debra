using AutoMapper;
using Debra_API.DTOs;
using Debra_API.DTOs.EventDTOs;
using Debra_API.DTOs.EventDTOs.TicketDTOs;
using Debra_API.Entities;
using Debra_API.Repositories.CustomerRepositories;
using Debra_API.Repositories.EventRepositories;
using Debra_API.Repositories.PartnerRepositories;
using Debra_API.Repositories.TicketDetailsRepositories;
using Debra_API.Repositories.TicketRepositories;
using Microsoft.AspNetCore.Mvc;

namespace Debra_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TicketController : Controller
    {
        private IMapper _mapper;
        private IEventRepository _eventRepository;
        private IPartnerRepository _partnerRepository;
        private ITicketDetailsRepository _ticketDetailsRepository;
        private ITicketRepository _ticketRepository;
        private ICustomerRepository _customerRepository;

        public TicketController(IMapper mapper, IEventRepository eventRepository,
            IPartnerRepository partnerRepository, ITicketDetailsRepository ticketDetailsRepository,
            ITicketRepository ticketRepository, ICustomerRepository customerRepository)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
            _partnerRepository = partnerRepository;
            _ticketDetailsRepository = ticketDetailsRepository;
            _ticketRepository = ticketRepository;
            _customerRepository = customerRepository;
        }


        [HttpPost]
        public IActionResult BuyTickets([FromBody] BuyTicketsDTO request)
        {
            if (! _ticketRepository.CheckAvailability(
                request.quantity, request.EventId))
            {
                return Ok(new OperationResultResponseDTO<string>
                {
                    Status = Status.Failed,
                    Result = "Tickets Not Available"
                }
                );
            }

            Customer customer = _mapper.Map<Customer>(request.Customer);

            int custId = _customerRepository.Add(customer);

            if (! _ticketRepository.ChangeStatus(request.quantity, request.EventId, custId))
            {
                return Ok(new OperationResultResponseDTO<string>
                {
                    Status = Status.Failed,
                    Result = "Error"
                }
                );
            }

            return Ok(new OperationResultResponseDTO<string>
            {
                Status = Status.Success,
                Result = "Purchase Successful"
            }
                );
        }

    }
}
