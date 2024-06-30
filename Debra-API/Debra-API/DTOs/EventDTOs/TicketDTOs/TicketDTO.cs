using Debra_API.DTOs.CustomerDTOs;
using Debra_API.Entities;
using System.ComponentModel.DataAnnotations;

namespace Debra_API.DTOs.EventDTOs.TicketDTOs
{
    public class TicketDTO
    {
        public string Id { get; set; } = null!;
        public bool IsSold { get; set; }
        public EventDTO Event { get; set; } = null!;
        public CategoryDTO Category { get; set; } = null!; // Required foreign key property
        public CustomerDTO? Customer { get; set; } = null!; // Optional foreign key property
    }
}
