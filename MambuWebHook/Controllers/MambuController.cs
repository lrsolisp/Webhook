using Entidades;
using MambuWebHook.Filters;
using Negocio;
using Negocio.Globales;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MambuWebHook.Controllers
{
    public class MambuController : Controller
    {

        [BasicAuthentication]
        public ActionResult Create()
        {
            System.IO.StreamReader reader = new System.IO.StreamReader(HttpContext.Request.InputStream);
            string rawSendGridJSON = reader.ReadToEnd();

            return new HttpStatusCodeResult(200);
        }

        /// <summary>
        /// Método para obtener la información
        /// faltante e insertar el nuevo contrato, credito
        /// amortizaciones y movimientos
        /// </summary>
        /// <returns></returns>
        [BasicAuthentication]
        public ActionResult NuevoContrato()
        {

            ContratoWebHookMambu contratoWebHook = new ContratoWebHookMambu();
            System.IO.StreamReader reader = new System.IO.StreamReader(HttpContext.Request.InputStream);
            string rawSendGridJSON = reader.ReadToEnd();
            contratoWebHook = new JavaScriptSerializer().Deserialize<ContratoWebHookMambu>(rawSendGridJSON);

            if (contratoWebHook != null)
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("idContrato", contratoWebHook.IdContrato);

                //Consulta para validar si el contrato ya existe.
                //Ya que la petición del webhook ha estado enviando valores duplicados
                string existe = OperacionesBD.ValidaExisteContrato(parametros);
                string existeCadena = (existe == "1" ? " existe" : " no existe");

                //Una vez que se valide que el contrato que esta por crearse no existe
                //se procede a obtener informacion de mambu para posteriormente agregar
                //la informacion concentrada en la BD de VFMéxico
                if (existe.Equals("0"))
                {
                    //Obtenemos las transacciones del contrato
                    List<Loan> loans = Operaciones.ObtenerCuentasPrestamo(contratoWebHook.IdContrato);

                    long contador = 0;

                    if (loans != null && loans.Count > 0)
                    {
                        Dictionary<string, string> datos = new Dictionary<string, string>();

                        foreach (var loan in loans)
                        {


                            int numeroPago = 1;

                            Dictionary<string, object> parametrosExisteCredito = new Dictionary<string, object>();
                            parametrosExisteCredito.Add("idCredito", contratoWebHook.IdCredito);

                            // se valida que ya exista el crédito 
                            string existeCredito = string.Empty;
                            existeCredito = OperacionesBD.ExisteCredito(parametrosExisteCredito);


                            //Creamos instancias de los objetos que se obtendra información
                            Credito credito = new Credito();
                            Cliente cliente = new Cliente();
                            Contrato contratoInsertar = new Contrato();

                            string keyGrupo = string.Empty;

                            credito.diasVencidos = 0;
                            credito.idCredito = contratoWebHook.IdCredito;

                            //Obtenemos las amortizaciones de contrato
                            List<Repayment> amortizaciones = Operaciones.ObtenerAmortizaciones(loan.id);

                            // datos de Sucursal
                            //Validar que el id de sucursal de contratowebhook sea el mismo
                            datos = Operaciones.ObtenerDatosSucursal(loan.assignedBranchKey);

                            credito.idSucursal = datos.Where(z => z.Key.Equals("idSucursal")).FirstOrDefault().Value.ToString();
                            credito.nombreSucursal = datos.Where(z => z.Key.Equals("nombreSucursal")).FirstOrDefault().Value.ToString();

                            Dictionary<string, string> datosCliente = new Dictionary<string, string>();
                            //Se obtiene el cliente y su informacion faltante
                            datosCliente = Operaciones.ObtenerDatosCliente(loan.accountHolderKey);

                            try
                            {
                                cliente.nombre = datosCliente.Where(z => z.Key.Equals("nombreCliente")).FirstOrDefault().Value.ToString();
                                cliente.apellidoPaterno = datosCliente.Where(z => z.Key.Equals("paternoCliente")).FirstOrDefault().Value.ToString();
                                cliente.apellidoMaterno = datosCliente.Where(z => z.Key.Equals("maternoCliente")).FirstOrDefault().Value.ToString();

                                //Agregamos al contrato obtenido desde el WebHook los campos que hacen falta
                                contratoWebHook.Cliente.nombre = cliente.nombre;
                                contratoWebHook.Cliente.apellidoPaterno = cliente.apellidoPaterno;
                                contratoWebHook.Cliente.apellidoMaterno = cliente.apellidoMaterno;
                                contratoWebHook.Cliente.fechaNacimiento = Convert.ToDateTime(datosCliente.Where(z => z.Key.Equals("fechaNacimiento")).FirstOrDefault().Value.ToString());
                                contratoWebHook.Cliente.RFC = datosCliente.Where(z => z.Key.Equals("rfc")).FirstOrDefault().Value.ToString();
                                contratoWebHook.Cliente.CURP = datosCliente.Where(z => z.Key.Equals("curp")).FirstOrDefault().Value.ToString();
                                contratoWebHook.Cliente.sexo = datosCliente.Where(z => z.Key.Equals("sexo")).FirstOrDefault().Value.ToString();
                                contratoWebHook.Cliente.coloniaPoblacion = datosCliente.Where(z => z.Key.Equals("coloniaPoblacion")).FirstOrDefault().Value.ToString();
                                contratoWebHook.Cliente.numeroTelefonico = datosCliente.Where(z => z.Key.Equals("numeroTelefonico")).FirstOrDefault().Value.ToString();

                                credito.keyGrupo = datos.Where(z => z.Key.Equals("keyGrupo")).FirstOrDefault().Value.ToString();
                            }
                            catch
                            {
                                credito.keyGrupo = loan.accountHolderKey;
                            }

                            credito.idCliente = contratoWebHook.Cliente.idCliente;

                            Cliente clienteCamposPersonalizados = new Cliente();

                            clienteCamposPersonalizados = Operaciones.ObtenerDatosClienteActualiza(loan.accountHolderKey);

                            //Obtenemos los datos de la identificacion capturada
                            contratoWebHook.Cliente.tipoDocumento = clienteCamposPersonalizados.idDocuments[0].issuingAuthority;
                            contratoWebHook.Cliente.vigenciaDocumento = clienteCamposPersonalizados.idDocuments[0].validUntil;

                            var customFields = clienteCamposPersonalizados.customInformation.Where(x => x.customFieldID.Equals("Edad_Clientes") && Convert.ToInt32(x.value) < 19).ToList().Count.ToString();

                            if (customFields == null)
                            {
                                contratoWebHook.Cliente.numeroDependientes = "0";
                            }
                            else
                            {
                                contratoWebHook.Cliente.numeroDependientes = customFields;
                            }

                            // datos del Producto                            
                            credito.nombreProducto = contratoWebHook.NombreDelProducto;
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
                                credito.idGrupo = contratoWebHook.Cliente.idCliente;
                                credito.nombreGrupo = cliente.nombre + " " + cliente.apellidoPaterno + " " + cliente.apellidoMaterno;
                            }

                            credito.metodologia = contratoWebHook.Metodologia;

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

                            //Obtener los datos de oficial de credito
                            List<CustomFieldValue> campo = Operaciones.ObtenerCampoPersonalizadoContrato(contratoWebHook.IdContrato, "Oficial_de_Credito_Asignado_Cuen", string.Empty);

                            var userKey = campo.FirstOrDefault(x => x.customFieldKey == ConstantesMambu.KEY_CAMPO_OFICIAL_CREDITO).linkedEntityKeyValue;

                            Usuario userMambu = Operaciones.ObtenerUsuarioMambu(userKey);


                            // datos del contrato
                            contratoInsertar.idCliente = contratoWebHook.Cliente.idCliente;
                            contratoInsertar.idContrato = loan.id;
                            contratoInsertar.idCredito = contratoWebHook.IdCredito;
                            contratoInsertar.montoCapital = loan.loanAmount;
                            contratoInsertar.montoComisiones = loan.feesPaid;
                            contratoInsertar.saldo = loan.principalBalance;
                            contratoInsertar.capitalPagado = loan.principalPaid;
                            contratoInsertar.interesPagado = loan.interestPaid;
                            contratoInsertar.estatus = loan.accountState;
                            contratoInsertar.subEstatus = loan.accountSubState;
                            contratoInsertar.beneficiario = contratoWebHook.Beneficiario;
                            contratoInsertar.pda = contratoWebHook.PDA;
                            contratoInsertar.sector = contratoWebHook.Sector;
                            contratoInsertar.subSector = contratoWebHook.SubSector;
                            contratoInsertar.propositoCredito = contratoWebHook.PropositoCredito;
                            contratoInsertar.empleosCreados = contratoWebHook.EmpleadosCreadosTC.ToString();
                            contratoInsertar.empleosSostenidos = contratoWebHook.EmpleadosSostenidosTC.ToString();
                            contratoInsertar.fuenteFondeo = contratoWebHook.FuenteFondeo;
                            contratoInsertar.valorBien = contratoWebHook.ValorBien;
                            contratoInsertar.formaDesembolso = contratoWebHook.FormaDesembolso;
                            contratoInsertar.nombreOficialCredito = contratoWebHook.NombreOficialCredito;
                            contratoInsertar.numeroOficialCredito = userMambu.id;
                            contratoInsertar.fechaCierre = Convert.ToDateTime(loan.closeDate.Year == 1 ? "01/01/1900" : loan.closeDate.ToString());

                            //Validamos que no exista el crédito
                            if (existeCredito.Equals("0"))
                            {
                                OperacionesBD.InsertarCredito(credito);
                            }
                            else
                            {

                            }

                            foreach (Repayment amortizacion in amortizaciones)
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


                            //Validar que el cliente exista en la BD, en caso de que ya se encuentre no se inserta
                            Dictionary<string, object> parametrosCliente = new Dictionary<string, object>();
                            parametrosCliente.Add("idCliente", contratoWebHook.Cliente.idCliente);

                            string existeCliente = OperacionesBD.ExisteCliente(parametrosCliente);

                            if (existeCliente.Equals("0"))
                            {
                                OperacionesBD.InsertarCliente(contratoWebHook.Cliente);

                            }
                            else
                            {
                            }

                            OperacionesBD.InsertarContrato(contratoInsertar);

                            List<Transaccion> transacciones = Operaciones.ObtenerTransacciones(Constantes.TRANSACTIONS_TYPE_DISBURSMENT, loan.encodedKey).ToList();

                            transacciones.AddRange(Operaciones.ObtenerTransacciones(Constantes.TRANSACTIONS_TYPE_REPAYMENT, loan.encodedKey).ToList());

                            foreach (Transaccion transaccion in transacciones)
                            {
                                Movimiento movimiento = new Movimiento();

                                movimiento.codigo = transaccion.type.Equals(Constantes.TRANSACTIONS_TYPE_REPAYMENT) ? Constantes.MOVIMIENTO_PAGO : Constantes.MOVIMIENTO_DESEMBOLSO;
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

                            List<AnidaFee> comitions = loan.disbursementDetails.fees;

                            //Validacion para comision 
                            foreach (var comition in comitions)
                            {
                                if (comition.fee.name.ToUpper().Contains("COMISIÓN") || comition.fee.name.ToUpper().Contains("COMISION"))
                                {
                                    Movimiento movimiento = new Movimiento();

                                    movimiento.codigo = "COMISIÓN";
                                    movimiento.fechaMovimiento = Convert.ToDateTime(comition.fee.creationDate);
                                    movimiento.fechaValor = Convert.ToDateTime(loan.disbursementDetails.disbursementDate);
                                    movimiento.idContrato = loan.id;
                                    movimiento.idTransaccion = -1;
                                    movimiento.montoCapital = 0;
                                    movimiento.montoInteres = 0;
                                    movimiento.montoTotal = Convert.ToDecimal(comition.fee.amount);
                                    movimiento.saldo = 0;

                                    OperacionesBD.InsertarMovimiento(movimiento);
                                }
                                else if (comition.fee.name.ToUpper().Contains("GASTO"))
                                {
                                    Movimiento movimiento = new Movimiento();

                                    movimiento.codigo = "SEGURO";
                                    movimiento.fechaMovimiento = Convert.ToDateTime(comition.fee.creationDate);
                                    movimiento.fechaValor = Convert.ToDateTime(loan.disbursementDetails.disbursementDate);
                                    movimiento.idContrato = loan.id;
                                    movimiento.idTransaccion = -1;
                                    movimiento.montoCapital = 0;
                                    movimiento.montoInteres = 0;
                                    movimiento.montoTotal = Convert.ToDecimal(comition.fee.amount);
                                    movimiento.saldo = 0;

                                    OperacionesBD.InsertarMovimiento(movimiento);
                                }
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
                            contratoWebHook = null;
                        }
                    }
                    else
                    {
                    }
                }
                else
                {
                }
            }
            else
            {
            }
            return new HttpStatusCodeResult(200);
        }

        /// <summary>
        /// Método para obtener pagos de mambu
        /// y aplicarlos en devengados
        /// </summary>
        /// <returns></returns>
        [BasicAuthentication]
        public ActionResult AplicarPagos()
        {
            //Se inicia el contrato pero unicamente se tendra la propiedad de idContrato
            //Obtendremos las demas propiedades para actualizarlas
            ContratoWebHookMambu contratoWebHook = new ContratoWebHookMambu();
            System.IO.StreamReader reader = new System.IO.StreamReader(HttpContext.Request.InputStream);
            string rawSendGridJSON = reader.ReadToEnd();

            contratoWebHook = new JavaScriptSerializer().Deserialize<ContratoWebHookMambu>(rawSendGridJSON);
            long contador = 0;
            if (contratoWebHook != null)
            {
                ////Obtenemos el contrato(s) asociado
                List<Loan> loans = Operaciones.ObtenerCuentasPrestamo(contratoWebHook.IdContrato);

                if (loans.Count > 0)
                {

                    List<Transaccion> transacciones = Operaciones.ObtenerTransacciones(Constantes.TRANSACTIONS_TYPE_REPAYMENT, loans.FirstOrDefault().encodedKey).ToList();
                    foreach (var transaccion in transacciones)
                    {

                        int numeroPago = 1;
                        string existe = OperacionesBD.ExisteTransaccion(transaccion.transactionId);

                        Loan contrato = Operaciones.ObtenerCuentaPrestamo(transaccion.parentAccountKey);

                        Movimiento movimiento = new Movimiento();

                        movimiento.codigo = Constantes.MOVIMIENTO_PAGO;
                        movimiento.fechaMovimiento = DateTime.Parse(transaccion.creationDate);
                        movimiento.fechaValor = DateTime.Parse(transaccion.entryDate);
                        movimiento.idContrato = contrato.id;
                        movimiento.idTransaccion = transaccion.transactionId;
                        movimiento.montoCapital = transaccion.principalPaid;
                        movimiento.montoInteres = transaccion.interestPaid;
                        movimiento.montoTotal = transaccion.amount;
                        movimiento.saldo = transaccion.principalBalance;

                        if (existe.Equals("0"))
                        {
                            long insertado = OperacionesBD.InsertarMovimiento(movimiento);

                            // amortizaciones del contrato
                            OperacionesBD.BorrarAmortizacionesContrato(contrato.id);

                            List<Repayment> amortizaciones = Operaciones.ObtenerAmortizaciones(contrato.id).OrderBy(x => x.dueDate).ToList();

                            // inserta el calendario de pagos
                            foreach (Repayment amortizacion in amortizaciones)
                            {
                                Pago pago = new Pago();

                                pago.numeroCuota = numeroPago;
                                pago.idContrato = contrato.id;
                                pago.estatus = amortizacion.state;
                                pago.fechaPago = DateTime.Parse(amortizacion.dueDate);
                                pago.fechaPagado = DateTime.Parse(amortizacion.repaidDate == null ? amortizacion.repaidDate = "01/01/1900" : amortizacion.repaidDate);
                                pago.capitalEsperado = decimal.Parse(amortizacion.principalDue);
                                pago.interesEsperado = decimal.Parse(amortizacion.interestDue);
                                pago.capitalPagado = decimal.Parse(amortizacion.principalPaid);
                                pago.interesPagado = decimal.Parse(amortizacion.interestPaid);

                                // inserta las Amortizaciones
                                OperacionesBD.InsertarAmortizaciones(pago);

                                pago = null;

                                numeroPago += 1;

                            }
                            // actualiza los datos del contrato
                            Dictionary<string, object> parametros = new Dictionary<string, object>();

                            parametros.Add("saldo", contrato.principalBalance);
                            parametros.Add("capitalPagado", contrato.principalPaid.ToString());
                            parametros.Add("interesPagado", contrato.interestPaid.ToString());
                            parametros.Add("estatus", contrato.accountState);
                            parametros.Add("idContrato", contrato.id);

                            OperacionesBD.ActualizarContrato(parametros);

                        }

                        movimiento = null;

                        contador += 1;
                    }
                    return new HttpStatusCodeResult(200);
                }
                else
                {
                    return new HttpStatusCodeResult(404);
                }
            }
            else
            {
                return new HttpStatusCodeResult(404);
            }
        }

        /// <summary>
        /// Método para obtener los pagos de mambu
        /// y se cancelan en devengados
        /// </summary>
        /// <param name="idContrato"></param>
        /// <returns></returns>
        [BasicAuthentication]
        public ActionResult CancelarPagos()
        {

            System.IO.StreamReader reader = new System.IO.StreamReader(HttpContext.Request.InputStream);
            string rawSendGridJSON = reader.ReadToEnd();

            //Convertimos a json el resultado para facilitar el acceso
            //a sus propiedades
            JObject json = JObject.Parse(rawSendGridJSON);

            string idContrato = json.Properties().FirstOrDefault().Value.ToString();

            long contador = 0;

            List<Transaccion> transaccions = Operaciones.ObtenerTransacciones(idContrato);

            var contratosAgrupados = (from x in transaccions
                                      group x by new
                                      {
                                          x.parentAccountKey
                                      } into campos
                                      select new
                                      {
                                          idContrato = campos.Key.parentAccountKey
                                      });
            foreach (var valor in contratosAgrupados)
            {
                int numeroPago = 1;

                Loan contrato = Operaciones.ObtenerCuentaPrestamo(valor.idContrato);

                OperacionesBD.BorrarMovimientosContratos(contrato.id);

                // obtiene nuevamente las transacciones del contrato
                List<Transaccion> transaccionesNuevas = Operaciones.ObtenerTransacciones(Constantes.TRANSACTIONS_TYPE_DISBURSMENT, valor.idContrato).ToList();
                transaccionesNuevas.AddRange(Operaciones.ObtenerTransacciones(Constantes.TRANSACTIONS_TYPE_REPAYMENT, valor.idContrato).ToList());

                foreach (Transaccion transaccion in transaccionesNuevas)
                {
                    Movimiento movimiento = new Movimiento();

                    movimiento.codigo = transaccion.type.Equals(Constantes.TRANSACTIONS_TYPE_REPAYMENT) ? Constantes.MOVIMIENTO_PAGO : Constantes.MOVIMIENTO_DESEMBOLSO;
                    movimiento.fechaMovimiento = DateTime.Parse(transaccion.creationDate);
                    movimiento.fechaValor = DateTime.Parse(transaccion.entryDate);
                    movimiento.idContrato = contrato.id;
                    movimiento.idTransaccion = transaccion.transactionId;
                    movimiento.montoCapital = transaccion.principalPaid;
                    movimiento.montoInteres = transaccion.interestPaid;
                    movimiento.montoTotal = transaccion.amount;
                    movimiento.saldo = transaccion.principalBalance;

                    OperacionesBD.InsertarMovimiento(movimiento);

                    movimiento = null;
                }

                // amortizaciones del contrato
                OperacionesBD.BorrarAmortizacionesContrato(contrato.id);

                List<Repayment> amortizaciones = Operaciones.ObtenerAmortizaciones(contrato.id).OrderBy(x => x.dueDate).ToList();

                // inserta el calendario de pagos
                foreach (Repayment amortizacion in amortizaciones)
                {
                    Pago pago = new Pago();

                    pago.numeroCuota = numeroPago;
                    pago.idContrato = contrato.id;
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

                // actualiza los datos del contrato
                Dictionary<string, object> parametros = new Dictionary<string, object>();

                parametros.Add("saldo", contrato.principalBalance);
                parametros.Add("capitalPagado", contrato.principalPaid.ToString());
                parametros.Add("interesPagado", contrato.interestPaid.ToString());
                parametros.Add("estatus", contrato.accountState);
                parametros.Add("idContrato", contrato.id);

                OperacionesBD.ActualizarContrato(parametros);

                contador += 1;
            }
            return new HttpStatusCodeResult(200);
        }


        /// <summary>
        /// Se usa el método una vez que el estatus de la cuenta sea cerrada
        /// y el subestado de la cuenta sea igual a retirado
        /// </summary>
        /// <param name="idContrato"></param>
        /// <returns></returns>
        [BasicAuthentication]
        public ActionResult RetirarContrato()
        {

            System.IO.StreamReader reader = new System.IO.StreamReader(HttpContext.Request.InputStream);
            string rawSendGridJSON = reader.ReadToEnd();

            //Convertimos a json el resultado para facilitar el acceso
            //a sus propiedades
            JObject json = JObject.Parse(rawSendGridJSON);

            string idContrato = json.Properties().FirstOrDefault().Value.ToString();

            // Borrar los movimientos obtenidos de mambu en la BD Devengados
            OperacionesBD.BorrarMovimientosContratos(idContrato);

            //Borrar las amortizaciones obtenidas de mambu en la BD Devengados
            OperacionesBD.BorrarAmortizacionesContrato(idContrato);

            //Obtener cliente asociado al contrato
            string idCliente = OperacionesBD.ObtenerClienteContrato(idContrato);

            //Borrar el cliente
            OperacionesBD.BorrarCliente(idCliente);

            //Borrar el contrato
            OperacionesBD.BorrarContrato(idContrato);

            return new HttpStatusCodeResult(200);
        }
    }
}