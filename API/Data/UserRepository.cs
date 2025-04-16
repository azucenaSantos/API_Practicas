
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            //return await _context.Users.ToListAsync(); //esto nos devuelve una lista de usuarios pero no nos devuelve la lista de 
            // fotos, está vacia, para eso hay que especificar el entity framework que tambien queremos eso
            return await _context.Users
            .Include(p => p.Photos)
            .ToListAsync();
        }


        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
            .Include(p => p.Photos)
            .SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
            //informamos al entity framework que una entidad ha sido actualizada pero no está guardada!!
        }

        //Funciones que hacen lo mismo que las anteriores pero al devolver un memberdto devuelven la info necesaria y no un gurruño de cosas
        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            return await _context.Users
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            return await _context.Users
            .Where(x => x.UserName == username)
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
        }
    }
}