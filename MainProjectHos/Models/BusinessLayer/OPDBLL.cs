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
    public class OPDBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public OPDBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataTable GetAllOPD()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllOPD");
            }
            catch (Exception ex)
            {
                Commons.FileLog("OPDBLL - GetAllOPD()", ex);
            }
            return ldt;
        }


        public int InsertOPD(EntityOPD entOPD)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@OPDDesc", DbType.String, entOPD.OPDDesc);
                cnt = mobjDataAcces.ExecuteQuery("sp_InsertOPD ", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("OPDBLL - InsertOPD(EntityOPD entOPD)", ex);
            }
            return cnt;
        }

        public DataTable GetOPDForEdit(string pstrOPDCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@PKId", DbType.String, pstrOPDCode);
                ldt = mobjDataAcces.GetDataTable("sp_GetOPDForEdit", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("OPDBLL - GetOPDForEdit(string pstrOPDCode)", ex);
            }
            return ldt;
        }

        public int UpdateOPD(EntityOPD entOPD)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@PKId", DbType.Int32, entOPD.OPDCode);
                Commons.ADDParameter(ref lstParam, "@OPDDesc", DbType.String, entOPD.OPDDesc);

                cnt = mobjDataAcces.ExecuteQuery("sp_UpdateOPD", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("OPDBLL -  UpdateOPD(EntityOPD entOPD)", ex);
            }

            return cnt;
        }

        public int DeleteOPD(EntityOPD entOPD)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@PKId", DbType.String, entOPD.OPDCode);
                cnt = mobjDataAcces.ExecuteQuery("sp_DeleteOPD", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("OPDBLL - DeleteOPD(EntityOPD entOPD)", ex);
            }
            return cnt;
        }
    }

}