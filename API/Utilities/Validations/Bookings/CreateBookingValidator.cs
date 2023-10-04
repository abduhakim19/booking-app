using API.DTOs.Bookings;
using FluentValidation;

namespace API.Utilities.Validations.Bookings
{
    public class CreateBookingValidator : AbstractValidator<CreateBookingDto>
    {
        public CreateBookingValidator() 
        {
            //validasi startdate
            RuleFor(b => b.StartDate).NotEmpty();
            //validasi enddate
            RuleFor(b => b.EndDate).NotEmpty();
            //validasi remarks
            RuleFor(b => b.Remarks).NotEmpty();
            //validasi RoomGuid
            RuleFor(b => b.RoomGuid).NotEmpty();
            //validasi employeeguid
            RuleFor(b => b.EmployeeGuid).NotEmpty();
        }
    }
}
