using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Entidades
{
    [Serializable]
    [JsonObject("group")]
    public class Grupo
    {

        public String encodedKey { get; set; }
        public String id { get; set; }
        public String groupName { get; set; }
        public String assignedUserKey { get; set; }
        public String assignedBranchKey { get; set; }
        public String loanCycle { get; set; }
        public string assignedCentreKey { get; set; }

        public List<ClienteMambu> groupMembers { get; set; }




        [XmlArray("customInformation")]
        [JsonProperty("customInformation")]
        public List<CustomInformation> customInformation { get; set; }
    }
}