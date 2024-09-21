using Microsoft.EntityFrameworkCore;
using ThunderAPI.Domain.Entities;

public class TestDbContext : DbContext
{
    public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }

    public DbSet<TaskEntity> Tasks { get; set; }
}
