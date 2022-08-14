using FluentValidation;

namespace Application.Core.SpecialistProfiles.Commands.CreateSpecialistProfile;

public sealed class CreateSpecialistProfileCommandValidator : AbstractValidator<CreateSpecialistProfileCommand>
{
    public CreateSpecialistProfileCommandValidator()
    {
        RuleFor(x => x.Age)
            .NotEmpty()
            .NotNull();
        RuleFor(x => x.BirthYear)
            .NotNull()
            .NotEmpty();
        RuleFor(x => x.FirstName)
            .NotNull()
            .MinimumLength(2)
            .NotEmpty();
        RuleFor(x => x.LastName)
            .NotEmpty()
            .MinimumLength(2)
            .NotEmpty();
    }
}
