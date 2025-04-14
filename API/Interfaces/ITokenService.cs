
using API.Entities;

namespace API.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user); //crea un token a partir de un usuario    
        //metodo que devuleve un string, le pasamos un AppUser como parámetro
        //Al tratarse de una interfaz, cualquier clase que implemente esta interfaz debe implementar este método    
    }
}