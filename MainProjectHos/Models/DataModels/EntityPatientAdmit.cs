using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityPatientAdmit
    /// </summary>
    public class EntityPatientAdmit
    {
        public EntityPatientAdmit()
        {

        }

        //

        public int Age { get; set; }
        public string AgeIn { get; set; }

        public string CompanyName { get; set; }
        public string PatientType { get; set; }

        public string PatientFirstName { get; set; }

        public string Dignosys { get; set; }
        public string IPDNo { get; set; }

        private int _AdmitId;

        private System.Nullable<System.DateTime> _AdmitDate;

        private System.Nullable<int> _PatientId;

        private System.Nullable<bool> _IsOPD;

        private System.Nullable<bool> _IsIPD;

        private System.Nullable<bool> _IsInsured;

        private System.Nullable<bool> _IsCompany;

        private System.Nullable<bool> _IsDelete;

        private System.Nullable<bool> _IsDischarge;

        private System.Nullable<int> _CompanyId;

        private System.Nullable<int> _InsuranceComId;


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

        public System.Nullable<System.DateTime> AdmitDate
        {
            get
            {
                return this._AdmitDate;
            }
            set
            {
                if ((this._AdmitDate != value))
                {
                    this._AdmitDate = value;
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

        public System.Nullable<bool> IsOPD
        {
            get
            {
                return this._IsOPD;
            }
            set
            {
                if ((this._IsOPD != value))
                {
                    this._IsOPD = value;
                }
            }
        }

        public System.Nullable<bool> IsIPD
        {
            get
            {
                return this._IsIPD;
            }
            set
            {
                if ((this._IsIPD != value))
                {
                    this._IsIPD = value;
                }
            }
        }

        public System.Nullable<bool> IsInsured
        {
            get
            {
                return this._IsInsured;
            }
            set
            {
                if ((this._IsInsured != value))
                {
                    this._IsInsured = value;
                }
            }
        }

        public System.Nullable<bool> IsCompany
        {
            get
            {
                return this._IsCompany;
            }
            set
            {
                if ((this._IsCompany != value))
                {
                    this._IsCompany = value;
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

        public System.Nullable<bool> IsDischarge
        {
            get
            {
                return this._IsDischarge;
            }
            set
            {
                if ((this._IsDischarge != value))
                {
                    this._IsDischarge = value;
                }
            }
        }

        public System.Nullable<int> CompanyId
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

        public System.Nullable<int> InsuranceComId
        {
            get
            {
                return this._InsuranceComId;
            }
            set
            {
                if ((this._InsuranceComId != value))
                {
                    this._InsuranceComId = value;
                }
            }
        }

        public string PatientAdmitTime { get; set; }
        public int DeptCategory { get; set; }
        public int DeptDoctorId { get; set; }
        public string OPDNo { get; set; }
    }
}