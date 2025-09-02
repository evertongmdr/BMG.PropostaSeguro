using AutoMapper;
using BMG.Core.Notifications;
using BMG.Propostas.Application.Services;
using BMG.Propostas.Domain.DTOs;
using BMG.Propostas.Domain.Entities;
using BMG.Propostas.Domain.Interfaces.Repositories;
using Moq;
using Moq.AutoMock;

namespace BMG.Propostas.Tests.Application
{
    public class PropostaServiceTests
    {
        private readonly AutoMocker _mocker;
        private readonly NotificationContext _notificationContext;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IPropostaRepository> _mockPropostaRepository;

        public PropostaServiceTests()
        {
            _mocker = new AutoMocker();

            _notificationContext = new NotificationContext();

            _mocker.Use(_notificationContext);

            _mockMapper = _mocker.GetMock<IMapper>();

            _mockPropostaRepository = _mocker.GetMock<IPropostaRepository>();
        }

        [Fact(DisplayName = "Deve retornar erro quando número da proposta já existir")]
        public async Task Proposta_NumeroPropostaExistente_DeveRetornarError()
        {
            //Arrange 

            var criarProposta = ObterProposta();

            var propsota = new Proposta { };

            _mockPropostaRepository.Setup(p => p.ObterPorNumeroAsync(criarProposta.NumeroProposta)).ReturnsAsync(propsota);

            var propostaService = _mocker.CreateInstance<PropostaService>();

            //Act

            var propostaId = await propostaService.CriarPropostaAsync(criarProposta);

            //Assert

            Assert.Equal(Guid.Empty, propostaId);
            Assert.Single(_notificationContext.Notifications);
            Assert.Contains($"Já existe uma proposta com o número {criarProposta.NumeroProposta}.", _notificationContext.Notifications.First().Message);

        }

        [Fact(DisplayName = "Deve retornar sucesso ao salvar proposta no banco")]
        public async Task Proposta_PropostaSalva_DeveRetornarSucesso()
        {
            //Arrange 

            var criarProposta = ObterProposta();

            var expectedPropostaId = Guid.NewGuid();

            var proposta = new Proposta
            {
                Id = expectedPropostaId,
                NumeroProposta = 5
            };

            _mockPropostaRepository.Setup(p => p.ObterPorNumeroAsync(criarProposta.NumeroProposta)).ReturnsAsync((Proposta)null);

            _mockPropostaRepository.Setup(wp => wp.UnitOfWork.Commit()).ReturnsAsync(true);

            _mockMapper.Setup(x => x.Map<Proposta>(It.IsAny<CriarPropostaDTO>())).Returns(proposta);


            var propostaService = _mocker.CreateInstance<PropostaService>();

            //Act

            var resultPropostaId = await propostaService.CriarPropostaAsync(criarProposta);

            //Assert

            Assert.Equal(expectedPropostaId, resultPropostaId);
            Assert.Empty(_notificationContext.Notifications);

            _mockPropostaRepository.Verify(wp => wp.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Deve retornar erro ao atualizar status da proposta com entrada inválida")]
        public async Task Proposta_StatusEntradaInvalido_DeveRetornarErro()
        {
            //Arrange 

            var atualizarStatusProposta = new AtualizarStatusPropostaDTO { Status = PropostaStatus.EmAnalise };

            var propostaId = Guid.NewGuid();

            var propostaService = _mocker.CreateInstance<PropostaService>();

            //Act

            await propostaService.AtualizarStatusPropostaAsync(propostaId, atualizarStatusProposta);

            //Assert

            Assert.Single(_notificationContext.Notifications);
            Assert.Contains("Status inválido. Somente os status 'Aprovada' ou 'Rejeitada' são permitidos.", _notificationContext.Notifications.First().Message);

        }

        [Fact(DisplayName = "Deve retornar erro ao tentar alterar status de proposta inexistente")]
        public async Task Proposta_NaoAtualizaStatusPropostaNaoExiste_DeveRetornarErro()
        {
            //Arrange 

            var atualizarStatusProposta = new AtualizarStatusPropostaDTO { Status = PropostaStatus.Aprovada };

            var propostaId = Guid.NewGuid();

            _mockPropostaRepository.Setup(p => p.ObterPorIdAsync(propostaId)).ReturnsAsync((Proposta)null);

            var propostaService = _mocker.CreateInstance<PropostaService>();

            //Act

            await propostaService.AtualizarStatusPropostaAsync(propostaId, atualizarStatusProposta);

            //Assert

            Assert.Single(_notificationContext.Notifications);
            Assert.Contains("Nenhuma proposta foi encontrada.", _notificationContext.Notifications.First().Message);

        }

        [Fact(DisplayName = "Não deve permitir alterar status de proposta que não está em análise")]
        public async Task Proposta_AtualizacaoStatusInvalida_DeveRetornarErro()
        {
            //Arrange 

            var atualizarStatusProposta = new AtualizarStatusPropostaDTO { Status = PropostaStatus.Aprovada };

            var propostaId = Guid.NewGuid();

            var proposta = new Proposta
            {
                Id = propostaId,
                NumeroProposta = 5,
                Status = PropostaStatus.Aprovada
            };

            _mockPropostaRepository.Setup(p => p.ObterPorIdAsync(propostaId)).ReturnsAsync(proposta);

            var propostaService = _mocker.CreateInstance<PropostaService>();

            //Act

            await propostaService.AtualizarStatusPropostaAsync(propostaId, atualizarStatusProposta);

            //Assert

            Assert.Single(_notificationContext.Notifications);
            Assert.Contains("Só é possível alterar o status de propostas que estejam 'Em análise'.", _notificationContext.Notifications.First().Message);

        }


        [Fact(DisplayName = "Deve permitir alterar status de proposta")]
        public async Task Proposta_AtualizacaoStatus_DeveRetornarSucesso()
        {
            //Arrange 

            var atualizarStatusProposta = new AtualizarStatusPropostaDTO { Status = PropostaStatus.Aprovada };

            var propostaId = Guid.NewGuid();

            var proposta = new Proposta
            {
                Id = propostaId,
                NumeroProposta = 5,
                Status = PropostaStatus.EmAnalise
            };

            _mockPropostaRepository.Setup(p => p.ObterPorIdAsync(propostaId)).ReturnsAsync(proposta);

            _mockPropostaRepository.Setup(wp => wp.UnitOfWork.Commit()).ReturnsAsync(true);

            var propostaService = _mocker.CreateInstance<PropostaService>();

            //Act

            await propostaService.AtualizarStatusPropostaAsync(propostaId, atualizarStatusProposta);

            //Assert

            Assert.Empty(_notificationContext.Notifications);

            _mockPropostaRepository.Verify(p => p.Atualizar(It.IsAny<Proposta>()), Times.Once);
            _mockPropostaRepository.Verify(wp => wp.UnitOfWork.Commit(), Times.Once);
        }


        private CriarPropostaDTO ObterProposta() => new CriarPropostaDTO
        {
            NumeroProposta = 1,
            Titulo = "Seguro Basic",
            Descricao = "Seguro Básico",
            CriadoPorUsuarioId = Guid.NewGuid(),
        };

    }
}
