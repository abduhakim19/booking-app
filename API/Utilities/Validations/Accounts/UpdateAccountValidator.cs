using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Accounts
{
    public class UpdateAccountValidator : AbstractValidator<AccountDto>
    {
        public UpdateAccountValidator() {
            // validasi Guid
            RuleFor(a => a.Guid)
                .NotEmpty();
            // validasi password
            RuleFor(a => a.Password)
                .NotEmpty();
            // validasi otp
            RuleFor(a => a.Otp)
                .NotEmpty();
            // validasi expired time
            RuleFor(a => a.ExpiredTime)
                .NotEmpty();
        }
    }
}
