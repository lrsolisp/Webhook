﻿using Entidades;
using Negocio.Globales;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Negocio
{
    public class Operaciones
    {
        /// <summary>
        /// Método para obtener las cuentas de prestamo (Contratos) con base al estatus
        /// </summary>
        /// <param name="idContrato"></param>
        /// <returns></returns>
        public static List<Loan> ObtenerCuentasPrestamo(string idContrato)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            int offset = 0;
            bool acaba = false;

            List<FilterConstraints> filterConstraints = new List<FilterConstraints>();

            FilterConstraints filterConstraint = new FilterConstraints();

            #region Filtros
           
            filterConstraint = null;
            filterConstraint = new Entidades.FilterConstraints();
            filterConstraint.filterSelection = Negocio.Globales.Constantes.FILTRO_CAMPO_ACCOUNT_ID;
            filterConstraint.filterElement = Negocio.Globales.Constantes.OPERADOR_EQUALS;
            filterConstraint.value = idContrato;
            filterConstraint.dataItemType = Negocio.Globales.Constantes.DATA_ITEM_TYPE_LOANS;
            filterConstraints.Add(filterConstraint);

            #endregion

            Filtros filtros = new Filtros();
            filtros.filterConstraints = filterConstraints;

            string json = JsonConvert.SerializeObject(filtros);

            List<Loan> contratos = null;
            List<Loan> acumuladoContratos = new List<Loan>();

            while (!acaba)
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["ambiente"] + "/loans/search?" + Constantes.LIMITE_CONSULTA + "&" + Constantes.OFFSET_CONSULTA + offset);

                req.Method = Constantes.METODO_POST;
                req.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(ConfigurationManager.AppSettings["user"] + ":" + ConfigurationManager.AppSettings["psw"]));
                req.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:31.0) Gecko/20100101 Firefox/31.0";
                req.ContentType = "application/json";

                JavaScriptSerializer js = new JavaScriptSerializer();
                js.MaxJsonLength = Int32.MaxValue;

                using (var streamWriter = new StreamWriter(req.GetRequestStream()))
                {
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)req.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var objText = streamReader.ReadToEnd();
                    contratos = js.Deserialize<List<Loan>>(objText);

                    if (contratos.Count <= 0)
                    {
                        acaba = true;
                    }
                    else
                    {
                        offset += 1000;
                        acumuladoContratos.AddRange(contratos);
                    }
                    contratos = null;
                }
            }
            return acumuladoContratos;
        }

        /// <summary>
        /// Método para obtener las cuentas de prestamo (Contratos) con base al estatus
        /// </summary>
        /// <param name="idContrato"></param>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public static List<Loan> ObtenerCuentasPrestamo(string idContrato, string estatus)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            int offset = 0;
            bool acaba = false;

            List<FilterConstraints> filterConstraints = new List<FilterConstraints>();

            FilterConstraints filterConstraint = new FilterConstraints();

            #region Filtros

            if (estatus != null)
            {
                filterConstraint = null;
                filterConstraint = new Entidades.FilterConstraints();
                filterConstraint.filterSelection = Negocio.Globales.ConstantesMambu.FILTRO_CAMPO_ESTATUS_CONTRATO;
                filterConstraint.filterElement = Negocio.Globales.ConstantesMambu.OPERADOR_EQUALS;
                filterConstraint.dataItemType = Negocio.Globales.ConstantesMambu.DATA_ITEM_TYPE_LOANS;
                filterConstraint.value = estatus;
                filterConstraints.Add(filterConstraint);
            }

            filterConstraint = null;
            filterConstraint = new Entidades.FilterConstraints();
            filterConstraint.filterSelection = Negocio.Globales.Constantes.FILTRO_CAMPO_ACCOUNT_ID;
            filterConstraint.filterElement = Negocio.Globales.Constantes.OPERADOR_EQUALS;
            filterConstraint.value = idContrato;
            filterConstraint.dataItemType = Negocio.Globales.Constantes.DATA_ITEM_TYPE_LOANS;
            filterConstraints.Add(filterConstraint);

            #endregion

            Filtros filtros = new Filtros();
            filtros.filterConstraints = filterConstraints;

            string json = JsonConvert.SerializeObject(filtros);

            List<Loan> contratos = null;
            List<Loan> acumuladoContratos = new List<Loan>();

            while(!acaba)
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["ambiente"] + "/loans/search?" + Constantes.LIMITE_CONSULTA + "&" + Constantes.OFFSET_CONSULTA + offset);

                req.Method = Constantes.METODO_POST;
                req.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(ConfigurationManager.AppSettings["user"] + ":" + ConfigurationManager.AppSettings["psw"]));
                req.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:31.0) Gecko/20100101 Firefox/31.0";
                req.ContentType = "application/json";

                JavaScriptSerializer js = new JavaScriptSerializer();
                js.MaxJsonLength = Int32.MaxValue;

                using(var streamWriter = new StreamWriter(req.GetRequestStream()))
                {
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)req.GetResponse();
                using(var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var objText = streamReader.ReadToEnd();
                    contratos = js.Deserialize<List<Loan>>(objText);

                    if(contratos.Count <= 0)
                    {
                        acaba = true;
                    }
                    else
                    {
                        offset += 1000;
                        acumuladoContratos.AddRange(contratos);
                    }
                    contratos = null;
                }
            }
            return acumuladoContratos;
        }

        /// <summary>
        /// Método para obtener las amortizaciones por contrato
        /// </summary>
        /// <param name="idContrato"></param>
        /// <returns></returns>
        public static List<Repayment> ObtenerAmortizaciones(string idContrato)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            WebRequest req = WebRequest.Create(ConfigurationManager.AppSettings["ambiente"] + "/loans/" + idContrato + "/repayments?" + Constantes.LIMITE_CONSULTA);

            req.Method = Constantes.METODO_GET;
            req.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(ConfigurationManager.AppSettings["user"] + ":" + ConfigurationManager.AppSettings["psw"]));


            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = Int32.MaxValue;

            var httpResponse = (HttpWebResponse)req.GetResponse();

            List<Repayment> amortizaciones = new List<Repayment>();

            try
            {
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var objText = streamReader.ReadToEnd();
                    amortizaciones = JsonConvert.DeserializeObject<List<Entidades.Repayment>>(objText);
                }
            }
            catch (Exception exception)
            {

            }

            return amortizaciones;
        }


        /// <summary>
        /// Método para obtener un campo personalizado de un contrato
        /// </summary>
        /// <param name="idContrato"></param>
        /// <param name="keyCampoPersonalizado"></param>
        /// <param name="tipoDato"></param>
        /// <returns></returns>
        public static string ObtenerCampoPersonalizadoContrato(string idContrato, string keyCampoPersonalizado, string tipoDato)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            string valorResultado = string.Empty;

            WebRequest req = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["ambiente"] + "/loans/" + idContrato + "/custominformation/" + keyCampoPersonalizado);

            req.Method = Constantes.METODO_GET;
            req.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(ConfigurationManager.AppSettings["user"] + ":" + ConfigurationManager.AppSettings["psw"]));

            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = Int32.MaxValue;

            var httpResponse = (HttpWebResponse)req.GetResponse();

            List<CustomFieldValue> campoPersonalizado = new List<CustomFieldValue>();

            try
            {
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var objText = streamReader.ReadToEnd();

                    campoPersonalizado = JsonConvert.DeserializeObject<List<Entidades.CustomFieldValue>>(objText);

                    if (campoPersonalizado != null)
                    {
                        if (tipoDato.Equals(Negocio.Globales.Constantes.OBJETO_USUARIO))
                        {
                            valorResultado = campoPersonalizado[0].linkedEntityKeyValue;
                        }
                        else
                        {
                            valorResultado = campoPersonalizado[0].value;
                        }
                    }
                }
            }
            catch (Exception exception)
            {

            }

            return valorResultado;
        }

        public static List<Transaccion> ObtenerTransacciones(string idContrato)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://visionfundmexico.sandbox.mambu.com/api/loans/" + idContrato + "/transactions" + Constantes.LIMITE_CONSULTA);

            req.Method = Constantes.METODO_GET;
            req.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(ConfigurationManager.AppSettings["user"] + ":" + ConfigurationManager.AppSettings["psw"]));
            req.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:31.0) Gecko/20100101 Firefox/31.0";

            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = Int32.MaxValue;

            var httpResponse = (HttpWebResponse)req.GetResponse();

            List<Entidades.Transaccion> transacciones = new List<Entidades.Transaccion>();

            try
            {
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var objText = streamReader.ReadToEnd();
                    transacciones = JsonConvert.DeserializeObject<List<Entidades.Transaccion>>(objText);
                }
            }
            catch (Exception exception)
            {

            }

            return transacciones;
        }
    }
}
