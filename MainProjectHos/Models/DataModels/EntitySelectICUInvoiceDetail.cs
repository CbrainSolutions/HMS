using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntitySelectICUInvoiceDetail
    /// </summary>
    public class EntitySelectICUInvoiceDetail
    {
        public EntitySelectICUInvoiceDetail()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private int _ICUInvoiceNo;

        private System.Nullable<int> _PatientAdmitId;

        private System.Nullable<System.DateTime> _ICUInvoiceDate;

        private System.Nullable<decimal> _NetAmount;

        private System.Nullable<decimal> _TotalAmount;

        private System.Nullable<decimal> _Discount;

        private System.Nullable<decimal> _Vat;

        private System.Nullable<decimal> _ServiceTax;

        private string _FullName;

        private int _AdmitId;

        public int ICUInvoiceNo
        {
            get
            {
                return this._ICUInvoiceNo;
            }
            set
            {
                if ((this._ICUInvoiceNo != value))
                {
                    this._ICUInvoiceNo = value;
                }
            }
        }

        public System.Nullable<int> PatientAdmitId
        {
            get
            {
                return this._PatientAdmitId;
            }
            set
            {
                if ((this._PatientAdmitId != value))
                {
                    this._PatientAdmitId = value;
                }
            }
        }

        public System.Nullable<System.DateTime> ICUInvoiceDate
        {
            get
            {
                return this._ICUInvoiceDate;
            }
            set
            {
                if ((this._ICUInvoiceDate != value))
                {
                    this._ICUInvoiceDate = value;
                }
            }
        }

        public System.Nullable<decimal> NetAmount
        {
            get
            {
                return this._NetAmount;
            }
            set
            {
                if ((this._NetAmount != value))
                {
                    this._NetAmount = value;
                }
            }
        }

        public System.Nullable<decimal> TotalAmount
        {
            get
            {
                return this._TotalAmount;
            }
            set
            {
                if ((this._TotalAmount != value))
                {
                    this._TotalAmount = value;
                }
            }
        }

        public System.Nullable<decimal> Discount
        {
            get
            {
                return this._Discount;
            }
            set
            {
                if ((this._Discount != value))
                {
                    this._Discount = value;
                }
            }
        }

        public System.Nullable<decimal> Vat
        {
            get
            {
                return this._Vat;
            }
            set
            {
                if ((this._Vat != value))
                {
                    this._Vat = value;
                }
            }
        }

        public System.Nullable<decimal> ServiceTax
        {
            get
            {
                return this._ServiceTax;
            }
            set
            {
                if ((this._ServiceTax != value))
                {
                    this._ServiceTax = value;
                }
            }
        }

        public string FullName
        {
            get
            {
                return this._FullName;
            }
            set
            {
                if ((this._FullName != value))
                {
                    this._FullName = value;
                }
            }
        }

        public int AdmitId
        {
            get
            {
                return this._AdmitId;
            }
            set
            {
                if ((this._AdmitId != value))
                {
                    this._AdmitId = value;
                }
            }
        }
    }
}