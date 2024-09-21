using Newtonsoft.Json;
using ThunderAPI.Api.HttpResponseCommon;

namespace ThunderAPI.Api.Middlewares
{
	public class ErrorHandlingMiddleware
	{
		private readonly RequestDelegate _next;

		public ErrorHandlingMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}

		private static Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			var response = ApiResponse.FromFailure("An error occurred while processing your request.");
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = StatusCodes.Status500InternalServerError;
			return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
		}
	}
}