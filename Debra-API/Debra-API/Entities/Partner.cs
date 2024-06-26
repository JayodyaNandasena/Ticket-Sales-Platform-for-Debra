using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Debra_API.Entities
{
    public class Partner
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime RegisteredDate { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Email { get; set; }
        public PartnerAccount Account { get; set; }
    }
}
