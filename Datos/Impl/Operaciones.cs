using Entidades;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Impl
{
    public class Operaciones
    {
        private ISqlMapper mapper;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Operaciones()
        {
            if(mapper == null)
            {
                mapper = Mapper.Instance();
            }
        }

        public string ValidaExisteContrato(Dictionary<string, object> parametros)
        {
            return mapper.QueryForObject<string>("ExisteContrato", parametros);
        }

        public string ExisteCredito(Dictionary<string, object> parametros)
        {
            return mapper.QueryForObject<string>("ExisteCredito", parametros);
        }

        public long InsertarCredito(Credito credito)
        {
            long insertado = -1;

            try
            {
                Object objeto = mapper.Insert("InsertarCredito", credito);

                if (objeto != null)
                {
                    insertado = (long)objeto;
                    objeto = null;
                }
            }
            catch(Exception e)
            {
                log.Error("ERROR --------- " + e.StackTrace);
            }

            return insertado;
        }

        public long InsertarCliente(Cliente cliente)
        {
            long insertado = -1;

            try
            {
                Object objeto = mapper.Insert("InsertarCliente", cliente);

                if (objeto != null)
                {
                    insertado = (long)objeto;
                    objeto = null;
                }
            }
            catch(Exception e)
            {
                log.Error("ERROR --------- "+ e.StackTrace);
            }

            return insertado;
        }

        public long InsertarContrato(Contrato contratoInsertar)
        {
            long insertado = -1;

            try
            {
                Object objeto = mapper.Insert("InsertarContrato", contratoInsertar);

                if (objeto != null)
                {
                    insertado = (long)objeto;
                    objeto = null;
                }
            }
            catch(Exception e)
            {
                log.Error("ERROR --------- " + e.StackTrace);
            }

            return insertado;
        }

        public void BorrarAmortizacionesContrato(string idContrato)
        {
            try
            {
                Object objeto = mapper.Delete("EliminarAmortizacionesContrato", idContrato);
            }
            catch(Exception e)
            {
                log.Error("ERROR --------- " + e.StackTrace);
            }
        }

        public string ExisteTransaccion(long transactionId)
        {
            return mapper.QueryForObject<string>("ExisteTransaccion", transactionId);
        }

        public void BorrarMovimientosContrato(string id)
        {
            try
            {
                Object objeto = mapper.Delete("EliminarMovimientosContrato", id);
            }
            catch (Exception e)
            {
                log.Error("ERROR --------- " + e.StackTrace);
            }            
        }

        public void ActualizarContrato(Dictionary<string, object> parametros)
        {            
            try
            {
                mapper.Update("ActualizarContrato", parametros);
            }
            catch (Exception e)
            {
                log.Error("ERROR --------- " + e.StackTrace);
            }
        }

        public string ExisteCliente(Dictionary<string, object> parametros)
        {
            return mapper.QueryForObject<string>("ExisteCliente", parametros);
        }

        public void BorrarCliente(string idCliente)
        {
            try
            {
                Object objeto = mapper.Delete("EliminarCliente", idCliente);
            }
            catch(Exception e)
            {
                log.Error("ERROR --------- " + e.StackTrace);
            }
        }

        public long InsertarMovimiento(Movimiento movimiento)
        {
            long insertado = -1;
            
            try
            {
                Object objeto = mapper.Insert("InsertarMovimiento", movimiento);

                if (objeto != null)
                {
                    insertado = (long)objeto;
                    objeto = null;
                }
            }
            catch (Exception e)
            {
                log.Error("ERROR --------- " + e.StackTrace);
            }
            return insertado;
        }

        public void BorrarContrato(string idContrato)
        {
            try
            {
                Object objeto = mapper.Delete("EliminarContrato", idContrato);
            }
            catch (Exception e)
            {
                log.Error("ERROR --------- " + e.StackTrace);
            }
        }

        public string ObtenerCliente(string idContrato)
        {
            return mapper.QueryForObject<System.String>("ObtenerCliente", idContrato);
        }

        public long InsertarAmortizacion(Pago pago)
        {
            long insertado = -1;

            try
            {
                Object objeto = mapper.Insert("InsertarAmortizacion", pago);

                if (objeto != null)
                {
                    insertado = (long)objeto;
                    objeto = null;
                }
            }
            catch (Exception e)
            {
                log.Error("ERROR --------- " + e.StackTrace);
            }

            return insertado;
        }

        public long InsertarGrupo(Dictionary<string, object> parametros)
        {
            long insertado = -1;

            Object objeto = mapper.Insert("InsertarGrupo", parametros);

            if (objeto != null)
            {
                insertado = (long)objeto;
                objeto = null;
            }

            return insertado;
        }

        public string ExisteGrupo(string idGrupo)
        {
            return mapper.QueryForObject<System.String>("ExisteGrupo", idGrupo);
        }

        public void Commit()
        {
            if (mapper != null)
            {
                mapper.CommitTransaction();
            }
        }

        public void RollBack()
        {
            if (mapper != null)
            {
                mapper.RollBackTransaction();
            }
        }

        public void BeginTransaction()
        {
            mapper.BeginTransaction(true);
        }
    }
}
