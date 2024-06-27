using System.ComponentModel.DataAnnotations.Schema;
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
        public Event Event { get; set; } = null!;
        [Required]
        public Category Category { get; set; } = null!; // Required foreign key property
        public Customer? Customer { get; set; } = null!; // Optional foreign key property
    }
}
