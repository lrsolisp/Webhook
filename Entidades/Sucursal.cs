using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entidades
{
    public class Sucursal
    {
        public String encodedKey { get; set; }
        public String id { get; set; }
        public String name { get; set; }

        public String phoneNumber { get; set; }

        public String emailAddress { get; set; }


        [XmlElement("address")]
        [JsonProperty("address")]
        public Address address { get; set; }
    }
}
