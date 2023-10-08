using System.Reflection.Metadata.Ecma335;

namespace API.Utilites.Enums
{   // Enum untuk database, karena tidak ada di mssql
    public enum StatusLevel
    {
        Requested = 1,
        OnBooking = 2,
        Completed = 3, 
        Cancelled = 4

    }
}
