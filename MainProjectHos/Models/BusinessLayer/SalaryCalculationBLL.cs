using MainProjectHos.Models.DataModels;
using MainProjectHos.Models.DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.BusinessLayer
{
    public class SalaryCalculationBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public SalaryCalculationBLL()
        {
            objData = new CriticareHospitalDataContext();
        }
        public CriticareHospitalDataContext objData { get; set; }

        public tblEmployee GetEmployee(int EmpId)
        {
            try
            {
                return (from tbl in objData.tblEmployees
                        where tbl.PKId == EmpId
                        select tbl).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tblSalary CheckExistRecord(int EmpId, string Month)
        {
            try
            {
                return (from tbl in objData.tblSalaries
                        where tbl.EmpId == EmpId
                        && tbl.Sal_Month == Month
                        select tbl).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<STP_Select_SalaryResult> GetSalary()
        {
            List<STP_Select_SalaryResult> ldt = null;
            try
            {
                ldt = objData.STP_Select_Salary().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }
        public int? Save(tblSalary sal, List<tblSalaryDetail> lst)
        {
            int? SalId = 0;
            try
            {
                objData.STP_Insert_tblSalary(Convert.ToInt32(sal.EmpId), Convert.ToDateTime(sal.SalDate), Convert.ToInt32(sal.No_of_Days), Convert.ToInt32(sal.LeavesTaken), Convert.ToInt32(sal.Attend_Days), Convert.ToString(sal.Sal_Month), Convert.ToDecimal(sal.NetPayment), Convert.ToBoolean(sal.IsPaymentDone), Convert.ToInt32(sal.OTHours), ref SalId);
                foreach (tblSalaryDetail item in lst)
                {
                    item.SalId = Convert.ToInt32(SalId);
                    objData.tblSalaryDetails.InsertOnSubmit(item);
                }
                objData.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return SalId;
        }
        public bool ValidateAllocation(tblSalary sal)
        {
            try
            {
                var res = (from tbl in objData.tblSalaries
                           where tbl.EmpId == sal.EmpId
                           && tbl.Sal_Month == sal.Sal_Month
                           select tbl).FirstOrDefault();
                if (res != null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Save(tblSalary sal)
        {
            try
            {
                int? SalId = 0;
                objData.STP_Insert_tblSalary(Convert.ToInt32(sal.EmpId), Convert.ToDateTime(sal.SalDate), Convert.ToInt32(sal.No_of_Days), Convert.ToInt32(sal.LeavesTaken), Convert.ToInt32(sal.Attend_Days), Convert.ToString(sal.Sal_Month), Convert.ToDecimal(sal.NetPayment), Convert.ToBoolean(sal.IsPaymentDone), Convert.ToInt32(sal.OTHours), ref SalId);
                return SalId.Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EntitySalary GetDetail(int LedgerId)
        {
            try
            {
                EntitySalary lst = (from tbl in objData.tblSalaries
                                    where tbl.IsDelete == false
                                    && tbl.SalId == LedgerId
                                    select new EntitySalary
                                    {
                                        IsPaymentDone = tbl.IsPaymentDone
                                    }).FirstOrDefault();

                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void Save(List<tblSalaryDetail> lst)
        {

            try
            {
                foreach (tblSalaryDetail item in lst)
                {
                    objData.tblSalaryDetails.InsertOnSubmit(item);
                }
                objData.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EntityEmployee> GetEmployee()
        {
            try
            {
                return (from tbl in objData.tblEmployees
                        where tbl.IsDelete == false
                        select new EntityEmployee
                        {
                            PKId = tbl.PKId,
                            EmpName = tbl.EmpFirstName + " " + tbl.EmpMiddleName + " " + tbl.EmpLastName
                        }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<EntityEmployee> GetEmployeeUserList()
        {
            try
            {
                return (from tbl in objData.tblEmployees
                        where tbl.IsDelete == false && tbl.UserType != "Doctor"
                        select new EntityEmployee
                        {
                            PKId = tbl.PKId,
                            EmpName = tbl.EmpFirstName + " " + tbl.EmpMiddleName + " " + tbl.EmpLastName
                        }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(tblSalary obj, List<tblSalaryDetail> lst)
        {
            try
            {
                tblSalary objcurrent = (from tbl in objData.tblSalaries
                                        where tbl.SalId == obj.SalId
                                        select tbl).FirstOrDefault();
                if (objcurrent != null)
                {
                    objcurrent.IsPaymentDone = obj.IsPaymentDone;
                    objcurrent.Attend_Days = obj.Attend_Days;
                    objcurrent.LeavesTaken = obj.LeavesTaken;
                    objcurrent.OTHours = obj.OTHours;
                    objcurrent.NetPayment = obj.NetPayment;
                }

                foreach (tblSalaryDetail item in lst)
                {
                    tblSalaryDetail objsal = (from tbl in objData.tblSalaryDetails
                                              where tbl.SalId == item.SalId
                                              && tbl.AllowanceDeduction_Id == item.AllowanceDeduction_Id
                                              && tbl.IsDelete == false
                                              select tbl).FirstOrDefault();
                    if (objsal != null)
                    {
                        objsal.Amount = item.Amount;
                        objsal.IsDelete = item.IsDelete;
                    }
                    else
                    {
                        objsal = item;
                        objData.tblSalaryDetails.InsertOnSubmit(objsal);

                    }
                }
                objData.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ValidateAllocation(tblSalary obj, int LedgerId)
        {
            try
            {
                var res = (from tbl in objData.tblSalaries
                           where tbl.EmpId == obj.EmpId
                         && tbl.Sal_Month == obj.Sal_Month
                         && tbl.SalId == LedgerId
                           select tbl).FirstOrDefault();
                if (res != null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EntityEmployee> GetDetails(int EmpId)
        {
            try
            {
                return (from tbl in objData.tblEmployees
                        where tbl.IsDelete == false
                        &&
                        tbl.PKId.Equals(EmpId)
                        select new EntityEmployee
                        {
                            PKId = tbl.PKId,
                            EmpName = tbl.EmpFirstName + " " + tbl.EmpMiddleName + " " + tbl.EmpLastName,
                            BasicSal = tbl.BasicSalary == null ? 0 : Convert.ToDecimal(tbl.BasicSalary),
                            EmpDOJ = Convert.ToDateTime(tbl.EmpDOJ)
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EntityAllowanceDeduction GetAllowances(int AllowanceId)
        {
            try
            {
                EntityAllowanceDeduction lst = (from tbl in objData.tblAllowanceDeductions
                                                where tbl.IsDelete == false
                                                &&
                                                tbl.IsAllowance == true
                                                &&
                                                tbl.AllowDedId == AllowanceId
                                                select new EntityAllowanceDeduction
                                                {
                                                    AllowDedId = tbl.AllowDedId,
                                                    Percentage = tbl.Percentage,
                                                    IsPercentage = tbl.IsPercentage,
                                                    IsFixed = tbl.IsFixed,
                                                    IsFlexible = tbl.IsFlexible,
                                                    IsBasic = tbl.IsBasic,
                                                    Amount = tbl.Amount

                                                }).FirstOrDefault();
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EntityAllowanceDeduction> GetAllowanceName()
        {
            try
            {
                return (from tbl in objData.tblAllowanceDeductions
                        where tbl.IsDelete == false
                        &&
                        tbl.IsAllowance == true
                        select new EntityAllowanceDeduction
                        {
                            AllowDedId = tbl.AllowDedId,
                            Description = tbl.Description
                        }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<EntityAllowanceDeduction> GetDeductionName()
        {
            try
            {
                return (from tbl in objData.tblAllowanceDeductions
                        where tbl.IsDelete == false
                        &&
                        tbl.IsDeduction == true
                        select new EntityAllowanceDeduction
                        {
                            AllowDedId = tbl.AllowDedId,
                            Description = tbl.Description
                        }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public EntityAllowanceDeduction GetDeduction(int DedId)
        {
            try
            {
                EntityAllowanceDeduction lst = (from tbl in objData.tblAllowanceDeductions
                                                where tbl.IsDelete == false
                                                &&
                                                tbl.IsDeduction == true
                                                &&
                                                tbl.AllowDedId == DedId
                                                select new EntityAllowanceDeduction
                                                {
                                                    AllowDedId = tbl.AllowDedId,
                                                    Percentage = tbl.Percentage,
                                                    IsPercentage = tbl.IsPercentage,
                                                    IsFixed = tbl.IsFixed,
                                                    IsFlexible = tbl.IsFlexible,
                                                    Amount = tbl.Amount

                                                }).FirstOrDefault();
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectEmployeeList(int id)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@SalId", DbType.String, id);
                ldt = mobjDataAcces.GetDataTable("STP_Select_SalaryEdit", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("EntityPathology - SelectPatientTest()", ex);
            }
            return ldt;
        }

        public List<EntityAllowanceDeduction> GetAllowancesForEdit(int salId)
        {
            List<EntityAllowanceDeduction> lst = new List<EntityAllowanceDeduction>();
            try
            {
                lst = (from tbl in objData.STP_Select_SalaryDetailsDeduction(salId)
                       select new EntityAllowanceDeduction
                       {
                           AllowDedId = tbl.AllowDedId,
                           SalDetail_Id = tbl.SalDetail_Id,
                           Description = tbl.Description,
                           Amount = tbl.Amount
                       }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }

        public List<EntityAllowanceDeduction> GetDeductionsForEdit(int Id)
        {
            List<EntityAllowanceDeduction> lst = new List<EntityAllowanceDeduction>();
            try
            {
                lst = (from tbl in objData.STP_Select_SalaryDetails(Id)
                       select new EntityAllowanceDeduction
                       {
                           AllowDedId = tbl.AllowDedId,
                           SalDetail_Id = tbl.SalDetail_Id,
                           Description = tbl.Description,
                           Amount = tbl.Amount
                       }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }
        public bool CheckIsBasic(int AllowanceId)
        {

            bool IsBasic = false;
            try
            {
                tblAllowanceDeduction obj = (from tbl in objData.tblAllowanceDeductions
                                             where tbl.IsDelete == false
                                             && tbl.AllowDedId == AllowanceId
                                             && tbl.IsBasic == true
                                             select tbl).FirstOrDefault();
                if (obj != null)
                {
                    IsBasic = true;
                }
                else
                {
                    IsBasic = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IsBasic;
        }

        public List<EntitySalary> SearchSalary(string Prefix)
        {
            List<EntitySalary> lst = null;
            try
            {
                lst = (from tbl in objData.STP_Select_Salary()
                       where
                       tbl.IsDelete == false
                       &&
                       tbl.EmpCode.ToUpper().ToString().Contains(Prefix.ToUpper()) || tbl.EmployeeName.ToUpper().Contains(Prefix.ToUpper()) ||
                       tbl.Sal_Month.ToString().ToUpper().Contains(Prefix.ToUpper())
                       select new EntitySalary
                       {
                           EmpCode = tbl.EmpCode,
                           EmployeeName = tbl.EmployeeName,
                           SalDate = tbl.SalDate,
                           Sal_Month = tbl.Sal_Month,
                           No_of_Days = tbl.No_of_Days,
                           Attend_Days = tbl.Attend_Days,
                           LeavesTaken = tbl.LeavesTaken,
                           OTHours = Convert.ToInt32(tbl.OTHours),
                           NetPayment = tbl.NetPayment
                       }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }
    }
}