using MainProjectHos.Models.DataLayer;
using MainProjectHos.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.BusinessLayer
{
    public class BirthCertificateBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();

        public BirthCertificateBLL()
        {
            objData = new CriticareHospitalDataContext();
        }
        public CriticareHospitalDataContext objData { get; set; }


        public List<EntityBirthCertificate> GetAllGender()
        {
            List<EntityBirthCertificate> lst = null;
            try
            {
                lst = (from tbl in objData.tblGenders
                       where tbl.IsDelete == false
                       select new EntityBirthCertificate
                       {
                           GenderID = tbl.PKId,
                           GenderDesc = tbl.GenderDesc
                       }).ToList();
                return lst;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<EntityBirthCertificate> GetAllBirthDetails()
        {
            List<EntityBirthCertificate> lst = null;
            try
            {
                lst = (from tbl in objData.tblBirthCertificates
                       join tbla in objData.tblPatientAdmitDetails
                       on tbl.PatientAdmitID equals tbla.AdmitId
                       join tblPat in objData.tblPatientMasters
                       on tbla.PatientId equals tblPat.PKId
                       join tblG in objData.tblGenders
                       on tbl.GenderID equals tblG.PKId
                       where tbl.IsDelete == false
                       select new EntityBirthCertificate
                       {
                           BirthID = tbl.BirthID,
                           BirthDate = tbl.BirthDate,
                           ChildName = tbl.ChildName,
                           GenderDesc = tblG.GenderDesc,
                           Height = tbl.Height,
                           Weight = tbl.Weight,
                           FullName = tblPat.PatientFirstName + ' ' + tblPat.PatientMiddleName + ' ' + tblPat.PatientLastName
                       }).ToList();
                return lst;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<EntityBirthCertificate> GetAllPatients()
        {
            List<EntityBirthCertificate> lst = null;
            try
            {
                lst = (from tbla in objData.tblPatientAdmitDetails
                       join tblPat in objData.tblPatientMasters
                       on tbla.PatientId equals tblPat.PKId
                       where tbla.IsDelete == false
                       && tbla.IsDischarge == false
                       && tblPat.Gender.Equals("2")
                       select new EntityBirthCertificate
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

        public int InsertBirthRecord(EntityBirthCertificate entDept)
        {
            try
            {
                tblBirthCertificate obj = new tblBirthCertificate()
                {
                    PatientAdmitID = entDept.PatientAdmitID,
                    GenderID = entDept.GenderID,
                    GrandFatherName = entDept.GrandFatherName,
                    ChildName = entDept.ChildName,
                    BirthDate = entDept.BirthDate,
                    BirthTime = entDept.BirthTime,
                    Height = entDept.Height,
                    Weight = entDept.Weight
                };
                objData.tblBirthCertificates.InsertOnSubmit(obj);
                objData.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EntityBirthCertificate> SelectBirthDetails(string Prefix)
        {
            List<EntityBirthCertificate> lst = null;
            try
            {
                lst = (from tbl in objData.tblBirthCertificates
                       join tbla in objData.tblPatientAdmitDetails
                       on tbl.PatientAdmitID equals tbla.AdmitId
                       join tblPat in objData.tblPatientMasters
                       on tbla.PatientId equals tblPat.PKId
                       join tblG in objData.tblGenders
                       on tbl.GenderID equals tblG.PKId
                       where tbl.IsDelete == false
                       && (tbl.ChildName.ToUpper().ToString().Trim().Contains(Prefix.ToUpper().ToString().Trim())
                       || tblG.GenderDesc.ToUpper().ToString().Trim().Contains(Prefix.ToUpper().ToString().Trim())
                       || (tblPat.PatientFirstName + ' ' + tblPat.PatientMiddleName + ' ' + tblPat.PatientLastName).ToString().ToUpper().Trim().Contains(Prefix.ToUpper().ToString().Trim()))
                       select new EntityBirthCertificate
                       {
                           BirthID = tbl.BirthID,
                           BirthDate = tbl.BirthDate,
                           ChildName = tbl.ChildName,
                           GenderDesc = tblG.GenderDesc,
                           Height = tbl.Height,
                           Weight = tbl.Weight,
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