using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entidades
{
    [Serializable]
    [JsonObject(Title = "pago")]
    public class Repayment
    {
        public String encodedKey { get; set; }
        public String parentAccountKey { get; set; }
        //public String dueDate { get; set; }

        [XmlElement("dueDate")]
        public DateTime fechaPago { get; set; }

        [XmlElement("fechaPago")]
        [JsonProperty("dueDate")]
        public string dueDate
        {
            get { return this.fechaPago.ToString(); }
            set
            {
                DateTime dt = DateTime.ParseExact(value, "yyyy-MM-dd'T'HH:mm:ssK", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
                this.fechaPago = DateTime.Parse(dt.ToString("yyyy-MM-dd hh:mm:ss tt"));
            }
        }

        public String principalDue { get; set; }
        public String principalPaid { get; set; }
        public String interestDue { get; set; }
        public String interestPaid { get; set; }
        public String feesDue { get; set; }
        public String feesPaid { get; set; }
        public String penaltyDue { get; set; }
        public String penaltyPaid { get; set; }
        public String state { get; set; }
        public String assignedUserKey { get; set; }
        public String assignedBranchKey { get; set; }
        public String taxInterestDue { get; set; }
        public String taxInterestPaid { get; set; }
        public String taxFeesDue { get; set; }
        public String taxFeesPaid { get; set; }
        public String taxPenaltyDue { get; set; }
        public String taxPenaltyPaid { get; set; }

        public string lastPaidDate { get; set; }
        public string lastPenaltyAppliedDate { get; set; }
        public string notes { get; set; }
        public string repaidDate { get; set; }
        public string assignedCentreKey { get; set; }

    }
}
