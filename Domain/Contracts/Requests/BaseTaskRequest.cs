namespace ThunderAPI.Domain.Contracts.Requests
{
	public class BaseTaskRequest
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime? DueDate { get; set; }
	}
}
