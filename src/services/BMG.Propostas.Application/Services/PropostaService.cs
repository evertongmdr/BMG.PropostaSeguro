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

        public PropostaService(NotificationContext notificationContext, IMapper mapper, IPropostaRepository propostaRepository) : base(notificationContext)
        {
            _mapper = mapper;
            _propostaRepository = propostaRepository;
        }
        public async Task<Proposta> ObterPropostaAsync(Guid id)
        {
            return await _propostaRepository.ObterPorIdAsync(id);
        }

        public async Task<PagedResult<Proposta>> ObterPropostasAsync(PropostaQueryParametersDTO propostaQueryParameters)
        {
            return await _propostaRepository.ObterPropostasAsync(propostaQueryParameters);
        }

        public async Task<Guid> CriarPropostaAsync(CriarPropostaRequestDTO criarPropostaDTO)
        {
            var propostaExistente = await _propostaRepository.ObterPorNumeroAsync(criarPropostaDTO.NumeroProposta);

            if (propostaExistente != null)
            {
                _notificationContext.AddNotification($"Já existe uma proposta com o número {criarPropostaDTO.NumeroProposta}.");
                return Guid.Empty;
            }

            var proposta = _mapper.Map<Proposta>(criarPropostaDTO);

            proposta.Status = PropostaStatus.EmAnalise;
            proposta.DataCriacao = DateTime.Now;

            _propostaRepository.Adicionar(proposta);

            await PersistirDados(_propostaRepository.UnitOfWork);

            return proposta.Id;
        }

        public async Task AtualizarStatusPropostaAsync(Guid propostaId, AtualizarStatusPropostaDTO atualizarStatusPropostaDTO)
        {
            var status = atualizarStatusPropostaDTO.Status;

            if (status != PropostaStatus.Aprovada && status != PropostaStatus.Rejeitada)
            {
                _notificationContext.AddNotification("Status inválido. Somente os status 'Aprovada' ou 'Rejeitada' são permitidos.");
                return;
            }

            var proposta = await _propostaRepository.ObterPorIdAsync(propostaId);

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

            proposta.Status = status;

            _propostaRepository.Atualizar(proposta);

            await PersistirDados(_propostaRepository.UnitOfWork);
        }
    }
}
