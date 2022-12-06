using FluentValidation;

namespace Models.Validations
{
    public class CityValidator : AbstractValidator<City>
    {
        public CityValidator()
        {
            RuleFor(c => c.Name).NotEmpty().NotNull();
        }
    }
}
