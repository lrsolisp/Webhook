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
        [DataMember (Name = "idContrato")]
        public string IdContrato { get; set; }

        [DataMember (Name = "idCliente")]
        public string IdCliente { get; set; }

        [DataMember (Name = "idCredito")]
        public string IdCredito { get; set; }

        [DataMember (Name = "nombreSucursal")]
        public string NombreSucursal { get; set; }

        [DataMember (Name = "nombreDelProducto")]
        public string NombreDelProducto { get; set; }

        [DataMember (Name = "metodologia")]
        public string Metodologia { get; set; }

        [DataMember (Name = "tasaInteres")]
        public double TasaInteres { get; set; }

        [DataMember (Name = "montoPrestamo")]
        public double MontoPrestamo { get; set; }

        [DataMember (Name = "estatus")]
        public string Estatus { get; set; }

        [DataMember (Name = "ingresosMensuales")]
        public double IngresosMensuales { get; set; }

        [DataMember (Name = "gastosMensuales")]
        public double GastosMensuales { get; set; }

        [DataMember (Name = "utilidad")]
        public string Utilidad { get; set; }

        [DataMember (Name = "firmaElectronica")]
        public string FirmaElectronica { get; set; }

        [DataMember (Name = "experienciaAnios")]
        public int ExperienciaAnios { get; set; }

        [DataMember (Name = "sector")]
        public string Sector { get; set; }

        [DataMember (Name = "actividadEconomica")]
        public string ActividadEconomica { get; set; }

        [DataMember (Name = "objectivoDelCredito")]
        public string ObjectivoDelCredito { get; set; }

        [DataMember (Name = "empleadosCreadosMT")]
        public int EmpleadosCreadosMT { get; set; }

        [DataMember (Name = "empleadosCreadosTC")]
        public int EmpleadosCreadosTC { get; set; }

        [DataMember (Name = "empleadosSostenidosMT")]
        public int EmpleadosSostenidosMT { get; set; }

        [DataMember (Name = "empleadosSostenidosTC")]
        public int EmpleadosSostenidosTC { get; set; }

        [DataMember (Name = "numeroDeNinosEmpleados")]
        public int NumeroDeNinosEmpleados { get; set; }

        [DataMember (Name = "tipoDeZona")]
        public string TipoDeZona { get; set; }

        [DataMember (Name = "municipio")]
        public string Municipio { get; set; }

        [DataMember (Name = "nombreLocalidad")]
        public string NombreLocalidad { get; set; }

        [DataMember (Name = "tipoLocalidad")]
        public string TipoLocalidad { get; set; }

        [DataMember (Name = "PDA")]
        public string PDA { get; set; }

        [DataMember (Name = "numeroNinosPatrocinados")]
        public int NumeroNinosPatrocinados { get; set; }

        [DataMember (Name = "rolEnElHogar")]
        public string RolEnElHogar { get; set; }

        [DataMember (Name = "estadoCivil")]
        public string EstadoCivil { get; set; }

        [DataMember (Name = "escolaridad")]
        public string Escolaridad { get; set; }

        [DataMember (Name = "tipoVivienda")]
        public string TipoVivienda { get; set; }

        [DataMember (Name = "pisoFirme")]
        public string PisoFirme { get; set; }

        [DataMember (Name = "lineaTelefonica")]
        public string LineaTelefonica { get; set; }

        [DataMember (Name = "internet")]
        public string Internet { get; set; }

        [DataMember (Name = "tvPorCable")]
        public string TvPorCable { get; set; }

        [DataMember (Name = "aguaPotable")]
        public string AguaPotable { get; set; }

        [DataMember (Name = "drenaje")]
        public string Drenaje { get; set; }

        [DataMember (Name = "redesSociales")]
        public string RedesSociales { get; set; }

        [DataMember (Name = "seguroSocial")]
        public string SeguroSocial { get; set; }

        [DataMember (Name = "seguroPopular")]
        public string SeguroPopular { get; set; }

        [DataMember (Name = "hablaDialecto")]
        public string HablaDialecto { get; set; }

        [DataMember (Name = "numeroFamiliaProspera")]
        public int NumeroFamiliaProspera { get; set; }

        [DataMember (Name = "numeroDeNinos")]
        public int NumeroDeNinos { get; set; }

        [DataMember (Name ="numeroIdentificacion")]
        public string NumeroIdentificacion { get; set; }

        [DataMember (Name ="beneficiario")]
        public string Beneficiario { get; set; }
    }
}
