using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityPurchaseOrder
    /// </summary>
    public class EntityIssueMaterial
    {
        public EntityIssueMaterial()
        {

        }

        private int _IssueId;

        private System.Nullable<int> _EmpId;

        private System.Nullable<System.DateTime> _IssueDate;

        private System.Nullable<decimal> _TotalAmount;

        private bool _IsDelete;

        public string EmployeeName { get; set; }

        public int IssueId
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

        public System.Nullable<int> EmpId
        {
            get
            {
                return this._EmpId;
            }
            set
            {
                if ((this._EmpId != value))
                {
                    this._EmpId = value;
                }
            }
        }

        public System.Nullable<System.DateTime> IssueDate
        {
            get
            {
                return this._IssueDate;
            }
            set
            {
                if ((this._IssueDate != value))
                {
                    this._IssueDate = value;
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

        public string PatientName { get; set; }

        public int PatientId { get; set; }
        public int PO_Id { get; set; }
    }
}