using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Text.Json;
using GestionITM.Domain.Models;
using GestionITM.Domain.Exceptions;

namespace GestionITM.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // flujo normal
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message); // guardar en log
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            
            var statusCode = ex switch
            {
                AppException appEx => appEx.StatusCode,          // usa el código de la excepción de dominio
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                ArgumentException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };

            context.Response.StatusCode = statusCode;

            // Si es AppException usamos su Message (es un mensaje de negocio limpio y puntual).
            // Si es cualquier otra excepción usamos un mensaje genérico para no exponer internos.
            var message = ex is AppException
                ? ex.Message
                : statusCode switch
                {
                    (int)HttpStatusCode.NotFound => "El recurso solicitado no fue encontrado en el sistema del ITM.",
                    (int)HttpStatusCode.BadRequest => "La petición enviada no es válida. Verifique los datos.",
                    _ => "Ocurrió un error inesperado. Por favor intente nuevamente más tarde."
                };

            var response = new ErrorResponse
            {
                StatusCode = statusCode,
                Message = message,
                // Solo mostramos el StackTrace en desarrollo, nunca en producción
                Details = _env.IsDevelopment() ? ex.StackTrace?.ToString() : null
            };

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);
        }
    }
}