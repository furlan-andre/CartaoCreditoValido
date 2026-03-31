using System.Net;
using System.Text.Json;
using CartaoCreditoValido.Domain.Commons.Exceptions;
using FluentValidation;

namespace CartaoCreditoValido.WebAPI.Middlewares;

public sealed class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";

            var messages = ex.Errors.Select(x => x.ErrorMessage).Distinct().ToArray();
            var payload = JsonSerializer.Serialize(new { messages });

            await context.Response.WriteAsync(payload);
        }
        catch (DomainException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";

            var payload = JsonSerializer.Serialize(new { message = ex.Message });
            await context.Response.WriteAsync(payload);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var payload = JsonSerializer.Serialize(new { message = ex.Message });
            await context.Response.WriteAsync(payload);
        }
    }
}

