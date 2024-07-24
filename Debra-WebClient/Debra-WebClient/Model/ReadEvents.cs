namespace Debra_WebClient.Model
{
	public class ReadEvents
	{
		public int Id { get; set; }
		public string Title { get; set; } = null!;
		public string Description { get; set; } = null!;
		public DateOnly Date { get; set; }
		public TimeOnly StartTime { get; set; }
		public TimeOnly EndTime { get; set; }
		public string Location { get; set; } = null!;
		public byte[] Image { get; set; } = [];

		public Partners Partner { get; set; } = null!;
		public ICollection<Musicians> Musicians { get; set; } = [];
		public ICollection<Bands> Bands { get; set; } = [];
	}
}
