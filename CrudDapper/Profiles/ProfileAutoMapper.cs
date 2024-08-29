using AutoMapper;
using CrudDapper.Models;
using CrudDapper.Dto;

namespace CrudDapper.Profiles;

public class ProfileAutoMapper : Profile
{
    public ProfileAutoMapper()
    {
        CreateMap<Usuario, UsuarioListarDto>();
    }

}
