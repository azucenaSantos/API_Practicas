using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController //Hereda de BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService; //Interfaz para el token


        public AccountController(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService; //Inyectamos el servicio de token
        }

        [HttpPost("register")] //POST: api/account/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto) //en vez de pasar nombre y contra pasamos un objeto con esas propiedades
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

            return new UserDto
            {
                Username = user.UserName, //Devolvemos el nombre de usuario
                Token = _tokenService.CreateToken(user) //Devolvemos el token creado por el servicio de token
            }; //Devolvemos el usuario creado
        }

        [HttpPost("login")] //POST: api/account/login
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            //Recibimos un objeto "loginDto" con el nombre de usuario y la contraseña
            //Buscamos el usuario en la base de datos
            var user = await _context.Users.SingleOrDefaultAsync(x =>
            x.UserName == loginDto.UserName); //Buscamos el usuario en la base de datos
                                              //Si no existe nos dará null por la funcion

            if (user == null) return Unauthorized("Invalid username"); //Si no existe devolvemos un error 401

            //Comprobamos la password
            using var hmac = new HMACSHA512(user.PasswordSalt); //Generamos el hash de la contraseña que se pasó anteriormente
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    return Unauthorized("Invalid password"); //Si la contraseña no coincide devolvemos un error 401
                }
            }
            return new UserDto
            {
                Username = user.UserName, //Devolvemos el nombre de usuario
                Token = _tokenService.CreateToken(user) //Devolvemos el token creado por el servicio de token
            }; //Devolvemos el usuario creado
        }

        //Verificamos si el usuario ya existe
        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower()); //Verificamos si existe un usuario con el mismo nombre de usuario
                                                                                         //x es un "Username" y a partir de el se comprueba si existe un usuario con el mismo nombre de usuario
        }


    }

}
