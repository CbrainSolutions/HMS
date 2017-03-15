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
    public class CurrencyBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public CurrencyBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataTable GetNewCurrencyCode()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetNewCurrencyCode");

            }
            catch (Exception ex)
            {

                Commons.FileLog("CurrencyBLL - GetNewCurrencyCode()", ex);
            }
            return ldt;
        }

        public DataTable GetAllCurrency()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllCurrency");
            }
            catch (Exception ex)
            {
                Commons.FileLog("CurrencyBLL - GetAllCurrency()", ex);
            }
            return ldt;
        }

        public int InsertCurrency(EntityCurrency entCurrency)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@CurrencyCode", DbType.String, entCurrency.CurrencyCode);
                Commons.ADDParameter(ref lstParam, "@CurrencyDesc", DbType.String, entCurrency.CurrencyDesc);
                Commons.ADDParameter(ref lstParam, "@EntryBy", DbType.String, entCurrency.EntryBy);
                cnt = mobjDataAcces.ExecuteQuery("sp_InsertCurrency", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("CurrencyBLL -  InsertCurrency(EntityCurrency entCurrency)", ex);
            }
            return cnt;
        }

        public DataTable GetCurrencyForEdit(string pstrCurrencyCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@CurrencyCode", DbType.String, pstrCurrencyCode);
                ldt = mobjDataAcces.GetDataTable("sp_GetCurrencyForEdit", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("CurrencyBLL  - GetCurrencyForEdit(string pstrCurrencyCode)", ex);
            }
            return ldt;
        }

        public int UpdateCurrency(EntityCurrency entCurrency)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@CurrencyCode", DbType.String, entCurrency.CurrencyCode);
                Commons.ADDParameter(ref lstParam, "@CurrencyDesc", DbType.String, entCurrency.CurrencyDesc);
                Commons.ADDParameter(ref lstParam, "@ChangeBy", DbType.String, entCurrency.ChangeBy);
                cnt = mobjDataAcces.ExecuteQuery("sp_UpdateCurrency", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("CurrencyBLL -  UpdateCurrency(EntityCurrency entCurrency)", ex);
            }
            return cnt;
        }

        public int DeleteCurrency(EntityCurrency entCurrency)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@CurrencyCode", DbType.String, entCurrency.CurrencyCode);
                cnt = mobjDataAcces.ExecuteQuery("sp_DeleteCurrency", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("CurrencyBLL - DeleteCurrency(EntityCurrency entCurrency)", ex);
            }
            return cnt;
        }
    }
}