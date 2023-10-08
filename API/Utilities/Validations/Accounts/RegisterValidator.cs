using API.DTOs.Accounts;
using API.Utilites.Enums;
using FluentValidation;

namespace API.Utilities.Validations.Accounts
{
    public class RegisterValidator : AbstractValidator<CreateRegisterDto>
    {
        public RegisterValidator()
        {

            // validasi Guid
            RuleFor(a => a.Email)
                .NotEmpty()
                .MaximumLength(100);
            // validasi password
            RuleFor(c => c.Password)
                .Equal(c => c.ConfirmPassword)
                .NotEmpty();

            RuleFor(c => c.FirstName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(c => c.LastName)
                .MaximumLength(100);

            RuleFor(c => c.Gender)
                .NotNull();

            RuleFor(c => c.HiringDate)
                .NotEmpty();

            RuleFor(e => e.BirthDate)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Now.AddYears(-18))
                    .WithMessage("Employee have to greater than 18 Years old");

            RuleFor(c => c.PhoneNumber)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(c  => c.Major)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(c => c.Degree)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(c => c.Gpa)
                .NotNull();

            RuleFor(c => c.UniversityCode)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(c => c.UniversityName)
                .NotNull()
                .MaximumLength(100);

        }
    }
}
