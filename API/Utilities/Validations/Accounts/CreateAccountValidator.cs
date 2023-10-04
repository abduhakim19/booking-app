using FluentValidation;
using API.DTOs.Accounts;

namespace API.Utilities.Validations.Accounts
{
    public class CreateAccountValidator : AbstractValidator<CreateAccountDto>
    {
        public CreateAccountValidator() 
        {
            // validasi Guid
            RuleFor(a => a.Guid)
                .NotEmpty();
            // validasi password
            RuleFor(a => a.Password)
                .NotEmpty();
            // validasi otp
            RuleFor(a => a.Otp)
                .NotEmpty();
            // validasi is_used
            RuleFor(a => a.IsUsed)
                .NotEmpty();
            // validasi expired time
            RuleFor(a => a.ExpiredTime)
                .NotEmpty();
        }
    }
}
