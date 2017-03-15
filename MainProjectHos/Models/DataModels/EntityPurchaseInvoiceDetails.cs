using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{

    /// <summary>
    /// Summary description for EntityPurchaseInvoiceDetails
    /// </summary>
    public class EntityPurchaseInvoiceDetails
    {
        public EntityPurchaseInvoiceDetails()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int PONo { get; set; }

        public int PurchaseInvoiceNo { get; set; }

        private System.Nullable<int> _PINoSrNo;

        private System.Nullable<int> _ProductCode;

        private System.Nullable<int> _InvoiceQty;

        private System.Nullable<decimal> _InvoicePrice;

        private System.Nullable<System.DateTime> _ExpiryDate;

        private string _BatchNo;

        private System.Nullable<decimal> _Amount;

        private System.Nullable<bool> _IsDelete;

        public string ProductName { get; set; }


        public System.Nullable<int> PINoSrNo
        {
            get
            {
                return this._PINoSrNo;
            }
            set
            {
                if ((this._PINoSrNo != value))
                {
                    this._PINoSrNo = value;
                }
            }
        }

        public System.Nullable<int> ProductCode
        {
            get
            {
                return this._ProductCode;
            }
            set
            {
                if ((this._ProductCode != value))
                {
                    this._ProductCode = value;
                }
            }
        }

        public System.Nullable<int> InvoiceQty
        {
            get
            {
                return this._InvoiceQty;
            }
            set
            {
                if ((this._InvoiceQty != value))
                {
                    this._InvoiceQty = value;
                }
            }
        }

        public System.Nullable<decimal> InvoicePrice
        {
            get
            {
                return this._InvoicePrice;
            }
            set
            {
                if ((this._InvoicePrice != value))
                {
                    this._InvoicePrice = value;
                }
            }
        }

        public System.Nullable<System.DateTime> ExpiryDate
        {
            get
            {
                return this._ExpiryDate;
            }
            set
            {
                if ((this._ExpiryDate != value))
                {
                    this._ExpiryDate = value;
                }
            }
        }

        public string BatchNo
        {
            get
            {
                return this._BatchNo;
            }
            set
            {
                if ((this._BatchNo != value))
                {
                    this._BatchNo = value;
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
    }
}