namespace Debra_API.DTOs.EventDTOs.TicketDTOs
{
    public class TicketCreateDTO
    {
        public TicketCreateDTO(int eventId, int categoryId)
        {
            EventId = eventId;
            CategoryId = categoryId;
            IsSold = false;
        }

        public string Id { get; set; } = null!;
        public bool IsSold { get; set; }
        public int EventId { get; set; }
        public int CategoryId { get; set; }
    }
}
