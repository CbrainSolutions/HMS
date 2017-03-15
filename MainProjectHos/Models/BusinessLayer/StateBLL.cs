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
    public class StateBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public StateBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataTable GetAllCountryForState()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllCountryForState");
            }
            catch (Exception ex)
            {

                Commons.FileLog("StateBLL -  GetAllCountryForState()", ex);
            }
            return ldt;
        }

        public DataTable GetNewStateCode()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetNewStateCode");

            }
            catch (Exception ex)
            {

                Commons.FileLog("StateBLL - GetNewStateCode()", ex);
            }
            return ldt;
        }

        public DataTable GetAllState()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllState");

            }
            catch (Exception ex)
            {

                Commons.FileLog("StateBLL - GetAllState()", ex);
            }
            return ldt;
        }
        public int InsertState(EntityState entState)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@StateCode", DbType.String, entState.StateCode);
                Commons.ADDParameter(ref lstParam, "@CountryId", DbType.Int32, entState.Country);
                Commons.ADDParameter(ref lstParam, "@StateDesc", DbType.String, entState.StateDesc);
                Commons.ADDParameter(ref lstParam, "@EntryBy", DbType.String, entState.EntryBy);
                cnt = mobjDataAcces.ExecuteQuery("sp_InsertState ", lstParam);
            }
            catch (Exception ex)
            {

                Commons.FileLog("StateBLL - InsertState(EntityState entState)", ex);
            }
            return cnt;
        }



        public DataTable GetStateForEdit(string pstrStateCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@StateCode", DbType.String, pstrStateCode);
                ldt = mobjDataAcces.GetDataTable("sp_GetStateForEdit", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("StateBLL - GetStateForEdit(string pstrStateCode)", ex);
            }
            return ldt;
        }

        public int UpdateState(EntityState entState)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@StateCode", DbType.String, entState.StateCode);
                Commons.ADDParameter(ref lstParam, "@CountryId", DbType.Int32, entState.Country);
                Commons.ADDParameter(ref lstParam, "@StateDesc", DbType.String, entState.StateDesc);
                Commons.ADDParameter(ref lstParam, "@ChangeBy", DbType.String, entState.ChangeBy);
                cnt = mobjDataAcces.ExecuteQuery("sp_UpdateState", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("StateBLL -  UpdateState(EntityState entState)", ex);
            }

            return cnt;
        }

        public int DeleteState(EntityState entState)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@StateCode", DbType.String, entState.StateCode);
                cnt = mobjDataAcces.ExecuteQuery("sp_DeleteState", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("StateBLL - DeleteState(EntityState entState)", ex);
            }
            return cnt;
        }
    }
}