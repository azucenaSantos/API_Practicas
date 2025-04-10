using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
//Añado servicio de "DataContext" para la base de datos
    //Añado el servicio y entre "<>" le paso el tipo de dato que va a manejar, en este caso "DataContext

builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
    //le pasamos una cadena de conexion, que obtenemos de la configuracion
});

builder.Services.AddCors(); //Servicio de CORS para permitir el acceso desde el frontend

var app = builder.Build();

// Configure the HTTP request pipeline.
//Añadimos el middleware de CORS para permitir el acceso desde el frontend
app.UseCors(builder=> builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

app.MapControllers();

app.Run();
