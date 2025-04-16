using System.Net;
using System.Text.Json;
using API.Errors;

namespace API.Middleware
{
    public class ExceptionMiddleware //Midleware: software que se integra en 
                                     //una canalización de aplicaciones para gestionar solicitudes y respuestas
    {

        readonly RequestDelegate _next;
        readonly ILogger<ExceptionMiddleware> _logger;
        readonly IHostEnvironment _env;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger,
        IHostEnvironment env) //Argumentos necesarios para nuestro Middleware
        {
            _next = next;
            _logger = logger;
            _env = env;

        }

        //Logica del middleware:

        //El middleware necesita que el metodo se llame asi el metodo
        public async Task InvokeAsync(HttpContext context)
        { //El HttpContext nos da acceso a la peticion Http que se está pasando por el midelware 
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message); //va a dar una salida al terminal donde veremos el mensaje del error
                context.Response.ContentType = "application/json";//lo que devolveremos al cliente
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                //Si estamos modo development hacemos un new ApiException, si no, na new ApiException pero con un error sencillo de entender no con el stacktrace
                var response = _env.IsDevelopment()
                    ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                    : new ApiException(context.Response.StatusCode, ex.Message, "Internal Server Error");

                //Tenemos que devolver esta respuesta como json:
                var options= new JsonSerializerOptions{PropertyNamingPolicy= JsonNamingPolicy.CamelCase};
                var json= JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
                //Devolverá la respuesta http cuando nos encontremos con una excepcion
            }

        }
        //Para usar este middleware se utiliza en el archivo "Program.cs"

    }
}