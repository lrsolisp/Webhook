using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Movimiento
    {
        public string idSucursal { get; set; }

        public string idContrato { get; set; }

        public string codigo { get; set; }

        public DateTime fechaMovimiento { get; set; }

        public DateTime fechaValor { get; set; }

        public long idTransaccion { get; set; }

        public decimal montoTotal { get; set; }

        public decimal montoCapital { get; set; }

        public decimal montoInteres { get; set; }

        public decimal saldo { get; set; }



    }
}
