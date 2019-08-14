using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Credito
    {

        public string idCredito { get; set; }
        public string idSucursal { get; set; }
        public string idCliente { get; set; }
        public string nombreSucursal { get; set; }
        public string idGrupo { get; set; }
        public string nombreGrupo { get; set; }
        public string keyProducto { get; set; }
        public string nombreProducto { get; set; }
        public string keyGrupo { get; set; }
        public string metodologia { get; set; }
        public string frecuenciaPagoUnidad { get; set; }
        public string estatus { get; set; }

        public int diasVencidos { get; set; }
        public int frecuenciaPagoNumero { get; set; }
        public int numeroPagos { get; set; }

        public decimal tasaAnual { get; set; }
        public decimal tasaMensual { get; set; }
        public decimal tasaDiaria { get; set; }

        public DateTime fechaEsperadaLiquidacion { get; set; }
        public DateTime fechaLiquidacion { get; set; }
        public DateTime fechaBaja { get; set; }
        public DateTime fechaDesembolso { get; set; }
        public DateTime fechaContrato { get; set; }



        public Credito()
        {
            fechaEsperadaLiquidacion = DateTime.Now;
        }

    }
}
