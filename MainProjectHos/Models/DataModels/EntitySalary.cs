using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{

    /// <summary>
    /// Summary description for EntitySalary
    /// </summary>
    public class EntitySalary
    {
        public EntitySalary()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private int _SalId;

        private int _EmpId;

        private System.DateTime _SalDate;

        private int _No_of_Days;

        private int _LeavesTaken;

        private int _Attend_Days;

        private string _Sal_Month;

        private decimal _NetPayment;

        private bool _IsPaymentDone;

        private bool _IsDelete;
        public string EmpCode { get; set; }
        public string EmployeeName { get; set; }
        public int OTHours { get; set; }
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

        public int EmpId
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

        public int No_of_Days
        {
            get
            {
                return this._No_of_Days;
            }
            set
            {
                if ((this._No_of_Days != value))
                {
                    this._No_of_Days = value;
                }
            }
        }

        public int LeavesTaken
        {
            get
            {
                return this._LeavesTaken;
            }
            set
            {
                if ((this._LeavesTaken != value))
                {
                    this._LeavesTaken = value;
                }
            }
        }

        public int Attend_Days
        {
            get
            {
                return this._Attend_Days;
            }
            set
            {
                if ((this._Attend_Days != value))
                {
                    this._Attend_Days = value;
                }
            }
        }

        public string Sal_Month
        {
            get
            {
                return this._Sal_Month;
            }
            set
            {
                if ((this._Sal_Month != value))
                {
                    this._Sal_Month = value;
                }
            }
        }

        public decimal NetPayment
        {
            get
            {
                return this._NetPayment;
            }
            set
            {
                if ((this._NetPayment != value))
                {
                    this._NetPayment = value;
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