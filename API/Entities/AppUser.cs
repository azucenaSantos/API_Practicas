namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; } //los "Id" se toman como clave primaria por convención, si no se especifica en la linea superior [Key]
        public string UserName { get; set; } //en API.csproj ponemos a disabled el Nullable Reference Types para que no nos pida el ? al final de la propiedad
        //string? --> opcional       
        
    }
}