using API.DTOs.AccountRoles;
using FluentValidation;

namespace API.Utilities.Validations.AccountRoles
{
    public class CreateAccountRoleValidator : AbstractValidator<CreateAccountRoleDto>
    {
        public CreateAccountRoleValidator() 
        {   // validasi account guid
            RuleFor(a => a.AccountGuid)
                .NotEmpty();
            //validasi roleguid
            RuleFor(a => a.RoleGuid)
                .NotEmpty();
        }
    }
}
