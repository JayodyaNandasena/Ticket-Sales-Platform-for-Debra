using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Debra_API.Entities
{
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public TimeOnly StartTime { get; set; }

        [Required]
        public TimeOnly EndTime { get; set; }

        [Required]
        public string Location { get; set; } = null!;

		public byte[]? Image { get; set; } = [];
		public int PartnerId { get; set; }

		[Required]
        public Partner Partner { get; set; } = null!;

        public ICollection<Ticket> Tickets { get; set; } = [];
        public ICollection<Musician> Musicians { get; set; } = [];
        public ICollection<Band> Bands { get; set; } = [];
    }
}
