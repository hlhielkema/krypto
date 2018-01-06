using System;
using System.Data.Linq.Mapping;

namespace ProjectBluefox.Database.Tables
{
    [Table(Name = "[Link.Category]")]
    public class LinkCategoryTable
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column]
        public Guid ExternalId { get; set; }

        [Column]
        public string Title { get; set; } // max 200

        [Column]
        public bool Deleted { get; set; }

        [Column]
        public int CreatedBy { get; set; }

        [Column]
        public DateTime DateCreated { get; set; }
    }
}