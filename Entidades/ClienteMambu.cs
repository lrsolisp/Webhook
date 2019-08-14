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
    [JsonObject("client")]
    public class ClienteMambu
    {


        [JsonProperty("client")]
        public ClienteMambu client { get; set; }


        [JsonProperty("encodedKey")]
        public String encodedKey { get; set; }


        [JsonProperty("state")]
        public String state { get; set; }


        [JsonProperty(PropertyName = "id")]
        public String id { get; set; }

        [XmlElement("firstName")]
        [JsonProperty(PropertyName = "firstName")]
        public String firstName { get; set; }

        [XmlElement("lastName")]
        [JsonProperty("lastName")]
        public String lastName { get; set; }

        [XmlElement("middleName")]
        public String middleName { get; set; }

        [XmlElement("birthDate")]
        [JsonProperty("birthDate")]
        public String birthDate { get; set; }

        [XmlElement("gender")]
        [JsonProperty("gender")]
        public String gender { get; set; }

        [XmlElement("loanCycle")]
        [JsonProperty("loanCycle")]
        public String loanCycle { get; set; }

        [XmlElement("groupLoanCycle")]
        [JsonProperty("groupLoanCycle")]
        public String groupLoanCycle { get; set; }


        [XmlElement("assignedBranchKey")]
        [JsonProperty("assignedBranchKey")]
        public String assignedBranchKey { get; set; }


        [XmlElement("homePhone")]
        [JsonProperty("homePhone")]
        public String homePhone { get; set; }


        [XmlElement("emailAddress")]
        [JsonProperty("emailAddress")]
        public String emailAddress { get; set; }


        [XmlElement("mobilePhone1")]
        [JsonProperty("mobilePhone1")]
        public String mobilePhone1 { get; set; }


        [XmlArray("customInformation")]
        [JsonProperty("customInformation")]
        public List<CustomInformation> customInformation { get; set; }

        [XmlArray("groupKeys")]
        [JsonProperty("groupKeys")]
        public List<String> groupKeys { get; set; }


        public string assignedCentreKey { get; set; }
    }
}
