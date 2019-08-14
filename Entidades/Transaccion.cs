using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Serializable]
    public class Transaccion
    {
        public int numeroTransaccion { get; set; }

        public string encodedKey { get; set; }
        public long transactionId { get; set; }
        public string parentAccountKey { get; set; }
        public string type { get; set; }

        public decimal amount { get; set; }
        public decimal principalPaid { get; set; }
        public decimal interestPaid { get; set; }
        public decimal interestFromArrearsAmount { get; set; }
        public decimal deferredInterestAmount { get; set; }
        public decimal feesPaid { get; set; }
        public decimal penaltyPaid { get; set; }
        public decimal taxOnInterestPaid { get; set; }
        public decimal taxOnInterestFromArrearsAmount { get; set; }
        public decimal taxOnFeesAmount { get; set; }
        public decimal taxOnPenaltyAmount { get; set; }
        public decimal deferredTaxOnInterestAmount { get; set; }
        public decimal advancePosition { get; set; }
        public decimal arrearsPosition { get; set; }
        public decimal expectedPrincipalRedraw { get; set; }
        public decimal balance { get; set; }
        public decimal redrawBalance { get; set; }
        public decimal principalBalance { get; set; }
        public string centreKey { get; set; }


        public DateTime fechaMovimiento = new DateTime(1900, 1, 1);
        public DateTime fechaValor = new DateTime(1900, 1, 1);



        [JsonProperty("creationDate")]
        public string creationDate
        {
            get { return fechaMovimiento.ToString(); }
            set
            {
                DateTime dt = DateTime.ParseExact(value, "yyyy-MM-dd'T'HH:mm:ssK", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
                this.fechaMovimiento = DateTime.Parse(dt.ToString("yyyy-MM-dd hh:mm:ss tt"));
            }
        }

        [JsonProperty("entryDate")]
        public string entryDate
        {
            get { return fechaValor.ToString(); }
            set
            {
                DateTime dt = DateTime.ParseExact(value, "yyyy-MM-dd'T'HH:mm:ssK", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
                this.fechaValor = DateTime.Parse(dt.ToString("yyyy-MM-dd hh:mm:ss tt"));
            }
        }
    }
}
