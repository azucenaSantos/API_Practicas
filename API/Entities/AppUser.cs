
using API.Extensions;

namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; } //los "Id" se toman como clave primaria por convención, si no se especifica en la linea superior [Key]

        //[Required] //El siguiente atributo indica que es requerido, en este caso bo lo usarmos
        public string UserName { get; set; } //en API.csproj ponemos a disabled el Nullable Reference Types para que no nos pida el ? al final de la propiedad
        //string? --> opcional       

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public DateOnly DateOfBirth { get; set; }
        
        public string KnownAs { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow; //Fecha de creación del usuario

        public DateTime LastActive { get; set; } = DateTime.UtcNow;

        public string Gender { get; set; }

        public string Introduction { get; set; }
        public string LookingFor { get; set; }

        public string Interests { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public List<Photo> Photos { get; set; }= new(); //Lista vacia de fotos
           //Como desde AppUser estamos llamando a otra entidad llamada Photos, a la hora de
           //crear la migración y crear la base de datos, va a crear una relacion entre estas dos
           //entidades, permitiendo que una foto tenga como clave foranea un id del usuario y que esta
           //pueda ser NULL -> este aspecto no nos interesa porque podrian quedar fotos sin id asociado.
           //Manejaremos las relationships manualmente nosotros :D
        //Metodo edad
        /*public int GetAge(){
            return DateOfBirth.CalculateAge();
        }*/
    }

}