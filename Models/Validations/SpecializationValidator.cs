using FluentValidation;

namespace Models.Validations
{
    public class SpecializationValidator : AbstractValidator<Specialization>
    {
        public SpecializationValidator()
        {
            RuleFor(c => c.Name).NotEmpty().NotNull();
            RuleFor(c => c.ExperienceSpecialization).NotEmpty();
            RuleFor(c => c.DoctorId).NotEmpty();
        }
    }
}
