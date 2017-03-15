using MainProjectHos.Models.DataModels;
using MainProjectHos.Models.DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.BusinessLayer
{
    public class ShiftBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();

        public ShiftBLL()
        {
            objData = new CriticareHospitalDataContext();
        }

        public CriticareHospitalDataContext objData { get; set; }

        public List<tblShiftMaster> GetAllShiftDetails()
        {
            List<tblShiftMaster> ldtShift = null;
            try
            {
                ldtShift = (from tbl in objData.tblShiftMasters
                            where tbl.IsDelete == false
                            select tbl).ToList();

            }
            catch (Exception ex)
            {
                Commons.FileLog("ShiftBLL - GetAllShiftDetails()", ex);
            }
            return ldtShift;
        }

        public bool IsRecordExists(EntityShift entDept)
        {
            bool flag = false;
            try
            {
                tblShiftMaster obj = (from tbl in objData.tblShiftMasters
                                      where tbl.IsDelete == false
                                      && tbl.ShiftName.ToUpper().ToString().Trim().Equals(entDept.ShiftName.ToUpper().ToString().Trim())
                                      || tbl.StartTime.CompareTo(entDept.StartTime) == 0
                                      || entDept.StartTime.CompareTo(tbl.EndTime) == -1
                                      || tbl.EndTime.CompareTo(entDept.EndTime) == 1
                                      select tbl).FirstOrDefault();
                if (obj != null)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flag;
        }

        public int InsertShift(EntityShift entDept)
        {
            try
            {
                tblShiftMaster obj = new tblShiftMaster()
                {
                    ShiftName = entDept.ShiftName,
                    StartTime = entDept.StartTime,
                    EndTime = entDept.EndTime
                };
                objData.tblShiftMasters.InsertOnSubmit(obj);
                objData.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EntityShift GetShift(int p)
        {
            EntityShift lobjShift = (from tbl in objData.tblShiftMasters
                                     where tbl.ShiftId.Equals(p)
                                     && tbl.IsDelete == false
                                     select new EntityShift { ShiftName = tbl.ShiftName, StartTime = tbl.StartTime, EndTime = tbl.EndTime }).FirstOrDefault();
            return lobjShift;
        }

        public int Update(EntityShift entDept)
        {
            try
            {
                tblShiftMaster test = (from tbl in objData.tblShiftMasters
                                       where tbl.IsDelete == false
                                       && tbl.ShiftId == entDept.ShiftId
                                       select tbl).FirstOrDefault();
                test.ShiftName = entDept.ShiftName;
                test.StartTime = entDept.StartTime;
                test.EndTime = entDept.EndTime;
                objData.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EntityEmployee> GetAllEmpDetails()
        {
            List<EntityEmployee> lst = (from tbl in objData.STP_NotAllocatedEmpToShift()
                                        select new EntityEmployee
                                        {
                                            PKId = tbl.PKId,
                                            EmpName = tbl.EmpFirstName + " " + tbl.EmpMiddleName + " " + tbl.EmpLastName
                                        }).ToList();
            return lst;
        }

        public DataTable GetAllShift()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllShift");
            }
            catch (Exception ex)
            {
                Commons.FileLog("ShiftBLL - GetAllShift()", ex);
            }
            return ldt;
        }

        public int GetShiftCount()
        {
            return GetAllShiftDetails().Count;
        }

        public EntityPatientAdmit ChechDischargeDone(int TestInvoiceNo)
        {
            EntityPatientAdmit entpat = null;

            try
            {
                entpat = (from tbl in objData.tblPatientAdmitDetails
                          join tbltest in objData.tblTestInvoices
                          on tbl.AdmitId equals tbltest.PatientId
                          where tbltest.IsDelete == false
                          && tbl.IsDischarge == false
                          && tbltest.TestInvoiceNo == TestInvoiceNo
                          select new EntityPatientAdmit { }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return entpat;
        }

        public EntityCustomerTransaction GetTransactionStatus(int InvoiceNo)
        {
            EntityCustomerTransaction transact = null;
            try
            {
                transact = (from tbl in objData.tblCustomerTransactions
                            where tbl.TransactionDocNo == InvoiceNo
                            && tbl.IsDelete == false
                            && tbl.TransactionType.ToUpper().Equals("TestInvoice".ToUpper())
                            select new EntityCustomerTransaction { }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return transact;
        }
    }

}