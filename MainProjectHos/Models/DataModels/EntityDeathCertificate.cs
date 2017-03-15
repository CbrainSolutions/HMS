using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityDeathCertificate
    /// </summary>
    public class EntityDeathCertificate
    {
        public EntityDeathCertificate()
        {

        }

        private int _DeathId;

        private int _PatientAdmitId;

        private System.DateTime _Death_Date;

        private System.DateTime _Death_Time;

        private string _Death_Reason;

        private bool _IsDelete;

        public string FullName { get; set; }

        public int DeathId
        {
            get
            {
                return this._DeathId;
            }
            set
            {
                if ((this._DeathId != value))
                {
                    this._DeathId = value;
                }
            }
        }

        public int PatientAdmitId
        {
            get
            {
                return this._PatientAdmitId;
            }
            set
            {
                if ((this._PatientAdmitId != value))
                {
                    this._PatientAdmitId = value;
                }
            }
        }

        public System.DateTime Death_Date
        {
            get
            {
                return this._Death_Date;
            }
            set
            {
                if ((this._Death_Date != value))
                {
                    this._Death_Date = value;
                }
            }
        }

        public System.DateTime Death_Time
        {
            get
            {
                return this._Death_Time;
            }
            set
            {
                if ((this._Death_Time != value))
                {
                    this._Death_Time = value;
                }
            }
        }

        public string Death_Reason
        {
            get
            {
                return this._Death_Reason;
            }
            set
            {
                if ((this._Death_Reason != value))
                {
                    this._Death_Reason = value;
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