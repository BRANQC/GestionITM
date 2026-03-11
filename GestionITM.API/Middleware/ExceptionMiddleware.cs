using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Text.Json;
using GestionITM.Domain.Models;

namespace GestionITM.API.Middleware
{
    // Nota pedagógica:
    // Este middleware vive en la capa API porque:
    // - Trabaja directamente con HttpContext, RequestDelegate y el pipeline HTTP de ASP.NET Core.
    // - Forma parte de la capa de presentación: se encarga de cómo respondemos al cliente (status codes, JSON de error).
    // La capa Infrastructure se enfoca en acceso a datos (DbContext, repositorios) y no debería depender de ASP.NET Core.
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next; // El siguiente paso en la tubería
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Intentar seguir el flujo normal
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message); // Guardar el error en el log
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            // ANTES EN CLASE: siempre devolvíamos 500 para cualquier excepción
            //   context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //   var response = new ErrorResponse
            //   {
            //       StatusCode = context.Response.StatusCode,
            //       Message = "Ocurrió un error interno en el servidor del ITM.",
            //       Details = _env.IsDevelopment() ? ex.StackTrace?.ToString() : null
            //   };
            // RETO: ahora usamos un switch para personalizar el StatusCode según el tipo de excepción

            // Determinar el código de estado según el tipo de excepción
            var statusCode = ex switch
            {
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                ArgumentException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };
            context.Response.StatusCode = statusCode;

            var response = new ErrorResponse
            {
                StatusCode = statusCode,
                Message = statusCode switch
                {
                    (int)HttpStatusCode.NotFound => "El recurso solicitado no fue encontrado en el sistema del ITM.",
                    (int)HttpStatusCode.BadRequest => "La petición enviada no es válida. Verifique los datos.",
                    _ => "Ocurrió un error interno en el servidor del ITM."
                },
                // Si estamos en desarrollo, mostramos el error real. En producción, no.
                Details = _env.IsDevelopment() ? ex.StackTrace?.ToString() : null
            };

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);
        }
    }
}