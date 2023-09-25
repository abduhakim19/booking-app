using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_account_roles")] // nama tabel untuk database
    public class AccountRole : BaseEntity // class Account mewarisi class BaeEntity
    {   // column("") => untuk nama kolom pada table
        [Column("account_guid")]
        public Guid AccountGuid { get; set; }

        [Column("role_guid")]
        public Guid RoleGuid { get; set; }
    }
}
