using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entidades
{
    [Serializable]
    [XmlRoot("User")]
    public class Usuario
    {

        [XmlElement("encodedKey")]
        public String encodedKey { get; set; }

        [XmlElement("id")]
        public String id { get; set; }

        [XmlElement("creationDate")]
        public String creationDate { get; set; }

        [XmlElement("lastModifiedDate")]
        public String lastModifiedDate { get; set; }

        [XmlElement("username")]
        public String username { get; set; }

        [XmlElement("email")]
        public String email { get; set; }

        [XmlElement("title")]
        public String title { get; set; }

        [XmlElement("firstName")]
        public String firstName { get; set; }

        [XmlElement("lastName")]
        public String lastName { get; set; }

        [XmlElement("homePhone")]
        public String homePhone { get; set; }

        [XmlElement("mobilePhone1")]
        public String mobilePhone1 { get; set; }

        [XmlElement("language")]
        public String language { get; set; }

        [XmlElement("userState")]
        public String userState { get; set; }

        [XmlElement("twoFactorAuthentication")]
        public String twoFactorAuthentication { get; set; }

        [XmlElement("isAdministrator")]
        public String isAdministrator { get; set; }

        [XmlElement("isTeller")]
        public String isTeller { get; set; }


        [XmlElement("isCreditOfficer")]
        public String isCreditOfficer { get; set; }


        [XmlElement("isSupport")]
        public String isSupport { get; set; }

        [XmlElement("assignedBranchKey")]
        public String assignedBranchKey { get; set; }

        public List<Usuario> user { get; set; }
    }
}
