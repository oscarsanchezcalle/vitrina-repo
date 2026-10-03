using System.Net;
using System.Text.Json;
using Vitrina.Domain.Exceptions;
using Vitrina.Dto.Common;

namespace Vitrina.Api.Middleware;

public sealed class GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
{
	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await next(context);
		}
		catch (Exception exception)
		{
			logger.LogError(exception, "Unhandled exception occurred while processing the request.");
			await WriteResponseAsync(context, exception);
		}
	}

	private static async Task WriteResponseAsync(HttpContext context, Exception exception)
	{
		if (context.Response.HasStarted)
		{
			throw exception;
		}

		var (statusCode, payload) = exception switch
		{
			EntityNotFoundException notFoundException => (HttpStatusCode.NotFound, ValidationResultDto.Failure(notFoundException.Message, notFoundException.Message)),
			DomainException domainException => (HttpStatusCode.BadRequest, ValidationResultDto.Failure(domainException.Message, domainException.Message)),
			InvalidOperationException invalidOperationException => (HttpStatusCode.InternalServerError, ValidationResultDto.Failure(invalidOperationException.Message, invalidOperationException.Message)),
			_ => (HttpStatusCode.InternalServerError, ValidationResultDto.Failure("An unexpected error occurred.", exception.Message))
		};

		context.Response.Clear();
		context.Response.StatusCode = (int)statusCode;
		context.Response.ContentType = "application/json";

		await context.Response.WriteAsync(JsonSerializer.Serialize(payload));
	}
}
