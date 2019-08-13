using Entidades;
using MambuWebHook.Filters;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MambuWebHook.Controllers
{
    public class MambuController : Controller
    {
        [BasicAuthentication]
        public ActionResult Create()
        {
            ContratoMambu contrato = new ContratoMambu();
            System.IO.StreamReader reader = new System.IO.StreamReader(HttpContext.Request.InputStream);
            string rawSendGridJSON = reader.ReadToEnd();
            contrato = new JavaScriptSerializer().Deserialize<ContratoMambu>(rawSendGridJSON);

            if (contrato != null)
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("idContrato", contrato.idContrato);
                //TODO: Crear consulta para validar si el contrato ya existe.
                string existe = OperacionesBD.ValidaExisteContrato(parametros);

                if(existe.Equals("0"))
                {
                    //TODO: Traer los datos necesarios del API de MAMBU
                    //TODO: Insertar el contrato en la BD
                }
            }
            return new HttpStatusCodeResult(200);
        }
    }
}