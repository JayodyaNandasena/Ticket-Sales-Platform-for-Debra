using System.ComponentModel.DataAnnotations;

namespace Debra_API.Entities
{
    public class AdminAccount
    {
        [Key]
        public required string Username { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
