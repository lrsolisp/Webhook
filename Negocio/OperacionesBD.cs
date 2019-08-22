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
            operacionesBD.BeginTransaction();

            try
            {
                operacionesBD.Commit();
                return operacionesBD.InsertarCliente(cliente);
            }
            catch(Exception e)
            {
                operacionesBD.RollBack();
                return 0;
            }            
        }

        public static long InsertarContrato(Contrato contratoInsertar)
        {
            return operacionesBD.InsertarContrato(contratoInsertar);
        }

        public static long InsertarMovimiento(Movimiento movimiento)
        {
            return operacionesBD.InsertarMovimiento(movimiento);
        }

        public static string ExisteCliente(Dictionary<string, object> parametros)
        {
            return operacionesBD.ExisteCliente(parametros);
        }

        public static string ExisteTransaccion(long transactionId)
        {
            return operacionesBD.ExisteTransaccion(transactionId);
        }

        public static void BorrarAmortizacionesContrato(string idContrato)
        {
            operacionesBD.BorrarAmortizacionesContrato(idContrato);
        }

        public static void ActualizarContrato(Dictionary<string, object> parametros)
        {
            operacionesBD.ActualizarContrato(parametros);
        }

        public static void BorrarMovimientosContratos(string id)
        {
            operacionesBD.BorrarMovimientosContrato(id);
        }

        public static void BorrarCliente(string idCliente)
        {
            throw new NotImplementedException();
        }

        public static Cliente ObtenerClienteContrato(string idContrato)
        {
            throw new NotImplementedException();
        }
    }
}
