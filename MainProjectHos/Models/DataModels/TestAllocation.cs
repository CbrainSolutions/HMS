using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{

    /// <summary>
    /// Summary description for TestAllocation
    /// </summary>
    public class TestAllocation
    {

        private int _TestAllocationId;

        private System.Nullable<int> _TestId;

        public System.Nullable<int> TestInvoiceNo { get; set; }

        private System.Nullable<bool> _IsTestDone;

        private System.Nullable<int> _TestDoneBy;

        private System.Nullable<bool> _IsDelete;

        private System.Nullable<int> _PatientId;

        private System.Nullable<System.DateTime> _TestDoneDate;
        public decimal Charges { get; set; }
        public decimal Amount { get; set; }
        public decimal DiscountAmount { get; set; }
        public string PatientName { get; set; }
        public string TestName { get; set; }

        public TestAllocation()
        {

        }

        public int TestAllocationId
        {
            get
            {
                return this._TestAllocationId;
            }
            set
            {
                if ((this._TestAllocationId != value))
                {
                    this._TestAllocationId = value;
                }
            }
        }

        public System.Nullable<int> TestId
        {
            get
            {
                return this._TestId;
            }
            set
            {
                if ((this._TestId != value))
                {
                    this._TestId = value;
                }
            }
        }

        public System.Nullable<bool> IsTestDone
        {
            get
            {
                return this._IsTestDone;
            }
            set
            {
                if ((this._IsTestDone != value))
                {
                    this._IsTestDone = value;
                }
            }
        }

        public System.Nullable<int> TestDoneBy
        {
            get
            {
                return this._TestDoneBy;
            }
            set
            {
                if ((this._TestDoneBy != value))
                {
                    this._TestDoneBy = value;
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

        public System.Nullable<int> AdmitId
        {
            get
            {
                return this._PatientId;
            }
            set
            {
                if ((this._PatientId != value))
                {
                    this._PatientId = value;
                }
            }
        }

        public System.Nullable<System.DateTime> TestDoneDate
        {
            get
            {
                return this._TestDoneDate;
            }
            set
            {
                if ((this._TestDoneDate != value))
                {
                    this._TestDoneDate = value;
                }
            }
        }


    }
}