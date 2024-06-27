using System.ComponentModel.DataAnnotations;

namespace Debra_API.Entities
{
    public class AdminAccount
    {
        [Key]
        public string Username { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
