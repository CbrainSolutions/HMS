using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityICUInvoice
    /// </summary>
    public class EntityICUInvoice
    {
        private int _ICUBedAllocId;

        private int _BedId;

        private int _FloorId;

        public DateTime ShiftDate { get; set; }

        private int _RoomId;

        public bool Is_ShiftIPD { get; set; }

        private System.Nullable<int> _DocId;

        private System.Nullable<int> _PatientId;

        private System.Nullable<System.DateTime> _AllocationDate;

        private System.Nullable<System.DateTime> _DischargeDate;

        private bool _IsDelete;

        private System.Nullable<decimal> _NetAmount;

        private System.Nullable<decimal> _TotalAmount;

        private System.Nullable<int> _Discount;

        private System.Nullable<decimal> _Tax1;

        private System.Nullable<decimal> _Tax2;


        public bool? IsCash { get; set; }

        public bool? IsDischarge { get; set; }

        public int ICUBedAllocId
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

        public int BedId
        {
            get
            {
                return this._BedId;
            }
            set
            {
                if ((this._BedId != value))
                {
                    this._BedId = value;
                }
            }
        }

        public int FloorId
        {
            get
            {
                return this._FloorId;
            }
            set
            {
                if ((this._FloorId != value))
                {
                    this._FloorId = value;
                }
            }
        }

        public int RoomId
        {
            get
            {
                return this._RoomId;
            }
            set
            {
                if ((this._RoomId != value))
                {
                    this._RoomId = value;
                }
            }
        }

        public System.Nullable<int> DocId
        {
            get
            {
                return this._DocId;
            }
            set
            {
                if ((this._DocId != value))
                {
                    this._DocId = value;
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

        public System.Nullable<System.DateTime> AllocationDate
        {
            get
            {
                return this._AllocationDate;
            }
            set
            {
                if ((this._AllocationDate != value))
                {
                    this._AllocationDate = value;
                }
            }
        }

        public System.Nullable<System.DateTime> DischargeDate
        {
            get
            {
                return this._DischargeDate;
            }
            set
            {
                if ((this._DischargeDate != value))
                {
                    this._DischargeDate = value;
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

        public System.Nullable<int> Discount
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

        public System.Nullable<decimal> Tax1
        {
            get
            {
                return this._Tax1;
            }
            set
            {
                if ((this._Tax1 != value))
                {
                    this._Tax1 = value;
                }
            }
        }

        public System.Nullable<decimal> Tax2
        {
            get
            {
                return this._Tax2;
            }
            set
            {
                if ((this._Tax2 != value))
                {
                    this._Tax2 = value;
                }
            }
        }

        public int InvoiceNo { get; set; }
    }
}