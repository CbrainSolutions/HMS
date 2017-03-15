using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityinsuranceClaimDetails
    /// </summary>
    public class EntityPrescriptionDetails
    {
        public EntityPrescriptionDetails()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int TempId { get; set; }
        public int MedicineId { get; set; }

        private int _PrescriptionDetailId;

        private System.Nullable<int> _Prescription_Id;

        private string _MedicineName;

        private string _Morning;

        private string _Afternoon;

        private string _Night;

        private string _NoOfDays;

        private string _Quantity;

        private System.Nullable<bool> _IsDressing;

        private System.Nullable<bool> _IsInjection;

        private string _InjectionName;

        private bool _IsDelete;

        public int PrescriptionDetailId
        {
            get
            {
                return this._PrescriptionDetailId;
            }
            set
            {
                if ((this._PrescriptionDetailId != value))
                {
                    this._PrescriptionDetailId = value;
                }
            }
        }

        public System.Nullable<int> Prescription_Id
        {
            get
            {
                return this._Prescription_Id;
            }
            set
            {
                if ((this._Prescription_Id != value))
                {
                    this._Prescription_Id = value;
                }
            }
        }

        public string MedicineName
        {
            get
            {
                return this._MedicineName;
            }
            set
            {
                if ((this._MedicineName != value))
                {
                    this._MedicineName = value;
                }
            }
        }

        public string Morning
        {
            get
            {
                return this._Morning;
            }
            set
            {
                if ((this._Morning != value))
                {
                    this._Morning = value;
                }
            }
        }

        public string Afternoon
        {
            get
            {
                return this._Afternoon;
            }
            set
            {
                if ((this._Afternoon != value))
                {
                    this._Afternoon = value;
                }
            }
        }

        public string Night
        {
            get
            {
                return this._Night;
            }
            set
            {
                if ((this._Night != value))
                {
                    this._Night = value;
                }
            }
        }

        public string NoOfDays
        {
            get
            {
                return this._NoOfDays;
            }
            set
            {
                if ((this._NoOfDays != value))
                {
                    this._NoOfDays = value;
                }
            }
        }

        public string Quantity
        {
            get
            {
                return this._Quantity;
            }
            set
            {
                if ((this._Quantity != value))
                {
                    this._Quantity = value;
                }
            }
        }

        public System.Nullable<bool> IsDressing
        {
            get
            {
                return this._IsDressing;
            }
            set
            {
                if ((this._IsDressing != value))
                {
                    this._IsDressing = value;
                }
            }
        }

        public System.Nullable<bool> IsInjection
        {
            get
            {
                return this._IsInjection;
            }
            set
            {
                if ((this._IsInjection != value))
                {
                    this._IsInjection = value;
                }
            }
        }

        public string InjectionName
        {
            get
            {
                return this._InjectionName;
            }
            set
            {
                if ((this._InjectionName != value))
                {
                    this._InjectionName = value;
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




        public string Investigation { get; set; }

        public string Impression { get; set; }

        public string AdviceNote { get; set; }
        public string ProductName { get; internal set; }
        public int ProductId { get; internal set; }
    }
}