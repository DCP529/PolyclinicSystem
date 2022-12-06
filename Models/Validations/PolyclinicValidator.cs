using FluentValidation;

namespace Models.Validations
{
    public class PolyclinicValidator : AbstractValidator<Polyclinic>
    {
        public PolyclinicValidator()
        {
            RuleFor(c => c.Address).NotEmpty().NotNull();
            RuleFor(c => c.ContactNumber).NotEmpty();
            RuleFor(c => c.Name).NotEmpty().NotNull();
            RuleFor(c => c.Image).NotEmpty();
        }
    }
}
