using BMG.Propostas.Domain.DTOs;
using FluentValidation;

namespace BMG.Propostas.Application.Validators
{
    public class CriarPropostaDtoValidator : AbstractValidator<CriarPropostaDTO>
    {
        public CriarPropostaDtoValidator()
        {

            RuleFor(b => b.NumeroProposta)
                 .GreaterThanOrEqualTo(0).WithMessage("O Número da Proposta deve ser informado.");

            RuleFor(b => b.Titulo)
                .NotEmpty().WithMessage("O Título da Proposta deve ser informado.");

            RuleFor(b => b.Descricao)
               .NotEmpty().WithMessage("A Descrição da Proposta deve ser informada.");

            RuleFor(b => b.CriadoPorUsuarioId)
                .NotEqual(Guid.Empty).WithMessage("O Usuário que está criando a proposta deve ser informado.");
        }
    }
}
