using BMG.Contratacao.Application.Services;
using BMG.Core.Messages.Integrations;
using BMG.Core.Notifications;
using BMG.MessageBus;
using Moq;
using Moq.AutoMock;

namespace BMG.Contratacao.Tests.Application
{
    public class ContratacaoServiceTests
    {
        private readonly AutoMocker _mocker;
        private readonly NotificationContext _notificationContext;

        public ContratacaoServiceTests()
        {
            _mocker = new AutoMocker();

            _notificationContext = new NotificationContext();

            _mocker.Use(_notificationContext);
        }
        [Fact(DisplayName = "Deve retornar erro quando a proposta não for informado")]
        public async Task Contratacao_PropostaNaoInformada_DeveRetornarErro()
        {
            //Arrange 
            var criarContratacao = new Domain.DTOs.CriarContratacaoDTO
            {
                PropostaId = Guid.Empty,
                ContratenteId = Guid.NewGuid()
            };

            var contratacaoService = _mocker.CreateInstance<ContratacaoService>();

            //Act

            await contratacaoService.ContratarPropostaAsync(criarContratacao);

            //Assert
            Assert.Single(_notificationContext.Notifications);
            Assert.Contains("A proposta de seguro deve ser informado.", _notificationContext.Notifications.First().Message);
        }

        [Fact(DisplayName = "Deve retornar erro quando o contratante não for informado")]

        public async Task Contratacao_ContratanteNaoInformado_DeveRetornarErro()
        {
            //Arrange 
            var criarContratacao = new Domain.DTOs.CriarContratacaoDTO
            {
                PropostaId = Guid.NewGuid(),
                ContratenteId = Guid.Empty
            };

            var contratacaoService = _mocker.CreateInstance<ContratacaoService>();

            //Act

            await contratacaoService.ContratarPropostaAsync(criarContratacao);

            //Assert

            Assert.Single(_notificationContext.Notifications);
            Assert.Contains("O contratante do seguro deve ser informado.", _notificationContext.Notifications.First().Message);
        }

        [Fact(DisplayName = "Deve publicar a mensagem da contratação na fila")]
        public async Task Contratacao_PublicaMensagemNaFila_DeveRetornarSucesso()
        {
            //Arrange 
            var criarContratacao = new Domain.DTOs.CriarContratacaoDTO
            {
                PropostaId = Guid.NewGuid(),
                ContratenteId = Guid.NewGuid()
            };

            var contratacaoService = _mocker.CreateInstance<ContratacaoService>();
            //Act

            await contratacaoService.ContratarPropostaAsync(criarContratacao);

            //Assert

            Assert.Empty(_notificationContext.Notifications);

            _mocker.GetMock<IMessageBus>().Verify(m => m.EnqueueAsync("ContratacaoSeguro", It.IsAny<RealizarContratacaoIntegrationEvent>()), Times.Once);
        }
    }
}
