using Debra_API.Entities;
using System.ComponentModel.DataAnnotations;

namespace Debra_API.DTOs.EventDTOs.TicketDTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public decimal CommisionPerTicket { get; set; }
        public List<TicketDTO> Tickets { get; } = [];
    }
}
