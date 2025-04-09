using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExamplesController : ControllerBase //ControllerBase es una clase base para controladores de API que no requieren vistas
    {
        private readonly DataContext _context; //acceso a la base de datos --> inicializada en el constructor

        public ExamplesController(DataContext context)
        {
            _context = context; //inicializamos el contexto de datos --> acceso a la base de datos
        }

        //Creamos un endpoint para obtener todos los ejemplos
        [HttpGet] 
        public ActionResult<IEnumerable<Example>> GetExample() //Obtenemos todos los ejemplos de la base de datos
        {
            var examples = _context.Examples.ToList(); //Obtenemos todos los ejemplos de la base de datos
            return examples; //Retornamos la lista de ejemplos
        }
    }
}