using MainProjectHos.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using MainProjectHos.Models.DataLayer;

namespace MainProjectHos.Models.BusinessLayer
{
    public class RegistrationBLL
    {
        clsDataAccess mobjDataAccess = new clsDataAccess();
        public RegistrationBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int GetInsertPass(EntityRegistration ent)
        {
            int lintCnt = 0;
            List<SqlParameter> lstParam = new List<SqlParameter>();
            try
            {
                Commons.ADDParameter(ref lstParam, "@UserName", DbType.String, ent.UserName);
                Commons.ADDParameter(ref lstParam, "@ConfirmPass", DbType.String, ent.ConfirmPassword);
                lintCnt = mobjDataAccess.ExecuteQuery("sp_UpdatePassInEmpMaster", lstParam);
            }
            catch (Exception ex)
            {

                Commons.FileLog("RegistrationBLL - GetInsertPass(EntityRegistration ent)", ex);
            }
            return lintCnt;

        }

        public int GetRegister(EntityRegistration ent)
        {
            int lintcnt = 0;
            List<string> lstspNames = new List<string>();
            List<List<SqlParameter>> lstParamVals = new List<List<SqlParameter>>();
            List<SqlParameter> lstParam;
            try
            {
                lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@UserName", DbType.String, ent.UserName);
                Commons.ADDParameter(ref lstParam, "@ConfirmPass", DbType.String, ent.ConfirmPassword);
                Commons.ADDParameter(ref lstParam, "@UserType", DbType.String, ent.UserType);
                lstspNames.Add("sp_InsertPassInLoginTable");
                lstParamVals.Add(lstParam);

                lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@UserCode", DbType.String, ent.UserName);
                Commons.ADDParameter(ref lstParam, "@ConfirmPass", DbType.String, ent.ConfirmPassword);
                Commons.ADDParameter(ref lstParam, "@UserType", DbType.String, ent.UserType);
                lstspNames.Add("sp_UpdatePassInEmpMaster");
                lstParamVals.Add(lstParam);

                lintcnt = mobjDataAccess.ExecuteTransaction(lstspNames, lstParamVals);

            }
            catch (Exception ex)
            {
                Commons.FileLog("RegistrationBLL - GetRegister(EntityRegistration ent)", ex);
            }

            return lintcnt;

        }

        public DataTable GetEmployee()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = mobjDataAccess.GetDataTable("sp_GetEmployees");
            }
            catch (Exception ex)
            {

                Commons.FileLog("RegistrationBLL - sp_GetEmployees()", ex);
            }
            return dt;

        }

        public DataTable GetDepartment(string pstrEmpCode)
        {
            DataTable dt = new DataTable();
            List<SqlParameter> lstParam = new List<SqlParameter>();
            try
            {
                Commons.ADDParameter(ref lstParam, "@UserCode", DbType.String, pstrEmpCode);
                dt = mobjDataAccess.GetDataTable("sp_GetDepartment", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("RegistrationBLL - GetDepartment(string pstrEmpCode)", ex);
            }
            return dt;
        }
    }
}