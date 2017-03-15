using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityInsuranceClaim
    /// </summary>
    public class EntityInsuranceClaim
    {
        public EntityInsuranceClaim()
        {
        }
        private int _ClaimId;

        private int _AdmitId;

        private int _CompanyId;

        private System.DateTime _ClaimDate;

        private System.DateTime _ApprovedDate;

        private decimal? _ApprovedAmount;

        private decimal _Total;

        private bool _IsApproved;

        private bool _IsDelete;
        public string Category { get; set; }
        public string PatientName { get; set; }
        public string CompanyName { get; set; }
        public int ComClaimId { get; set; }
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

        public int AdmitId
        {
            get
            {
                return this._AdmitId;
            }
            set
            {
                if ((this._AdmitId != value))
                {
                    this._AdmitId = value;
                }
            }
        }

        public int CompanyId
        {
            get
            {
                return this._CompanyId;
            }
            set
            {
                if ((this._CompanyId != value))
                {
                    this._CompanyId = value;
                }
            }
        }

        public System.DateTime ClaimDate
        {
            get
            {
                return this._ClaimDate;
            }
            set
            {
                if ((this._ClaimDate != value))
                {
                    this._ClaimDate = value;
                }
            }
        }

        public System.DateTime ApprovedDate
        {
            get
            {
                return this._ApprovedDate;
            }
            set
            {
                if ((this._ApprovedDate != value))
                {
                    this._ApprovedDate = value;
                }
            }
        }

        public decimal? ApprovedAmount
        {
            get
            {
                return this._ApprovedAmount;
            }
            set
            {
                if ((this._ApprovedAmount != value))
                {
                    this._ApprovedAmount = value;
                }
            }
        }

        public decimal Total
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

        public bool IsApproved
        {
            get
            {
                return this._IsApproved;
            }
            set
            {
                if ((this._IsApproved != value))
                {
                    this._IsApproved = value;
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