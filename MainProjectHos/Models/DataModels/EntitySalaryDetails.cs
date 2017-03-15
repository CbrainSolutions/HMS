using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{

    /// <summary>
    /// Summary description for EntitySalaryDetails
    /// </summary>
    public class EntitySalaryDetails
    {
        public EntitySalaryDetails()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private int _SalDetail_Id;

        private int _SalId;

        private System.DateTime _SalDate;

        private int _AllowanceDeduction_Id;

        private decimal _Amount;

        private bool _IsPaymentDone;

        private bool _IsDelete;

        public int SalDetail_Id
        {
            get
            {
                return this._SalDetail_Id;
            }
            set
            {
                if ((this._SalDetail_Id != value))
                {
                    this._SalDetail_Id = value;
                }
            }
        }

        public int SalId
        {
            get
            {
                return this._SalId;
            }
            set
            {
                if ((this._SalId != value))
                {
                    this._SalId = value;
                }
            }
        }

        public System.DateTime SalDate
        {
            get
            {
                return this._SalDate;
            }
            set
            {
                if ((this._SalDate != value))
                {
                    this._SalDate = value;
                }
            }
        }

        public int AllowanceDeduction_Id
        {
            get
            {
                return this._AllowanceDeduction_Id;
            }
            set
            {
                if ((this._AllowanceDeduction_Id != value))
                {
                    this._AllowanceDeduction_Id = value;
                }
            }
        }

        public decimal Amount
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

        public bool IsPaymentDone
        {
            get
            {
                return this._IsPaymentDone;
            }
            set
            {
                if ((this._IsPaymentDone != value))
                {
                    this._IsPaymentDone = value;
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