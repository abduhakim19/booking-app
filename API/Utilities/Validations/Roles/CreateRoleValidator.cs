using API.DTOs.Roles;
using FluentValidation;

namespace API.Utilities.Validations.Roles
{
    public class CreateRoleValidator : AbstractValidator<CreateRoleDto>
    {
        public CreateRoleValidator() 
        {
            //validasi name
            RuleFor(r => r.Name).NotEmpty().MaximumLength(100);
        }
    }
}
