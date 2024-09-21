namespace ThunderAPI.Domain.Contracts.Requests;
public class UpdateTaskRequest : BaseTaskRequest
{
	public bool IsCompleted { get; set; }
}
