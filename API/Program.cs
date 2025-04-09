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

builder.Services.AddCors();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(builder=> builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));

app.MapControllers();

app.Run();
