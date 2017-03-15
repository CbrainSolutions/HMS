using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MainProjectHos.Models.DataModels
{

    /// <summary>
    /// Summary description for EntityBedAllocToPatient
    /// </summary>
    public class EntityBedAllocToPatient
    {
        public EntityBedAllocToPatient()
        {

        }

        public int ShiftBedId { get; set; }

        public bool Is_ShiftTo_ICU { get; set; }

        public bool Is_ShiftTo_IPD { get; set; }

        private int _BedAllocId;

        private int _BedId;

        private int _FloorId;

        public DateTime? ShiftDate { get; set; }

        private int _RoomId;

        private System.Nullable<int> _PatientId;

        private System.Nullable<System.DateTime> _AllocationDate;

        private System.Nullable<System.DateTime> _DischargeDate;

        private bool _IsDelete;

        public string Room_No { get; set; }

        public string Floor_Name { get; set; }

        public string Full_Name { get; set; }

        public decimal Charges { get; set; }

        public string BedNo { get; set; }

        public string CategoryDesc { get; set; }

        public int consumedays { get; set; }

        public int BedAllocId
        {
            get
            {
                return this._BedAllocId;
            }
            set
            {
                if ((this._BedAllocId != value))
                {
                    this._BedAllocId = value;
                }
            }
        }

        public int BedId
        {
            get
            {
                return this._BedId;
            }
            set
            {
                if ((this._BedId != value))
                {
                    this._BedId = value;
                }
            }
        }

        public int FloorId
        {
            get
            {
                return this._FloorId;
            }
            set
            {
                if ((this._FloorId != value))
                {
                    this._FloorId = value;
                }
            }
        }

        public int RoomId
        {
            get
            {
                return this._RoomId;
            }
            set
            {
                if ((this._RoomId != value))
                {
                    this._RoomId = value;
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

        public System.Nullable<System.DateTime> AllocationDate
        {
            get
            {
                return this._AllocationDate;
            }
            set
            {
                if ((this._AllocationDate != value))
                {
                    this._AllocationDate = value;
                }
            }
        }

        public System.Nullable<System.DateTime> DischargeDate
        {
            get
            {
                return this._DischargeDate;
            }
            set
            {
                if ((this._DischargeDate != value))
                {
                    this._DischargeDate = value;
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