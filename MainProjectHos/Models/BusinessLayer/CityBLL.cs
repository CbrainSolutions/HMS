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
    public class CityBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public CityBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //public DataTable GetNewCityCode()
        //{
        //    DataTable ldt = new DataTable();
        //    try
        //    {
        //        ldt = mobjDataAcces.GetDataTable("sp_GetNewCityCode");
        //    }
        //    catch (Exception ex)
        //    {
        //        Commons.FileLog("CityBLL - GetNewCityCode()", ex);
        //    }
        //    return ldt;
        //}

        public DataTable GetAllCity()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllCity");
            }
            catch (Exception ex)
            {
                Commons.FileLog("CityBLL - GetAllCity()", ex);
            }
            return ldt;
        }
        public DataTable GetAllState()
        {

            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllStateForCity");
            }
            catch (Exception ex)
            {
                Commons.FileLog("CityBLL - GetAllState()", ex);
            }
            return ldt;
        }

        public int InsertCity(EntityCity entCity)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@StateId", DbType.Int32, entCity.State);
                Commons.ADDParameter(ref lstParam, "@CityDesc", DbType.String, entCity.CityDesc);
                Commons.ADDParameter(ref lstParam, "@EntryBy", DbType.String, entCity.EntryBy);
                cnt = mobjDataAcces.ExecuteQuery("sp_InsertCity ", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("CityBLL - InsertCity(EntityCity entCity)", ex);
            }
            return cnt;
        }

        public DataTable GetCityForEdit(string pstrCityCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@PKId", DbType.String, pstrCityCode);
                ldt = mobjDataAcces.GetDataTable("sp_GetCityForEdit", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("CityBLL - GetCityForEdit(string pstrCityCode)", ex);
            }
            return ldt;
        }

        public int UpdateCity(EntityCity entCity)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@PKId", DbType.Int32, entCity.CityCode);
                Commons.ADDParameter(ref lstParam, "@StateId", DbType.Int32, entCity.State);
                Commons.ADDParameter(ref lstParam, "@CityDesc", DbType.String, entCity.CityDesc);
                Commons.ADDParameter(ref lstParam, "@ChangeBy", DbType.String, entCity.ChangeBy);
                cnt = mobjDataAcces.ExecuteQuery("sp_UpdateCity", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("CityBLL -  UpdateCity(EntityCity entCity)", ex);
            }

            return cnt;
        }

        public int DeleteCity(EntityCity entCity)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@PKId", DbType.String, entCity.CityCode);
                cnt = mobjDataAcces.ExecuteQuery("sp_DeleteCity", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("CityBLL - DeleteCity(EntityCity entCity)", ex);
            }
            return cnt;
        }
    }
}