using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Debra_API.Entities
{
    public class PartnerAccount
    {
        [Key]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        [ForeignKey("Partner")]
        public string PartnerId { get; set; }
        public Partner Partner { get; set; }
    }
}

