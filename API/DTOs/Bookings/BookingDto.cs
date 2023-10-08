using API.DTOs.Accounts;
using API.Models;
using API.Utilites.Enums;

namespace API.DTOs.Bookings
{
    public class BookingDto
    {
        public Guid Guid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public StatusLevel Status {  get; set; }
        public string Remarks { get; set; }
        public Guid RoomGuid { get; set; }
        public Guid EmployeeGuid { get; set; }


        public static explicit operator BookingDto(Booking booking)
        {
            return new BookingDto
            {
                Guid = booking.Guid,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                Remarks = booking.Remarks,
                Status = booking.Status,
                RoomGuid = booking.RoomGuid,
                EmployeeGuid = booking.EmployeeGuid,
            };
        }

        public static implicit operator Booking(BookingDto bookingDto)
        {
            return new Booking
            {
                Guid = bookingDto.Guid,
                StartDate = bookingDto.StartDate,
                EndDate = bookingDto.EndDate,
                Remarks = bookingDto.Remarks,
                Status = bookingDto.Status,
                RoomGuid = bookingDto.RoomGuid,
                EmployeeGuid = bookingDto.EmployeeGuid,
            };
        }
    }
}
