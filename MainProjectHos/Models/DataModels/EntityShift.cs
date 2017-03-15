using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityShift
    /// </summary>
    public class EntityShift
    {
        public EntityShift()
        {

        }

        private int _ShiftId;

        private string _ShiftName;

        private System.DateTime _StartTime;

        private System.DateTime _EndTime;

        private bool _IsDelete;

        public int EmpID { get; set; }

        public int ShiftId
        {
            get
            {
                return this._ShiftId;
            }
            set
            {
                if ((this._ShiftId != value))
                {
                    this._ShiftId = value;
                }
            }
        }

        public string ShiftName
        {
            get
            {
                return this._ShiftName;
            }
            set
            {
                if ((this._ShiftName != value))
                {
                    this._ShiftName = value;
                }
            }
        }

        public System.DateTime StartTime
        {
            get
            {
                return this._StartTime;
            }
            set
            {
                if ((this._StartTime != value))
                {
                    this._StartTime = value;
                }
            }
        }

        public System.DateTime EndTime
        {
            get
            {
                return this._EndTime;
            }
            set
            {
                if ((this._EndTime != value))
                {
                    this._EndTime = value;
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