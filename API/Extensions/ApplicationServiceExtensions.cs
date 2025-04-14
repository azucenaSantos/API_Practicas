
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions //Creamos la clase estática para 
    // poder usar los metodos de dentro de ella sin necesidad de instanciar la clase
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration config)
        {
            //Añado servicio de "DataContext" para la base de datos
            //Añado el servicio y entre "<>" le paso el tipo de dato que va a manejar, en este caso "DataContext

            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
                //le pasamos una cadena de conexion, que obtenemos de la configuracion
            });

            services.AddCors(); //Servicio de CORS para permitir el acceso desde el frontend

            //Añadimos el servicio que hemos creado manualmente, sin ser del framwork
            services.AddScoped<ITokenService, TokenService>(); //Implementa la interfaz y el la clase del servicio creado

            //Retornamos una coleccione de servicios
            return services; //Retornamos la coleccion de servicios para que se pueda usar en el programa.cs
        }

    }
}