using Entidades;
using MambuWebHook.Filters;
using Negocio;
using Negocio.Globales;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

                //Consulta para validar si el contrato ya existe.
                //Ya que la petición del webhook ha estado enviando valores duplicados
                string existe = OperacionesBD.ValidaExisteContrato(parametros);

                //Una vez que se valide que el contrato que esta por crearse no existe
                //se procede a obtener informacion de mambu para posteriormente agregar
                //la informacion concentrada en la BD de VFMéxico
                if(existe.Equals("0"))
                {
                    //Obtenemos las transacciones del contrato
                    List<Loan> loans = Operaciones.ObtenerCuentasPrestamo(ConstantesMambu.ESTATUS_ACTIVO, null, null, null, DateTime.Now.ToString("yyyy-MM-dd"), ConstantesMambu.OPERADOR_ON);

                    long contador = 0;

                    if (loans != null && loans.Count > 0)
                    {
                        Dictionary<string, string> datos = new Dictionary<string, string>();

                        foreach(var loan in loans)
                        {
                            Debug.Print("Contrato Nuevo : " + contador.ToString() + " de " + loans.Count().ToString());

                            int numeroPago = 1;
                            string idCredito = Operaciones.ObtenerCampoPersonalizadoContrato(loan.id, ConstantesMambu.KEY_CAMPO_ID_CREDITO, string.Empty);

                            Dictionary<string, object> parametrosExisteCredito = new Dictionary<string, object>();
                            parametros.Add("idCredito", idCredito);

                            // se valida que ya exista el crédito 
                            existe = string.Empty;
                            existe = Negocio.OperacionesBD.ExisteCredito(parametrosExisteCredito);

                            //Creamos instancias de los objetos que se obtendra información
                            Credito credito = new Credito();
                            Cliente cliente = new Cliente();
                            Contrato contratoInsertar = new Contrato();

                            string keyGrupo = string.Empty;

                            credito.diasVencidos = 0;
                            credito.idCredito = idCredito;

                            //Obtenemos las amortizaciones de contrato
                            List<Repayment> amortizaciones = Operaciones.ObtenerAmortizaciones(loan.id);
                        }
                    }
                    //List<Transaccion> transacciones =  Operaciones.ObtenerTransacciones(contrato.idContrato);
                    //TODO: Insertar el contrato en la BD
                }
            }
            return new HttpStatusCodeResult(200);
        }
    }
}