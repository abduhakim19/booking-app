using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Accounts
{
    public class ChangePasswordValidator : AbstractValidator<CreateChangePasswordDto>
    {
        public ChangePasswordValidator() 
        {
            RuleFor(c => c.Otp)
                .NotNull();
            RuleFor(c => c.Email)
                .EmailAddress()
                .NotEmpty();
            RuleFor(c => c.NewPasword)
                .Equal(c => c.ConfirmPassword)
                .NotEmpty();
        }
        
    }
}
