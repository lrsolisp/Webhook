using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entidades
{
    public class ContratoWebHookMambu
    {
        [DataMember(Name = "idContrato")]
        public string IdContrato { get; set; }

        [DataMember(Name = "idCredito")]
        public string IdCredito { get; set; }

        [DataMember(Name = "nombreSucursal")]
        public string NombreSucursal { get; set; }

        [DataMember(Name = "nombreDelProducto")]
        public string NombreDelProducto { get; set; }

        [DataMember(Name = "metodologia")]
        public string Metodologia { get; set; }

        [DataMember(Name = "tasaInteres")]
        public double TasaInteres { get; set; }

        [DataMember(Name = "montoPrestamo")]
        public double MontoPrestamo { get; set; }

        [DataMember(Name = "estatus")]
        public string Estatus { get; set; }

        [DataMember(Name = "ingresosMensuales")]
        public string IngresosMensuales { get; set; }

        [DataMember(Name = "gastosMensuales")]
        public string GastosMensuales { get; set; }

        [DataMember(Name = "utilidad")]
        public string Utilidad { get; set; }

        [DataMember(Name = "firmaElectronica")]
        public string FirmaElectronica { get; set; }

        [DataMember(Name = "experienciaAnios")]
        public string ExperienciaAnios { get; set; }

        [DataMember(Name = "sector")]
        public string Sector { get; set; }

        [DataMember(Name = "subSector")]
        public string SubSector { get; set; }

        [DataMember(Name = "actividadEconomica")]
        public string ActividadEconomica { get; set; }

        [DataMember(Name = "objectivoDelCredito")]
        public string ObjectivoDelCredito { get; set; }

        [DataMember(Name = "empleadosCreadosMT")]
        public string EmpleadosCreadosMT { get; set; }

        [DataMember(Name = "empleadosCreadosTC")]
        public string EmpleadosCreadosTC { get; set; }

        [DataMember(Name = "empleadosSostenidosMT")]
        public string EmpleadosSostenidosMT { get; set; }

        [DataMember(Name = "empleadosSostenidosTC")]
        public string EmpleadosSostenidosTC { get; set; }

        [DataMember(Name = "numeroDeNinosEmpleados")]
        public string NumeroDeNinosEmpleados { get; set; }

        [DataMember(Name = "PDA")]
        public string PDA { get; set; }

        [DataMember(Name = "numeroIdentificacion")]
        public string NumeroIdentificacion { get; set; }

        [DataMember(Name = "numeroDeNinos")]
        public string NumeroDeNinos { get; set; }

        [DataMember(Name = "beneficiario")]
        public string Beneficiario { get; set; }

        [DataMember(Name = "cliente")]
        public Cliente Cliente { get; set; }

        [DataMember(Name = "propositoCredito")]
        public string PropositoCredito { get; set; }

        [DataMember(Name = "fuenteFondeo")]
        public string FuenteFondeo { get; set; }

        [DataMember(Name = "valorBien")]
        public string ValorBien { get; set; }

        [DataMember(Name = "formaDesembolso")]
        public string FormaDesembolso { get; set; }

        [DataMember(Name = "nombreOficialCredito")]
        public string NombreOficialCredito { get; set; }

        [XmlArray("identificationdocument")]
        [XmlArrayItem("identificationdocument")]
        public List<Identificationdocument> identificationdocument { get; set; }
    }
}
