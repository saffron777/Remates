using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GCIT.Core.Entities
{
    [Table("Transactions")]
    public class Transactions
    {
        [Key]
        public long TransactionsId { get; set; }
        public int PlayerId { get; set; }
        [StringLength(50)]
        public string User { get; set; }
        [StringLength(150)]
        public string Token { get; set; }
        [StringLength(50)]
        public string IP { get; set; }
        public string Transaction { get; set; }
        public DateTime Date { get; set; }
        public Decimal? PlayerBalance { get; set; }
        public Decimal? Amount { get; set; }
        [StringLength(50)]
        public string Action { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        [StringLength(50)]
        public string RestMethod { get; set; }
        [StringLength(4000)]
        public string JsonRequest { get; set; }
        [StringLength(150)]
        public string GameId { get; set; }
    }
}
