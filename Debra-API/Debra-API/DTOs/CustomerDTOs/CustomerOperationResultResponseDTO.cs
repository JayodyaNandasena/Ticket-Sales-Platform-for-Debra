using Debra_API.Entities;

namespace Debra_API.DTOs.AdminAccountDTOs
{
    public class CustomerOperationResultResponseDTO
    {
        public string Status { get; set; }
        public CustomerDTO Customer { get; set; }
    }
}
