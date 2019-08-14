using Atlassian.Jira;
using Braintree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entidades
{
    [Serializable]
    [XmlRoot("contrato")]
    public class Loan
    {

        public String encodedKey { get; set; }

        [XmlElement("id")]
        public String id { get; set; }

        public String idCredito { get; set; }

        public String keyOficialCredito { get; set; }
        public String nombreProducto { get; set; }
        public String nombreMetodologia { get; set; }

        public int numeroCuotas { get; set; }
        public int numeroCuotaAtrasada { get; set; }
        public DateTime fechaProximoPago { get; set; }



        public String accountHolderKey { get; set; }
        public String accountHolderType { get; set; }
        public String activationTransactionKey { get; set; }
        public String accountState { get; set; }
        public String accountSubState { get; set; }
        public String productTypeKey { get; set; }
        public String loanName { get; set; }
        public String periodicPayment { get; set; }
        public String scheduleDueDatesMethod { get; set; }
        public String gracePeriodType { get; set; }
        public String repaymentPeriodUnit { get; set; }
        public String interestChargeFrequency { get; set; }
        public String interestCalculationMethod { get; set; }
        public String interestBalanceCalculationMethod { get; set; }
        public String repaymentScheduleMethod { get; set; }
        public String paymentMethod { get; set; }
        public String interestApplicationMethod { get; set; }
        public String assignedUserKey { get; set; }
        public String assignedBranchKey { get; set; }
        public String notes { get; set; }
        public String interestRateSource { get; set; }
        public String accruedInterest { get; set; }
        public String accruedPenalty { get; set; }
        public String loanPenaltyCalculationMethod { get; set; }
        public String lineOfCreditKey { get; set; }




        [XmlElement("fechaDesembolso")]
        public string fechaDesembolso
        {
            get
            {
                return this.disbursementDate.ToString();
            }
            set
            {
                if (value == null)
                {
                    this.disbursementDate = DateTime.Parse("01/01/1900");
                }
                else
                {
                    this.disbursementDate = DateTime.Parse(value.Replace(" a. m.", string.Empty).Replace(" p. m.", string.Empty));
                }
            }
        }

        [XmlElement("fechaEsperadaDesembolso")]
        public string fechaEsperadaDesembolso
        {
            get
            {
                return this.expectedDisbursementDate.ToString();
            }
            set
            {
                if (value == null)
                {
                    this.expectedDisbursementDate = DateTime.Parse("01/01/1900");
                }
                else
                {
                    this.expectedDisbursementDate = DateTime.Parse(value.Replace(" a. m.", string.Empty).Replace(" p. m.", string.Empty));
                }
            }
        }

        [XmlElement("fechaDeCreacion")]
        public string fechaDeCreacion
        {
            get
            {
                return this.creationDate.ToString();
            }
            set
            {
                if (value == null)
                {
                    this.creationDate = DateTime.Parse("01/01/1900");
                }
                else
                {
                    this.creationDate = DateTime.Parse(value.Replace(" a. m.", string.Empty).Replace(" p. m.", string.Empty));
                }
            }
        }

        [XmlElement("fechaInicioPagos")]
        private string fechaInicioPagos
        {
            get
            {
                return this.fechaInicioPagos;
            }
            set
            {
                if (value == null)
                {
                    this.firstRepaymentDate = DateTime.Parse("01/01/1900");
                }
                else
                {
                    this.firstRepaymentDate = DateTime.Parse(value.Replace(" a. m.", string.Empty).Replace(" p. m.", string.Empty));
                }
            }
        }

        [XmlElement("fechaAprobacion")]
        private string fechaAprobacion
        {
            get
            {
                return this.approvedDate.ToString();
            }
            set
            {
                if (value == null)
                {
                    this.approvedDate = DateTime.Parse("01/01/1900");
                }
                else
                {
                    this.approvedDate = DateTime.Parse(value.Replace(" a. m.", string.Empty).Replace(" p. m.", string.Empty));
                }
            }
        }

        [XmlElement("fechaUltimaModificacion")]
        private string fechaUltimaModificacion
        {
            get
            {
                return this.lastModifiedDate.ToString();
            }
            set
            {
                if (value == null)
                {
                    this.lastModifiedDate = DateTime.Parse("01/01/1900");
                }
                else
                {
                    this.lastModifiedDate = DateTime.Parse(value.Replace(" a. m.", string.Empty).Replace(" p. m.", string.Empty));
                }
            }
        }

        [XmlElement("fechaAtraso")]
        private string fechaAtraso
        {
            get
            {
                return this.lastSetToArrearsDate.ToString();
            }
            set
            {
                if (value == null)
                {
                    this.lastSetToArrearsDate = DateTime.Parse("01/01/1900");
                }
                else
                {
                    this.lastSetToArrearsDate = DateTime.Parse(value.Replace(" a. m.", string.Empty).Replace(" p. m.", string.Empty));
                }
            }
        }

        [XmlElement("fechaUltimoInteresAplicado")]
        private string fechaUltimoInteresAplicado
        {
            get
            {
                return this.lastInterestAppliedDate.ToString();
            }
            set
            {
                if (value == null)
                {
                    this.lastInterestAppliedDate = DateTime.Parse("01/01/1900");
                }
                else
                {
                    this.lastInterestAppliedDate = DateTime.Parse(value.Replace(" a. m.", string.Empty).Replace(" p. m.", string.Empty));
                }
            }
        }

        public double diasVencidos { get; set; }

        public DateTime creationDate
        {
            get;
            set;
        }

        public DateTime expectedDisbursementDate
        {
            get;
            set;
        }

        public DateTime disbursementDate
        {
            get;
            set;
        }

        public DateTime firstRepaymentDate
        {
            get;
            set;
        }

        public DateTime approvedDate
        {
            get;
            set;
        }

        public DateTime lastModifiedDate
        {
            get;
            set;
        }

        public DateTime lastSetToArrearsDate
        {
            get;
            set;
        }

        public DateTime lastInterestAppliedDate
        {
            get;
            set;
        }


        public decimal montoCuota { get; set; }
        public decimal porcentajePago { get; set; }
        public decimal loanAmount { get; set; }
        public decimal principalDue { get; set; }
        public decimal principalPaid { get; set; }
        public decimal principalBalance { get; set; }
        public decimal interestDue { get; set; }
        public decimal interestPaid { get; set; }
        public decimal interestBalance { get; set; }
        public decimal feesDue { get; set; }
        public decimal feesPaid { get; set; }
        public decimal feesBalance { get; set; }
        public decimal penaltyDue { get; set; }
        public decimal penaltyPaid { get; set; }
        public decimal penaltyBalance { get; set; }
        public decimal interestRate { get; set; }

        public Boolean hasCustomSchedule { get; set; }

        public int repaymentPeriodCount { get; set; }
        public int repaymentInstallments { get; set; }
        public int gracePeriod { get; set; }
        public int principalRepaymentInterval { get; set; }

        [XmlElement("homePhone")]
        public String homePhone { get; set; }


        [XmlElement("emailAddress")]
        public String emailAddress { get; set; }


        [XmlElement("mobilePhone1")]
        public String mobilePhone1 { get; set; }

        public Boolean esGrupal { get; set; }


        [XmlArray("customFieldValues")]
        [XmlArrayItem("customFieldValue")]
        public List<CustomFieldValue> customFieldValues { get; set; }


        public DisbursementDetails disbursementDetails { get; set; }
    }
}
