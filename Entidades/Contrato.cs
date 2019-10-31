using System;
using System.Collections.Generic;
using System.Globalization;
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
        public string beneficiario { get; set; }

        public string pda { get; set; }

        public string sector { get; set; }

        public string subSector { get; set; }

        public string propositoCredito { get; set; }

        public string empleosCreados { get; set; }

        public string empleosSostenidos { get; set; }

        public string fuenteFondeo { get; set; }

        public string valorBien { get; set; }

        public string formaDesembolso { get; set; }

        public string numeroOficialCredito { get; set; }

        public string nombreOficialCredito { get; set; }

        public DateTime fechaCierre { get; set; }
    }
}
