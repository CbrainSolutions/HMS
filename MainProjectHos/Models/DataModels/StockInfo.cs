using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityCompany
    /// </summary>
    public class StockInfo
    {
        public StockInfo()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private string _ProductName;

        private System.Nullable<int> _ProductId;

        private System.Nullable<int> _InwardQty;

        private System.Nullable<int> _OutwardQty;

        private System.Nullable<int> _ClosingStock;


        public string ProductName
        {
            get
            {
                return this._ProductName;
            }
            set
            {
                if ((this._ProductName != value))
                {
                    this._ProductName = value;
                }
            }
        }

        public System.Nullable<int> ProductId
        {
            get
            {
                return this._ProductId;
            }
            set
            {
                if ((this._ProductId != value))
                {
                    this._ProductId = value;
                }
            }
        }

        public System.Nullable<int> InwardQty
        {
            get
            {
                return this._InwardQty;
            }
            set
            {
                if ((this._InwardQty != value))
                {
                    this._InwardQty = value;
                }
            }
        }

        public System.Nullable<int> OutwardQty
        {
            get
            {
                return this._OutwardQty;
            }
            set
            {
                if ((this._OutwardQty != value))
                {
                    this._OutwardQty = value;
                }
            }
        }


        public System.Nullable<int> ClosingStock
        {
            get
            {
                return this._ClosingStock;
            }
            set
            {
                if ((this._ClosingStock != value))
                {
                    this._ClosingStock = value;
                }
            }
        }

    }
}