using Datos.Impl;
using Entidades;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            long response = 0;
            try
            {
                operacionesBD.BeginTransaction();
                response = operacionesBD.InsertarCredito(credito);
                operacionesBD.Commit();
                return response;
            }
            catch (Exception e)
            {
                operacionesBD.RollBack();
                return response;
            }
        }

        public static long InsertarAmortizaciones(Pago pago)
        {
            long response = 0;
            try
            {
                operacionesBD.BeginTransaction();
                response = operacionesBD.InsertarAmortizacion(pago);
                operacionesBD.Commit();
                return response;
            }
            catch (Exception e)
            {
                operacionesBD.RollBack();
                return response;
            }
        }

        public static long InsertarCliente(Cliente cliente)
        {
            long response = 0;
            try
            {
                operacionesBD.BeginTransaction();
                response = operacionesBD.InsertarCliente(cliente);
                operacionesBD.Commit();
                return response;
            }
            catch (Exception e)
            {
                Debug.Print("Error " + e.Message);
                operacionesBD.RollBack();
                return response;
            }
        }

        public static long InsertarContrato(Contrato contratoInsertar)
        {
            long response = 0;
            try
            {
                operacionesBD.BeginTransaction();
                response = operacionesBD.InsertarContrato(contratoInsertar);
                operacionesBD.Commit();
                return response;
            }
            catch (Exception e)
            {
                operacionesBD.RollBack();
                return response;
            }
        }

        public static long InsertarMovimiento(Movimiento movimiento)
        {
            long response = 0;
            try
            {
                operacionesBD.BeginTransaction();
                response = operacionesBD.InsertarMovimiento(movimiento);
                operacionesBD.Commit();
                return response;
            }
            catch (Exception e)
            {
                operacionesBD.RollBack();
                return response;
            }
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
            try
            {
                operacionesBD.BeginTransaction();
                operacionesBD.BorrarAmortizacionesContrato(idContrato);
                operacionesBD.Commit();
            }
            catch
            {
                operacionesBD.RollBack();
            }
        }

        public static void ActualizarContrato(Dictionary<string, object> parametros)
        {
            try
            {
                operacionesBD.BeginTransaction();
                operacionesBD.ActualizarContrato(parametros);
                operacionesBD.Commit();
            }
            catch
            {
                operacionesBD.RollBack();
            }
        }

        public static void BorrarMovimientosContratos(string id)
        {
            try
            {
                operacionesBD.BeginTransaction();
                operacionesBD.BorrarMovimientosContrato(id);
                operacionesBD.Commit();
            }
            catch
            {
                operacionesBD.RollBack();
            }
        }

        public static void BorrarCliente(string idCliente)
        {
            try
            {
                operacionesBD.BeginTransaction();
                operacionesBD.BorrarCliente(idCliente);
                operacionesBD.Commit();
            }
            catch
            {
                operacionesBD.RollBack();
            }
        }

        public static string ObtenerClienteContrato(string idContrato)
        {
            return operacionesBD.ObtenerCliente(idContrato);
        }

        public static void BorrarContrato(string idContrato)
        {
            try
            {
                operacionesBD.BeginTransaction();
                operacionesBD.BorrarContrato(idContrato);
                operacionesBD.Commit();
            }
            catch
            {
                operacionesBD.RollBack();
            }
        }

        public static string ExisteGrupo(string idGrupo)
        {
            return operacionesBD.ExisteGrupo(idGrupo);
        }

        public static void InsertarGrupo(Dictionary<string, object> parametrosGrupo)
        {
            operacionesBD.InsertarGrupo(parametrosGrupo);
        }
    }
}
