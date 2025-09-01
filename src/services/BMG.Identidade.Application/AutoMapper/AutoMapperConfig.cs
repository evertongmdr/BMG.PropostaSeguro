using AutoMapper;
using BMG.Identidade.Domain.DTOs;
using BMG.Identidade.Domain.Entities;

namespace BMG.Identidade.Application.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<RegistrarUsuarioDTO, Usuario>();
        }
    }
}
