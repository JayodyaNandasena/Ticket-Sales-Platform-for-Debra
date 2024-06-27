using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Debra_API.Entities
{
    public class Partner
    {
        public Partner()
        {
        }

        public Partner(int id, string name, DateTime registeredDate, string type, string email, PartnerAccount account)
        {
            Id = id;
            Name = name;
            RegisteredDate = registeredDate;
            Type = type;
            Email = email;
            Account = account;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime RegisteredDate { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Email { get; set; }
        public PartnerAccount Account { get; set; }


    }
}
