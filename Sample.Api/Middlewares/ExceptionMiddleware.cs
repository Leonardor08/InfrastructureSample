using Sample.Application.Services;
using Sample.Application.Interfaces.Repositories;
using Sample.Domain.Models;
using Sample.Domain.Resources.Constants;
using System.Net;
using System.Text.Json;

namespace Sample.Api.Middlewares;

public class ExceptionMiddleware(RequestDelegate next, IHostEnvironment env, ILogger<ExceptionMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly IHostEnvironment _env = env;
	private readonly ILogger<ExceptionMiddleware> _logger = logger;


	public async Task InvokeAsync(HttpContext context, IRepository<ErrorLog, Guid> repository)
    {
		context.Request.EnableBuffering();
		try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
			await HandleExceptionAsync(context, ex, _env, repository);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex, IHostEnvironment env, IRepository<ErrorLog, Guid> repository)
    {
        ExceptionErrorPersistence serviceLogPersistance = new(repository);
        context.Response.ContentType = MiddlewareConstants.JSON;
        int statusCode = (int)HttpStatusCode.InternalServerError;
        string result = string.Empty;

        switch (ex)
        {
            case ValidatorException validatorException:
				result = JsonSerializer.Serialize(new Response { Success = false, Message = MiddlewareConstants.VALIDATION_ERROR, Errors = validatorException.Errors });
				context.Response.StatusCode = statusCode;
                await SavingExceptions(context, ex, serviceLogPersistance);
                break;
			case InvalidOperationException invalidOperationException:
				result = JsonSerializer.Serialize(new Response { Success = false, Message = invalidOperationException.Message, Errors = [new() { Code= MiddlewareConstants.INVALID_CODE_ERROR , Message = invalidOperationException.Message }] });
                context.Response.StatusCode = statusCode;
                await SavingExceptions(context, ex, serviceLogPersistance);
                break;
            case UnauthorizedAccessException unauthorizedAccessException:
				result = JsonSerializer.Serialize(new Response { Success = false, Message = unauthorizedAccessException.Message, Errors = [new() { Code = MiddlewareConstants.INVALID_CODE_ERROR, Message = unauthorizedAccessException.Message }] });
				context.Response.StatusCode = statusCode;
                await SavingExceptions(context, ex, serviceLogPersistance);
                break;
            case FormatException formatException:
				result = JsonSerializer.Serialize(new Response { Success = false, Message = formatException.Message, Errors = [new() { Code = MiddlewareConstants.INVALID_CODE_ERROR, Message = formatException.Message }] });
                context.Response.StatusCode = statusCode;
                await SavingExceptions(context, ex, serviceLogPersistance);
                break;
            case TimeoutException timeOutException:
				result = JsonSerializer.Serialize(new Response { Success = false, Message = timeOutException.Message, Errors = [new() { Code = MiddlewareConstants.INVALID_CODE_ERROR, Message = timeOutException.Message }] });
                context.Response.StatusCode = statusCode;
                await SavingExceptions(context, ex, serviceLogPersistance);
                break;
            case IOException IOException:
				result = JsonSerializer.Serialize(new Response { Success = false, Message = IOException.Message, Errors = [new() { Code = MiddlewareConstants.INVALID_CODE_ERROR, Message = IOException.Message }] });
                context.Response.StatusCode = statusCode;
                break;
        }
        if (string.IsNullOrEmpty(result) &&  env.IsDevelopment())
        {
			result = JsonSerializer.Serialize(new Response { Success = false, Message = MiddlewareConstants.INTERNAL_ERROR, Errors = [new(){ Code = statusCode.ToString(), Message = MiddlewareConstants.INTERNAL_ERROR }] });
            context.Response.StatusCode = statusCode;
            await SavingExceptions(context, ex, serviceLogPersistance);
        }
        else if(string.IsNullOrEmpty(result) && env.IsProduction())
        {
			result = JsonSerializer.Serialize(new Response { Success = false, Message = MiddlewareConstants.INTERNAL_ERROR, Errors = [new() { Code = statusCode.ToString(), Message = MiddlewareConstants.INTERNAL_ERROR }] });
			context.Response.StatusCode = statusCode;
            await SavingExceptions(context, ex, serviceLogPersistance);
        }

        await context.Response.WriteAsync(result);
    }

    private async Task SavingExceptions(HttpContext context, Exception ex, ExceptionErrorPersistence serviceLogPersistance)
    {
		
		ErrorLog log = new() { Request = $" {context.Request.Method}  {context.Request.Path} {await ReadRequestBodyAsync(context.Request)} ", LogException = ex.Message, StackTrace = ex.StackTrace ?? string.Empty };
		_logger.LogError("ERROR en  {Path}, error {error}",log.Request,ex.Message );
	}
	private static async Task<string> ReadRequestBodyAsync(HttpRequest request)
	{
		request.Body.Position = 0;
		using StreamReader reader = new(request.Body, leaveOpen: true);
		string requestBody = await reader.ReadToEndAsync();
		request.Body.Position = 0;
		return requestBody;
	}
}
