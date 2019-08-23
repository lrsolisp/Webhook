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

            Object objeto = mapper.Insert("InsertarCredito", credito);

            if(objeto != null)
            {
                insertado = (long)objeto;
                objeto = null;
            }

            return insertado;
        }

        public long InsertarCliente(Cliente cliente)
        {
            long insertado = -1;

            Object objeto = mapper.Insert("InsertarCliente", cliente);

            if (objeto != null)
            {
                insertado = (long)objeto;
                objeto = null;
            }

            return insertado;
        }

        public long InsertarContrato(Contrato contratoInsertar)
        {
            long insertado = -1;

            Object objeto = mapper.Insert("InsertarContrato", contratoInsertar);

            if (objeto != null)
            {
                insertado = (long)objeto;
                objeto = null;
            }

            return insertado;
        }

        public void BorrarAmortizacionesContrato(string idContrato)
        {
            Object objeto = mapper.Delete("EliminarAmortizacionesContrato", idContrato);
        }

        public string ExisteTransaccion(long transactionId)
        {
            return mapper.QueryForObject<string>("ExisteTransaccion", transactionId);
        }

        public void BorrarMovimientosContrato(string id)
        {
            Object objeto = mapper.Delete("EliminarMovimientosContrato", id);
        }

        public void ActualizarContrato(Dictionary<string, object> parametros)
        {
            mapper.Update("ActualizarContrato", parametros);
        }

        public string ExisteCliente(Dictionary<string, object> parametros)
        {
            return mapper.QueryForObject<string>("ExisteCliente", parametros);
        }

        public void BorrarCliente(string idCliente)
        {
            Object objeto = mapper.Delete("EliminarCliente", idCliente);
        }

        public long InsertarMovimiento(Movimiento movimiento)
        {
            long insertado = -1;

            Object objeto = mapper.Insert("InsertarMovimiento", movimiento);

            if (objeto != null)
            {
                insertado = (long)objeto;
                objeto = null;
            }

            return insertado;
        }

        public void BorrarContrato(string idContrato)
        {
            Object objeto = mapper.Delete("EliminarContrato", idContrato);
        }

        public string ObtenerCliente(string idContrato)
        {
            return mapper.QueryForObject<System.String>("ObtenerCliente", idContrato);
        }

        public long InsertarAmortizacion(Pago pago)
        {
            long insertado = -1;

            Object objeto = mapper.Insert("InsertarAmortizacion", pago);

            if (objeto != null)
            {
                insertado = (long)objeto;
                objeto = null;
            }

            return insertado;
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
