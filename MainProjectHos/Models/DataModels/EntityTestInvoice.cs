using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityTestInvoice
    /// </summary>
    public class EntityTestInvoice
    {
        public EntityTestInvoice()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private int _TestInvoiceNo;

        private System.Nullable<System.DateTime> _TestInvoiceDate;

        private System.Nullable<int> _PatientId;

        private System.Nullable<bool> _IsDelete;

        private System.Nullable<decimal> _Amount;

        private System.Nullable<decimal> _Discount;
        public int TestId { get; set; }

        public int TestInvoiceNo
        {
            get
            {
                return this._TestInvoiceNo;
            }
            set
            {
                if ((this._TestInvoiceNo != value))
                {
                    this._TestInvoiceNo = value;
                }
            }
        }

        public System.Nullable<System.DateTime> TestInvoiceDate
        {
            get
            {
                return this._TestInvoiceDate;
            }
            set
            {
                if ((this._TestInvoiceDate != value))
                {
                    this._TestInvoiceDate = value;
                }
            }
        }

        public System.Nullable<int> PatientId
        {
            get
            {
                return this._PatientId;
            }
            set
            {
                if ((this._PatientId != value))
                {
                    this._PatientId = value;
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

        public System.Nullable<decimal> Amount
        {
            get
            {
                return this._Amount;
            }
            set
            {
                if ((this._Amount != value))
                {
                    this._Amount = value;
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

        public string PatientName { get; set; }

        public string Address { get; set; }


    }
}