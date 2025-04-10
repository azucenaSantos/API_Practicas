
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController //Hereda de BaseApiController
    {
        private readonly DataContext _context;

        public AccountController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("register")] //POST: api/account/register
        public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto) //en vez de pasar nombre y contra pasamos un objeto con esas propiedades
        {

            if (await UserExists(registerDto.UserName)) //Verificamos si el usuario ya existe con el metodo privado
            {
                return BadRequest("Username is taken"); //Si existe devolvemos un error 400
            }
            using var hmac = new HMACSHA512(); //Genera un hash para la contraseña

            var user = new AppUser
            { //creamos una nueva entidad "AppUser"
                UserName = registerDto.UserName.ToLower(), //Convertimos el nombre de usuario a minusculas
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)), //Convierte la contraseña a bytes y genera el hash
                PasswordSalt = hmac.Key
            };

            //Le decimos al framework que quremos añadir un usuario
            _context.Users.Add(user);
            //Para guardar al usuario debemos hacer:
            await _context.SaveChangesAsync(); //Guarda los cambios en la base de datos

            //Como el el método especificamos que devolvemos un AppUser, debemos devolverlo
            return user; //Devolvemos el usuario creado
        }

        //Verificamos si el usuario ya existe
        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower()); //Verificamos si existe un usuario con el mismo nombre de usuario
                                                                                         //x es un "Username" y a partir de el se comprueba si existe un usuario con el mismo nombre de usuario
        }


    }

}
