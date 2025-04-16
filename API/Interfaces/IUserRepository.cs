using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        //Metodos que queremos implementar
        void Update(AppUser user);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserByIdAsync(int id);
        Task<AppUser> GetUserByUsernameAsync(string username);

        //Con las anteriores el select que esta actuando sobre la base de datos sigue recogiendo valores del appuser
        //que no queremos, por eso creamos nuevas funciones de get pero que en vez de devolver appusers
        //devuelvan memberdto con las propiedades que de verdad queremos que haga la consulta de select
        Task<IEnumerable<MemberDto>> GetMembersAsync();
        Task<MemberDto> GetMemberAsync(string username);
    }
}