using BMG.Propostas.Application.Validators;
using BMG.Propostas.Domain.DTOs;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

namespace BMG.Propostas.Tests.Application.Validations
{
    public class CriarPropostaDtoValidatorTests
    {
        private readonly CriarPropostaDtoValidator _validator;

        public CriarPropostaDtoValidatorTests()
        {
            _validator = new CriarPropostaDtoValidator();

        }

        [Theory(DisplayName = "Valida campos obrigatórios de CriarPropostaDTO")]
        [InlineData(-1, "Título", "Descrição", "7ce14b3a-358a-4d76-8637-5387c33fd521", "O Número da Proposta deve ser informado.")]
        [InlineData(1, null, "Descricao", "7ce14b3a-358a-4d76-8637-5387c33fd521", "O Título da Proposta deve ser informado.")]
        [InlineData(1, "Titulo", null, "7ce14b3a-358a-4d76-8637-5387c33fd521", "A Descrição da Proposta deve ser informada.")]
        [InlineData(1, "Titulo", "Descricao", "00000000-0000-0000-0000-000000000000", "O Usuário que está criando a proposta deve ser informado.")]
        public void CriarPropostaDtoValidator_DadosObrigatorioInvalido_DeveRetornarErro(int numeroProposta, string titulo, string descricao,
            string criadoPorUsuarioIdStr, string mensagemEsperada)
        {
            // Arrange
            var proposta = new CriarPropostaDTO
            {
                NumeroProposta = numeroProposta,
                Titulo = titulo,
                Descricao = descricao,
                CriadoPorUsuarioId = new Guid(criadoPorUsuarioIdStr)
            };

            // Act
            var result = _validator.Validate(proposta);

            // Assert

            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Contains(result.Errors, e => e.ErrorMessage == mensagemEsperada);
        }

    }
}
