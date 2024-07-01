using Debra_API.Entities;
using System.ComponentModel.DataAnnotations;

namespace Debra_API.DTOs.EventDTOs.TicketDTOs
{
    public class CategoryDTO
    {
        public CategoryDTO(decimal unitPrice, decimal commisionPerTicket)
        {
            UnitPrice = unitPrice;
            CommisionPerTicket = commisionPerTicket;
        }

        public CategoryDTO(int id, decimal unitPrice, decimal commisionPerTicket)
        {
            Id = id;
            UnitPrice = unitPrice;
            CommisionPerTicket = commisionPerTicket;
        }

        public int Id { get; set; }
        //public string Name { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public decimal CommisionPerTicket { get; set; }
        //public List<EventTicketCreateDTO> Tickets { get; } = [];
    }
}
