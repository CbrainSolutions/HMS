using MainProjectHos.Models.DataModels;
using MainProjectHos.Models.DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.BusinessLayer
{
    public class PatientAllocDocBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();

        public PatientAllocDocBLL()
        {
            objData = new CriticareHospitalDataContext();
        }

        public CriticareHospitalDataContext objData { get; set; }

        public System.Data.DataTable GetAllNonAllocPatient()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllNonAllocPatientList");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable GetAllDoctor()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllDoctorList");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public EntityPatientAlloc GetPatientType(int Val)
        {
            try
            {
                EntityPatientAlloc objPat = (from tbl in objData.tblPatientMasters
                                             join tblAdmit in objData.tblPatientAdmitDetails
                                             on tbl.PKId equals tblAdmit.PatientId
                                             where tblAdmit.AdmitId.Equals(Val)
                                             select new EntityPatientAlloc { PatType = tblAdmit.IsIPD.Value ? "IPD" : "OPD", AdmitDate = Convert.ToDateTime(tblAdmit.AdmitDate) }).FirstOrDefault();
                return objPat;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public EntityShift GetStartEndTime(int Doc)
        {
            try
            {
                EntityShift objShi = (from tbl in objData.tblShiftMasters
                                      join tblS in objData.tblShiftAllocEmps
                                      on tbl.ShiftId equals tblS.Shift_Id
                                      join tblE in objData.tblEmployees
                                      on tblS.Emp_Id equals tblE.PKId
                                      where tblE.PKId == Doc
                                      && tblE.IsDelete.Equals(false)
                                      select new EntityShift { StartTime = tbl.StartTime, EndTime = tbl.EndTime }).FirstOrDefault();
                return objShi;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }



        public bool IsRecordExists(string Pat_id, string Empid, DateTime App_Date)
        {
            bool flag1 = false;
            try
            {
                tblPatientAllocToDoc objPat = (from tbl in objData.tblPatientAllocToDocs
                                               where tbl.PatientId.Equals(Pat_id)
                                               && tbl.AppDate.CompareTo(App_Date) == 0
                                               && tbl.DocId.Equals(Empid)
                                               select tbl).FirstOrDefault();
                if (objPat != null)
                {
                    flag1 = true;
                }
                else
                {
                    flag1 = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flag1;
        }

        public int InsertAllocPat(EntityPatientAlloc entPat)
        {
            try
            {
                tblPatientAllocToDoc obj = new tblPatientAllocToDoc()
                {
                    PatientId = entPat.PatientId,
                    DocId = entPat.DocId,
                    AppDate = entPat.AppDate,
                    Charges = entPat.Charges
                };
                objData.tblPatientAllocToDocs.InsertOnSubmit(obj);
                objData.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EntityPatAllocDoc> GetAllAllocPatient()
        {
            try
            {
                return (from tbl in objData.tblPatientAllocToDocs
                        join tblAdmit in objData.tblPatientAdmitDetails
                        on tbl.PatientId equals tblAdmit.AdmitId
                        join tblPat in objData.tblPatientMasters
                        on tblAdmit.PatientId equals tblPat.PKId
                        join tblShift in objData.tblShiftAllocEmps
                        on tbl.DocId equals tblShift.Emp_Id
                        join tblEmp in objData.tblEmployees
                        on tblShift.Emp_Id equals tblEmp.PKId
                        join tblShMas in objData.tblShiftMasters
                        on tblShift.Shift_Id equals tblShMas.ShiftId
                        join tblI in objData.tblInitials
                        on tblPat.Initial equals tblI.PKId
                        where tbl.IsDelete == false
                        select new EntityPatAllocDoc
                        {
                            PKId = tbl.PateintAllocId,
                            PatientName = tblPat.PatientFirstName + " " + tblPat.PatientMiddleName + " " + tblPat.PatientLastName,
                            Emp_Id = tblEmp.PKId,
                            EmpName = tblEmp.EmpFirstName + " " + tblEmp.EmpMiddleName + " " + tblEmp.EmpLastName,
                            ShiftName = tblShMas.ShiftName,
                            AppointDate = tbl.AppDate,
                            Charges = tbl.Charges,
                            StartTime = tblShMas.StartTime,
                            EndTime = tblShMas.EndTime
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EntityPatAllocDoc SelectPatRecForEdit(int patId)
        {
            try
            {
                return (from tbl in objData.tblPatientAllocToDocs
                        join tblPat in objData.tblPatientMasters
                        on tbl.PatientId equals tblPat.PKId
                        join tblShift in objData.tblShiftAllocEmps
                        on tbl.DocId equals tblShift.Emp_Id
                        join tblEmp in objData.tblEmployees
                        on tblShift.Emp_Id equals tblEmp.PKId
                        join tblShMas in objData.tblShiftMasters
                        on tblShift.Shift_Id equals tblShMas.ShiftId
                        join tblI in objData.tblInitials
                        on tblPat.Initial equals tblI.PKId
                        where tbl.IsDelete == false
                        && tbl.PatientId.Equals(patId)
                        orderby tbl.PateintAllocId descending
                        select new EntityPatAllocDoc
                        {
                            PKId = tblPat.PKId,
                            PatientName = tblI.InitialDesc + " " + tblPat.PatientFirstName + " " + tblPat.PatientMiddleName + " " + tblPat.PatientLastName,
                            Emp_Id = tblEmp.PKId,
                            Pat_Type = tblPat.PatientType,
                            EmpName = tblEmp.EmpFirstName + " " + tblEmp.EmpMiddleName + " " + tblEmp.EmpLastName,
                            ShiftName = tblShMas.ShiftName,
                            AppointDate = tbl.AppDate,
                            Charges = tbl.Charges,
                            StartTime = tblShMas.StartTime,
                            EndTime = tblShMas.EndTime
                        }).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int Update(EntityPatAllocDoc objT)
        {
            try
            {
                tblPatientAllocToDoc test = (from tbl in objData.tblPatientAllocToDocs
                                             where tbl.IsDelete == false
                                             && tbl.PateintAllocId == objT.PKId
                                             select tbl).FirstOrDefault();
                test.DocId = objT.Emp_Id;
                test.AppDate = objT.AppointDate;
                test.Charges = objT.Charges;
                objData.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EntityPatAllocDoc> SelectPatientAllocation(string Prefix)
        {
            List<EntityPatAllocDoc> lst = null;
            try
            {
                lst = (from tbl in GetAllAllocPatient()
                       where
                       tbl.PatientName.ToString().ToUpper().Contains(Prefix.ToUpper()) || tbl.EmpName.ToString().ToUpper().Contains(Prefix.ToUpper()) || tbl.PKId.ToString().ToUpper().Contains(Prefix.ToUpper())
                       select new EntityPatAllocDoc
                       {
                           PKId = tbl.PKId,
                           PatientName = tbl.PatientName,
                           EmpName = tbl.EmpName,
                           AppointDate = tbl.AppointDate,
                           StartTime = tbl.StartTime,
                           EndTime = tbl.EndTime,
                           Charges = tbl.Charges
                       }).ToList();
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool GetDischargeInfo(int patId)
        {
            bool IsDischarge = false;
            try
            {
                EntityPatientAdmit admit = (from tbl in objData.tblPatientAllocToDocs
                                            join tblAdmit in objData.tblPatientAdmitDetails
                                            on tbl.PatientId equals tblAdmit.AdmitId
                                            where tbl.IsDelete == false
                                            && tbl.PateintAllocId == patId
                                            select new EntityPatientAdmit
                                            {
                                                AdmitId = tbl.PatientId,
                                                IsDischarge = tblAdmit.IsDischarge
                                            }).FirstOrDefault();
                if (admit != null)
                {
                    IsDischarge = Convert.ToBoolean(admit.IsDischarge);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IsDischarge;
        }

    }
}