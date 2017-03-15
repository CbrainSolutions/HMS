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
    public class DesignationBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();

        public DesignationBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataTable GetNewDesignationCode()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetNewDesignationCode");
            }
            catch (Exception ex)
            {
                Commons.FileLog("DesignationBLL - GetNewDesignationCode()", ex);
            }
            return ldt;
        }

        public DataTable GetAllDesignations()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllDesignations");
            }
            catch (Exception ex)
            {
                Commons.FileLog("DesignationBLL - GetAllDesignations()", ex);
            }
            return ldt;
        }

        public DataTable GetDesignationForEdit(string pstrDesignationCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@DesignationCode", DbType.String, pstrDesignationCode);
                ldt = mobjDataAcces.GetDataTable("sp_GetDesignationForEdit", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("DesignationBLL - GetDesignationForEdit(string pstrDesignationCode)", ex);
            }
            return ldt;
        }

        public int InsertDesignation(EntityDesignation entDesignation)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@DesignationCode", DbType.String, entDesignation.DesignationCode);
                Commons.ADDParameter(ref lstParam, "@DesignationDesc", DbType.String, entDesignation.DesignationDesc);
                Commons.ADDParameter(ref lstParam, "@EntryBy", DbType.String, entDesignation.EntryBy);
                cnt = mobjDataAcces.ExecuteQuery("sp_InsertDesignation", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("DesignationBLL -  InsertDesignation(EntityDesignation entDesignation)", ex);
            }

            return cnt;
        }

        public int UpdateDesignation(EntityDesignation entDesignation)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@DesignationCode", DbType.String, entDesignation.DesignationCode);
                Commons.ADDParameter(ref lstParam, "@DesignationDesc", DbType.String, entDesignation.DesignationDesc);
                Commons.ADDParameter(ref lstParam, "@ChangeBy", DbType.String, entDesignation.ChangeBy);
                cnt = mobjDataAcces.ExecuteQuery("sp_UpdateDesignationMaster", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("DesignationBLL -  UpdateDesignation(EntityDesignation entDesignation)", ex);
            }

            return cnt;
        }

        public int DeleteDesignation(EntityDesignation entDesignation)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@DesignationCode", DbType.String, entDesignation.DesignationCode);
                cnt = mobjDataAcces.ExecuteQuery("sp_DeleteDesignation", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("DesignationBLL - DeleteDesignation(EntityDesignation entDesignation)", ex);
            }
            return cnt;
        }
    }
}