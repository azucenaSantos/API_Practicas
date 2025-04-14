

using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services,
        IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        //Reglas de validacion
                        ValidateIssuerSigningKey = true, //Validar la clave de firma del emisor
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.
                            UTF8.GetBytes(config["TokenKey"])), //Clave simetrica para firmar el token
                        ValidateIssuer = false, //Validar el emisor
                        ValidateAudience = false //Validar el receptor
                    };
                });
            //Retornamos una coleccion de servicios
            return services;
        }

    }
}