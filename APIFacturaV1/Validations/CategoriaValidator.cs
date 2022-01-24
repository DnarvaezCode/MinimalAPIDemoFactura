using APIFacturaV1.DTOs;
using APIFacturaV1.Models;
using FluentValidation;

namespace APIFacturaV1.Validations
{
    public class CategoriaValidator : AbstractValidator<CategoriaDTO>
    {
        public CategoriaValidator()
        {
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("Campo requerido.").MinimumLength(1);
        }
    }
}
