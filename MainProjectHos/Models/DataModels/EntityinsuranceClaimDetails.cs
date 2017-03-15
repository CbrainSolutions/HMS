using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityinsuranceClaimDetails
    /// </summary>
    public class EntityinsuranceClaimDetails
    {
        public EntityinsuranceClaimDetails()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int TempId { get; set; }

        private int _ClaimDetailId;

        private int _ClaimId;

        private int _BillNo;

        private string _BillType;

        private System.DateTime _BillDate;

        private decimal _BillAmount;

        private bool _IsOtherBill;

        private bool _IsDelete;

        public int ComClaimDetailId { get; set; }
        public int ComClaimId { get; set; }

        public int ClaimDetailId
        {
            get
            {
                return this._ClaimDetailId;
            }
            set
            {
                if ((this._ClaimDetailId != value))
                {
                    this._ClaimDetailId = value;
                }
            }
        }

        public int ClaimId
        {
            get
            {
                return this._ClaimId;
            }
            set
            {
                if ((this._ClaimId != value))
                {
                    this._ClaimId = value;
                }
            }
        }

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

        public string BillType
        {
            get
            {
                return this._BillType;
            }
            set
            {
                if ((this._BillType != value))
                {
                    this._BillType = value;
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

        public decimal BillAmount
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

        public bool IsOtherBill
        {
            get
            {
                return this._IsOtherBill;
            }
            set
            {
                if ((this._IsOtherBill != value))
                {
                    this._IsOtherBill = value;
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