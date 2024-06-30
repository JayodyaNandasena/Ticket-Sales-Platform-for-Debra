namespace Debra_API.DTOs.EventDTOs
{
    public class BandDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string ImageBase64 { get; set; } = null!;
        public List<EventDTO> Events { get; set; } = [];
    }
}
