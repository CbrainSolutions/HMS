using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityBirthCertificate
    /// </summary>
    public class EntityBirthCertificate
    {
        public EntityBirthCertificate()
        {

        }

        private int _BirthID;

        private int _PatientAdmitID;

        private int _GenderID;

        private string _GrandFatherName;

        private string _ChildName;

        private System.DateTime _BirthDate;

        private System.DateTime _BirthTime;

        private decimal _Height;

        private decimal _Weight;

        private bool _IsDelete;

        public string GenderDesc { get; set; }

        public string FullName { get; set; }

        public int BirthID
        {
            get
            {
                return this._BirthID;
            }
            set
            {
                if ((this._BirthID != value))
                {
                    this._BirthID = value;
                }
            }
        }

        public int PatientAdmitID
        {
            get
            {
                return this._PatientAdmitID;
            }
            set
            {
                if ((this._PatientAdmitID != value))
                {
                    this._PatientAdmitID = value;
                }
            }
        }

        public int GenderID
        {
            get
            {
                return this._GenderID;
            }
            set
            {
                if ((this._GenderID != value))
                {
                    this._GenderID = value;
                }
            }
        }

        public string GrandFatherName
        {
            get
            {
                return this._GrandFatherName;
            }
            set
            {
                if ((this._GrandFatherName != value))
                {
                    this._GrandFatherName = value;
                }
            }
        }

        public string ChildName
        {
            get
            {
                return this._ChildName;
            }
            set
            {
                if ((this._ChildName != value))
                {
                    this._ChildName = value;
                }
            }
        }

        public System.DateTime BirthDate
        {
            get
            {
                return this._BirthDate;
            }
            set
            {
                if ((this._BirthDate != value))
                {
                    this._BirthDate = value;
                }
            }
        }

        public System.DateTime BirthTime
        {
            get
            {
                return this._BirthTime;
            }
            set
            {
                if ((this._BirthTime != value))
                {
                    this._BirthTime = value;
                }
            }
        }

        public decimal Height
        {
            get
            {
                return this._Height;
            }
            set
            {
                if ((this._Height != value))
                {
                    this._Height = value;
                }
            }
        }

        public decimal Weight
        {
            get
            {
                return this._Weight;
            }
            set
            {
                if ((this._Weight != value))
                {
                    this._Weight = value;
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