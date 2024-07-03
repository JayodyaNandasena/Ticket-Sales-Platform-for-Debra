namespace Debra_API.DTOs.EventDTOs.TicketDTOs
{
    public class TicketCreateDTO
    {
        public string Id { get; set; } = null!;
        public bool IsSold { get; set; } = false;
        public int EventId { get; set; }
        public int DetailsId { get; set; }
    }
}
