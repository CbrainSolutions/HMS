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
    public class DepartmentBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();

        public DepartmentBLL()
        {
            objData = new CriticareHospitalDataContext();
            //
            // TODO: Add constructor logic here
            //
        }
        public CriticareHospitalDataContext objData { get; set; }

        public EntityDepartment GetDepartMent(EntityDepartment ent)
        {
            EntityDepartment obj = (from tbl in objData.tblDepartments
                                    where tbl.DeptDesc.Equals(ent.DeptDesc)
                                    && tbl.DeptCode.Equals(ent.DeptCode)
                                    select new EntityDepartment { DeptCode = tbl.DeptCode, DeptDesc = tbl.DeptDesc }).FirstOrDefault();
            return obj;
        }
        public EntityDepartment GetDepartMentByName(EntityDepartment ent)
        {
            EntityDepartment obj = (from tbl in objData.tblDepartments
                                    where tbl.DeptDesc.Equals(ent.DeptDesc)

                                    select new EntityDepartment { DeptCode = tbl.DeptCode, DeptDesc = tbl.DeptDesc }).FirstOrDefault();
            return obj;
        }

        public DataTable GetNewDeptCode()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetNewDeptCode");
            }
            catch (Exception ex)
            {
                Commons.FileLog("DepartmentBLL - GetNewDeptCode()", ex);
            }
            return ldt;
        }

        public DataTable GetAllDepartments()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllDepartments");
            }
            catch (Exception ex)
            {
                Commons.FileLog("DepartmentBLL - GetAllDepartments()", ex);
            }
            return ldt;
        }

        public DataTable GetDepartmentForEdit(string pstrDeptCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@DeptCode", DbType.String, pstrDeptCode);
                ldt = mobjDataAcces.GetDataTable("sp_GetDepartmentForEdit", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("DepartmentBLL - GetDepartmentForEdit(string pstrDeptCode)", ex);
            }
            return ldt;
        }

        public int InsertDepartment(EntityDepartment entDept)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@DeptCode", DbType.String, entDept.DeptCode);
                Commons.ADDParameter(ref lstParam, "@DeptDesc", DbType.String, entDept.DeptDesc);
                Commons.ADDParameter(ref lstParam, "@EntryBy", DbType.String, entDept.EntryBy);
                Commons.ADDParameter(ref lstParam, "@UserType", DbType.String, entDept.DeptDesc);
                cnt = mobjDataAcces.ExecuteQuery("sp_InsertDepartment", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("DepartmentBLL -  InsertDepartment(EntityDepartment entDept)", ex);
            }

            return cnt;
        }

        public int UpdateDepartment(EntityDepartment entDept)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@DeptCode", DbType.String, entDept.DeptCode);
                Commons.ADDParameter(ref lstParam, "@DeptDesc", DbType.String, entDept.DeptDesc);
                Commons.ADDParameter(ref lstParam, "@ChangeBy", DbType.String, entDept.ChangeBy);
                cnt = mobjDataAcces.ExecuteQuery("sp_UpdateDepartment", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("DepartmentBLL -  UpdateDepartment(EntityDepartment entDept)", ex);
            }

            return cnt;
        }

        public int DeleteDepartment(EntityDepartment entDept)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@DeptCode", DbType.String, entDept.DeptCode);
                cnt = mobjDataAcces.ExecuteQuery("sp_DeleteDepartment", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("DepartmentBLL - DeleteDepartment(EntityDepartment entDept)", ex);
            }
            return cnt;
        }
    }
}