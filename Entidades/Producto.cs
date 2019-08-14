using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [JsonObject(Title = "producto")]
    public class Producto
    {
        public String encodedKey { get; set; }
        public String id { get; set; }
        public String productName { get; set; }
        public String productDescription { get; set; }
        public String loanProductType { get; set; }
        public Boolean activated { get; set; }



    }
}
