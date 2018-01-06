using System;
using System.Data.Linq.Mapping;

namespace ProjectBluefox.Database.Tables
{
    [Table(Name = "[Currency.Exchange]")]
    public class CurrencyExchangeTable
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column]
        public int Currency { get; set; }

        [Column]
        public int Exchange { get; set; }

        [Column]
        public int CreatedBy { get; set; }

        [Column]
        public DateTime DateCreated { get; set; }
    }
}