using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels

{
    /// <summary>
    public class EntityPrescription
    {
        public EntityPrescription()
        {
        }
        private int _Prescription_Id;
        private bool _IsDelete;
        public string PatientName { get; set; }
        public string MedicineName { get; set; }
        public int Quantity { get; set; }
        public int Prescription_Id
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

        public DateTime? Prescription_Date { get; set; }
        public int? AdmitId { get; set; }

        public int DeptCategory { get; set; }
        public string DeptDoctor { get; set; }
        public string CategoryName { get; set; }

        public string Investigation { get; set; }

        public string Impression { get; set; }

        public string AdviceNote { get; set; }

        public string Remarks { get; set; }
    }
}
