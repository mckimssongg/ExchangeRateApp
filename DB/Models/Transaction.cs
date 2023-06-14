using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int UserRefID { get; set; }
        [ForeignKey("UserRefID")]
        public virtual User User { get; set; }
        public string SourceCurrency { get; set; }
        public string DestinationCurrency { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal? DestinationExchangeRate { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
