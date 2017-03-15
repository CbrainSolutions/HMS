using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{

    /// <summary>
    /// Summary description for EntityICUInvoiceDetail
    /// </summary>
    public class EntityICUInvoiceDetail
    {
        public EntityICUInvoiceDetail()
        {

        }

        public string ChargesName { get; set; }

        public decimal Charges { get; set; }

        public int Quantity { get; set; }

        public int NoofDays { get; set; }

        public string PatientName { get; set; }

        public decimal? Discount { get; set; }

        public decimal? Total { get; set; }

        public decimal? NetAmount { get; set; }

        public decimal? Vat { get; set; }

        public decimal? Service { get; set; }
        //
        private int _ICUSRlNo;

        private System.Nullable<int> _ICUBedAllocId;

        private System.Nullable<int> _ChargesId;

        private System.Nullable<decimal> _Amount;

        private System.Nullable<bool> _IsDelete;



        public int ICUSRlNo
        {
            get
            {
                return this._ICUSRlNo;
            }
            set
            {
                if ((this._ICUSRlNo != value))
                {
                    this._ICUSRlNo = value;
                }
            }
        }

        public System.Nullable<int> ICUBedAllocId
        {
            get
            {
                return this._ICUBedAllocId;
            }
            set
            {
                if ((this._ICUBedAllocId != value))
                {
                    this._ICUBedAllocId = value;
                }
            }
        }

        public System.Nullable<int> ChargesId
        {
            get
            {
                return this._ChargesId;
            }
            set
            {
                if ((this._ChargesId != value))
                {
                    this._ChargesId = value;
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

        public int TempId { get; set; }
    }
}