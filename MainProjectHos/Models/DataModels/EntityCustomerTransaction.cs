using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityCustomerTransaction
    /// </summary>
    public class EntityCustomerTransaction
    {
        public EntityCustomerTransaction()
        {

        }
        private int _ReceiptNo;

        public int BankId { get; set; }

        public decimal? Outstanding { get; set; }

        public int SupplierId { get; set; }

        public string PatientType { get; set; }

        public string PatientCode { get; set; }

        public string PatientName { get; set; }

        public string SupplierName { get; set; }


        public string Address { get; set; }

        private System.Nullable<System.DateTime> _ReceiptDate;

        private System.Nullable<int> _PatientId;

        private System.Nullable<int> _TransactionId;

        private string _TransactionType;

        private System.Nullable<int> _TransactionDocNo;

        private System.Nullable<bool> _IsCash;

        private System.Nullable<bool> _ISCheque;

        private System.Nullable<System.DateTime> _ChequeDate;

        private string _ChequeNo;

        private string _BankName;

        private System.Nullable<decimal> _Amount;

        private System.Nullable<decimal> _PayAmount;

        private System.Nullable<decimal> _BillAmount;

        private System.Nullable<decimal> _Discount;

        private System.Nullable<bool> _IsDelete;

        //public string PatientName { get; set; }

        //public string Address { get; set; }

        public int ReceiptNo
        {
            get
            {
                return this._ReceiptNo;
            }
            set
            {
                if ((this._ReceiptNo != value))
                {
                    this._ReceiptNo = value;
                }
            }
        }

        public System.Nullable<System.DateTime> ReceiptDate
        {
            get
            {
                return this._ReceiptDate;
            }
            set
            {
                if ((this._ReceiptDate != value))
                {
                    this._ReceiptDate = value;
                }
            }
        }

        public System.Nullable<int> PatientId
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

        public System.Nullable<int> TransactionId
        {
            get
            {
                return this._TransactionId;
            }
            set
            {
                if ((this._TransactionId != value))
                {
                    this._TransactionId = value;
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

        public System.Nullable<int> TransactionDocNo
        {
            get
            {
                return this._TransactionDocNo;
            }
            set
            {
                if ((this._TransactionDocNo != value))
                {
                    this._TransactionDocNo = value;
                }
            }
        }

        public System.Nullable<bool> IsCash
        {
            get
            {
                return this._IsCash;
            }
            set
            {
                if ((this._IsCash != value))
                {
                    this._IsCash = value;
                }
            }
        }

        public System.Nullable<bool> ISCheque
        {
            get
            {
                return this._ISCheque;
            }
            set
            {
                if ((this._ISCheque != value))
                {
                    this._ISCheque = value;
                }
            }
        }

        public System.Nullable<System.DateTime> ChequeDate
        {
            get
            {
                return this._ChequeDate;
            }
            set
            {
                if ((this._ChequeDate != value))
                {
                    this._ChequeDate = value;
                }
            }
        }

        public string ChequeNo
        {
            get
            {
                return this._ChequeNo;
            }
            set
            {
                if ((this._ChequeNo != value))
                {
                    this._ChequeNo = value;
                }
            }
        }

        public string BankName
        {
            get
            {
                return this._BankName;
            }
            set
            {
                if ((this._BankName != value))
                {
                    this._BankName = value;
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

        public System.Nullable<decimal> PayAmount
        {
            get
            {
                return this._PayAmount;
            }
            set
            {
                if ((this._PayAmount != value))
                {
                    this._PayAmount = value;
                }
            }
        }

        public System.Nullable<decimal> BillAmount
        {
            get
            {
                return this._BillAmount;
            }
            set
            {
                if ((this._BillAmount != value))
                {
                    this._BillAmount = value;
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

        public int InsuranceId { get; internal set; }
        public int CompanyId { get; internal set; }
        public string CompanyName { get; internal set; }
        public string InsuranceName { get; internal set; }
        public decimal AdvanceAmount { get; internal set; }
        public bool IsCard { get; internal set; }
        public bool IsRTGS { get; internal set; }
        public string BankRefNo { get; internal set; }
        public string BillRefNo { get; internal set; }
        public string PatientCategory { get; internal set; }
        public string EmpName { get; internal set; }
        public decimal TDSAmt { get; internal set; }
    }
}