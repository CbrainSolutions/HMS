using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityMakeDischarge
    /// </summary>
    public class EntityMakeDischarge
    {
        public EntityMakeDischarge()
        {
        }
        private string _HB;

        private string _TLC;

        private string _PLC;

        private string _BUL;

        private string _Creat;

        private string _SE;

        private string _BSL;

        private string _PP;

        private string _R;

        private string _Urine;

        private string _HIV;

        private string _HBSAG;


        private int _DichargeId;

        private int _PatientId;

        private string _TypeOfDischarge;

        private string _Diagnosis;

        private string _Surgery;

        private string _HistoryClinical;

        private string _XRay;

        private string _USG;

        private string _Others;

        private string _OperativeNotes;

        private string _TreatmentInHospitalisation;

        private string _TreatmentOnDischarge;

        private string _FollowUp;

        private System.Nullable<System.DateTime> _DischargeReceiptDate;

        private bool _IsDelete;

        public string PatName { get; set; }

        public string MRN { get; set; }

        public int DichargeId
        {
            get
            {
                return this._DichargeId;
            }
            set
            {
                if ((this._DichargeId != value))
                {
                    this._DichargeId = value;
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



        public string TypeOfDischarge
        {
            get
            {
                return this._TypeOfDischarge;
            }
            set
            {
                if ((this._TypeOfDischarge != value))
                {
                    this._TypeOfDischarge = value;
                }
            }
        }

        public string Diagnosis
        {
            get
            {
                return this._Diagnosis;
            }
            set
            {
                if ((this._Diagnosis != value))
                {
                    this._Diagnosis = value;
                }
            }
        }

        public string Surgery
        {
            get
            {
                return this._Surgery;
            }
            set
            {
                if ((this._Surgery != value))
                {
                    this._Surgery = value;
                }
            }
        }

        public string HistoryClinical
        {
            get
            {
                return this._HistoryClinical;
            }
            set
            {
                if ((this._HistoryClinical != value))
                {
                    this._HistoryClinical = value;
                }
            }
        }

        public string XRay
        {
            get
            {
                return this._XRay;
            }
            set
            {
                if ((this._XRay != value))
                {
                    this._XRay = value;
                }
            }
        }

        public string USG
        {
            get
            {
                return this._USG;
            }
            set
            {
                if ((this._USG != value))
                {
                    this._USG = value;
                }
            }
        }

        public string Others
        {
            get
            {
                return this._Others;
            }
            set
            {
                if ((this._Others != value))
                {
                    this._Others = value;
                }
            }
        }

        public string OperativeNotes
        {
            get
            {
                return this._OperativeNotes;
            }
            set
            {
                if ((this._OperativeNotes != value))
                {
                    this._OperativeNotes = value;
                }
            }
        }

        public string TreatmentInHospitalisation
        {
            get
            {
                return this._TreatmentInHospitalisation;
            }
            set
            {
                if ((this._TreatmentInHospitalisation != value))
                {
                    this._TreatmentInHospitalisation = value;
                }
            }
        }

        public string TreatmentOnDischarge
        {
            get
            {
                return this._TreatmentOnDischarge;
            }
            set
            {
                if ((this._TreatmentOnDischarge != value))
                {
                    this._TreatmentOnDischarge = value;
                }
            }
        }

        //[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FollowUp", DbType = "VarChar(MAX)")]
        public string FollowUp
        {
            get
            {
                return this._FollowUp;
            }
            set
            {
                if ((this._FollowUp != value))
                {
                    this._FollowUp = value;
                }
            }
        }

        public System.Nullable<System.DateTime> DischargeReceiptDate
        {
            get
            {
                return this._DischargeReceiptDate;
            }
            set
            {
                if ((this._DischargeReceiptDate != value))
                {
                    this._DischargeReceiptDate = value;
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

        public string HB
        {
            get
            {
                return this._HB;
            }
            set
            {
                if ((this._HB != value))
                {
                    this._HB = value;
                }
            }
        }

        public string TLC
        {
            get
            {
                return this._TLC;
            }
            set
            {
                if ((this._TLC != value))
                {
                    this._TLC = value;
                }
            }
        }

        public string PLC
        {
            get
            {
                return this._PLC;
            }
            set
            {
                if ((this._PLC != value))
                {
                    this._PLC = value;
                }
            }
        }

        public string BUL
        {
            get
            {
                return this._BUL;
            }
            set
            {
                if ((this._BUL != value))
                {
                    this._BUL = value;
                }
            }
        }

        public string Creat
        {
            get
            {
                return this._Creat;
            }
            set
            {
                if ((this._Creat != value))
                {
                    this._Creat = value;
                }
            }
        }

        public string SE
        {
            get
            {
                return this._SE;
            }
            set
            {
                if ((this._SE != value))
                {
                    this._SE = value;
                }
            }
        }

        public string BSL
        {
            get
            {
                return this._BSL;
            }
            set
            {
                if ((this._BSL != value))
                {
                    this._BSL = value;
                }
            }
        }

        public string PP
        {
            get
            {
                return this._PP;
            }
            set
            {
                if ((this._PP != value))
                {
                    this._PP = value;
                }
            }
        }

        public string R
        {
            get
            {
                return this._R;
            }
            set
            {
                if ((this._R != value))
                {
                    this._R = value;
                }
            }
        }

        public string Urine
        {
            get
            {
                return this._Urine;
            }
            set
            {
                if ((this._Urine != value))
                {
                    this._Urine = value;
                }
            }
        }

        public string HIV
        {
            get
            {
                return this._HIV;
            }
            set
            {
                if ((this._HIV != value))
                {
                    this._HIV = value;
                }
            }
        }

        public string HBSAG
        {
            get
            {
                return this._HBSAG;
            }
            set
            {
                if ((this._HBSAG != value))
                {
                    this._HBSAG = value;
                }
            }
        }

        public object NameOfSurgery { get; internal set; }
        public string OperationalProcedure { get; internal set; }
        public string Haemogram { get; internal set; }
        public string AdviceOnDischarge { get; internal set; }
        public string ECG { get; internal set; }
        public string SCreat { get; internal set; }
        public string SElect { get; internal set; }
        public string UrineR { get; internal set; }
        public string Temp { get; internal set; }
        public string Pulse { get; internal set; }
        public string BP { get; internal set; }
        public string RespRate { get; internal set; }
        public string Pallor { get; internal set; }
        public string Oedema { get; internal set; }
        public string Cyanosis { get; internal set; }
        public string Clubbing { get; internal set; }
        public string Icterus { get; internal set; }
        public string Skin { get; internal set; }
        public string RespSystem { get; internal set; }
        public string CNS { get; internal set; }
        public string PerAbd { get; internal set; }
        public string CVS { get; internal set; }
        public int SurgeryId { get; internal set; }
        public string PreparedByName { get; internal set; }
    }
}