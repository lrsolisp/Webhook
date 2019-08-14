using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entidades
{
    [Serializable]
    [XmlRoot("address")]
    [JsonObject(Title = "address")]
    public class Address
    {
        [XmlElement("encodedKey")]
        [JsonProperty("encodedKey")]
        public String encodedKey { get; set; }

        [XmlElement("parentKey")]
        [JsonProperty("parentKey")]
        public String parentKey { get; set; }

        [XmlElement("line1")]
        [JsonProperty("line1")]
        public String line1 { get; set; }

        [XmlElement("line2")]
        [JsonProperty("line2")]
        public String line2 { get; set; }

        [XmlElement("city")]
        [JsonProperty("city")]
        public String city { get; set; }

        [XmlElement("region")]
        [JsonProperty("region")]
        public String region { get; set; }

        [XmlElement("postcode")]
        [JsonProperty("postcode")]
        public String postcode { get; set; }

        [XmlElement("country")]
        [JsonProperty("country")]
        public String country { get; set; }

        [XmlElement("indexInList")]
        [JsonProperty("indexInList")]
        public String indexInList { get; set; }
    }
}
