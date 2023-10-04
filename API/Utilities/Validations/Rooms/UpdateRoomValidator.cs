using API.DTOs.Rooms;
using FluentValidation;

namespace API.Utilities.Validations.Rooms
{
    public class UpdateRoomValidator : AbstractValidator<RoomDto>
    {
        public UpdateRoomValidator()
        {
            //validasi guid
            RuleFor(r => r.Guid).NotEmpty();
            // validasi nama
            RuleFor(r => r.Name).NotEmpty().MaximumLength(100);
            //validasi floor
            RuleFor(r => r.Floor).NotEmpty();
            //validasi capacity
            RuleFor(r => r.Capacity).NotEmpty();
        }
    }
}
