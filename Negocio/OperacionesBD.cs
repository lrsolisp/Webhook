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

        public static long InsertarCredito(Credito credito)
        {
            return operacionesBD.InsertarCredito(credito);
        }

        public static long InsertarAmortizaciones(Pago pago)
        {
            return operacionesBD.InsertarAmortizacion(pago);
        }

        public static long InsertarCliente(Cliente cliente)
        {
            return operacionesBD.InsertarCliente(cliente);
        }

        public static long InsertarContrato(Contrato contratoInsertar)
        {
            return operacionesBD.InsertarContrato(contratoInsertar);
        }

        public static long InsertarMovimiento(Movimiento movimiento)
        {
            return operacionesBD.InsertarMovimiento(movimiento);
        }
    }
}
