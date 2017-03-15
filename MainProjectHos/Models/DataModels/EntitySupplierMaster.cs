using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntitySupplierMaster
    /// </summary>
    public class EntitySupplierMaster
    {
        public EntitySupplierMaster()
        {

        }

        private int _PKId;

        private string _SupplierCode;

        private string _SupplierName;

        private string _Address;

        private string _PhoneNo;

        private string _MobileNo;

        private string _VATCSTNo;

        private string _ExciseNo;

        private string _Email;

        private string _ServiceTaxNo;

        private decimal _TotalInwardAmnt;

        private decimal _TotCumInwardAmnt;

        private string _EntryBy;

        private System.Nullable<System.DateTime> _EntryDate;

        private string _ChangeBy;

        private System.Nullable<System.DateTime> _ChangeDate;

        private bool _IsDelete;

        public int PKId
        {
            get
            {
                return this._PKId;
            }
            set
            {
                if ((this._PKId != value))
                {
                    this._PKId = value;
                }
            }
        }

        public string SupplierCode
        {
            get
            {
                return this._SupplierCode;
            }
            set
            {
                if ((this._SupplierCode != value))
                {
                    this._SupplierCode = value;
                }
            }
        }

        public string SupplierName
        {
            get
            {
                return this._SupplierName;
            }
            set
            {
                if ((this._SupplierName != value))
                {
                    this._SupplierName = value;
                }
            }
        }

        public string Address
        {
            get
            {
                return this._Address;
            }
            set
            {
                if ((this._Address != value))
                {
                    this._Address = value;
                }
            }
        }

        public string PhoneNo
        {
            get
            {
                return this._PhoneNo;
            }
            set
            {
                if ((this._PhoneNo != value))
                {
                    this._PhoneNo = value;
                }
            }
        }

        public string MobileNo
        {
            get
            {
                return this._MobileNo;
            }
            set
            {
                if ((this._MobileNo != value))
                {
                    this._MobileNo = value;
                }
            }
        }

        public string VATCSTNo
        {
            get
            {
                return this._VATCSTNo;
            }
            set
            {
                if ((this._VATCSTNo != value))
                {
                    this._VATCSTNo = value;
                }
            }
        }

        public string ExciseNo
        {
            get
            {
                return this._ExciseNo;
            }
            set
            {
                if ((this._ExciseNo != value))
                {
                    this._ExciseNo = value;
                }
            }
        }

        public string Email
        {
            get
            {
                return this._Email;
            }
            set
            {
                if ((this._Email != value))
                {
                    this._Email = value;
                }
            }
        }

        public string ServiceTaxNo
        {
            get
            {
                return this._ServiceTaxNo;
            }
            set
            {
                if ((this._ServiceTaxNo != value))
                {
                    this._ServiceTaxNo = value;
                }
            }
        }

        public decimal TotalInwardAmnt
        {
            get
            {
                return this._TotalInwardAmnt;
            }
            set
            {
                if ((this._TotalInwardAmnt != value))
                {
                    this._TotalInwardAmnt = value;
                }
            }
        }

        public decimal TotCumInwardAmnt
        {
            get
            {
                return this._TotCumInwardAmnt;
            }
            set
            {
                if ((this._TotCumInwardAmnt != value))
                {
                    this._TotCumInwardAmnt = value;
                }
            }
        }

        public string EntryBy
        {
            get
            {
                return this._EntryBy;
            }
            set
            {
                if ((this._EntryBy != value))
                {
                    this._EntryBy = value;
                }
            }
        }

        public System.Nullable<System.DateTime> EntryDate
        {
            get
            {
                return this._EntryDate;
            }
            set
            {
                if ((this._EntryDate != value))
                {
                    this._EntryDate = value;
                }
            }
        }

        public string ChangeBy
        {
            get
            {
                return this._ChangeBy;
            }
            set
            {
                if ((this._ChangeBy != value))
                {
                    this._ChangeBy = value;
                }
            }
        }

        public System.Nullable<System.DateTime> ChangeDate
        {
            get
            {
                return this._ChangeDate;
            }
            set
            {
                if ((this._ChangeDate != value))
                {
                    this._ChangeDate = value;
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