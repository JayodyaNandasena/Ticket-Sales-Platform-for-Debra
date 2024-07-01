namespace Debra_WebClient.Model
{
    public class Partners
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime RegisteredDate { get; set; }
        public string Type { get; set; }
        public string Email { get; set; }
        public PartnerAccounts Account { get; set; }
    }
}
