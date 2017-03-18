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
    public class NurseBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public NurseBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataTable GetNewReligionCode()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetNewNurseCode");

            }
            catch (Exception ex)
            {

                Commons.FileLog("NurseBLL - GetNewReligionCode()", ex);
            }
            return ldt;
        }

        public DataTable GetAllReligion()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllNurse");

            }
            catch (Exception ex)
            {

                Commons.FileLog("NurseBLL - GetAllNurse()", ex);
            }
            return ldt;
        }

        public DataTable GetAllIPDPatient()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllIPDPatientListNew");

            }
            catch (Exception ex)
            {

                Commons.FileLog("NurseBLL - GetAllNurse()", ex);
            }
            return ldt;
        }

        public int InsertReligion(EntityNurse entReligion)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@NurseCode", DbType.String, entReligion.NurseCode);
                Commons.ADDParameter(ref lstParam, "@NurseName", DbType.String, entReligion.NurseName);
                Commons.ADDParameter(ref lstParam, "@DeptId", DbType.Int32, entReligion.DeptId);
                Commons.ADDParameter(ref lstParam, "@DepartmentName", DbType.String, entReligion.DepartmentName);
                cnt = mobjDataAcces.ExecuteQuery("sp_InsertNurse ", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("NurseBLL - InsertReligion(EntityReligion entReligion)", ex);
            }
            return cnt;
        }

        public DataTable GetReligionForEdit(string pstrReligionCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@NurseCode", DbType.String, pstrReligionCode);
                ldt = mobjDataAcces.GetDataTable("sp_GetNurseForEdit", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("NurseBLL  - GetNurseForEdit(string pstrReligionCode)", ex);
            }
            return ldt;
        }

        public int UpdateReligion(EntityNurse entReligion)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@NurseCode", DbType.String, entReligion.NurseCode);
                Commons.ADDParameter(ref lstParam, "@NurseName", DbType.String, entReligion.NurseName);
                Commons.ADDParameter(ref lstParam, "@DeptId", DbType.Int32, entReligion.DeptId);
                Commons.ADDParameter(ref lstParam, "@DepartmentName", DbType.String, entReligion.DepartmentName);
                cnt = mobjDataAcces.ExecuteQuery("sp_UpdateNurse", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("NurseBLL -  UpdateNurse(EntityNurse entReligion)", ex);
            }

            return cnt;
        }

        public int DeleteReligion(EntityNurse entReligion)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@NurseCode", DbType.String, entReligion.NurseCode);
                cnt = mobjDataAcces.ExecuteQuery("sp_DeleteNurse", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("NurseBLL - DeleteNurse(EntityNurse entReligion)", ex);
            }
            return cnt;
        }
    }
}