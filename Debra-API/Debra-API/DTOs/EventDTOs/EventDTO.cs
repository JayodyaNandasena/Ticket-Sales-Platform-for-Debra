using Debra_API.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Debra_API.DTOs.PartnerDTOs;
using Debra_API.DTOs.EventDTOs.TicketDTOs;

namespace Debra_API.DTOs.EventDTOs
{
    public class EventDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateOnly Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Location { get; set; } = null!;
        public string ImageBase64 { get; set; } = null!;
        public PartnerDTO Partner { get; set; } = null!;
        public List<TicketDTO> Tickets { get; set; } = [];
        public List<MusicianDTO> Musicians { get; set; } = [];
        public List<BandDTO> Bands { get; set; } = [];
    }
}
