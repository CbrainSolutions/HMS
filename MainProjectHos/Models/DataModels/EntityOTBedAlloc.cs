using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityOtBedAlloc
    /// </summary>
    public class EntityOTBedAlloc
    {
        public EntityOTBedAlloc()
        {

        }

        public int AssistantId { get; set; }

        public int AnestheticId { get; set; }

        private int _OTBedAllocId;

        private int _BedId;

        private int _FloorId;

        private int _RoomId;

        private System.Nullable<int> _OperCatId;

        private System.Nullable<int> _DocId;

        private System.Nullable<int> _PatientId;

        private System.Nullable<System.DateTime> _AllocationDate;

        private System.Nullable<System.DateTime> _DischargeDate;

        private bool _IsDelete;

        public int OperationId { get; set; }

        public int OTBedAllocId
        {
            get
            {
                return this._OTBedAllocId;
            }
            set
            {
                if ((this._OTBedAllocId != value))
                {
                    this._OTBedAllocId = value;
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

        public System.Nullable<int> OperCatId
        {
            get
            {
                return this._OperCatId;
            }
            set
            {
                if ((this._OperCatId != value))
                {
                    this._OperCatId = value;
                }
            }
        }

        public System.Nullable<int> DocId
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

        public string TypeOfAnaesthetist { get; internal set; }
        public string Implant { get; internal set; }
        public string MaterialHPE { get; internal set; }
        public string Surgeon { get; internal set; }
        public string AnaestheticNote { get; internal set; }
        public string SurgeryNote { get; internal set; }
    }
}