using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityICUInvoiceDischarge
    /// </summary>
    public class EntityICUInvoiceDischarge
    {
        public EntityICUInvoiceDischarge()
        {

        }

        private int _ICUInvoiceNo;

        private System.Nullable<System.DateTime> _ICUInvoiceDate;

        private System.Nullable<int> _PatientAdmitId;

        private System.Nullable<decimal> _NetAmount;

        private System.Nullable<decimal> _TotalAmount;

        private System.Nullable<int> _Discount;

        private System.Nullable<int> _Vat;

        private System.Nullable<int> _ServiceTax;

        private System.Nullable<bool> _IsDelete;

        private System.Nullable<bool> _IsShiftToIPD;

        private System.Nullable<System.DateTime> _ShiftDate;

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

        public System.Nullable<int> Discount
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

        public System.Nullable<int> Vat
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

        public System.Nullable<int> ServiceTax
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

        public System.Nullable<bool> IsDelete
        {
            get
            {
                return this._IsDelete;
            }
            set
            {
                if ((this._IsDelete != value))
                {
                    this._IsDelete = value;
                }
            }
        }

        public System.Nullable<bool> IsShiftToIPD
        {
            get
            {
                return this._IsShiftToIPD;
            }
            set
            {
                if ((this._IsShiftToIPD != value))
                {
                    this._IsShiftToIPD = value;
                }
            }
        }

        public System.Nullable<System.DateTime> ShiftDate
        {
            get
            {
                return this._ShiftDate;
            }
            set
            {
                if ((this._ShiftDate != value))
                {
                    this._ShiftDate = value;
                }
            }
        }
    }
}