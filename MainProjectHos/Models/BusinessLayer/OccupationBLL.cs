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
    public class OccupationBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public OccupationBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        //public DataTable GetNewOccupationCode()
        //{
        //    DataTable ldt = new DataTable();
        //    try
        //    {
        //        ldt = mobjDataAcces.GetDataTable("sp_GetNewOccupationCode");

        //    }
        //    catch (Exception ex)
        //    {

        //        Commons.FileLog("OccupationBLL - GetNewOccupationCode()", ex);
        //    }
        //    return ldt;
        //}

        public DataTable GetAllOccupation()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllOccupation");

            }
            catch (Exception ex)
            {

                Commons.FileLog("OccupationBLL - GetAllOccupation()", ex);
            }
            return ldt;
        }
        public int InsertOccupation(EntityOccupation entOccupation)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@OccupationDesc", DbType.String, entOccupation.OccupationDesc);
                Commons.ADDParameter(ref lstParam, "@EntryBy", DbType.String, entOccupation.EntryBy);
                cnt = mobjDataAcces.ExecuteQuery("sp_InsertOccupation ", lstParam);
            }
            catch (Exception ex)
            {

                Commons.FileLog("OccupationBLL - InsertOccupation(EntityOccupation entOccupation)", ex);
            }
            return cnt;
        }



        public DataTable GetOccupationForEdit(int pstrOccupationCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@PKId", DbType.Int32, pstrOccupationCode);
                ldt = mobjDataAcces.GetDataTable("sp_GetOccupationForEdit", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("OccupationBLL - GetOccupationForEdit(string pstrOccupationCode)", ex);
            }
            return ldt;
        }

        public int UpdateOccupation(EntityOccupation entOccupation)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@PKId", DbType.Int32, entOccupation.PKId);
                Commons.ADDParameter(ref lstParam, "@OccupationDesc", DbType.String, entOccupation.OccupationDesc);
                Commons.ADDParameter(ref lstParam, "@ChangeBy", DbType.String, entOccupation.ChangeBy);
                cnt = mobjDataAcces.ExecuteQuery("sp_UpdateOccupation", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("OccupationBLL -  UpdateOccupation(EntityOccupation entOccupation)", ex);
            }

            return cnt;
        }

        public int DeleteOccupation(EntityOccupation entOccupation)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@PKId", DbType.Int32, entOccupation.PKId);
                cnt = mobjDataAcces.ExecuteQuery("sp_DeleteOccupation", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("OccupationBLL - DeleteOccupation(EntityOccupation entOccupation)", ex);
            }
            return cnt;
        }
    }
}