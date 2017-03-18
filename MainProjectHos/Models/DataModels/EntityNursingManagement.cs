using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{

    public class EntityNursingManagement
    {
        public EntityNursingManagement()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private bool _IsDelete;
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

        #region "Properties"
        public int SrNo { get; set; }
        public int NurseId { get; set; }
        public string NurseName { get; set; }
        public string Department { get; set; }
        public DateTime TreatmentDate { get; set; }
        public int CategoryId { get; set; }
        public string CategoryDesc { get; set; }

        #endregion
    }

    public class EntityNursingManagementDetails
    {
        public EntityNursingManagementDetails()
        {
        }
        public int TempId { get; set; }

        private int _SrDetailId;

        private System.Nullable<int> _SrNo;

        private bool _IsDelete;

        public int SrDetailId
        {
            get
            {
                return this._SrDetailId;
            }
            set
            {
                if ((this._SrDetailId != value))
                {
                    this._SrDetailId = value;
                }
            }
        }

        public System.Nullable<int> SrNo
        {
            get
            {
                return this._SrNo;
            }
            set
            {
                if ((this._SrNo != value))
                {
                    this._SrNo = value;
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

        public int NurseId { get; set; }
        public string NurseName { get; set; }
        public string Department { get; set; }
        public DateTime TreatmentDate { get; set; }
        public int CategoryId { get; set; }
        public string CategoryDesc { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string TreatmentTime { get; set; }
        public string InjectableMedications { get; set; }
        public string Infusions { get; set; }
        public string Oral { get; set; }
        public string NursingCare { get; set; }

    }
}