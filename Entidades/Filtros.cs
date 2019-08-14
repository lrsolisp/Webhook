using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Serializable]
    [JsonObject(Title = "Filtros")]
    public class Filtros
    {
        [JsonProperty("filterConstraints")]
        public IList<FilterConstraints> filterConstraints { get; set; }


        public Filtros()
        {
            this.filterConstraints = new List<FilterConstraints>();
        }
    }
}
