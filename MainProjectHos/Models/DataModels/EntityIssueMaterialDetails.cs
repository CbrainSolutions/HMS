using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityPurchaseOrderDetails
    /// </summary>
    public class EntityIssueMaterialDetails
    {
        public EntityIssueMaterialDetails()
        {

        }

        public int TempId { get; set; }

        private int _SR_No;

        public string BatchNo { get; set; }

        public DateTime? ExpiryDate { get; set; }

        private System.Nullable<int> _IssueId;

        private System.Nullable<int> _ProductId;

        private System.Nullable<int> _Quantity;

        private System.Nullable<decimal> _Price;

        private System.Nullable<decimal> _Total;

        private bool _IsDelete;

        public string ProductName { get; set; }

        public string EmployeeName { get; set; }

        public int EmpId { get; set; }

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

        public System.Nullable<int> IssueId
        {
            get
            {
                return this._IssueId;
            }
            set
            {
                if ((this._IssueId != value))
                {
                    this._IssueId = value;
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

        public System.Nullable<decimal> Price
        {
            get
            {
                return this._Price;
            }
            set
            {
                if ((this._Price != value))
                {
                    this._Price = value;
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

        public decimal? Rate { get; set; }


        public int PatientId { get; set; }

        public string FullName { get; set; }

        public DateTime IssueDate { get; set; }

    }
}