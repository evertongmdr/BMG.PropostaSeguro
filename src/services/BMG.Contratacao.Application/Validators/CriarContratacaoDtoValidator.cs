using BMG.Contratacao.Domain.DTOs;
using FluentValidation;

namespace BMG.Contratacao.Application.Validators
{
    public class CriarContratacaoDtoValidator : AbstractValidator<CriarContratacaoDTO>
    {
        public CriarContratacaoDtoValidator()
        {
            RuleFor(b => b.PropostaId)
               .NotEqual(Guid.Empty).WithMessage("A proposta de seguro deve ser informado.");

            RuleFor(b => b.ContratenteId)
               .NotEqual(Guid.Empty).WithMessage("O responsável por contratar o seguro deve ser informado.");
        }
    }
}
