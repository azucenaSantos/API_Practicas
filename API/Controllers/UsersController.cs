using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    /*[ApiController]
    [Route("api/[controller]")] //api/users --> asi es como accedemos a este controlador*/

    [Authorize] //Requiere autenticacion para acceder a TODOS los endpoint(httpget, httpgetId) --> solo los usuarios autenticados (pasan un token) pueden acceder a este controlador
    //No necesitamos lo anterior porque UserController hereda de BaseApiController
    public class UsersController : BaseApiController //ControllerBase es una clase base para controladores de API que no requieren vistas
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        //Endpoint para obtener todos los usuarios
        [HttpGet] //GET api/users
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            //return Ok(await _userRepository.GetUsersAsync()); 
            //Nos da un problema de bucle de objetos porque está en bucle entre el appuser y el photo -> mappeador solucion
            var users= await _userRepository.GetMembersAsync(); 

            var usersToReturn= _mapper.Map<IEnumerable<MemberDto>>(users);
            return Ok(usersToReturn);
        }

        //Endpoint para obtener un usuario solo x id
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username) 
        {
            //hacemos lo mismo y mapeamos los resultados:
            return await _userRepository.GetMemberAsync(username);

        }
    }
}