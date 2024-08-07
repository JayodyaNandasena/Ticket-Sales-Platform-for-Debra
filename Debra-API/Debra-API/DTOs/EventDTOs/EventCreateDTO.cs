﻿using Debra_API.DTOs.PartnerDTOs;
using Debra_API.DTOs.EventDTOs.TicketDTOs;

namespace Debra_API.DTOs.EventDTOs
{
    public class EventCreateDTO
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string Location { get; set; } = null!;
        public string Image { get; set; } = null!;
        public int PartnerId { get; set; }
        public EventTicketCreateDTO? Tickets { get; set; }
        public List<MusicianDTO> Musicians { get; set; } = [];
        public List<BandDTO> Bands { get; set; } = [];
    }
}
