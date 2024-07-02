using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Debra_API.Entities
{
    public class Musician
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public byte[]? Image { get; set; } = [];

        public int EventId { get; set; }
        //[JsonIgnore]
        [Required]
        public Event Event { get; set; }
	}
}
