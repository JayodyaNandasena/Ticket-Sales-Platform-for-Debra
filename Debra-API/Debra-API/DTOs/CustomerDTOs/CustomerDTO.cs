using System.ComponentModel.DataAnnotations;

namespace Debra_API.Entities
{
    public class CustomerDTO
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Mobile { get; set; }
        public required string Email { get; set; }
    }
}
