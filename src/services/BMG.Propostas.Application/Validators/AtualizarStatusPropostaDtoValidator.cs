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
                .Must(status => status == PropostaStatus.Aprovada || status == PropostaStatus.Rejeitada)
                    .WithMessage("O status da proposta deve ser 'Aprovada' ou 'Rejeitada'.");
        }
    }
}
