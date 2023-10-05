using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Accounts
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            // validasi Guid
            RuleFor(a => a.Email)
                .EmailAddress()
                .NotEmpty();
            // validasi password
            RuleFor(a => a.Password)
                .NotEmpty();
        }
    }
}
