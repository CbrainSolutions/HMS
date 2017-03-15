using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityStockDetails
    /// </summary>
    public class EntityStockDetails
    {
        public EntityStockDetails()
        {

        }

        private int _StockId;

        private int _ProductId;

        private string _OpeningQty;

        private System.Nullable<System.DateTime> _OpeningQtyDate;

        private string _InwardQty;

        private System.Nullable<decimal> _InwardPrice;

        private string _OutwardQty;

        private System.Nullable<decimal> _OutwardPrice;

        private System.Nullable<int> _DocumentNo;

        private string _TransactionType;

        private bool _IsDelete;

        public int StockId
        {
            get
            {
                return this._StockId;
            }
            set
            {
                if ((this._StockId != value))
                {
                    this._StockId = value;
                }
            }
        }

        public int ProductId
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

        public string OpeningQty
        {
            get
            {
                return this._OpeningQty;
            }
            set
            {
                if ((this._OpeningQty != value))
                {
                    this._OpeningQty = value;
                }
            }
        }

        public System.Nullable<System.DateTime> OpeningQtyDate
        {
            get
            {
                return this._OpeningQtyDate;
            }
            set
            {
                if ((this._OpeningQtyDate != value))
                {
                    this._OpeningQtyDate = value;
                }
            }
        }

        public string InwardQty
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

        public System.Nullable<decimal> InwardPrice
        {
            get
            {
                return this._InwardPrice;
            }
            set
            {
                if ((this._InwardPrice != value))
                {
                    this._InwardPrice = value;
                }
            }
        }

        public string OutwardQty
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

        public System.Nullable<decimal> OutwardPrice
        {
            get
            {
                return this._OutwardPrice;
            }
            set
            {
                if ((this._OutwardPrice != value))
                {
                    this._OutwardPrice = value;
                }
            }
        }

        public System.Nullable<int> DocumentNo
        {
            get
            {
                return this._DocumentNo;
            }
            set
            {
                if ((this._DocumentNo != value))
                {
                    this._DocumentNo = value;
                }
            }
        }

        public string TransactionType
        {
            get
            {
                return this._TransactionType;
            }
            set
            {
                if ((this._TransactionType != value))
                {
                    this._TransactionType = value;
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