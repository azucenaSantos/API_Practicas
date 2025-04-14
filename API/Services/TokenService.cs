using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService //Implementa la interfaz creada anteriormente
    {
        private readonly SymmetricSecurityKey _key;   
        public TokenService(IConfiguration config)
        {
            _key= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])); //Crea una clave simétrica a partir de la clave de configuración
            
        }
        //Tiene que implementar el método de la interfaz
        public string CreateToken(AppUser user)
        {
            var claims= new List <Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName) //se establece como token el nombre de usuario con una Claim de tipo NameId
            };

            //Credenciales de firma sobre el token
            var creds= new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature); //se establece el algoritmo de firma HmacSha512
            var tokenDescriptor= new SecurityTokenDescriptor //se crea un descriptor de token
            {
                Subject= new ClaimsIdentity(claims),
                Expires= DateTime.Now.AddDays(7), //se establece la fecha de expiración del token a 7 días
                SigningCredentials= creds //se establecen las credenciales de firma
            };
            var tokenHandler= new JwtSecurityTokenHandler(); //se crea un manejador de token JWT
            var token= tokenHandler.CreateToken(tokenDescriptor); //se crea el token a partir del descriptor

            //Devolvemos el token
            return tokenHandler.WriteToken(token); //se convierte el token a string y se devuelve
        }
    }
}