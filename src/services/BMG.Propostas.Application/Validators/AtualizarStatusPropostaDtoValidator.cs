using BMG.Propostas.Domain.DTOs;
using BMG.Propostas.Domain.Entities;
using FluentValidation;

namespace BMG.Propostas.Application.Validators
{
    public class AtualizarStatusPropostaDtoValidator : AbstractValidator<AtualizarStatusPropostaDTO>
    {
        public AtualizarStatusPropostaDtoValidator()
        {
            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("Status da proposta inválido.");
        }
    }
}
