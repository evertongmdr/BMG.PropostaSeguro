using BMG.Contratacao.Application.Validators;
using BMG.Contratacao.Domain.DTOs;

namespace BMG.Contratacao.Tests.Application.Validations
{
    public class CriarContratacaoDtoValidatorTests
    {
        private readonly CriarContratacaoDtoValidator _validator;

        public CriarContratacaoDtoValidatorTests()
        {
            _validator = new CriarContratacaoDtoValidator();
        }

        [Theory(DisplayName = "Valida campos obrigatórios de CriarContratacaoDTO")]
        [InlineData("7ce14b3a-358a-4d76-8637-5387c33fd521", "00000000-0000-0000-0000-000000000000", "O responsável por contratar o seguro deve ser informado.")]
        [InlineData("00000000-0000-0000-0000-000000000000", "7ce14b3a-358a-4d76-8637-5387c33fd521", "A proposta de seguro deve ser informado.")]
        public void CriarContratacaoDtoValidatorTests_DadosObrigatorioNaoInformado_DeveRetornarErro(string propostaIdStr, string contratanteIdStr, string mensagemEsperada)
        {
            // Arrange

            var criarContratacao = new CriarContratacaoDTO
            {
                PropostaId = new Guid(propostaIdStr),
                ContratenteId = new Guid(contratanteIdStr)

            };

            // Act

            var result = _validator.Validate(criarContratacao);

            // Assert

            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Contains(result.Errors, e => e.ErrorMessage == mensagemEsperada);
        }
    }
}
