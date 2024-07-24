using Debra_API.DTOs.CustomerDTOs;

namespace Debra_API.DTOs.EventDTOs.TicketDTOs
{
    public class BuyTicketsDTO
    {
        public int EventId { get; set; }
        public CustomerDTO Customer { get; set; }
        public int quantity { get; set; }
    }
}
