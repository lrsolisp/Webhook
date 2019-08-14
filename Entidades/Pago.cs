using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Pago
    {

        public int numeroCuota { get; set; }
        public string idContrato { get; set; }
        public DateTime fechaPago { get; set; }
        public DateTime fechaPagado { get; set; }
        public decimal capitalPagado { get; set; }
        public decimal capitalEsperado { get; set; }
        public decimal interesPagado { get; set; }
        public decimal interesEsperado { get; set; }
        public string estatus { get; set; }

    }
}
