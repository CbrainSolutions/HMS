using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityICUInvoiceDischargeDetails
    /// </summary>
    public class EntityICUInvoiceDischargeDetails
    {
        public EntityICUInvoiceDischargeDetails()
        {

        }

        private int _ICUSRlNo;

        private System.Nullable<int> _ICUInvoiceNo;

        private System.Nullable<int> _ICUBedAllocId;

        private System.Nullable<int> _DOCAllocId;

        private System.Nullable<int> _ChargesId;

        private System.Nullable<decimal> _Amount;

        private System.Nullable<bool> _IsDelete;

        private System.Nullable<int> _NoofDays;

        private System.Nullable<decimal> _Charge;

        private System.Nullable<int> _Quantity;

        public int ICUSRlNo
        {
            get
            {
                return this._ICUSRlNo;
            }
            set
            {
                if ((this._ICUSRlNo != value))
                {
                    this._ICUSRlNo = value;
                }
            }
        }

        public System.Nullable<int> ICUInvoiceNo
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

        public System.Nullable<int> ICUBedAllocId
        {
            get
            {
                return this._ICUBedAllocId;
            }
            set
            {
                if ((this._ICUBedAllocId != value))
                {
                    this._ICUBedAllocId = value;
                }
            }
        }

        public System.Nullable<int> DOCAllocId
        {
            get
            {
                return this._DOCAllocId;
            }
            set
            {
                if ((this._DOCAllocId != value))
                {
                    this._DOCAllocId = value;
                }
            }
        }

        public System.Nullable<int> ChargesId
        {
            get
            {
                return this._ChargesId;
            }
            set
            {
                if ((this._ChargesId != value))
                {
                    this._ChargesId = value;
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

        public System.Nullable<int> NoofDays
        {
            get
            {
                return this._NoofDays;
            }
            set
            {
                if ((this._NoofDays != value))
                {
                    this._NoofDays = value;
                }
            }
        }

        public System.Nullable<decimal> Charge
        {
            get
            {
                return this._Charge;
            }
            set
            {
                if ((this._Charge != value))
                {
                    this._Charge = value;
                }
            }
        }

        public System.Nullable<int> Quantity
        {
            get
            {
                return this._Quantity;
            }
            set
            {
                if ((this._Quantity != value))
                {
                    this._Quantity = value;
                }
            }
        }

        public string ChargesName { get; set; }

        public decimal NetAmount { get; set; }

        public int Discount { get; set; }

        public int Service { get; set; }

        public int Vat { get; set; }
    }
}