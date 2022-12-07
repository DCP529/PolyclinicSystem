using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models.ModelsDb
{
    [Table(name: "account")]
    public class AccountDb
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("role_id")]
        public Guid RoleId { get; set; }

        [JsonIgnore]
        public RoleDb Role { get; set; } = new RoleDb();
    }
}
