namespace Debra_WebClient.Model
{
    public class CreateEvents
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string Location { get; set; } = null!;
        public string Image { get; set; } = null!;
        public int PartnerId { get; set; }
        public CreateTickets? Tickets { get; set; }
        public List<Musicians> Musicians { get; set; } = [];
        public List<Bands> Bands { get; set; } = [];
    }
}
