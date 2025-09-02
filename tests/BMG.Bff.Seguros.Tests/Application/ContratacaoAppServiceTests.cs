using BMG.Bff.Seguros.Application;
using BMG.Bff.Seguros.Models.Contratacao;
using BMG.Bff.Seguros.Models.Identidade;
using BMG.Bff.Seguros.Models.Proposta;
using BMG.Bff.Seguros.Services;
using BMG.Core.Communication;
using BMG.Core.Notifications;
using Moq;
using Moq.AutoMock;

namespace BMG.Bff.Seguros.Tests.Application
{
    public class ContratacaoAppServiceTests
    {
        private readonly AutoMocker _mocker;

        private readonly NotificationContext _notificationContext;

        public ContratacaoAppServiceTests()
        {
            _mocker = new AutoMocker();

            _notificationContext = new NotificationContext();

            _mocker.Use(_notificationContext);

        }

        [Fact(DisplayName = "Não deve permitir a realização da contratação do seguro quando o contratante não for encontrado")]

        public async Task Contratacao_ContratanteNaoEncontrado_DeveRetornarErro()
        {
            //Arrange 

            var registrarContratacao = new RegistrarContratacaoDTO
            {
                PropostaId = Guid.NewGuid(),
                ContratenteId = Guid.NewGuid()
            };

            var respostaApiIdentidade = new ApiResponse<UsuarioDTO>
            {
                Success = false,
                Data = null,
                ResponseResult = new ResponseResult
                {
                    Status = 404,
                    Title = "Not Found",
                    Errors = new ResponseErrorMessages
                    {
                        Mensagens = new List<string> { "Usuário não encontrado." }
                    }
                },
            };

            _mocker.GetMock<IIdentidadeService>().Setup(x => x.ObterUsuarioPorIdAsync(registrarContratacao.ContratenteId))
                .ReturnsAsync(respostaApiIdentidade);

            var contratacaoAppService = _mocker.CreateInstance<ContratacaoAppService>();

            //Act

            await contratacaoAppService.ContratarPropostaAsync(registrarContratacao);

            //Assert

            Assert.Single(_notificationContext.Notifications);

            Assert.Contains("O Contratente da proposta não foi encontrado.",
                _notificationContext.Notifications.First().Message);
        }

        [Fact(DisplayName = "Não deve permitir a realização da contratação do seguro quando a proposta não for encontrado")]

        public async Task Contratacao_PropostaNaoEncontrado_DeveRetornarErro()
        {
            //Arrange 

            var registrarContratacao = new RegistrarContratacaoDTO
            {
                PropostaId = Guid.NewGuid(),
                ContratenteId = Guid.NewGuid()
            };

            var usuario = new UsuarioDTO { };

            var respostaApiIdentidade = new ApiResponse<UsuarioDTO>
            {
                Success = true,
                Data = usuario,
                ResponseResult = null
            };

            var respostaApiProposta = new ApiResponse<PropostaDTO>
            {
                Success = false,
                Data = null,
                ResponseResult = new ResponseResult
                {
                    Status = 404,
                    Title = "Not Found",
                    Errors = new ResponseErrorMessages
                    {
                        Mensagens = new List<string> { "Proposta não encontrado." }
                    }
                },
            };

            _mocker.GetMock<IIdentidadeService>().Setup(x => x.ObterUsuarioPorIdAsync(registrarContratacao.ContratenteId))
            .ReturnsAsync(respostaApiIdentidade);

            _mocker.GetMock<IPropostaService>().Setup(x => x.ObterPropostaPorIdAsync(registrarContratacao.PropostaId))
                .ReturnsAsync(respostaApiProposta);

            var contratacaoAppService = _mocker.CreateInstance<ContratacaoAppService>();

            //Act

            await contratacaoAppService.ContratarPropostaAsync(registrarContratacao);

            //Assert

            Assert.Single(_notificationContext.Notifications);

            Assert.Contains("Proposta não encontrado.",
                _notificationContext.Notifications.First().Message);
        }

        [Fact(DisplayName = "Não deve permitir a realização da contratação do seguro se a proposta não estiver com status 'Aprovada'")]

        public async Task Contratacao_PropostaStatusInconsistente_DeveRetornarErro()
        {
            //Arrange 

            var registrarContratacao = new RegistrarContratacaoDTO
            {
                PropostaId = Guid.NewGuid(),
                ContratenteId = Guid.NewGuid()
            };

            var usuario = new UsuarioDTO { };

            var respostaApiIdentidade = new ApiResponse<UsuarioDTO>
            {
                Success = true,
                Data = usuario,
                ResponseResult = null
            };

            var proposta = new PropostaDTO
            {
                Status = PropostaStatus.EmAnalise
            };

            var respostaApiProposta = new ApiResponse<PropostaDTO>
            {
                Success = true,
                Data = proposta,
                ResponseResult = null
            };

            _mocker.GetMock<IIdentidadeService>().Setup(x => x.ObterUsuarioPorIdAsync(registrarContratacao.ContratenteId))
            .ReturnsAsync(respostaApiIdentidade);

            _mocker.GetMock<IPropostaService>().Setup(x => x.ObterPropostaPorIdAsync(registrarContratacao.PropostaId))
                .ReturnsAsync(respostaApiProposta);

            var contratacaoAppService = _mocker.CreateInstance<ContratacaoAppService>();

            //Act

            await contratacaoAppService.ContratarPropostaAsync(registrarContratacao);

            //Assert

            Assert.Single(_notificationContext.Notifications);

            Assert.Contains("A proposta só pode ser contratada quando estiver com status 'Aprovada'.",
                _notificationContext.Notifications.First().Message);
        }

        [Fact(DisplayName = "Deve permitir a realização da contratação do seguro")]

        public async Task Contratacao_RealizaContratacaoSeguro_DeveRetornarSucesso()
        {
            //Arrange 

            var registrarContratacao = new RegistrarContratacaoDTO
            {
                PropostaId = Guid.NewGuid(),
                ContratenteId = Guid.NewGuid()
            };

            var usuario = new UsuarioDTO { };

            var respostaApiIdentidade = new ApiResponse<UsuarioDTO>
            {
                Success = true,
                Data = usuario,
                ResponseResult = null
            };

            var proposta = new PropostaDTO
            {
                Status = PropostaStatus.Aprovada
            };

            var respostaApiProposta = new ApiResponse<PropostaDTO>
            {
                Success = true,
                Data = proposta,
                ResponseResult = null
            };

            var respostaApiContratacao = new ApiResponse
            {
                Success = true,
                ResponseResult = null
            };

            _mocker.GetMock<IIdentidadeService>().Setup(x => x.ObterUsuarioPorIdAsync(registrarContratacao.ContratenteId))
            .ReturnsAsync(respostaApiIdentidade);

            _mocker.GetMock<IPropostaService>().Setup(x => x.ObterPropostaPorIdAsync(registrarContratacao.PropostaId))
                .ReturnsAsync(respostaApiProposta);

            _mocker.GetMock<IContratacaoService>().Setup(x => x.ContratarPropostaAsync(registrarContratacao))
              .ReturnsAsync(respostaApiContratacao);

            var contratacaoAppService = _mocker.CreateInstance<ContratacaoAppService>();

            //Act

            await contratacaoAppService.ContratarPropostaAsync(registrarContratacao);

            //Assert

            Assert.Empty(_notificationContext.Notifications);

            _mocker.GetMock<IContratacaoService>().Verify(x => x.ContratarPropostaAsync(It.IsAny<RegistrarContratacaoDTO>()), Times.Once);
        }
    }
}
