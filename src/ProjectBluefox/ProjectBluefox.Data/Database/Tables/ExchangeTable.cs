using System;
using System.Data.Linq.Mapping;

namespace ProjectBluefox.Database.Tables
{
    [Table(Name = "[Exchange]")]
    public class ExchangeTable
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column]
        public Guid ExternalId { get; set; }

        [Column]
        public string DisplayName { get; set; }

        [Column]
        public string Url { get; set; }

        [Column]
        public bool Deleted { get; set; }

        [Column]
        public int CreatedBy { get; set; }

        [Column]
        public DateTime DateCreated { get; set; }
    }
}