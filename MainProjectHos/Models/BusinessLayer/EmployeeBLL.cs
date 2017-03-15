using MainProjectHos.Models.DataLayer;
using MainProjectHos.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.BusinessLayer
{
    public class EmployeeBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();

        public EmployeeBLL()
        {
            objData = new CriticareHospitalDataContext();
        }

        public CriticareHospitalDataContext objData { get; set; }

        public DataTable GetNewEmpCode()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetNewEmpCode");
            }
            catch (Exception ex)
            {
                Commons.FileLog("EmployeeBLL - GetNewEmpCode()", ex);
            }
            return ldt;
        }

        public DataTable GetDepartments()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetDepartmentsForEmp");
            }
            catch (Exception ex)
            {
                Commons.FileLog("DepartmentBLL - GetDepartments()", ex);
            }
            return ldt;
        }

        public List<EntityEmployee> SelectAllEmployee()
        {
            List<EntityEmployee> ldt = null;
            try
            {
                ldt = (from tbl in objData.sp_SelectAllEmployee()
                       select new EntityEmployee
                       {
                           EmpCode = tbl.EmpCode,
                           EmpFirstName = tbl.EmpFirstName,
                           EmpMiddleName = tbl.EmpMiddleName,
                           EmpLastName = tbl.EmpLastName,
                           EmpAddress = tbl.EmpAddress,
                           EmpDOB = Convert.ToDateTime(tbl.EmpDOB),
                           EmpDOJ = Convert.ToDateTime(tbl.EmpDOJ),
                           BankName = tbl.BankName,
                           BankACNo = tbl.BankACNo,
                           PFNo = tbl.PFNo,
                           PanNo = tbl.PanNo,
                           BasicSal = Convert.ToDecimal(tbl.BasicSalary)
                       }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable SelectEmployeeForEdit(string pstrEmpCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@EmpCode", DbType.String, pstrEmpCode);
                ldt = mobjDataAcces.GetDataTable("sp_SelectEmployee", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("EmployeeBLL - SelectAllEmployee()", ex);
            }
            return ldt;
        }

        public DataTable GetEmployeeName(string pstrEmpCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@EmpCode", DbType.String, pstrEmpCode);
                ldt = mobjDataAcces.GetDataTable("GetEmpName", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("EmployeeBLL - GetEmployeeName(string pstrEmpCode)", ex);
            }
            return ldt;
        }

        private List<SqlParameter> CreateParameterInsertEmp(EntityEmployee entEmployee)
        {
            List<SqlParameter> lstParam = new List<SqlParameter>();
            Commons.ADDParameter(ref lstParam, "@EmpCode", DbType.String, entEmployee.EmpCode);
            Commons.ADDParameter(ref lstParam, "@EmpFirstName", DbType.String, entEmployee.EmpFirstName);
            Commons.ADDParameter(ref lstParam, "@EmpMiddleName", DbType.String, entEmployee.EmpMiddleName);
            Commons.ADDParameter(ref lstParam, "@EmpLastName", DbType.String, entEmployee.EmpLastName);
            Commons.ADDParameter(ref lstParam, "@EmpAddress", DbType.String, entEmployee.EmpAddress);
            Commons.ADDParameter(ref lstParam, "@EmpDOB", DbType.DateTime, entEmployee.EmpDOB);
            Commons.ADDParameter(ref lstParam, "@EmpDOJ", DbType.DateTime, entEmployee.EmpDOJ);
            Commons.ADDParameter(ref lstParam, "@DeptId", DbType.String, entEmployee.DeptId);
            return lstParam;
        }

        private List<SqlParameter> CreateParameterDeleteEmp(EntityEmployee entEmployee)
        {
            List<SqlParameter> lstParam = new List<SqlParameter>();
            Commons.ADDParameter(ref lstParam, "@EmpCode", DbType.String, entEmployee.EmpCode);
            Commons.ADDParameter(ref lstParam, "@Discontinued", DbType.Boolean, entEmployee.DisContinued);
            Commons.ADDParameter(ref lstParam, "@DiscontRemark", DbType.String, entEmployee.DisContRemark);
            return lstParam;
        }

        public int UpdateEmployee(EntityEmployee entEmployee)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@EmpCode", DbType.String, entEmployee.EmpCode);
                Commons.ADDParameter(ref lstParam, "@EmpFirstName", DbType.String, entEmployee.EmpFirstName);
                Commons.ADDParameter(ref lstParam, "@EmpMiddleName", DbType.String, entEmployee.EmpMiddleName);
                Commons.ADDParameter(ref lstParam, "@EmpLastName", DbType.String, entEmployee.EmpLastName);
                Commons.ADDParameter(ref lstParam, "@EmpAddress", DbType.String, entEmployee.EmpAddress);
                Commons.ADDParameter(ref lstParam, "@EmpDOB", DbType.DateTime, entEmployee.EmpDOB);
                Commons.ADDParameter(ref lstParam, "@EmpDOJ", DbType.DateTime, entEmployee.EmpDOJ);
                Commons.ADDParameter(ref lstParam, "@DeptId", DbType.String, entEmployee.DeptId);
                Commons.ADDParameter(ref lstParam, "@ChangeBy", DbType.String, entEmployee.ChangeBy);
                Commons.ADDParameter(ref lstParam, "@DesigId", DbType.Int32, entEmployee.DesignationId);
                Commons.ADDParameter(ref lstParam, "@BankName", DbType.String, entEmployee.BankName);
                Commons.ADDParameter(ref lstParam, "@BankACNo", DbType.String, entEmployee.BankACNo);
                Commons.ADDParameter(ref lstParam, "@PFNo", DbType.String, entEmployee.PFNo);
                Commons.ADDParameter(ref lstParam, "@PanNo", DbType.String, entEmployee.PanNo);
                Commons.ADDParameter(ref lstParam, "@BasicSalary", DbType.Decimal, entEmployee.BasicSal);
                cnt = mobjDataAcces.ExecuteQuery("sp_UpdateEmployee", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("EmployeeBLL - CreateParameterUpdateEmp(EntityEmployee entEmployee)", ex);
            }
            return cnt;
        }

        public int InsertEmployee(EntityEmployee entEmployee)
        {
            int cnt = 0;

            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@EmpCode", DbType.String, entEmployee.EmpCode);
                Commons.ADDParameter(ref lstParam, "@EmpFirstName", DbType.String, entEmployee.EmpFirstName);
                Commons.ADDParameter(ref lstParam, "@EmpMiddleName", DbType.String, entEmployee.EmpMiddleName);
                Commons.ADDParameter(ref lstParam, "@EmpLastName", DbType.String, entEmployee.EmpLastName);
                Commons.ADDParameter(ref lstParam, "@EmpAddress", DbType.String, entEmployee.EmpAddress);
                Commons.ADDParameter(ref lstParam, "@EmpDOB", DbType.DateTime, entEmployee.EmpDOB);
                Commons.ADDParameter(ref lstParam, "@EmpDOJ", DbType.DateTime, entEmployee.EmpDOJ);
                Commons.ADDParameter(ref lstParam, "@DeptId", DbType.String, entEmployee.DeptId);
                Commons.ADDParameter(ref lstParam, "@EntryBy", DbType.String, entEmployee.EntryBy);
                Commons.ADDParameter(ref lstParam, "@DesigId", DbType.Int32, entEmployee.DesignationId);
                Commons.ADDParameter(ref lstParam, "@UserType", DbType.String, entEmployee.UserType);
                Commons.ADDParameter(ref lstParam, "@BankName", DbType.String, entEmployee.BankName);
                Commons.ADDParameter(ref lstParam, "@BankACNo", DbType.String, entEmployee.BankACNo);
                Commons.ADDParameter(ref lstParam, "@PFNo", DbType.String, entEmployee.PFNo);
                Commons.ADDParameter(ref lstParam, "@PanNo", DbType.String, entEmployee.PanNo);
                Commons.ADDParameter(ref lstParam, "@BasicSalary", DbType.Decimal, entEmployee.BasicSal);
                cnt = mobjDataAcces.ExecuteQuery("sp_InsertEmployee", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("EmployeeBLL - InsertEmployee(EntityEmployee entEmployee) ", ex);
            }
            return cnt;
        }

        public int DeleteEmployee(List<EntityEmployee> lstEntEmployee)
        {
            int cnt = 0;
            List<string> lstspNames = new List<string>();
            List<List<SqlParameter>> lstParamVals = new List<List<SqlParameter>>();
            try
            {
                foreach (EntityEmployee entEmployee in lstEntEmployee)
                {
                    lstspNames.Add("sp_DeleteEmployee");
                    lstParamVals.Add(CreateParameterDeleteEmp(entEmployee));

                    lstspNames.Add("sp_DeleteEmployeeLogin");
                    List<SqlParameter> lstParam = new List<SqlParameter>();
                    Commons.ADDParameter(ref lstParam, "@EmpCode", DbType.String, entEmployee.EmpCode);
                    Commons.ADDParameter(ref lstParam, "@Discontinued", DbType.Boolean, entEmployee.DisContinued);
                    lstParamVals.Add(lstParam);
                }
                cnt = mobjDataAcces.ExecuteTransaction(lstspNames, lstParamVals);
            }
            catch (Exception ex)
            {
                Commons.FileLog("EmployeeBLL - DeleteEmployee(List<EntityEmployee> lstEntEmployee) ", ex);
            }
            return cnt;
        }

        //public List<EntityEmployee> GetAllocatedEmployee(int p)
        //{
        //try
        //{
        //    return (from tbl in objData.STP_AllocatedStudents(ClassId, DivId, YearId)
        //            select new ClassAllocationInfo
        //            {
        //                FirstName = tbl.FirstName + " " + tbl.MiddleName + " " + tbl.LastName,
        //                RegNo = tbl.RegNo,
        //                StudentId = tbl.StudentId,
        //                AllocationId = tbl.ClassAlloc_Id
        //            }).ToList();
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
        //}

        public List<EntityFacAllocEmp> GetAllocatedEmpToShift(int ShiftId)
        {
            try
            {
                List<STP_AllocatedEmployeeResult> lst = objData.STP_AllocatedEmployee(ShiftId).ToList();
                return (from tbl in lst
                        select new EntityFacAllocEmp
                        {
                            Emp_Id = tbl.PKId,
                            FullName = tbl.EmpFirstName + " " + tbl.EmpMiddleName + " " + tbl.EmpLastName
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public EntityFacAllocEmp GetAllocatedEmp(int ShiftId, int EmpId)
        //{

        //}

        public bool Save(List<tblShiftAllocEmp> lst)
        {
            try
            {
                foreach (tblShiftAllocEmp item in lst)
                {
                    objData.tblShiftAllocEmps.InsertOnSubmit(item);
                }
                objData.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool GetAEmpIdOnShiftId(int ShiftId, int EmpId)
        {
            bool flag1 = false;
            tblShiftAllocEmp obj = new tblShiftAllocEmp();
            try
            {
                obj = (from tbl in objData.tblShiftAllocEmps
                       where tbl.IsDelete == false
                       && tbl.Shift_Id.Equals(ShiftId)
                       && tbl.Emp_Id.Equals(EmpId)
                       select tbl).FirstOrDefault();
                if (obj != null)
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

        public List<EntityEmployee> SelectEmployee(string Prefix)
        {
            List<EntityEmployee> lst = null;
            try
            {
                lst = (from tbl in objData.sp_SelectAllEmployee()
                       where
                       tbl.EmpCode.ToString().ToUpper().Contains(Prefix.ToUpper()) || tbl.EmpFirstName.ToString().ToUpper().Contains(Prefix.ToUpper()) ||
                       tbl.EmpMiddleName.ToString().ToUpper().Contains(Prefix.ToUpper()) || tbl.EmpLastName.ToString().ToUpper().Contains(Prefix.ToUpper()) ||
                       tbl.EmpAddress.ToString().ToUpper().Contains(Prefix.ToUpper())
                       select new EntityEmployee
                       {
                           EmpCode = tbl.EmpCode,
                           EmpFirstName = tbl.EmpFirstName,
                           EmpMiddleName = tbl.EmpMiddleName,
                           EmpLastName = tbl.EmpLastName,
                           EmpAddress = tbl.EmpAddress,
                           EmpDOB = Convert.ToDateTime(tbl.EmpDOB),
                           EmpDOJ = Convert.ToDateTime(tbl.EmpDOJ),
                           BankName = tbl.BankName,
                           BankACNo = tbl.BankACNo,
                           PFNo = tbl.PFNo,
                           PanNo = tbl.PanNo,
                           BasicSal = Convert.ToDecimal(tbl.BasicSalary)

                       }).ToList();
                return lst;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GetEmpName(string UserCode)
        {
            EntityEmployee entEmpp = new EntityEmployee();

            entEmpp = (from tbl in objData.tblEmployees
                       where tbl.EmpCode.Equals(UserCode)
                       && tbl.IsDelete == false
                       select new EntityEmployee { EmpFirstName = tbl.EmpFirstName + ' ' + tbl.EmpMiddleName + ' ' + tbl.EmpLastName }).FirstOrDefault();

            return entEmpp.EmpFirstName;
        }
    }
}