using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityPurchaseOrder
    /// </summary>
    public class EntityPurchaseOrder
    {
        public EntityPurchaseOrder()
        {

        }

        private int _PO_Id;

        private System.Nullable<int> _VendorId;

        private System.Nullable<System.DateTime> _PO_Date;

        private System.Nullable<decimal> _PO_Amount;

        private bool _IsDelete;

        public string VendorName { get; set; }

        public int PO_Id
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

        public System.Nullable<int> VendorId
        {
            get
            {
                return this._VendorId;
            }
            set
            {
                if ((this._VendorId != value))
                {
                    this._VendorId = value;
                }
            }
        }

        public System.Nullable<System.DateTime> PO_Date
        {
            get
            {
                return this._PO_Date;
            }
            set
            {
                if ((this._PO_Date != value))
                {
                    this._PO_Date = value;
                }
            }
        }

        public System.Nullable<decimal> PO_Amount
        {
            get
            {
                return this._PO_Amount;
            }
            set
            {
                if ((this._PO_Amount != value))
                {
                    this._PO_Amount = value;
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