using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Globales
{
    public class Constantes
    {
        public const string SEPARADOR_LOG = ">";

        public const string METODO_GET = "GET";
        public const string METODO_POST = "POST";
        public const string METODO_PATCH = "PATCH";

        public const string API_MAMBU_LOAN = "loans";
        public const string API_MAMBU_GROUP = "groups";
        public const string API_MAMBU_CLIENT = "clients";
        public const string API_MAMBU_SEARCH = "search";
        public const string API_MAMBU_BRANCH = "branches";
        public const string API_MAMBU_PRODUCT = "loanproducts";
        public const string API_MAMBU_TRANSACTION = "transactions";
        public const string API_MAMBU_CUSTOMINFORMATION = "custominformation";
        public const string API_MAMBU_USER = "users";
        public const string API_MAMBU_LINES_OF_CREDIT = "linesofcredit";

        public const string FULLDETAILS = "?fulldetails=true";

        public const string ESTATUS_RESPONSE_ERROR = "-1";

        public const string TRANSACTIONS = "transactions";
        public const string TRANSACTIONS_TYPE = "/transactions?type=";
        public const string TRANSACTIONS_TYPE_DISBURSMENT = "DISBURSMENT";
        public const string TRANSACTIONS_TYPE_REPAYMENT = "REPAYMENT";
        public const string TRANSACTIONS_DATE = "date=";

        public const string LIMITE_CONSULTA = "limit=1000";
        public const string OFFSET_CONSULTA = "offset=";

        public const String ESTATUS_PAGO_PENDIENTE = "PENDING";
        public const String ESTATUS_PAGO_ATRASADO = "LATE";
        public const String ESTATUS_APROBADO = "APPROVED";
        public const String ESTATUS_PAGO_PARCIAL = "PARTIALLY_PAID";
        public const String ESTATUS_APROBADO_PARCIAL = "PARTIAL_APPROVED";
        public const String ESTATUS_ACTIVO = "ACTIVE";
        public const String ESTATUS_ACTIVO_CON_RETRASOS = "ACTIVE_IN_ARREARS";


        public const string REPAYMENT_AMOUNT = "amount=";
        public const string REPAYMENT_DATE = "date=";
        public const string REPAYMENT_RECEIPTNUMBER = "receiptNumber=";
        public const string REPAYMENT_METHOD = "method=";
        public const string REPAYMENT_RECEIPT = "RECEIPT";
        public const string REPAYMENT_NOTES = "notes=";
        public const string ERROR_APLICADO = "APLICADO";
        public const string ERROR_NO_APLICADO = "NO APLICADO";
        public const string ERROR = "ERROR";



        public const string OBJETO_CONTRATO = "CONTRATO";
        public const string OBJETO_CLIENTE = "CLIENTE";
        public const string OBJETO_GRUPO = "GRUPO";
        public const string OBJETO_TRANSACCION = "TRANSACCION";
        public const string OBJETO_USUARIO = "USUARIO";


        public const string RESPUESTA_CODE_EXITOSO = "0";
        public const string RESPUESTA_STATUS_EXITOSO = "SUCCESS";


        public const String ID_CAMPO_ID_PAGO_FOLIO = "Folio_Transacciones";
        public const String ID_CAMPO_ID_PAGO_BANCO = "Banco_Deposito_Transacciones";
        public const String ID_CAMPO_METODOLOGIA = "Metodología_Cuentas_de_Préstamo";


        public const String FILTRO_CAMPO_ENCODED_KEY = "ENCODED_KEY";
        public const String FILTRO_CAMPO_ACCOUNT_ID = "ACCOUNT_ID";

        public const String DATA_FIELD_TYPE_CUSTOM = "CUSTOM";
        public const String DATA_ITEM_TYPE_LOANS = "LOANS";
        public const String DATA_ITEM_TYPE_CLIENT = "CLIENT";
        public const String DATA_ITEM_TYPE_GROUP = "GROUP";
        public const String DATA_ITEM_TYPE_DISBURSEMENT_DETAILS = "DISBURSEMENT_DETAILS";



        public const String OPERADOR_EQUALS = "EQUALS";
        public const String OPERADOR_BETWEEN = "BETWEEN";
        public const String OPERADOR_IN = "IN";
        public const String OPERADOR_MORE_THAN = "MORE_THAN";


        public const String MOVIMIENTO_DESEMBOLSO = "DESEMBOLSO";
        public const String MOVIMIENTO_PAGO = "PAGO";


        public const String KEY_CAMPO_ID_CREDITO = "8a818f0e54c838c50154cb7d50675085";


        public static readonly string[] pagos = { "REPAYMENT", "REPAYMENT_ADJUSTMENT", "DEFERRED_INTEREST_PAID" };

        public static readonly string[] metodologiasUDE = { "UDE TRADICIONAL", "UDE 2.0", "UDE 3.0", "GS AHORROS (FLAT)", "GS TRADICIONAL (FLAT)" };


        public const Decimal IVA = 16.0M;

        public const string TIPO_CREDITO_GRUPAL = "GRUPAL";

        public const string TIPO_CREDITO_INDIVIDUAL = "INDIVIDUAL";

    }
}
