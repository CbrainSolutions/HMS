using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MainProjectHos.Models.BusinessLayer;
using MainProjectHos.Models.DataLayer;
using MainProjectHos.Models.DataModels;
using System.Data;
using System.Drawing;

namespace MainProjectHos.Models.BusinessLayer
{
    public class MedicalCertificateBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();

        public MedicalCertificateBLL()
        {
            objData = new CriticareHospitalDataContext();
        }
        public CriticareHospitalDataContext objData { get; set; }


        public List<EntityMedicalCertificate> GetAllBirthDetails()
        {
            List<EntityMedicalCertificate> lst = null;
            try
            {
                lst = (from tbl in objData.tblMedicalCertificates
                       join tbla in objData.tblPatientAdmitDetails
                       on tbl.PatientAdmitID equals tbla.AdmitId
                       join tblPat in objData.tblPatientMasters
                       on tbla.PatientId equals tblPat.PKId
                       where tbl.IsDelete == false
                       select new EntityMedicalCertificate
                       {
                           CertiId = tbl.CertiID,
                           Age = Convert.ToInt32(tbl.Age),
                           DischargeOn = Convert.ToDateTime(tbl.DischargeOn),
                           AdvisedRestDays = tbl.AdvisedRestDays,
                           ContinuedRestDays = tbl.ContinuedRestDays,
                           ContinueRestFrom = Convert.ToDateTime(tbl.ContinueRestFrom),
                           AdvisedRestFrom = Convert.ToDateTime(tbl.AdvisedRestFrom),
                           IndoorOn = Convert.ToDateTime(tbl.IndoorOn),
                           Daignosis = tbl.Daignosis,
                           OPDFrom = Convert.ToDateTime(tbl.OPDFrom),
                           OPDTo = Convert.ToDateTime(tbl.OPDTo),
                           OperatedFor = tbl.OperatedFor,
                           OperatedForOn = Convert.ToDateTime(tbl.OperatedForOn),
                           WorkFrom = Convert.ToDateTime(tbl.WorkFrom),
                           FullName = tblPat.PatientFirstName + ' ' + tblPat.PatientMiddleName + ' ' + tblPat.PatientLastName
                       }).ToList();
                return lst;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<EntityMedicalCertificate> GetAllPatients()
        {
            List<EntityMedicalCertificate> lst = null;
            try
            {
                lst = (from tbla in objData.tblPatientAdmitDetails
                       join tblPat in objData.tblPatientMasters
                       on tbla.PatientId equals tblPat.PKId
                       where tbla.IsDelete == false
                       && tbla.IsDischarge == false
                       select new EntityMedicalCertificate
                       {
                           PatientAdmitID = tbla.AdmitId,
                           FullName = tblPat.PatientFirstName + ' ' + tblPat.PatientMiddleName + ' ' + tblPat.PatientLastName
                       }).ToList();
                return lst;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int InsertBirthRecord(EntityMedicalCertificate entDept)
        {
            try
            {
                tblMedicalCertificate obj = new tblMedicalCertificate()
                {
                    PatientAdmitID = entDept.PatientAdmitID,
                    Age = entDept.Age,
                    Daignosis = entDept.Daignosis,
                    OPDFrom = entDept.OPDFrom,
                    OPDTo = entDept.OPDTo,
                    IndoorOn = entDept.IndoorOn,
                    DischargeOn = entDept.DischargeOn,
                    OperatedFor = entDept.OperatedFor,
                    OperatedForOn = entDept.OperatedForOn,
                    AdvisedRestDays = entDept.AdvisedRestDays,
                    AdvisedRestFrom = entDept.AdvisedRestFrom,
                    ContinuedRestDays = entDept.ContinuedRestDays,
                    ContinueRestFrom = entDept.ContinueRestFrom,
                    WorkFrom = entDept.WorkFrom
                };
                objData.tblMedicalCertificates.InsertOnSubmit(obj);
                objData.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EntityMedicalCertificate> SelectBirthDetails(string Prefix)
        {
            List<EntityMedicalCertificate> lst = null;
            try
            {
                lst = (from tbl in objData.tblMedicalCertificates
                       join tbla in objData.tblPatientAdmitDetails
                       on tbl.PatientAdmitID equals tbla.AdmitId
                       join tblPat in objData.tblPatientMasters
                       on tbla.PatientId equals tblPat.PKId
                       where tbl.IsDelete == false
                       && (tblPat.PatientFirstName + ' ' + tblPat.PatientMiddleName + ' ' + tblPat.PatientLastName).ToString().ToUpper().Trim().Contains(Prefix.ToUpper().ToString().Trim())
                       select new EntityMedicalCertificate
                       {
                           CertiId = tbl.CertiID,
                           Age = Convert.ToInt32(tbl.Age),
                           DischargeOn = Convert.ToDateTime(tbl.DischargeOn),
                           AdvisedRestDays = tbl.AdvisedRestDays,
                           ContinuedRestDays = tbl.ContinuedRestDays,
                           ContinueRestFrom = Convert.ToDateTime(tbl.ContinueRestFrom),
                           AdvisedRestFrom = Convert.ToDateTime(tbl.AdvisedRestFrom),
                           IndoorOn = Convert.ToDateTime(tbl.IndoorOn),
                           Daignosis = tbl.Daignosis,
                           OPDFrom = Convert.ToDateTime(tbl.OPDFrom),
                           OPDTo = Convert.ToDateTime(tbl.OPDTo),
                           OperatedFor = tbl.OperatedFor,
                           OperatedForOn = Convert.ToDateTime(tbl.OperatedForOn),
                           WorkFrom = Convert.ToDateTime(tbl.WorkFrom),
                           FullName = tblPat.PatientFirstName + ' ' + tblPat.PatientMiddleName + ' ' + tblPat.PatientLastName
                       }).ToList();
                return lst;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}