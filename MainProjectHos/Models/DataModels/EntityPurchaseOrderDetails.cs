using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityPurchaseOrderDetails
    /// </summary>
    public class EntityPurchaseOrderDetails
    {
        public EntityPurchaseOrderDetails()
        {

        }

        private int _SR_No;

        public int InvoiceQuantity { get; set; }

        private System.Nullable<int> _PO_Id;

        private System.Nullable<int> _Product_Id;

        private System.Nullable<int> _Quantity;

        private System.Nullable<decimal> _Rate;

        private System.Nullable<decimal> _Total;

        private bool _IsDelete;

        public string ProductName { get; set; }

        public string VendorName { get; set; }

        public int VendorId { get; set; }

        public decimal NetTotal { get; set; }

        public int SR_No
        {
            get
            {
                return this._SR_No;
            }
            set
            {
                if ((this._SR_No != value))
                {
                    this._SR_No = value;
                }
            }
        }

        public System.Nullable<int> PO_Id
        {
            get
            {
                return this._PO_Id;
            }
            set
            {
                if ((this._PO_Id != value))
                {
                    this._PO_Id = value;
                }
            }
        }

        public System.Nullable<int> Product_Id
        {
            get
            {
                return this._Product_Id;
            }
            set
            {
                if ((this._Product_Id != value))
                {
                    this._Product_Id = value;
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

        public System.Nullable<decimal> Rate
        {
            get
            {
                return this._Rate;
            }
            set
            {
                if ((this._Rate != value))
                {
                    this._Rate = value;
                }
            }
        }

        public System.Nullable<decimal> Total
        {
            get
            {
                return this._Total;
            }
            set
            {
                if ((this._Total != value))
                {
                    this._Total = value;
                }
            }
        }

        public bool IsDelete
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