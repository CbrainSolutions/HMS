using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityPathology
    /// </summary>
    public class EntityPathology
    {
        public EntityPathology()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private int _LabId;

        private int _PatientId;

        private int _TestId;

        private System.DateTime _TestDate;

        private bool _IsDelete;
        public int TestParaId { get; set; }
        public string TestParaName { get; set; }
        public decimal Min { get; set; }
        public decimal Max { get; set; }
        public int Unit { get; set; }
        public int TestMethodId { get; set; }
        public string TestMethodName { get; set; }
        public string unitname { get; set; }
        public string PatientName { get; set; }
        public string TestName { get; set; }
        public string FinalComment { get; set; }
        public int TestDoneById { get; set; }
        public int LabDetailId { get; set; }
        public decimal Result { get; set; }
        public string Comment { get; set; }
        public int LabId
        {
            get
            {
                return this._LabId;
            }
            set
            {
                if ((this._LabId != value))
                {
                    this._LabId = value;
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

        public int TestId
        {
            get
            {
                return this._TestId;
            }
            set
            {
                if ((this._TestId != value))
                {
                    this._TestId = value;
                }
            }
        }

        public System.DateTime TestDate
        {
            get
            {
                return this._TestDate;
            }
            set
            {
                if ((this._TestDate != value))
                {
                    this._TestDate = value;
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