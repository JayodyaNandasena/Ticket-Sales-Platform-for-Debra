namespace Debra_WebClient.Model
{
    public enum UserRole
    {
        ADMIN,PARTNER,CUSTOMER
    }

    public class User
    {
        public string UserName { get; set; }
        public UserRole Role { get; set; }
    }
}
