using System.ComponentModel.DataAnnotations;

namespace Debra_API.Entities
{
    public class Customer
    {
        [Key]
        public required string Id { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Mobile { get; set; }
        [Required]
        public required string Email { get; set; }
    }
}
