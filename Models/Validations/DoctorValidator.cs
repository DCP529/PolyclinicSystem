using FluentValidation;

namespace Models.Validations
{
    public class DoctorValidator : AbstractValidator<Doctor>
    {
        public DoctorValidator()
        {
            RuleFor(c => c.FIO).NotEmpty().NotNull();
            RuleFor(c => c.ContactNumber).NotEmpty();
            RuleFor(c => c.AdmissionCost).NotEmpty();
            RuleFor(c => c.Image).NotEmpty();
            RuleFor(c => c.FullDescription).NotEmpty().NotNull();
            RuleFor(c => c.ShortDescription).NotEmpty().NotNull();
        }
    }
}
