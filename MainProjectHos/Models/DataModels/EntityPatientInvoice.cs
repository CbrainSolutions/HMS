using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityPatientInvoice
    /// </summary>
    public class EntityPatientInvoice
    {
        public EntityPatientInvoice()
        {

        }

        public bool IsFixed { get; set; }

        public decimal FixedDiscount { get; set; }

        private int _BillNo;

        private System.DateTime _BillDate;

        private int _PatientId;

        private System.Nullable<decimal> _NetAmount;

        private System.Nullable<decimal> _Amount;

        private System.Nullable<decimal> _Discount;

        private System.Nullable<bool> _IsDelete;

        private System.Nullable<decimal> _Vat;

        private System.Nullable<decimal> _Service;

        public string Description { get; set; }

        public int BillNo
        {
            get
            {
                return this._BillNo;
            }
            set
            {
                if ((this._BillNo != value))
                {
                    this._BillNo = value;
                }
            }
        }

        public System.DateTime BillDate
        {
            get
            {
                return this._BillDate;
            }
            set
            {
                if ((this._BillDate != value))
                {
                    this._BillDate = value;
                }
            }
        }

        public int PatientId
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

        public System.Nullable<decimal> Discount
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

        public System.Nullable<decimal> Vat
        {
            get
            {
                return this._Vat;
            }
            set
            {
                if ((this._Vat != value))
                {
                    this._Vat = value;
                }
            }
        }

        public System.Nullable<decimal> Service
        {
            get
            {
                return this._Service;
            }
            set
            {
                if ((this._Service != value))
                {
                    this._Service = value;
                }
            }
        }

        public DateTime AllocDate { get; set; }

        public int NoOfDays { get; set; }

        public int OtherID { get; set; }

        public int BedAllocId { get; set; }

        public int DocAllocId { get; set; }

        public int OTBedAllocId { get; set; }

        public bool IsCash { get; set; }
        public string BillType { get; set; }
        public string PreparedByName { get; set; }
        public decimal TotalAdvance { get; set; }
        public decimal BalanceAmount { get; set; }
        public decimal ReceivedAmount { get; set; }
        public decimal RefundAmount { get; set; }
    }
}