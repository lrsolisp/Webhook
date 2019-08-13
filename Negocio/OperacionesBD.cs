using Datos.Impl;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class OperacionesBD
    {
        private static Operaciones operacionesBD = new Operaciones();

        public static string ValidaExisteContrato(Dictionary<string, object> parametros)
        {
            return operacionesBD.ValidaExisteContrato(parametros);
        }
    }
}
