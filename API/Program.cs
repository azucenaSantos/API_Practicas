using API.Extensions;
using API.Middleware;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration); //Llamamos al metodo de la clase "ApplicationServiceExtensions" para añadir los servicios (que están llamados en applicationserviceextensions.cs)
builder.Services.AddIdentityServices(builder.Configuration); //Llamamos al metodo de la clase "IdentityServiceExtensions" para añadir los servicios (que están llamados en identityserviceextensions.cs)

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
//Añadimos el middleware de CORS para permitir el acceso desde el frontend
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod()
    .WithOrigins("https://localhost:4200"));

//Añadimos el middleware de autenticacion para validar el token
app.UseAuthentication(); //Middleware de autenticacion --> ¿Token valido?
app.UseAuthorization(); //Middleware de autorizacion --> Token valido, ¿Qué tienes permitido hacer?

app.MapControllers();

app.Run();
