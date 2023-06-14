using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    public class ExchangeRate
    {
        public int ExchangeRateID { get; set; }
        public string CurrencyBase { get; set; }
        public string CurrencyDestination { get; set; }
        public decimal Rate { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
