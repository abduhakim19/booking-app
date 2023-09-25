using API.Utilites.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_tr_bookings")]
    public class Booking : BaseEntity
    {
        // column("") => untuk nama kolom pada table
        [Column("start_date")]
        public DateTime StartDate { get; set; }

        [Column("end_date")]
        public DateTime EndDate { get; set; }

        [Column("status_level")]
        public StatusLevel Status { get; set; } // tipe enum StatusLevel

        [Column("remarks", TypeName = "nvarchar(MAX)")]
        public string Remarks { get; set; }

        [Column("room_guid")]
        public Guid RoomGuid { get; set; }
        [Column("employee_guid")]
        public Guid EmployeeGuid { get; set; }
    }
}
