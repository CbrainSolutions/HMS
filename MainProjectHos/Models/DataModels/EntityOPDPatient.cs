using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityOPDPatient
    /// </summary>
    public class EntityOPDPatient
    {
        public EntityOPDPatient()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string PatientCode { get; set; }
        public string PatientName { get; set; }
        public string DrugName { get; set; }
        public bool Morning { get; set; }
        public bool AfterNoon { get; set; }
        public bool Night { get; set; }
        public string EntryBy { get; set; }
        public string XRay { get; set; }
        public string BloodTest { get; set; }
        public int Quantity { get; set; }
        public string Symptoms { get; set; }
        public string Diagnosis { get; set; }
        public int AppointNO { get; set; }
        public decimal InjectionCharge { get; set; }
        public decimal DressingCharge { get; set; }
        public decimal RevisitCharge { get; set; }
        public decimal ConsultantCharge { get; set; }
        public decimal TotalOPDBill { get; set; }
        public string PatientVisitType { get; set; }
        public int ConsultedBy { get; set; }
        public int TotalInjections { get; set; }
        public string DiagnosisCode { get; set; }
    }
}