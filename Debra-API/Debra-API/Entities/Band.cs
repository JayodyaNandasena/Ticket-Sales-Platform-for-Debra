using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Debra_API.Entities
{
    public class Band
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public byte[]? Image { get; set; } = [];
		//[JsonIgnore]
		public int EventId { get; set; }

		[Required]
		public required Event Event { get; set; }
	}
}
