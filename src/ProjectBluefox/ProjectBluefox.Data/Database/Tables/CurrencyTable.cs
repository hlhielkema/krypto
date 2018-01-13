using System;
using System.Data.Linq.Mapping;

namespace ProjectBluefox.Database.Tables
{
    [Table(Name = "[Currency]")]
    public class CurrencyTable
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column]
        public Guid ExternalId { get; set; }

        [Column]
        public string DisplayName { get; set; } // max 100

        [Column]
        public string ShortCode { get; set; } // max 3

        [Column]
        public bool Deleted { get; set; }

        [Column]
        public int CreatedBy { get; set; }

        [Column]
        public DateTime DateCreated { get; set; }
    }
}