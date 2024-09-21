

using ThunderAPI.Business.Services;
using ThunderAPI.Domain.Interfaces;

namespace ThunderAPI.Api.Configurations
{
	public static class ConfigurationServices
	{
		public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<ITaskService, TaskService>();
			return services;
		}
		public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<ITaskRepository>(provider =>
			 new TaskRepository(configuration.GetConnectionString("DefaultConnection")));
			return services;
		}
	}
}