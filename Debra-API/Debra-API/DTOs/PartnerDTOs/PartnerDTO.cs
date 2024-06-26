namespace Debra_API.DTOs.PartnerDTOs
{
    public class PartnerDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime RegisteredDate { get; set; }
        public string Type { get; set; }
        public string Email { get; set; }
        public PartnerAccountDTO Account { get; set; }
    }
}
