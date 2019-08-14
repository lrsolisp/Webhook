using Atlassian.Jira;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entidades
{
    [Serializable]
    [XmlRoot("customFieldValue")]
    public class CustomFieldValue
    {
        public String encodedKey { get; set; }
        public String parentKey { get; set; }
        public String customFieldKey { get; set; }
        public CustomField customField { get; set; }
        public String value { get; set; }
        public String customFieldID { get; set; }
        public String linkedEntityKeyValue { get; set; }
        public String customFieldSetGroupIndex { get; set; }
    }
}
