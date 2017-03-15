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
    public class ReligionBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public ReligionBLL()
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
                ldt = mobjDataAcces.GetDataTable("sp_GetNewReligionCode");

            }
            catch (Exception ex)
            {

                Commons.FileLog("ReligionBLL - GetNewReligionCode()", ex);
            }
            return ldt;
        }

        public DataTable GetAllReligion()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllReligion");

            }
            catch (Exception ex)
            {

                Commons.FileLog("ReligionBLL - GetAllReligion()", ex);
            }
            return ldt;
        }

        public int InsertReligion(EntityReligion entReligion)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@ReligionCode", DbType.String, entReligion.ReligionCode);
                Commons.ADDParameter(ref lstParam, "@ReligionDesc", DbType.String, entReligion.ReligionDesc);
                Commons.ADDParameter(ref lstParam, "@EntryBy", DbType.String, entReligion.EntryBy);
                cnt = mobjDataAcces.ExecuteQuery("sp_InsertReligion ", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("ReligionBLL - InsertReligion(EntityReligion entReligion)", ex);
            }
            return cnt;
        }

        public DataTable GetReligionForEdit(string pstrReligionCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@ReligionCode", DbType.String, pstrReligionCode);
                ldt = mobjDataAcces.GetDataTable("sp_GetReligionForEdit", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("ReligionBLL  - GetReligionForEdit(string pstrReligionCode)", ex);
            }
            return ldt;
        }

        public int UpdateReligion(EntityReligion entReligion)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@ReligionCode", DbType.String, entReligion.ReligionCode);
                Commons.ADDParameter(ref lstParam, "@ReligionDesc", DbType.String, entReligion.ReligionDesc);
                Commons.ADDParameter(ref lstParam, "@ChangeBy", DbType.String, entReligion.ChangeBy);
                cnt = mobjDataAcces.ExecuteQuery("sp_UpdateReligion", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("ReligionBLL -  UpdateReligion(EntityReligion entReligion)", ex);
            }

            return cnt;
        }

        public int DeleteReligion(EntityReligion entReligion)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@ReligionCode", DbType.String, entReligion.ReligionCode);
                cnt = mobjDataAcces.ExecuteQuery("sp_DeleteReligion", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("ReligionBLL - DeleteReligion(EntityReligion entReligion)", ex);
            }
            return cnt;
        }
    }

}