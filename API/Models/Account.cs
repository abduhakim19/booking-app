using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_accounts")] // nama tabel untuk database
    public class Account : BaseEntity // class Account mewarisi class BaeEntity
    {
        // column("") => untuk  nama kolom dan tipe data pada table
        // TypeName =  => tipe data beserta panjangnya
        [Column("password", TypeName = "nvarchar(MAX)")] 
        public string Password { get; set; }
        // column("") => untuk nama kolom pada table
        [Column("is_deleted")]
        public bool isDeleted { get; set; }

        [Column("otp")]
        public int Otp {  get; set; }

        [Column("is_used")]
        public bool IsUsed {  get; set; }

        [Column("expired_time")]
        public DateTime ExpiredTime { get; set; }
    }
}
