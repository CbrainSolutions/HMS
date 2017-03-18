using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityPatientMaster
    /// </summary>
    public class EntityPatientMaster
    {
        public EntityPatientMaster()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region "Propeties"

        public string PatientCode { get; set; }
        public int PatientInitial { get; set; }
        public string FullName { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientMiddleName { get; set; }
        public string PatientLastName { get; set; }
        public DateTime PatientAdmitDate { get; set; }
        public string PatientAdmitTime { get; set; }
        public string PatientType { get; set; }
        public string PatientAddress { get; set; }
        public string PatientContactNo { get; set; }
        public DateTime? BirthDate { get; set; }
        public string ReferedBy { get; set; }
        public int FloorNo { get; set; }
        public int WardNo { get; set; }
        public int BedNo { get; set; }
        public string ReasonForAdmit { get; set; }
        public int Gender { get; set; }
        public int Age { get; set; }
        public int Occupation { get; set; }
        public string City { get; set; }
        public String State { get; set; }
        public string Country { get; set; }
        public int ConsultingDr { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyContact { get; set; }
        public string EntryBy { get; set; }
        public string ChangeBy { get; set; }
        public bool IsDischarged { get; set; }
        public int Religion { get; set; }
        public int Caste { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public Byte FileData { get; set; }
        public Byte[] IDProof { get; set; }
        public Byte[] InsurenaceProof { get; set; }
        public Byte[] PatientPhoto { get; set; }
        public string InsuranceCompName { get; set; }
        public bool lsInsurance { get; set; }
        public string PatientHistory { get; set; }
        public string OPDRoom { get; set; }
        public string PersonalHistory { get; set; }
        public string PastMedicalHistory { get; set; }
        public string FamilyHistory { get; set; }
        public string BloodGroup { get; set; }
        public string DiscontRemark { get; set; }
        public bool Discontinued { get; set; }
        public string GenderDesc { get; set; }
        public int PatientId { get; set; }
        public int CompanyId { get; set; }
        public int InsuranceCompID { get; set; }
        public int AdmitId { get; set; }
        public string OPDNo { get; set; }

        #endregion

        public string Dignosys { get; set; }

        public int PKId { get; set; }

        public string IPDNo { get; set; }
        public string AgeIn { get; set; }

        public int DeptCategory { get; set; }

        public string EmpName { get; set; }
        public int DeptDoctorId { get; set; }
        //public byte[] EndoscopyFile { get; set; }
        //public byte[] AudiometryFile { get; set; }

        public string Weight { get; set; }
        public string CompName { get; internal set; }
        public string InsuName { get; internal set; }
    }
}