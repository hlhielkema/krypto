using System;
using System.Data.Linq.Mapping;

namespace ProjectBluefox.Database.Tables
{
    [Table(Name = "[Account]")]
    public class AccountTable
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column]
        public string Username { get; set; } // max 50, allways lowercase, no spaces

        [Column]
        public string Password { get; set; } // hash, max 70

        [Column]
        public string EmailAddress { get; set; } // hash, max 200

        [Column]
        public bool Enabled { get; set; }

        [Column]
        public int Role { get; set; }

        [Column]
        public DateTime? LastLogon { get; set; }
    }
}