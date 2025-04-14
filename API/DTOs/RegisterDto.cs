
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        //[Phone]
        //[StringLength(8, MinimumLength = 4)] --> diferentes validaciones
        public string Password { get; set; }

    }
}