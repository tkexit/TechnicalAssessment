using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace WebApi.Exceptions
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

		private Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			var result = new Error("Unexpected", exception.Message);
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = 500; //TODO add 4** error
			var serializeError = JsonSerializer.Serialize(result);
			return context.Response.WriteAsync(serializeError);
		}
	}
}