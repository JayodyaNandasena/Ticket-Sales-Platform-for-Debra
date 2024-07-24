using System.ComponentModel.DataAnnotations;

namespace Debra_API.Entities
{
    public class Ticket
    {
        [Key]
        public string Id { get; set; } = null!;
        [Required]
        public bool IsSold { get; set; }
        [Required]
        public int EventId { get; set; }
        [Required]
        public Event Event { get; set; } = null!;
        [Required]
        public int DetailsId {  get; set; }
		[Required]
        public TicketDetails TicketDetails { get; set; } = null!; // Required foreign key property
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; } = null!; // Optional foreign key property
    }
}
