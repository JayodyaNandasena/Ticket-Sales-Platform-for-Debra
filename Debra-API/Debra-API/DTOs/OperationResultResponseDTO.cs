namespace Debra_API.DTOs
{
    public class OperationResultResponseDTO<T>
    {
        public string Status { get; set; }
        public T Result { get; set; }
    }
}
