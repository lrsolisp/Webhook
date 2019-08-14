using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entidades
{
    public class DisbursementDetails
    {
        [XmlArray("fees")]
        [XmlArrayItem("fee")]
        public List<AnidaFee> fees { get; set; }


        public String expectedDisbursementDate
        {
            get;
            set;
        }

        public String disbursementDate
        {
            get;
            set;
        }

        public String firstRepaymentDate
        {
            get;
            set;
        }
    }
}
