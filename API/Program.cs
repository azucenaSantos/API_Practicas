using System.Text;
using API.Data;
using API.Extensions;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

/*//ESTOS SERVICIOS DE LA APLICACION PASAN A ESTAR EN APPLICATIONSERVICEEXTENSIONS.CS	
//Añado servicio de "DataContext" para la base de datos
//Añado el servicio y entre "<>" le paso el tipo de dato que va a manejar, en este caso "DataContext

builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
    //le pasamos una cadena de conexion, que obtenemos de la configuracion
});

builder.Services.AddCors(); //Servicio de CORS para permitir el acceso desde el frontend

//Añadimos el servicio que hemos creado manualmente, sin ser del framwork
builder.Services.AddScoped<ITokenService, TokenService>();*/ //Implementa la interfaz y el la clase del servicio creado

builder.Services.AddApplicationServices(builder.Configuration); //Llamamos al metodo de la clase "ApplicationServiceExtensions" para añadir los servicios (que están llamados en applicationserviceextensions.cs)



//ESTE SERVICIO DE IDENTITY PASA A ESTAR EN IDENTITYSERVICEEXTENSIONS.CS
//Servicio de Identity para la autenticacion de usuarios
/*builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        //Reglas de validacion
        ValidateIssuerSigningKey = true, //Validar la clave de firma del emisor
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.
            UTF8.GetBytes(builder.Configuration["TokenKey"])), //Clave simetrica para firmar el token
        ValidateIssuer = false, //Validar el emisor
        ValidateAudience = false //Validar el receptor
    };
});*/

builder.Services.AddIdentityServices(builder.Configuration); //Llamamos al metodo de la clase "IdentityServiceExtensions" para añadir los servicios (que están llamados en identityserviceextensions.cs)

var app = builder.Build();



// Configure the HTTP request pipeline.
//Añadimos el middleware de CORS para permitir el acceso desde el frontend
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod()
    .WithOrigins("https://localhost:4200"));

//Añadimos el middleware de autenticacion para validar el token
app.UseAuthentication(); //Middleware de autenticacion --> ¿Token valido?
app.UseAuthorization(); //Middleware de autorizacion --> Token valido, ¿Qué tienes permitido hacer?

app.MapControllers();

app.Run();
