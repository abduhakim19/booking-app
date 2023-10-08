using API.Models;
using API.Utilites.Enums;

namespace API.DTOs.Bookings
{
    public class BookingRoomTodayDto
    {
        public Guid BookingGuid { get; set; }
        public string RoomName { get; set; }
        public string Status { get; set; }
        public int Floor {  get; set; }
        public string BookedBy {  get; set; }

    }
}
