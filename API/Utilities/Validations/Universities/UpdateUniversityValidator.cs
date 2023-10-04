using API.DTOs.Universities;
using FluentValidation;

namespace API.Utilities.Validations.Universities
{
    public class UpdateUniversityValidator : AbstractValidator<UniversityDto>
    {
        public UpdateUniversityValidator()
        {
            //validasi guid
            RuleFor(u => u.Guid).NotEmpty();
            // validasi code
            RuleFor(u => u.Code).NotEmpty().MaximumLength(50);
            // validasi name
            RuleFor(U => U.Name).NotEmpty().MaximumLength(100);
        }
    }
}
