using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityBankMaster
    /// </summary>
    public class EntityBankMaster
    {
        public EntityBankMaster()
        {

        }

        private int _BankId;

        private string _BankName;

        private string _BankAddress;

        private string _BranchName;

        private string _IFSCCode;

        private string _MISCCode;

        private string _AccountNo;

        private string _City;

        private string _Pin;

        private string _CustomerId;

        private string _PhNo;

        private string _MobileNo;

        private System.Nullable<bool> _IsDelete;


        public int BankId
        {
            get
            {
                return this._BankId;
            }
            set
            {
                if ((this._BankId != value))
                {
                    this._BankId = value;
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

        public string BankAddress
        {
            get
            {
                return this._BankAddress;
            }
            set
            {
                if ((this._BankAddress != value))
                {
                    this._BankAddress = value;
                }
            }
        }

        public string BranchName
        {
            get
            {
                return this._BranchName;
            }
            set
            {
                if ((this._BranchName != value))
                {
                    this._BranchName = value;
                }
            }
        }

        public string IFSCCode
        {
            get
            {
                return this._IFSCCode;
            }
            set
            {
                if ((this._IFSCCode != value))
                {
                    this._IFSCCode = value;
                }
            }
        }

        public string MISCCode
        {
            get
            {
                return this._MISCCode;
            }
            set
            {
                if ((this._MISCCode != value))
                {
                    this._MISCCode = value;
                }
            }
        }

        public string AccountNo
        {
            get
            {
                return this._AccountNo;
            }
            set
            {
                if ((this._AccountNo != value))
                {
                    this._AccountNo = value;
                }
            }
        }

        public string City
        {
            get
            {
                return this._City;
            }
            set
            {
                if ((this._City != value))
                {
                    this._City = value;
                }
            }
        }

        public string Pin
        {
            get
            {
                return this._Pin;
            }
            set
            {
                if ((this._Pin != value))
                {
                    this._Pin = value;
                }
            }
        }

        public string CustomerId
        {
            get
            {
                return this._CustomerId;
            }
            set
            {
                if ((this._CustomerId != value))
                {
                    this._CustomerId = value;
                }
            }
        }

        public string PhNo
        {
            get
            {
                return this._PhNo;
            }
            set
            {
                if ((this._PhNo != value))
                {
                    this._PhNo = value;
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
    }
}