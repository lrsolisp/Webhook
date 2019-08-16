using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Cliente
    {
        public string idCliente { get; set; }
        public string nombre { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string fechaNacimiento { get; set; }

        [DataMember(Name = "rfc")]
        public string RFC { get; set; }

        [DataMember(Name = "curp")]
        public string CURP { get; set; }

        [DataMember(Name = "cp")]
        public string CP { get; set; }

        [DataMember(Name = "tipoDeZona")]
        public string TipoDeZona { get; set; }

        [DataMember(Name = "municipio")]
        public string municipio { get; set; }

        [DataMember(Name = "nombreLocalidad")]
        public string NombreLocalidad { get; set; }

        [DataMember(Name = "tipoLocalidad")]
        public string TipoLocalidad { get; set; }

        public string nacionalidad { get; set; }

        [DataMember(Name = "numeroNinosPatrocinados")]
        public int NumeroNinosPatrocinados { get; set; }

        [DataMember(Name = "rolEnElHogar")]
        public string RolEnElHogar { get; set; }

        [DataMember(Name = "estadoCivil")]
        public string EstadoCivil { get; set; }

        [DataMember(Name = "escolaridad")]
        public string Escolaridad { get; set; }

        [DataMember(Name = "tipoVivienda")]
        public string TipoVivienda { get; set; }

        [DataMember(Name = "pisoFirme")]
        public string PisoFirme { get; set; }

        [DataMember(Name = "lineaTelefonica")]
        public string LineaTelefonica { get; set; }

        [DataMember(Name = "internet")]
        public string Internet { get; set; }

        [DataMember(Name = "tvPorCable")]
        public string TvPorCable { get; set; }

        [DataMember(Name = "aguaPotable")]
        public string AguaPotable { get; set; }

        [DataMember(Name = "drenaje")]
        public string Drenaje { get; set; }

        [DataMember(Name = "redesSociales")]
        public string RedesSociales { get; set; }

        [DataMember(Name = "seguroSocial")]
        public string SeguroSocial { get; set; }

        [DataMember(Name = "seguroPopular")]
        public string SeguroPopular { get; set; }

        [DataMember(Name = "hablaDialecto")]
        public string HablaDialecto { get; set; }

        [DataMember(Name = "numeroFamiliaProspera")]
        public int NumeroFamiliaProspera { get; set; }

        public string sexo { get; set; }

        public string numeroDependientes { get; set; }
        
        public string direccion { get; set; }

        public string coloniaPoblacion { get; set; }

        public string delegacionMunicipio { get; set; }

        public string ciudad { get; set; }

        public string estado { get; set; }        

        public string numeroTelefonico { get; set; }                               
    }
}
