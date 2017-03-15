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
    public class GetLoginBLL
    {
        clsDataAccess mobjDataAccess = new clsDataAccess();

        public GetLoginBLL()
        {
            objData = new CriticareHospitalDataContext();
        }

        public CriticareHospitalDataContext objData { get; set; }


        public DataTable GetLogin(EntityLogin entLogin)
        {
            DataTable dt = new DataTable();
            List<SqlParameter> lstParam = new List<SqlParameter>();
            try
            {
                Commons.ADDParameter(ref lstParam, "@UserName", DbType.String, entLogin.UserName);
                Commons.ADDParameter(ref lstParam, "@Password", DbType.String, entLogin.Password);
                Commons.ADDParameter(ref lstParam, "@UserType", DbType.String, entLogin.UserType);
                dt = mobjDataAccess.GetDataTable("sp_GetLogin", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("GetLoginBLL - GetLogin(EntityLogin entLogin)", ex);
            }
            return dt;

        }

        public int ChangePassword(EntityLogin entLogin)
        {
            int lintCnt = 0;
            List<SqlParameter> listparam = new List<SqlParameter>();
            try
            {
                Commons.ADDParameter(ref listparam, "@UserName", DbType.String, entLogin.UserName);
                Commons.ADDParameter(ref listparam, "@ConfirmPass", DbType.String, entLogin.ConfirmPass);
                lintCnt = mobjDataAccess.ExecuteQuery("sp_ChangePassword", listparam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("GetLoginBLL - ChangePassword(EntityLogin entLogin)", ex);
            }
            return lintCnt;
        }

        public DataTable GetDepartments()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = mobjDataAccess.GetDataTable("sp_GetDepartments");
            }
            catch (Exception ex)
            {

                Commons.FileLog("RegistrationBLL - GetDepartments()", ex);
            }
            return dt;

        }


        public int Update(EntityLogin entLog)
        {
            int Result = 0;
            tblLogin objUser = (from tbl in objData.tblLogins
                                where tbl.PKId.Equals(entLog.PKId)
                                select tbl).FirstOrDefault();
            objUser.Password = entLog.Password;
            objUser.IsFirstLogin = false;
            objData.SubmitChanges();
            if (objUser != null)
            {
                Result = 1;
            }
            else
            {
                Result = 0;
            }
            return Result;
        }

        public bool CheckLogin(string UserName)
        {
            bool flag1 = false;
            try
            {
                tblLogin objCharge = (from tbl in objData.tblLogins
                                      where tbl.UserName.ToUpper().ToString().Trim().Equals(UserName)
                                      && tbl.IsFirstLogin.Equals(true)
                                      select tbl).FirstOrDefault();
                if (objCharge != null)
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
    }
}