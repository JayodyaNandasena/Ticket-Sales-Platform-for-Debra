using Debra_API.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Debra_API.DTOs.PartnerDTOs;

namespace Debra_API.DTOs.EventDTOs
{
	public class EventReadDTO
	{
		public int Id { get; set; }
		public string Title { get; set; } = null!;
		public string Description { get; set; } = null!;
		public DateOnly Date { get; set; }
		public TimeOnly StartTime { get; set; }
		public TimeOnly EndTime { get; set; }
		public string Location { get; set; } = null!;
		public string ImageBase64 { get; set; }

		public PartnerDTO Partner { get; set; } = null!;

		//public ICollection<Ticket> Tickets { get; set; } = [];
		public ICollection<MusicianDTO> Musicians { get; set; } = [];
		public ICollection<BandDTO> Bands { get; set; } = [];
	}
}
