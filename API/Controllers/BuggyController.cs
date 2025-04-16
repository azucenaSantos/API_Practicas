//Controlador que solo devolverá respuestas de error HTTP al cliente.
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly DataContext _context;

        public BuggyController(DataContext context)
        {
            _context = context;
        }

        [Authorize] //Asegurarnos de que se hace el request siguiente cuando no está autorizado -> dará error que es lo que queremos
        //Requests
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            return "secret text";

            //401 Unauthorize

        }

        [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotFound()
        {
            var thing = _context.Users.Find(-1);//nunca tendremos un uruario con id -1
            if (thing == null) return NotFound();
            return thing;

            //404 not found
        }

        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {

            var thing = _context.Users.Find(-1);
            var thingToReturn = thing.ToString(); //nunca podremos convertir a string un elemento que sabemos que es null (no hay usuario con id -1== nul)
            return thingToReturn; //lanzará una excepcion

            //System.NullReferenceException: Object reference not set to an instance of an object.
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("This was not a good request"); //siempre lanzará una bad request, su unico funcionamento

            //This was not a good request
        }

    }
}