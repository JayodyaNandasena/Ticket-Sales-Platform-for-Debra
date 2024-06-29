namespace Debra_API.DTOs
{
    public class OperationResultResponseDTO<T>
    {
        public string Status { get; set; } = null!;
        public T Result { get; set; }
	}
}
