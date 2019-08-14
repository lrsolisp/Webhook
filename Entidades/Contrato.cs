using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Contrato
    {
        public string idCredito { get; set; }
        public string idContrato { get; set; }
        public string idCliente { get; set; }
        public decimal montoCapital { get; set; }
        public decimal montoComisiones { get; set; }
        public decimal saldo { get; set; }
        public decimal capitalPagado { get; set; }
        public decimal interesPagado { get; set; }
        public string estatus { get; set; }
        public string subEstatus { get; set; }

    }
}
