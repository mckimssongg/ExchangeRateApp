using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models.DTO
{
    public class TransactionDTO
    {
        public int UserRefId { get; set; }

        public string? UserName { get; set; }
        public string SourceCurrency { get; set; }
        public string DestinationCurrency { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal? DestinationExchangeRate { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
