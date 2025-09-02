using BMG.Propostas.Application.Validators;
using BMG.Propostas.Domain.DTOs;
using BMG.Propostas.Domain.Entities;

namespace BMG.Propostas.Tests.Application.Validations
{
    public class AtualizarStatusPropostaDtoValidatorTests
    {
        private readonly AtualizarStatusPropostaDtoValidator _validator;

        public AtualizarStatusPropostaDtoValidatorTests()
        {
            _validator = new AtualizarStatusPropostaDtoValidator();
        }

        [Fact(DisplayName = "AtualizarStatusPropostaDtoValidator: Deve retornar erro quando o status informado for inválido")]
        public void AtualizarStatusPropostaDtoValidator_EnumNaoExiste_DeveRetornarErro()
        {
            // Arrange

            var atualizarStatus = new AtualizarStatusPropostaDTO
            {
                Status = (PropostaStatus)100
            };

            // Act

            var result = _validator.Validate(atualizarStatus);

            // Assert

            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Equal("Status da proposta inválido.", result.Errors.First().ErrorMessage);
        }
    }
}
