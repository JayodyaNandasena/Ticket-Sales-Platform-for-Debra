namespace Debra_API.DTOs.EventDTOs.TicketDTOs
{
    public class EventTicketCreateDTO
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Commision { get; set; }
        //public string Id { get; set; } = null!;
        //public bool IsSold { get; set; }
        //public EventCreateDTO Event { get; set; } = null!;
        //public CategoryDTO Category { get; set; } = null!; // Required foreign key property
        //public CustomerDTO? Customer { get; set; } = null!; // Optional foreign key property
    }
}
