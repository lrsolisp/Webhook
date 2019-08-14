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
