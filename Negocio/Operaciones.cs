using Entidades;
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

        public static Dictionary<string, string> ObtenerDatosSucursal(string id)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls12;

            Dictionary<string, string> objeto = new Dictionary<string, string>();

            WebRequest req = WebRequest.Create(ConfigurationManager.AppSettings["ambiente"] + "/" + Negocio.Globales.Constantes.API_MAMBU_BRANCH + "/" + id + "?fulldetails=true");

            req.ContentType = "application/json; charset=utf-8";
            req.Method = Negocio.Globales.Constantes.METODO_GET;
            req.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(ConfigurationManager.AppSettings["user"] + ":" + ConfigurationManager.AppSettings["psw"]));

            Entidades.Sucursal sucursal = new Entidades.Sucursal();
            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = Int32.MaxValue;

            var httpResponse = (HttpWebResponse)req.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var objText = streamReader.ReadToEnd();
                sucursal = js.Deserialize<Entidades.Sucursal>(objText);
            }

            if (sucursal != null)
            {
                objeto.Add("idSucursal", sucursal.id);
                objeto.Add("nombreSucursal", sucursal.name);

                sucursal = null;
            }

            return objeto;
        }

        public static Dictionary<string, string> ObtenerDatosCliente(string id)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            ClienteMambu objeto = new ClienteMambu();
            Dictionary<string, string> datos = new Dictionary<string, string>();


            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://visionfundmexico.mambu.com/api/" + Constantes.API_MAMBU_CLIENT + "/" + id + "?fulldetails=true");

            req.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:31.0) Gecko/20100101 Firefox/31.0";
            req.Method = Constantes.METODO_GET;
            req.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(ConfigurationManager.AppSettings["user"] + ":" + ConfigurationManager.AppSettings["psw"]));



            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = Int32.MaxValue;

            var httpResponse = (HttpWebResponse)req.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var objText = streamReader.ReadToEnd();
                objeto = js.Deserialize<ClienteMambu>(objText);
            }

            if (objeto != null)
            {
                datos.Add("idCliente", objeto.client.id);
                datos.Add("nombreCliente", objeto.client.firstName);
                datos.Add("paternoCliente", objeto.client.middleName);
                datos.Add("maternoCliente", objeto.client.lastName);
                datos.Add("fechaNacimiento", objeto.client.birthDate);
                datos.Add("rfc", objeto.customInformation.FirstOrDefault(i => i.customFieldKey == ConstantesMambu.KEY_CAMPO_RFC_CLIENTE).value);
                datos.Add("curp", objeto.customInformation.FirstOrDefault(i => i.customFieldKey == ConstantesMambu.KEY_CAMPO_CURP).value);
                datos.Add("sexo", objeto.client.gender);
                datos.Add("direccion", objeto.customInformation.FirstOrDefault(i => i.customFieldKey == ConstantesMambu.KEY_CAMPO_CALLE).value + " "
                                        + objeto.customInformation.FirstOrDefault(i => i.customFieldKey == ConstantesMambu.KEY_CAMPO_NUMERO_EXTERIOR).value + " "
                                        + objeto.customInformation.FirstOrDefault(i => i.customFieldKey == ConstantesMambu.KEY_CAMPO_NUMERO_INTERIOR).value);
                datos.Add("coloniaPoblacion", objeto.customInformation.FirstOrDefault(i => i.customFieldKey == ConstantesMambu.KEY_CAMPO_COLONIA).value);
                
                if(objeto.client.homePhone == null)
                {
                    datos.Add("numeroTelefonico", objeto.client.mobilePhone1);
                }                    
                else
                {
                    datos.Add("numeroTelefonico", objeto.client.homePhone);
                }
                
                if (objeto.groupKeys.Count > 0)
                {
                    datos.Add("keyGrupo", objeto.groupKeys.FirstOrDefault().ToString());
                }
                else
                {
                    datos.Add("keyGrupo", objeto.id);
                }

                objeto = null;
            }

            return datos;

        }

        public static Cliente ObtenerDatosClienteMambu(string id)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            Cliente cliente = new Cliente();
            ClienteMambu clienteMambu = new ClienteMambu();
            Dictionary<string, string> datos = new Dictionary<string, string>();


            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://visionfundmexico.mambu.com/api/" + Constantes.API_MAMBU_CLIENT + "/" + id + "?fulldetails=true");

            req.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:31.0) Gecko/20100101 Firefox/31.0";
            req.Method = Constantes.METODO_GET;
            req.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(ConfigurationManager.AppSettings["user"] + ":" + ConfigurationManager.AppSettings["psw"]));



            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = Int32.MaxValue;

            var httpResponse = (HttpWebResponse)req.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var objText = streamReader.ReadToEnd();
                clienteMambu = js.Deserialize<ClienteMambu>(objText);
            }

            if (clienteMambu != null)
            {
                cliente.idCliente = clienteMambu.client.id;
                cliente.nombre = clienteMambu.client.firstName;
                cliente.apellidoPaterno = clienteMambu.client.lastName;
                cliente.apellidoMaterno = clienteMambu.client.middleName;

                var rfc = clienteMambu.customInformation.FirstOrDefault(i => i.customFieldID == ConstantesMambu.ID_CAMPO_RFC_CLIENTE).value;
            }

            return cliente;

        }

        public static Dictionary<string, string> ObtenerDatosGrupo(string id)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;


            Grupo objeto = new Grupo();
            Dictionary<string, string> datos = new Dictionary<string, string>();


            WebRequest req = WebRequest.Create(ConfigurationManager.AppSettings["ambiente"] +"/"+ Negocio.Globales.Constantes.API_MAMBU_GROUP + "/" + id);

            req.ContentType = "application/json; charset=utf-8";
            req.Method = Negocio.Globales.Constantes.METODO_GET;
            req.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(ConfigurationManager.AppSettings["user"] + ":" + ConfigurationManager.AppSettings["psw"]));


            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = Int32.MaxValue;

            var httpResponse = (HttpWebResponse)req.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var objText = streamReader.ReadToEnd();
                objeto = js.Deserialize<Grupo>(objText);
            }

            if (objeto != null)
            {
                datos.Add("idGrupo", objeto.id);
                datos.Add("nombreGrupo", objeto.groupName);
                objeto = null;
            }

            return datos;
        }

        public static Dictionary<string, string> ObtenerDatosProducto(string id)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            Producto objeto = new Producto();
            Dictionary<string, string> datos = new Dictionary<string, string>();


            WebRequest req = WebRequest.Create("https://visionfundmexico.mambu.com/api/" + Constantes.API_MAMBU_PRODUCT + "/" + id + "?fulldetails=true");
            req.ContentType = "application/json; charset=utf-8";
            req.Method = Negocio.Globales.Constantes.METODO_GET;
            req.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(ConfigurationManager.AppSettings["user"] + ":" + ConfigurationManager.AppSettings["psw"]));


            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = Int32.MaxValue;

            var httpResponse = (HttpWebResponse)req.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var objText = streamReader.ReadToEnd();
                objeto = js.Deserialize<Producto>(objText);
            }

            if (objeto != null)
            {
                datos.Add("nombreProducto", objeto.productName);
                objeto = null;
            }

            return datos;
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

                    campoPersonalizado = JsonConvert.DeserializeObject<List<CustomFieldValue>>(objText);

                    if (campoPersonalizado != null)
                    {
                        if (tipoDato.Equals(Constantes.OBJETO_USUARIO))
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

        public static List<Transaccion> ObtenerTransacciones(string tipo, string keyContrato)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            int offset = 0;
            bool acaba = false;

            Filtros filtros = new Filtros();
            FilterConstraints filterConstraint = new FilterConstraints();

            if (tipo != null)
            {
                // Tipo de Transacción
                filterConstraint = new FilterConstraints();
                filterConstraint.filterSelection = ConstantesMambu.FILTRO_SELECCION_EVENT;
                filterConstraint.filterElement = ConstantesMambu.OPERADOR_EQUALS;
                filterConstraint.value = tipo;
                filterConstraint.dataItemType = ConstantesMambu.DATA_ITEM_TYPE_LOAN_TRANSACTION;
                filtros.filterConstraints.Add(filterConstraint);
            }



            //  Key Contrato
            filterConstraint = null;
            filterConstraint = new FilterConstraints();
            filterConstraint.filterSelection = ConstantesMambu.FILTRO_CAMPO_PARENT_ACCOUNT_KEY;
            filterConstraint.filterElement = ConstantesMambu.OPERADOR_EQUALS;
            filterConstraint.value = keyContrato;
            filterConstraint.dataItemType = ConstantesMambu.DATA_ITEM_TYPE_LOAN_TRANSACTION;
            filtros.filterConstraints.Add(filterConstraint);


            //  Fue Reversado?
            filterConstraint = null;
            filterConstraint = new FilterConstraints();
            filterConstraint.filterSelection = ConstantesMambu.FILTRO_CAMPO_WAS_REVERSED;
            filterConstraint.filterElement = ConstantesMambu.OPERADOR_EQUALS;
            filterConstraint.value = ConstantesMambu.VALOR_CAMPO_FALSO;
            filterConstraint.dataItemType = ConstantesMambu.DATA_ITEM_TYPE_LOAN_TRANSACTION;
            filtros.filterConstraints.Add(filterConstraint);


            string json = JsonConvert.SerializeObject(filtros);

            List<Transaccion> transacciones = null;
            List<Transaccion> acumuladoTransacciones = new List<Transaccion>();


            while (!acaba)
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://visionfundmexico.mambu.com/api/loans/transactions/search?" + Constantes.LIMITE_CONSULTA + "&" + Constantes.OFFSET_CONSULTA + offset);

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
                    transacciones = js.Deserialize<List<Transaccion>>(objText);

                    if (transacciones.Count <= 0)
                    {
                        acaba = true;
                    }
                    else
                    {
                        offset += 1000;
                        acumuladoTransacciones.AddRange(transacciones);
                    }

                    transacciones = null;
                }

            }

            return acumuladoTransacciones;
        }

        public static List<Transaccion> ObtenerTransacciones(string tipo, string fechaInicio, string fechaFin, bool reversado)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            int offset = 0;
            bool acaba = false;

            Filtros filtros = new Filtros();
            FilterConstraints filterConstraint = new FilterConstraints();

            // Tipo de Transacción
            filterConstraint = new FilterConstraints();
            filterConstraint.filterSelection = ConstantesMambu.FILTRO_SELECCION_EVENT;
            filterConstraint.filterElement = ConstantesMambu.OPERADOR_EQUALS;
            filterConstraint.value = tipo;
            filterConstraint.dataItemType = ConstantesMambu.DATA_ITEM_TYPE_LOAN_TRANSACTION;
            filtros.filterConstraints.Add(filterConstraint);


            //  Rango de Fechas
            filterConstraint = null;
            filterConstraint = new FilterConstraints();
            filterConstraint.filterSelection = ConstantesMambu.FILTRO_CAMPO_TRANSACTION_DATE;
            filterConstraint.filterElement = ConstantesMambu.OPERADOR_BETWEEN;
            filterConstraint.value = fechaInicio;
            filterConstraint.secondValue = fechaFin;
            filterConstraint.dataItemType = ConstantesMambu.DATA_ITEM_TYPE_LOAN_TRANSACTION;
            filtros.filterConstraints.Add(filterConstraint);


            //  Fue Reversado?
            filterConstraint = null;
            filterConstraint = new FilterConstraints();
            filterConstraint.filterSelection = ConstantesMambu.FILTRO_CAMPO_WAS_REVERSED;
            filterConstraint.filterElement = ConstantesMambu.OPERADOR_EQUALS;
            filterConstraint.value = reversado ? ConstantesMambu.VALOR_CAMPO_VERDADERO : ConstantesMambu.VALOR_CAMPO_FALSO;
            filterConstraint.dataItemType = ConstantesMambu.DATA_ITEM_TYPE_LOAN_TRANSACTION;
            filtros.filterConstraints.Add(filterConstraint);


            string json = JsonConvert.SerializeObject(filtros);

            List<Transaccion> transacciones = null;
            List<Transaccion> acumuladoTransacciones = new List<Transaccion>();


            while (!acaba)
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://visionfundmexico.mambu.com/api/loans/transactions/search?" + Constantes.LIMITE_CONSULTA + "&" + Constantes.OFFSET_CONSULTA + offset);

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
                    transacciones = js.Deserialize<List<Transaccion>>(objText);

                    if (transacciones.Count <= 0)
                    {
                        acaba = true;
                    }
                    else
                    {
                        offset += 1000;
                        acumuladoTransacciones.AddRange(transacciones);
                    }

                    transacciones = null;
                }

            }

            return acumuladoTransacciones;
        }
    }
}
