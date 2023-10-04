using API.DTOs.Educations;
using FluentValidation;

namespace API.Utilities.Validations.Educations
{
    public class CreateEducationValidator : AbstractValidator<CreateEducationDto>
    {
        public CreateEducationValidator() 
        {
            // validasi guid
            RuleFor(e => e.Guid).NotEmpty();
            // validasi major
            RuleFor(e => e.Major).NotEmpty().MaximumLength(100);
            // validasi degree
            RuleFor(e => e.Degree).NotEmpty().MaximumLength(100);
            // validasi gpa
            RuleFor(e => e.Guid).NotEmpty();
            //validasi university guid
            RuleFor(e => e.UniversityGuid).NotEmpty();
        }
    }
}
