namespace Debra_API.DTOs
{
    public enum Status
    {
        Failed,
        Success        
    }

    public class OperationResultResponseDTO<T>
    {
        public Status Status { get; set; }
        public T? Result { get; set; }
	}
}
