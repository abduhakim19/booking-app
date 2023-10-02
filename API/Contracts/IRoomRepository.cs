using API.Models;

namespace API.Contracts
{   //Interface RoomRepository
    public interface IRoomRepository
    {
        IEnumerable<Room> GetAll(); // return Ienumerable
        Room? GetByGuid(Guid guid); // return objek Room boleh null
        Room? Create(Room room); // return objek Room boleh null
        bool Update(Room room); // return bool
        bool Delete(Room room); // return bool 
    }
}
