using AutoMapper;
using BMG.Core.DTOs;
using BMG.Core.Notifications;
using BMG.Propostas.Application.Interfaces;
using BMG.Propostas.Domain.DTOs;
using BMG.Propostas.Domain.Entities;
using BMG.Propostas.Domain.Interfaces.Repositories;

namespace BMG.Propostas.Application.Services
{
    public class PropostaService : NotifiableService, IPropostaService
    {
        private readonly IPropostaRepository _propostaRepository;
        private readonly IMapper _mapper;

        public PropostaService(NotificationContext notificationContext, IPropostaRepository propostaRepository) : base(notificationContext)
        {
            _propostaRepository = propostaRepository;
        }
        public async Task<Proposta> ObterProposta(Guid id)
        {
            return await _propostaRepository.ObterPorId(id);
        }

        public async Task<PagedResult<Proposta>> ObterPropostas(PropostaQueryParametersDTO propostaQueryParameters)
        {
            return await _propostaRepository.ObterPropostas(propostaQueryParameters);
        }

        public async Task<Guid> CriarProposta(CriarPropostaRequestDTO criarPropostaDTO)
        {
            var proposta = _mapper.Map<Proposta>(criarPropostaDTO);

            proposta.Status = PropostaStatus.EmAnalise;

            _propostaRepository.Adicionar(proposta);

            await PersistirDados(_propostaRepository.UnitOfWork);

            return proposta.Id;
        }

        public async Task AtualizarStatusProposta(Guid propostaId, AtualizarStatusPropostaDTO atualizarStatusPropostaDTO)
        {

            var proposta = await _propostaRepository.ObterPorId(propostaId);

            if (proposta == null)
            {
                _notificationContext.AddNotification("Nenhuma proposta foi encontrada.");
                return;
            }

            if (proposta.Status != PropostaStatus.EmAnalise)
            {
                _notificationContext.AddNotification("Só é possível alterar o status de propostas que estejam 'Em análise'.");
                return;
            }

            proposta.Status = atualizarStatusPropostaDTO.Status;

            _propostaRepository.Atualizar(proposta);

            await PersistirDados(_propostaRepository.UnitOfWork);
        }
    }
}
