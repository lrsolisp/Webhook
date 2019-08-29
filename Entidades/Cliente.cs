using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entidades
{
    public class Cliente
    {
        public string idCliente { get; set; }

        [DataMember(Name = "rfc")]
        public string RFC { get; set; }

        [DataMember(Name = "curp")]
        public string CURP { get; set; }

        public string nacionalidad { get; set; }

        [DataMember(Name = "cp")]
        public string CP { get; set; }

        [DataMember(Name = "estadoCliente")]
        public string estado { get; set; }

        [DataMember(Name = "nombreLocalidad")]
        public string NombreLocalidad { get; set; }

        [DataMember(Name = "numeroNinosPatrocinados")]
        public string NumeroNinosPatrocinados { get; set; }      

        [DataMember(Name = "estadoCivil")]
        public string EstadoCivil { get; set; }

        [DataMember(Name = "escolaridad")]
        public string Escolaridad { get; set; }
        
        public string scoreCredito { get; set; }

        public string cicloCliente { get; set; }

        public string tipoAsentamiento { get; set; }

        public string nombre { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public DateTime fechaNacimiento { get; set; }

        public string sexo { get; set; }

        public string numeroDependientes { get; set; }

        public string calle { get; set; }

        public string numExt { get; set; }

        public string numInt { get; set; }

        public string coloniaPoblacion { get; set; }

        public string delegacionMunicipio { get; set; }

        public string ciudad { get; set; }

        public string numeroTelefonico { get; set; }
        public string numeroCelular { get; set; }

        [XmlArray("customInformation")]
        [JsonProperty("customInformation")]
        public List<CustomInformation> customInformation { get; set; }

    }
}
