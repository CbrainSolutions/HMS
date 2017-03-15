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
    public class GenderBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public GenderBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataTable GetNewGenderCode()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetNewGenderCode");

            }
            catch (Exception ex)
            {

                Commons.FileLog("GenderBLL - GetNewGenderCode()", ex);
            }
            return ldt;
        }

        public DataTable GetAllGender()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllGender");

            }
            catch (Exception ex)
            {

                Commons.FileLog("GenderBLL - GetAllGender()", ex);
            }
            return ldt;
        }

        public int InsertGender(EntityGender entGender)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@GenderCode", DbType.String, entGender.GenderCode);
                Commons.ADDParameter(ref lstParam, "@GenderDesc", DbType.String, entGender.GenderDesc);
                Commons.ADDParameter(ref lstParam, "@EntryBy", DbType.String, entGender.EntryBy);
                cnt = mobjDataAcces.ExecuteQuery("sp_InsertGender ", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("GenderBLL - InsertGender(EntityGender entGender)", ex);
            }
            return cnt;
        }

        public DataTable GetGenderForEdit(string pstrGenderCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@GenderCode", DbType.String, pstrGenderCode);
                ldt = mobjDataAcces.GetDataTable("sp_GetGenderForEdit", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("GenderBLL  - GetGenderForEdit(string pstrGenderCode)", ex);
            }
            return ldt;
        }

        public int UpdateGender(EntityGender entGender)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@GenderCode", DbType.String, entGender.GenderCode);
                Commons.ADDParameter(ref lstParam, "@GenderDesc", DbType.String, entGender.GenderDesc);
                Commons.ADDParameter(ref lstParam, "@ChangeBy", DbType.String, entGender.ChangeBy);
                cnt = mobjDataAcces.ExecuteQuery("sp_UpdateGender", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("GenderBLL -  UpdateGender(EntityGender entGender)", ex);
            }

            return cnt;
        }

        public int DeleteGender(EntityGender entGender)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@GenderCode", DbType.String, entGender.GenderCode);
                cnt = mobjDataAcces.ExecuteQuery("sp_DeleteGender", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("GenderTypeBLL - DeleteGender(EntityGender entGender)", ex);
            }
            return cnt;
        }
    }
}