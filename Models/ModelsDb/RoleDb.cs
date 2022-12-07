using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ModelsDb
{
    [Table(name: "roles")]
    public class RoleDb
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("role")]
        public string RoleName { get; set; }

        public List<AccountDb> Accounts = new List<AccountDb>();
    }
}
