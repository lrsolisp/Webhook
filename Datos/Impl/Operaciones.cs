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
            return mapper.QueryForObject<String>("ExisteContrato", parametros);
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
