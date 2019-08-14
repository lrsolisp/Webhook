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
                    List<Loan> loans = Operaciones.ObtenerCuentasPrestamo(contrato.idContrato);

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
                            parametrosExisteCredito.Add("idCredito", idCredito);

                            // se valida que ya exista el crédito 
                            existe = string.Empty;
                            existe = OperacionesBD.ExisteCredito(parametrosExisteCredito);

                            //Creamos instancias de los objetos que se obtendra información
                            Credito credito = new Credito();
                            Cliente cliente = new Cliente();
                            Contrato contratoInsertar = new Contrato();

                            string keyGrupo = string.Empty;

                            credito.diasVencidos = 0;
                            credito.idCredito = idCredito;

                            //Obtenemos las amortizaciones de contrato
                            List<Repayment> amortizaciones = Operaciones.ObtenerAmortizaciones(loan.id);


                            // datos de Sucursal
                            datos = Operaciones.ObtenerDatosSucursal(loan.assignedBranchKey);
                            credito.idSucursal = datos.Where(z => z.Key.Equals("idSucursal")).FirstOrDefault().Value.ToString();
                            credito.nombreSucursal = datos.Where(z => z.Key.Equals("nombreSucursal")).FirstOrDefault().Value.ToString();

                            //Se obtiene el cliente y su informacion asociada
                            datos = Operaciones.ObtenerDatosCliente(loan.accountHolderKey);

                            try
                            {
                                cliente.idCliente = datos.Where(z => z.Key.Equals("idCliente")).FirstOrDefault().Value.ToString();
                                cliente.nombre = datos.Where(z => z.Key.Equals("nombreCliente")).FirstOrDefault().Value.ToString();
                                cliente.apellidoPaterno = datos.Where(z => z.Key.Equals("paternoCliente")).FirstOrDefault().Value.ToString();
                                cliente.apellidoMaterno = datos.Where(z => z.Key.Equals("maternoCliente")).FirstOrDefault().Value.ToString();

                                credito.keyGrupo = datos.Where(z => z.Key.Equals("keyGrupo")).FirstOrDefault().Value.ToString();
                            }
                            catch
                            {
                                credito.keyGrupo = loan.accountHolderKey;
                            }

                            credito.idCliente = cliente.idCliente;


                            // datos del Producto
                            datos = Operaciones.ObtenerDatosProducto(loan.productTypeKey);
                            credito.nombreProducto = datos.Where(z => z.Key.Equals("nombreProducto")).FirstOrDefault().Value.ToString();
                            credito.keyProducto = loan.productTypeKey;

                            // si no es individual entonces va a buscar los datos del Grupo
                            if (!loan.accountHolderKey.Equals(credito.keyGrupo))
                            {
                                Dictionary<string, string> datosGrupo = Operaciones.ObtenerDatosGrupo(credito.keyGrupo);

                                credito.idGrupo = datosGrupo.Where(z => z.Key.Equals("idGrupo")).FirstOrDefault().Value.ToString();
                                credito.nombreGrupo = datosGrupo.Where(z => z.Key.Equals("nombreGrupo")).FirstOrDefault().Value.ToString();
                            }
                            else
                            {
                                credito.idGrupo = cliente.idCliente;
                                credito.nombreGrupo = cliente.nombre + " " + cliente.apellidoPaterno + " " + cliente.apellidoMaterno;
                            }

                            credito.metodologia = Negocio.Operaciones.ObtenerCampoPersonalizadoContrato(loan.id, Negocio.Globales.ConstantesMambu.ID_CAMPO_METODOLOGIA, string.Empty);


                            // la última fecha de las amortizaciones es la Fecha Esperada de Liquidación
                            credito.fechaEsperadaLiquidacion = DateTime.Parse(amortizaciones.LastOrDefault().dueDate);
                            credito.fechaContrato = DateTime.Parse(loan.fechaDeCreacion);
                            credito.fechaDesembolso = DateTime.Parse(loan.disbursementDetails.expectedDisbursementDate);
                            credito.fechaBaja = DateTime.Parse("01/01/1900");
                            credito.fechaLiquidacion = DateTime.Parse("01/01/1900");

                            credito.frecuenciaPagoNumero = loan.repaymentPeriodCount;
                            credito.frecuenciaPagoUnidad = loan.repaymentPeriodUnit;

                            credito.numeroPagos = amortizaciones.Count();

                            credito.tasaAnual = loan.interestRate;
                            credito.tasaMensual = credito.tasaAnual / 12;
                            credito.tasaDiaria = credito.tasaAnual / 360;

                            credito.estatus = loan.accountState;


                            // datos del contrato
                            contratoInsertar.idCliente = cliente.idCliente;
                            contratoInsertar.idContrato = loan.id;
                            contratoInsertar.idCredito = idCredito;
                            contratoInsertar.montoCapital = loan.loanAmount;
                            contratoInsertar.montoComisiones = loan.feesPaid;
                            contratoInsertar.saldo = loan.principalBalance;
                            contratoInsertar.capitalPagado = loan.principalPaid;
                            contratoInsertar.interesPagado = loan.interestPaid;
                            contratoInsertar.estatus = loan.accountState;
                            contratoInsertar.subEstatus = loan.accountSubState;


                            parametros.Clear();
                            parametros.Add("idCredito", idCredito);

                            //Validamos que no exista el crédito
                            if(existe.Equals("0"))
                            {
                                OperacionesBD.InsertarCredito(credito);
                            }

                            foreach (Entidades.Repayment amortizacion in amortizaciones)
                            {
                                Pago pago = new Pago();

                                pago.numeroCuota = numeroPago;
                                pago.idContrato = loan.id;
                                pago.estatus = amortizacion.state;
                                pago.fechaPago = DateTime.Parse(amortizacion.dueDate);
                                pago.fechaPagado = DateTime.Parse(amortizacion.repaidDate == null ? amortizacion.repaidDate = "01/01/1900" : amortizacion.repaidDate);
                                pago.capitalEsperado = decimal.Parse(amortizacion.principalDue);
                                pago.interesEsperado = decimal.Parse(amortizacion.interestDue);
                                pago.capitalPagado = decimal.Parse(amortizacion.principalPaid);
                                pago.interesPagado = decimal.Parse(amortizacion.interestPaid);


                                // inserta las Amortizaciones
                                OperacionesBD.InsertarAmortizaciones(pago);

                                numeroPago += 1;
                            }

                            OperacionesBD.InsertarCliente(cliente);

                            OperacionesBD.InsertarContrato(contratoInsertar);

                            List<Transaccion> transacciones = Operaciones.ObtenerTransacciones(Constantes.TRANSACTIONS_TYPE_DISBURSMENT, loan.encodedKey).ToList();

                            transacciones.AddRange(Negocio.Operaciones.ObtenerTransacciones(Constantes.TRANSACTIONS_TYPE_REPAYMENT, loan.encodedKey).ToList());

                            foreach (Entidades.Transaccion transaccion in transacciones)
                            {
                                Movimiento movimiento = new Movimiento();

                                movimiento.codigo = transaccion.type.Equals(Negocio.Globales.Constantes.TRANSACTIONS_TYPE_REPAYMENT) ? Negocio.Globales.Constantes.MOVIMIENTO_PAGO : Negocio.Globales.Constantes.MOVIMIENTO_DESEMBOLSO;
                                movimiento.fechaMovimiento = DateTime.Parse(transaccion.creationDate);
                                movimiento.fechaValor = DateTime.Parse(transaccion.entryDate);
                                movimiento.idContrato = loan.id;
                                movimiento.idTransaccion = transaccion.transactionId;
                                movimiento.montoCapital = transaccion.principalPaid;
                                movimiento.montoInteres = transaccion.interestPaid;
                                movimiento.montoTotal = transaccion.amount;
                                movimiento.saldo = transaccion.principalBalance;

                                OperacionesBD.InsertarMovimiento(movimiento);
                            }

                            transacciones.Clear();
                            transacciones = null;

                            amortizaciones.Clear();
                            amortizaciones = null;

                            datos.Clear();
                            datos = null;
                            datos = new Dictionary<string, string>();

                            cliente = null;
                            credito = null;
                            contrato = null;
                        }
                    }
                }
            }
            return new HttpStatusCodeResult(200);
        }
    }
}