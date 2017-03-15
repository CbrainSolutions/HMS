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
    public class CountryBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public CountryBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataTable GetNewCountryCode()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetNewCountryCode");

            }
            catch (Exception ex)
            {

                Commons.FileLog("CountryBLL - GetNewCountryCode()", ex);
            }
            return ldt;
        }

        public DataTable GetAllCountry()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllCountry");

            }
            catch (Exception ex)
            {

                Commons.FileLog("CountryBLL - GetAllCountry()", ex);
            }
            return ldt;
        }
        public int InsertCountry(EntityCountry entCountry)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@CountryCode", DbType.String, entCountry.CountryCode);
                Commons.ADDParameter(ref lstParam, "@CountryDesc", DbType.String, entCountry.CountryDesc);
                Commons.ADDParameter(ref lstParam, "@EntryBy", DbType.String, entCountry.EntryBy);
                cnt = mobjDataAcces.ExecuteQuery("sp_InsertCountry ", lstParam);
            }
            catch (Exception ex)
            {

                Commons.FileLog("CountryBLL - InsertCountry(EntityCountry entCountry)", ex);
            }
            return cnt;
        }



        public DataTable GetCountryForEdit(string pstrCountryCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@CountryCode", DbType.String, pstrCountryCode);
                ldt = mobjDataAcces.GetDataTable("sp_GetCountryForEdit", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("CountryBLL - GetCountryForEdit(string pstrCountryCode)", ex);
            }
            return ldt;
        }

        public int UpdateCountry(EntityCountry entCountry)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@CountryCode", DbType.String, entCountry.CountryCode);
                Commons.ADDParameter(ref lstParam, "@CountryDesc", DbType.String, entCountry.CountryDesc);
                Commons.ADDParameter(ref lstParam, "@ChangeBy", DbType.String, entCountry.ChangeBy);
                cnt = mobjDataAcces.ExecuteQuery("sp_UpdateCountry", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("CountryBLL -  UpdateCountry(EntityCountry entCountry)", ex);
            }

            return cnt;
        }

        public int DeleteCountry(EntityCountry entCountry)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@CountryCode", DbType.String, entCountry.CountryCode);
                cnt = mobjDataAcces.ExecuteQuery("sp_DeleteCountry", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("CountryBLL - DeleteCountry(EntityCountry entCountry)", ex);
            }
            return cnt;
        }
    }
}