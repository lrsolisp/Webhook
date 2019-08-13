using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ContratoMambu
    {
        [DataMember]
        public string idContrato { get; set; }

        [DataMember]
        public string idCliente { get; set; }

        [DataMember]
        public string idCredito { get; set; }

        [DataMember]
        public string nombreSucursal { get; set; }

        [DataMember]
        public string nombreDelProducto { get; set; }

        [DataMember]
        public string metodologia { get; set; }

        [DataMember]
        public double tasaInteres { get; set; }

        [DataMember]
        public double montoPrestamo { get; set; }

        [DataMember]
        public string estatus { get; set; }

        [DataMember]
        public double ingresosMensuales { get; set; }

        [DataMember]
        public double gastosMensuales { get; set; }

        [DataMember]
        public string utilidad { get; set; }

        [DataMember]
        public string firmaElectronica { get; set; }

        [DataMember]
        public int experienciaAnios { get; set; }

        [DataMember]
        public string sector { get; set; }

        [DataMember]
        public string actividadEconomica { get; set; }

        [DataMember]
        public string objectivoDelCredito { get; set; }

        [DataMember]
        public int empleadosCreadosMT { get; set; }

        [DataMember]
        public int empleadosCreadosTC { get; set; }

        [DataMember]
        public int empleadosSostenidosMT { get; set; }

        [DataMember]
        public int empleadosSostenidosTC { get; set; }

        [DataMember]
        public int numeroDeNinosEmpleados { get; set; }

        [DataMember]
        public string tipoDeZona { get; set; }

        [DataMember]
        public string municipio { get; set; }

        [DataMember]
        public string nombreLocalidad { get; set; }

        [DataMember]
        public string tipoLocalidad { get; set; }

        [DataMember]
        public string PDA { get; set; }

        [DataMember]
        public int numeroNinosPatrocinados { get; set; }

        [DataMember]
        public string rolEnElHogar { get; set; }

        [DataMember]
        public string estadoCivil { get; set; }

        [DataMember]
        public string escolaridad { get; set; }

        [DataMember]
        public string tipoVivienda { get; set; }

        [DataMember]
        public string pisoFirme { get; set; }

        [DataMember]
        public string lineaTelefonica { get; set; }

        [DataMember]
        public string internet { get; set; }

        [DataMember]
        public string tvPorCable { get; set; }

        [DataMember]
        public string aguaPotable { get; set; }

        [DataMember]
        public string drenaje { get; set; }

        [DataMember]
        public string redesSociales { get; set; }

        [DataMember]
        public string seguroSocial { get; set; }

        [DataMember]
        public string seguroPopular { get; set; }

        [DataMember]
        public string hablaDialecto { get; set; }

        [DataMember]
        public int numeroFamiliaProspera { get; set; }

        [DataMember]
        public int numeroDeNinos { get; set; }
    }
}
