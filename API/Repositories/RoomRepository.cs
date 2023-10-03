using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{   // class RoomRepository inheritance interface IRoomRepository
    public class RoomRepository : GeneralRepository<Room>,IRoomRepository
    {
        public RoomRepository(BookingManagementDbContext context) : base(context) { }

    }
}
