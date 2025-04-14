using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    /*[ApiController]
    [Route("api/[controller]")] //api/users --> asi es como accedemos a este controlador*/

    [Authorize] //Requiere autenticacion para acceder a TODOS los endpoint(httpget, httpgetId) --> solo los usuarios autenticados (pasan un token) pueden acceder a este controlador
    //No necesitamos lo anterior porque UserController hereda de BaseApiController
    public class UsersController : BaseApiController //ControllerBase es una clase base para controladores de API que no requieren vistas
    {
        private readonly DataContext _context; //Inyectamos el contexto de datos para acceder a la base de datos

        public UsersController(DataContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        //Endpoint para obtener todos los usuarios
        [HttpGet] //GET api/users
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync(); //Obtenemos todos los usuarios de la base de datos
            return users; //Retornamos la lista de usuarios
        }

        //Endpoint para obtener un usuario solo x id
        [HttpGet("{id}")] //GET api/users/1 --> especificamos el id del usuario a obtener
        public async Task<ActionResult<AppUser>> GetUser(int id) //Solo devuelve un usuario, pasamos id por argumento
        {
            /*var user = _context.Users.Find(id); //Buscamos el usuario por su id en la lista de usuario
            if (user == null) //Si no existe el usuario
            {
                return NotFound(); //Retornamos un 404 Not Found
            }
            return user; //Retornamos el usuario encontrado*/
            return await _context.Users.FindAsync(id);
        }
    }
}