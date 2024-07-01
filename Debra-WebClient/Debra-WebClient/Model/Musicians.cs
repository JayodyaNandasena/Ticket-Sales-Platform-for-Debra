namespace Debra_WebClient.Model
{
    public class Musicians
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string ImageBase64 { get; set; } = null!;
        public List<Events> Events { get; set; } = [];
    }
}
