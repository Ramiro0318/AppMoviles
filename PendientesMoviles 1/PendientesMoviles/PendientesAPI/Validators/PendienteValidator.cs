using FluentValidation;
using PendientesAPI.DTOs;

namespace PendientesAPI.Validators;

public class PendienteValidator : AbstractValidator<PendienteRequestDTO>
{
    public PendienteValidator()
    {
        RuleFor(x => x.Titulo)
            .NotEmpty().WithMessage("El título es requerido.")
            .MaximumLength(100).WithMessage("El título no puede exceder 100 caracteres.");

        RuleFor(x => x.Descripcion)
            .MaximumLength(500).WithMessage("La descripción no puede exceder 500 caracteres.")
            .When(x => x.Descripcion != null);

        RuleFor(x => x.Estado)
            .Must(e => e == "Pendiente" || e == "Completado")
            .WithMessage("El estado debe ser 'Pendiente' o 'Completado'.");
    }
}