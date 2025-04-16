
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [StringLength(8, MinimumLength =4)] //Validacion del campo siguiente (password)
        //[Phone] //tipo de campo diferente
        public string Password { get; set; }

    }
}