using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;

namespace Debra_API.Entities
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //[Required]
        //public string Name { get; set; } = null!;
        [Required]
        public decimal UnitPrice { get; set; }
        [Required]
        public decimal CommisionPerTicket { get; set; }
        [JsonIgnore]
        public ICollection<Ticket> Tickets { get; } = [];


    }
}
