using FluentValidation;
using API.DTOs.AccountRoles;
namespace API.Utilities.Validations.AccountRoles
{
    public class UpdateAccountRoleValidator : AbstractValidator<AccountRoleDto>
    {
        public UpdateAccountRoleValidator() 
        {
            // validasi guid
            RuleFor(a => a.Guid)
                .NotEmpty();
            // validasi account guid
            RuleFor(a => a.AccountGuid)
                .NotEmpty();
            //validasi roleguid
            RuleFor(a => a.RoleGuid)
                .NotEmpty();
        }
    }
}
