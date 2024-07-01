namespace Debra_WebClient.Model
{
    public class Events
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string Location { get; set; } = null!;
        public string ImageBase64 { get; set; } = null!;
        public Partners Partner { get; set; } = null!;
        public List<Tickets> Tickets { get; set; } = [];
        public List<Musicians> Musicians { get; set; } = [];
        public List<Bands> Bands { get; set; } = [];
    }
}
