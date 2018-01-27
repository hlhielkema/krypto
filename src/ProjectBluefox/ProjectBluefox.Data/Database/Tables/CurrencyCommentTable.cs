using System;
using System.Data.Linq.Mapping;

namespace ProjectBluefox.Database.Tables
{
    [Table(Name = "[Currency.Comment]")]
    public class CurrencyCommentTable
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column]
        public Guid ExternalId { get; set; }

        [Column]
        public int Currency { get; set; }

        [Column]
        public int CreatedBy { get; set; }

        [Column]
        public int? DeletedBy { get; set; }

        [Column]
        public DateTime DateCreated { get; set; }

        [Column]
        public int Vote { get; set; }

        [Column]
        public string Message { get; set; } // max 2000        

        [Column]
        public bool Deleted { get; set; }
    }
}