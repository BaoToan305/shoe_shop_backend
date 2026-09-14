using Microsoft.AspNetCore.Mvc;
using shoe_shop_backend.Application.ResquestResponse;
using shoe_shop_backend.Domain.Exception;
using System.Net;
using System.Text.Json;

namespace shoe_shop_backend.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger,
        IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
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

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var (statusCode, defaultMessage) = ex switch
        {
            NotFoundException => (HttpStatusCode.NotFound, "Không tìm thấy dữ liệu"),
            ValidationAppException => (HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ"),
            BusinessRuleException => (HttpStatusCode.BadRequest, "Yêu cầu không hợp lệ"),
            BadRequestException => (HttpStatusCode.BadRequest, "Yêu cầu không hợp lệ"),
            ForbiddenException => (HttpStatusCode.Forbidden, "Không có quyền truy cập"),
            ConflictException => (HttpStatusCode.Conflict, "Xung đột dữ liệu"),
            _ => (HttpStatusCode.InternalServerError, "Đã xảy ra lỗi hệ thống")
        };

        if (statusCode == HttpStatusCode.InternalServerError)
            _logger.LogError(ex, "Unhandled exception occurred");
        else
            _logger.LogWarning(ex, "Handled exception occurred: {Message}", ex.Message);

        var message = _env.IsDevelopment() || statusCode != HttpStatusCode.InternalServerError
            ? ex.Message
            : "Đã có lỗi xảy ra, vui lòng thử lại sau.";

        object? data = null;

        if (ex is ValidationAppException validationEx && validationEx.Errors.Count > 0)
        {
            data = new { errors = validationEx.Errors };
        }

        if (_env.IsDevelopment() && statusCode == HttpStatusCode.InternalServerError)
        {
            data = new { stackTrace = ex.StackTrace };
        }

        // Truyền đúng statusCode vào ApiResponse để field "status" phản ánh đúng thực tế
        var response = ApiResponse<object>.Fail(message, statusCode, data);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var result = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(result);
    }
}

public static class ExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}

public class UnauthorizedAppException : Exception
{
    public UnauthorizedAppException(string message = "Token không hợp lệ hoặc đã hết hạn")
        : base(message) { }
}