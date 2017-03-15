using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MainProjectHos.Models.DataModels;
using MainProjectHos.Models.DataLayer;

namespace MainProjectHos.Models.BusinessLayer
{
    public class ChangePasswordBLL
    {
        clsDataAccess mobjDataAccess = new clsDataAccess();
        public ChangePasswordBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int ChangePassword(EntityLogin entLogin)
        {
            int lintCnt = 0;

            List<SqlParameter> lstparam = new List<SqlParameter>();
            try
            {
                Commons.ADDParameter(ref lstparam, "@UserName", DbType.String, entLogin.UserName);
                Commons.ADDParameter(ref lstparam, "@ConfirmPass", DbType.String, entLogin.ConfirmPass);
                lintCnt = mobjDataAccess.ExecuteQuery("sp_ChangePassword", lstparam);

            }
            catch (Exception ex)
            {
                Commons.FileLog("GetChangePasswordBLL - ChangePassword(EntityLogin entLogin)", ex);
            }
            return lintCnt;
        }

        public DataTable Checkpass(EntityLogin entLogin)
        {
            DataTable ldt = new DataTable();
            List<SqlParameter> lstparam = new List<SqlParameter>();

            try
            {
                Commons.ADDParameter(ref lstparam, "@UserName", DbType.String, entLogin.UserName);
                ldt = mobjDataAccess.GetDataTable("sp_CheckPass", lstparam);
            }
            catch (Exception ex)
            {

                Commons.FileLog("GetChangePasswordBLL -Checkpass(EntityLogin entChk)", ex); ;
            }
            return ldt;
        }
    }
}