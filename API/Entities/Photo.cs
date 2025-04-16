
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Photos")] //Especificamos el nombre de la tabla que generará la bd
    public class Photo
    {
        public int Id { get; set; }

        public string Url { get; set; }    

        public bool IsMain { get; set; }

        public string PublicId { get; set; }    

        //Añadimos propiedades de relacion
        public int AppUserId { get; set; }

        public AppUser AppUser { get; set; }
        //Creamos una nueva migración tras añadir las propiedades de relacion
    }
}