using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Identificationdocument
    {

        public string encodedKey { get; set; }
        public string clientKey { get; set; }
        public string documentType { get; set; }
        public string documentId { get; set; }
        public string issuingAuthority { get; set; }
        public DateTime validUntil { get; set; }
    }
}
