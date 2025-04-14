
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
        
    }
}