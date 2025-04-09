using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //api/users --> asi es como accedemos a este controlador
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        //Endpoint para obtener todos los usuarios
        [HttpGet] //GET api/users
        public ActionResult<IEnumerable<AppUser>> GetUsers()
        {
            var users= _context.Users.ToList(); //Obtenemos todos los usuarios de la base de datos
            return users; //Retornamos la lista de usuarios
        }

        //Endpoint para obtener un usuario solo x id
        [HttpGet("{id}")] //GET api/users/1 --> especificamos el id del usuario a obtener
        public ActionResult<AppUser> GetUser(int id) //Solo devuelve un usuario, pasamos id por argumento
        {
            /*var user = _context.Users.Find(id); //Buscamos el usuario por su id en la lista de usuario
            if (user == null) //Si no existe el usuario
            {
                return NotFound(); //Retornamos un 404 Not Found
            }
            return user; //Retornamos el usuario encontrado*/
            return _context.Users.Find(id);
        }
    }
}