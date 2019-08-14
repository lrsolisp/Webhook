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
        private static Datos.Impl.Operaciones operacionesBD = new Datos.Impl.Operaciones();

        public static string ValidaExisteContrato(Dictionary<string, object> parametros)
        {
            return operacionesBD.ValidaExisteContrato(parametros);
        }

        public static string ExisteCredito(Dictionary<string, object> parametros)
        {
            return operacionesBD.ExisteCredito(parametros);
        }
    }
}
