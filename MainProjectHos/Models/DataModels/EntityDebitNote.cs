using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityPurchaseInvoice
    /// </summary>
    public class EntityDebitNote
    {
        public EntityDebitNote()
        {

        }

        public string Address { get; set; }

        private System.Nullable<int> _SupplierId;

        private System.Nullable<bool> _IsDelete;

        private System.Nullable<decimal> _Amount;

        private System.Nullable<decimal> _NetAmount;

        private System.Nullable<int> _Tax1;

        private System.Nullable<int> _Tax2;

        private System.Nullable<int> _Discount;

        public string SupplierName { get; set; }

        public System.Nullable<int> SupplierId
        {
            get
            {
                return this._SupplierId;
            }
            set
            {
                if ((this._SupplierId != value))
                {
                    this._SupplierId = value;
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

        public System.Nullable<int> Tax1
        {
            get
            {
                return this._Tax1;
            }
            set
            {
                if ((this._Tax1 != value))
                {
                    this._Tax1 = value;
                }
            }
        }

        public System.Nullable<int> Tax2
        {
            get
            {
                return this._Tax2;
            }
            set
            {
                if ((this._Tax2 != value))
                {
                    this._Tax2 = value;
                }
            }
        }

        public string ProductName { get; set; }

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

        public int DNNo { get; set; }

        public DateTime? DNDate { get; set; }

        public int? PONo { get; set; }

        public decimal PIDate { get; set; }


    }
}