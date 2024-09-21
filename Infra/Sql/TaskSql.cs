public static class TaskSql
{
	public const string GetTaskById = "SELECT * FROM Tasks WHERE Id = @Id";
	public const string GetAllTasks = "SELECT Id, Title, Description, CreatedAt, DueDate, IsCompleted  FROM Tasks";
	public const string AddTask = "INSERT INTO Tasks (Id, Title, Description, CreatedAt, IsCompleted) VALUES (@Id, @Title, @Description, @CreatedAt, @IsCompleted)";
	public const string UpdateTask = "UPDATE Tasks SET Title = @Title, Description = @Description, DueDate = @DueDate WHERE Id = @Id";
	public const string DeleteTask = "DELETE FROM Tasks WHERE Id = @Id";
}
