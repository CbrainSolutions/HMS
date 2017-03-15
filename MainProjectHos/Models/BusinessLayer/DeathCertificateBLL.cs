using MainProjectHos.Models.DataLayer;
using MainProjectHos.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.BusinessLayer
{
    public class DeathCertificateBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();

        public DeathCertificateBLL()
        {
            objData = new CriticareHospitalDataContext();
        }
        public CriticareHospitalDataContext objData { get; set; }

        public List<EntityDeathCertificate> GetAllDeathDetails()
        {
            List<EntityDeathCertificate> lst = null;
            try
            {
                lst = (from tbl in objData.tblDeathCertificates
                       join tbla in objData.tblPatientAdmitDetails
                       on tbl.PatientAdmitId equals tbla.AdmitId
                       join tblPat in objData.tblPatientMasters
                       on tbla.PatientId equals tblPat.PKId
                       where tbl.IsDelete == false
                       select new EntityDeathCertificate
                       {
                           DeathId = tbl.DeathId,
                           Death_Date = tbl.Death_Date,
                           Death_Reason = tbl.Death_Reason,
                           Death_Time = tbl.Death_Time,
                           PatientAdmitId = tbl.PatientAdmitId,
                           FullName = tblPat.PatientFirstName + ' ' + tblPat.PatientMiddleName + ' ' + tblPat.PatientLastName
                       }).ToList();
                return lst;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<EntityDeathCertificate> GetAllPatients()
        {
            List<EntityDeathCertificate> lst = null;
            try
            {
                lst = (from tbla in objData.tblPatientAdmitDetails
                       join tblPat in objData.tblPatientMasters
                       on tbla.PatientId equals tblPat.PKId
                       where tbla.IsDelete == false
                       && tbla.IsDischarge == false
                       select new EntityDeathCertificate
                       {
                           PatientAdmitId = tbla.AdmitId,
                           FullName = tblPat.PatientFirstName + ' ' + tblPat.PatientMiddleName + ' ' + tblPat.PatientLastName
                       }).ToList();
                return lst;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int InsertDeathRecord(EntityDeathCertificate entDept)
        {
            try
            {
                tblDeathCertificate obj = new tblDeathCertificate()
                {
                    PatientAdmitId = entDept.PatientAdmitId,
                    Death_Date = entDept.Death_Date,
                    Death_Time = entDept.Death_Time,
                    Death_Reason = entDept.Death_Reason
                };
                objData.tblDeathCertificates.InsertOnSubmit(obj);

                EntityPatientAdmit admit = new PatientMasterBLL().GetPatientAdmitDetails(entDept.PatientAdmitId);
                if (admit != null)
                {
                    tblPatientMaster patient = new PatientMasterBLL().GetPatientDetailsByPatientIdForUpdate(Convert.ToInt32(admit.PatientId));
                    if (patient != null)
                    {
                        patient.IsDeath = true;
                    }
                }
                objData.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EntityDeathCertificate> SelectDeathDetails(string Prefix)
        {
            List<EntityDeathCertificate> lst = null;
            try
            {
                lst = (from tbl in objData.tblDeathCertificates
                       join tbla in objData.tblPatientAdmitDetails
                       on tbl.PatientAdmitId equals tbla.AdmitId
                       join tblPat in objData.tblPatientMasters
                       on tbla.PatientId equals tblPat.PKId
                       where tbl.IsDelete == false
                       && (tbl.Death_Reason.ToUpper().ToString().Trim().Contains(Prefix.ToUpper().ToString().Trim())
                       || (tblPat.PatientFirstName + ' ' + tblPat.PatientMiddleName + ' ' + tblPat.PatientLastName).ToString().ToUpper().Trim().Contains(Prefix.ToUpper().ToString().Trim()))
                       select new EntityDeathCertificate
                       {
                           DeathId = tbl.DeathId,
                           Death_Date = tbl.Death_Date,
                           Death_Reason = tbl.Death_Reason,
                           Death_Time = tbl.Death_Time,
                           PatientAdmitId = tbl.PatientAdmitId,
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