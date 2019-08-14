using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Fee
    {
        public String encodedKey { get; set; }
        public String name { get; set; }
        public String amount { get; set; }
        public String amountCalculationMethod { get; set; }
        public String trigger { get; set; }
        public String feeApplication { get; set; }
        public String active { get; set; }
        public String creationDate { get; set; }
        public String amortizationProfile { get; set; }
        public String percentageAmount { get; set; }
    }
}
