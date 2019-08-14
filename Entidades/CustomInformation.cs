using Newtonsoft.Json;
using System;

namespace Entidades
{
    [Serializable]
    [JsonObject(Title = "customInformation")]
    public class CustomInformation
    {
        public String encodedKey { get; set; }
        public String parentKey { get; set; }
        public String customFieldKey { get; set; }
        public CustomField customField { get; set; }
        public String value { get; set; }
        public String customFieldID { get; set; }
        public String customFieldSetGroupIndex { get; set; }

    }
}