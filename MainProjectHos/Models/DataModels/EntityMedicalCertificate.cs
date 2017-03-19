using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    public class EntityMedicalCertificate
    {
        public EntityMedicalCertificate()
        {

        }

        private int _CertiId;

        private int _PatientAdmitID;

        private int _Age;
        private string _AdvisedRestDays;
        private string _ContinuedRestDays;

        private string _Daignosis;

        private string _OperatedFor;

        private System.DateTime _OPDFrom;

        private System.DateTime _OPDTo;
        private System.DateTime _IndoorOn;

        private System.DateTime _DischargeOn;
        private System.DateTime _OperatedForOn;

        private System.DateTime _AdvisedRestFrom;

        private System.DateTime _ContinueRestFrom;

        private System.DateTime _WorkFrom;

        private bool _IsDelete;


        public string FullName { get; set; }

        public int CertiId
        {
            get
            {
                return this._CertiId;
            }
            set
            {
                if ((this._CertiId != value))
                {
                    this._CertiId = value;
                }
            }
        }

        public int Age
        {
            get
            {
                return this._Age;
            }
            set
            {
                if ((this._Age != value))
                {
                    this._Age = value;
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

        public string Daignosis
        {
            get
            {
                return this._Daignosis;
            }
            set
            {
                if ((this._Daignosis != value))
                {
                    this._Daignosis = value;
                }
            }
        }

        public string AdvisedRestDays
        {
            get
            {
                return this._AdvisedRestDays;
            }
            set
            {
                if ((this._AdvisedRestDays != value))
                {
                    this._AdvisedRestDays = value;
                }
            }
        }
        public string ContinuedRestDays
        {
            get
            {
                return this._ContinuedRestDays;
            }
            set
            {
                if ((this._ContinuedRestDays != value))
                {
                    this._ContinuedRestDays = value;
                }
            }
        }

        public string OperatedFor
        {
            get
            {
                return this._OperatedFor;
            }
            set
            {
                if ((this._OperatedFor != value))
                {
                    this._OperatedFor = value;
                }
            }
        }

        public System.DateTime OPDFrom
        {
            get
            {
                return this._OPDFrom;
            }
            set
            {
                if ((this._OPDFrom != value))
                {
                    this._OPDFrom = value;
                }
            }
        }

        public System.DateTime OPDTo
        {
            get
            {
                return this._OPDTo;
            }
            set
            {
                if ((this._OPDTo != value))
                {
                    this._OPDTo = value;
                }
            }
        }

        public System.DateTime IndoorOn
        {
            get
            {
                return this._IndoorOn;
            }
            set
            {
                if ((this._IndoorOn != value))
                {
                    this._IndoorOn = value;
                }
            }
        }

        public System.DateTime DischargeOn
        {
            get
            {
                return this._DischargeOn;
            }
            set
            {
                if ((this._DischargeOn != value))
                {
                    this._DischargeOn = value;
                }
            }
        }

        public System.DateTime OperatedForOn
        {
            get
            {
                return this._OperatedForOn;
            }
            set
            {
                if ((this._OperatedForOn != value))
                {
                    this._OperatedForOn = value;
                }
            }
        }

        public System.DateTime AdvisedRestFrom
        {
            get
            {
                return this._AdvisedRestFrom;
            }
            set
            {
                if ((this._AdvisedRestFrom != value))
                {
                    this._AdvisedRestFrom = value;
                }
            }
        }

        public System.DateTime ContinueRestFrom
        {
            get
            {
                return this._ContinueRestFrom;
            }
            set
            {
                if ((this._ContinueRestFrom != value))
                {
                    this._ContinueRestFrom = value;
                }
            }
        }

        public System.DateTime WorkFrom
        {
            get
            {
                return this._WorkFrom;
            }
            set
            {
                if ((this._WorkFrom != value))
                {
                    this._WorkFrom = value;
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