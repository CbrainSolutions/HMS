using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityPatientAlloc
    /// </summary>
    public class EntityPatientAlloc
    {
        public EntityPatientAlloc()
        {

        }

        public DateTime AdmitDate { get; set; }

        private int _PateintAllocId;

        private int _PatientId;

        private int _DocId;

        private System.DateTime _AppDate;

        private bool _IsDelete;

        public string PatType { get; set; }

        public decimal Charges { get; set; }

        public int PateintAllocId
        {
            get
            {
                return this._PateintAllocId;
            }
            set
            {
                if ((this._PateintAllocId != value))
                {
                    this._PateintAllocId = value;
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

        public int DocId
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

        public System.DateTime AppDate
        {
            get
            {
                return this._AppDate;
            }
            set
            {
                if ((this._AppDate != value))
                {
                    this._AppDate = value;
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