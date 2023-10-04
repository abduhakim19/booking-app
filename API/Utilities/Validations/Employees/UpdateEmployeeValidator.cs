using API.DTOs.Employees;
using API.Models;
using FluentValidation;

namespace API.Utilities.Validations.Employees
{
    public class UpdateEmployeeValidator : AbstractValidator<EmployeeDto>
    {
        public UpdateEmployeeValidator() {
            // validasi firstname
            RuleFor(e => e.Guid)
                .NotEmpty();
            //validasi Nik
            RuleFor(e => e.Nik)
                .NotEmpty().MaximumLength(6);
            // validasi firstname
            RuleFor(e => e.FirstName)
                .NotEmpty().MaximumLength(100);
            //validasi gender enum
            RuleFor(e => e.Gender)
                .IsInEnum();
            // validasi birthdate
            RuleFor(e => e.BirthDate)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Now.AddYears(-18))
                    .WithMessage("Employee have to greater than 18 Years old");
            // validasi tanggal rekrut
            RuleFor(e => e.HiringDate)
                .NotEmpty();
            // validasi Email
            RuleFor(e => e.Email)
               .NotEmpty()
               .EmailAddress()
               .MaximumLength(100);
            // validasi nomor telepon
            RuleFor(e => e.PhoneNumber)
               .NotEmpty()
               .MinimumLength(10)
               .MaximumLength(20);
        }
    }
}
