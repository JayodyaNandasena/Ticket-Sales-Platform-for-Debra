namespace Debra_WebClient.Model
{
    public class Tickets
    {
        public string Id { get; set; } = null!;
        public bool IsSold { get; set; }
        public CreateEvents Event { get; set; } = null!;
        public TicketDetails Category { get; set; } = null!;
        public Customers? Customer { get; set; } = null!;
    }
}
