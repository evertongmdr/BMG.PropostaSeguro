using BMG.Identidade.Domain.DTOs;
using FluentValidation;

namespace BMG.Identidade.Application.Validators
{
    public class RegistrarUsuarioDtoValidator : AbstractValidator<RegistrarUsuarioDTO>
    {
        public RegistrarUsuarioDtoValidator()
        {
            RuleFor(b => b.Login)
               .NotEmpty().WithMessage("O Login deve ser informado.");

            RuleFor(b => b.Nome)
               .NotEmpty().WithMessage("O Nome deve ser informado.");

            RuleFor(b => b.TipoUsuario)
                .IsInEnum().WithMessage("O Tipo de Usuário é inválido.");
        }
    }
}
