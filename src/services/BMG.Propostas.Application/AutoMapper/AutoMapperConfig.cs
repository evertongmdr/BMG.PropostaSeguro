using AutoMapper;
using BMG.Propostas.Domain.DTOs;
using BMG.Propostas.Domain.Entities;

namespace BMG.Propostas.Application.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<CriarPropostaRequestDTO, Proposta>();

        }
    }
}
