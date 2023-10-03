using API.Models;
using API.Utilites.Enums;

namespace API.DTOs.Bookings
{
    public class CreateBookingDto
    {
        public Guid Guid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Remarks { get; set; }
        public Guid RoomGuid { get; set; }
        public Guid EmployeeGuid { get; set; }

        public static implicit operator Booking(CreateBookingDto createBookingDto)
        {
            return new Booking
            {
                RoomGuid = createBookingDto.RoomGuid,
                EmployeeGuid = createBookingDto.EmployeeGuid,
                StartDate = createBookingDto.StartDate,
                EndDate = createBookingDto.EndDate,
                Remarks = createBookingDto.Remarks,
                Status = 0,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
