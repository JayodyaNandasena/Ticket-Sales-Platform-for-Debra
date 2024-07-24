using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Debra_API.Entities
{
    public class Partner
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public DateTime RegisteredDate { get; set; }
        [Required]
        public string Type { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        public PartnerAccount Account { get; set; } = null!;
        [JsonIgnore]
        public ICollection<Event> Events { get; } = [];
    }
}
