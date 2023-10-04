using API.DTOs.Roles;
using FluentValidation;

namespace API.Utilities.Validations.Roles
{
    public class UpdateRoleValidator : AbstractValidator<RoleDto>
    {
        public UpdateRoleValidator() 
        {
            // validasi guid
            RuleFor(r => r.Guid).NotEmpty();
            //validasi name
            RuleFor(r => r.Name).NotEmpty().MaximumLength(100);
        }
    }
}
