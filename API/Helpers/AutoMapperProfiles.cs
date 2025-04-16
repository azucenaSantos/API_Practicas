
using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        //Tenemos que decirle al automapa de donde queremos ir a donde queremos ir    

        public AutoMapperProfiles()
        {
            //De donde a donde: de AppUser al MemberDto
            CreateMap<AppUser, MemberDto>()
            .ForMember(dest=> dest.PhotoUrl, opt=>opt.MapFrom(src=>src.Photos.FirstOrDefault(x=> x.IsMain).Url))
            .ForMember(dest=>dest.Age, opt=>opt.MapFrom(src=> src.DateOfBirth.CalculateAge()));
            //Mapeo individual de una propiedad para guardar en el MemberDto una propiedad
            //que no tiene el AppUser que equivale solo a la url de la foto que tiene asignada

            //Esto lo hacemos porque un usuario va a poder tener varias fotos en su perfil y solo una será
            //la que tenga isMain a true, asi que en esa propiedad del memberdto se guardará la imagen main de todas
            //las que tenga x perfil de usuario.
            CreateMap<Photo, PhotoDto>();           
        }    
    }
}
