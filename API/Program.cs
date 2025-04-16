using API.Data;
using API.Extensions;
using API.Middleware;
using Microsoft.EntityFrameworkCore;


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

//Cuando se inicia la app:
using var scope= app.Services.CreateScope(); //ACCESO A TODOS LOS SERVICIOS CREADOS DENTRO DE ESTA CLASE
var services= scope.ServiceProvider;
try{
    var context= services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync(); //crea todo de nuevo, bd y semilla
    await Seed.SeedUsers(context);    
}catch(Exception ex){
    var logger= services.GetService<ILogger<Program>>();
    logger.LogError(ex, "An error ocurred during migration");
}


app.Run();
