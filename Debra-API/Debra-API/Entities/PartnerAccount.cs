using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Debra_API.Entities
{
    public class PartnerAccount
    {
        [Key]
        public string Username { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
		[ForeignKey("Partner")]
		public int PartnerId { get; set; }
		[JsonIgnore]
		public Partner Partner { get; set; } = null!;
	}
}

