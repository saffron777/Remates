using System;
using System.Collections.Generic;
using System.Text;

namespace GCIT.Core.Models
{
    public class TransactionModel
    {
        public int PlayerId { get; set; }
        
        public string User { get; set; }
        
        public string Token { get; set; }
        
        public string IP { get; set; }
        public string Transaction { get; set; }
        public DateTime Date { get; set; }
        public Decimal? PlayerBalance { get; set; }
        public Decimal? Amount { get; set; }
        
        public string Action { get; set; }
        
        public string Description { get; set; }
        
        public string RestMethod { get; set; }
        
        public string JsonRequest { get; set; }
        
        public string GameId { get; set; }
    }
}
