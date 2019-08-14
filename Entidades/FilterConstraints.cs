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
    [XmlRoot("filterConstraints")]
    [JsonObject(Title = "filterConstraints")]
    public class FilterConstraints
    {

        [XmlElement("filterSelection", IsNullable = false)]
        [JsonProperty(PropertyName = "filterSelection", NullValueHandling = NullValueHandling.Ignore)]
        public String filterSelection { get; set; }


        [XmlElement("filterElement", IsNullable = false)]
        [JsonProperty(PropertyName = "filterElement", NullValueHandling = NullValueHandling.Ignore)]
        public String filterElement { get; set; }


        [XmlElement("dataItemType", IsNullable = false)]
        [JsonProperty(PropertyName = "dataItemType", NullValueHandling = NullValueHandling.Ignore)]
        public String dataItemType { get; set; }


        [XmlElement("dataFieldType", IsNullable = false)]
        [JsonProperty(PropertyName = "dataFieldType", NullValueHandling = NullValueHandling.Ignore)]
        public String dataFieldType { get; set; }


        [XmlElement("value", IsNullable = false)]
        [JsonProperty(PropertyName = "value", NullValueHandling = NullValueHandling.Ignore)]
        public String value { get; set; }


        [XmlElement("secondValue", IsNullable = false)]
        [JsonProperty(PropertyName = "secondValue", NullValueHandling = NullValueHandling.Ignore)]
        public String secondValue { get; set; }


        [XmlElement("values", IsNullable = false)]
        [JsonProperty(PropertyName = "values", NullValueHandling = NullValueHandling.Ignore)]
        public List<String> values { get; set; }

    }
}
