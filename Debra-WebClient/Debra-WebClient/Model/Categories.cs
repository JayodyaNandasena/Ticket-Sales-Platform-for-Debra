namespace Debra_WebClient.Model
{
    public class Categories
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public decimal CommisionPerTicket { get; set; }
        public List<Tickets> Tickets { get; } = [];
    }
}
