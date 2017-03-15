using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MainProjectHos.Models.DataModels;
using MainProjectHos.Models.DataLayer;
using System.Data;
using System.Data.SqlClient;

namespace MainProjectHos.Models.BusinessLayer
{
    public class UnitBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public UnitBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataTable GetNewUnitCode()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetNewUnitCode");
            }
            catch (Exception ex)
            {
                Commons.FileLog("UnitBLL - GetNewUnitCode()", ex);
            }
            return ldt;
        }

        public DataTable GetAllUnit()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllUnit");

            }
            catch (Exception ex)
            {

                Commons.FileLog("UnitBLL - GetAllUnit()", ex);
            }
            return ldt;
        }
        public int InsertUnit(EntityUnit entUnit)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@UnitCode", DbType.String, entUnit.UnitCode);
                Commons.ADDParameter(ref lstParam, "@UnitDesc", DbType.String, entUnit.UnitDesc);

                cnt = mobjDataAcces.ExecuteQuery("sp_InsertUnit ", lstParam);
            }
            catch (Exception ex)
            {

                Commons.FileLog("UnitBLL - InsertUnit(EntityUnit entUnit)", ex);
            }
            return cnt;
        }



        public DataTable GetUnitForEdit(string pstrUnitCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@UnitCode", DbType.Int32, pstrUnitCode);
                ldt = mobjDataAcces.GetDataTable("sp_GetUnitForEdit", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("UnitBLL - GetUnitForEdit(string pstrUnitCode)", ex);
            }
            return ldt;
        }

        public int UpdateUnit(EntityUnit entUnit)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@UnitCode", DbType.String, entUnit.UnitCode);
                Commons.ADDParameter(ref lstParam, "@UnitDesc", DbType.String, entUnit.UnitDesc);

                cnt = mobjDataAcces.ExecuteQuery("sp_UpdateUnit", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("UnitBLL -  UpdateUnit(EntityUnit entUnit)", ex);
            }
            return cnt;
        }

        public int DeleteUnit(EntityUnit entUnit)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@PKId", DbType.Int32, entUnit.PKId);
                cnt = mobjDataAcces.ExecuteQuery("sp_DeleteUnit", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("UnitBLL - DeleteUnit(EntityUnit entUnit)", ex);
            }
            return cnt;
        }
    }
}