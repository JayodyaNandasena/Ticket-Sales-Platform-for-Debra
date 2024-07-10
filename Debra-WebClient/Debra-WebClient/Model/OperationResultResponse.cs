namespace Debra_WebClient.Model
{
	public enum Status
	{
		Failed,
		Success
	}

	public class OperationResultResponse<T>
	{
		public Status Status { get; set; }
		public T? Result { get; set; }
	}
}
