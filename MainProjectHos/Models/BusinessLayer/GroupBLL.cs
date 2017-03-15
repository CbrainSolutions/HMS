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
    public class GroupBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public GroupBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //public DataTable GetNewGroupCode()
        //{
        //    DataTable ldt = new DataTable();
        //    try
        //    {
        //        ldt = mobjDataAcces.GetDataTable("sp_GetNewGroupCode");

        //    }
        //    catch (Exception ex)
        //    {

        //        Commons.FileLog("GroupBLL - GetNewGroupCode()", ex);
        //    }
        //    return ldt;
        //}

        public DataTable GetAllGroup()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllGroup");

            }
            catch (Exception ex)
            {

                Commons.FileLog("GroupBLL - GetAllGroup()", ex);
            }
            return ldt;
        }

        public int InsertGroup(EntityGroup entGroup)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@GroupDesc", DbType.String, entGroup.GroupDesc);
                Commons.ADDParameter(ref lstParam, "@EntryBy", DbType.String, entGroup.EntryBy);
                cnt = mobjDataAcces.ExecuteQuery("sp_InsertGroup ", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("GroupBLL - InsertGroup(EntityGroup entGroup)", ex);
            }
            return cnt;
        }

        public DataTable GetGroupForEdit(int pintGroupCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@PKId", DbType.Int32, pintGroupCode);
                ldt = mobjDataAcces.GetDataTable("sp_GetGroupForEdit", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("GroupBLL  - GetGroupForEdit(int pintGroupCode)", ex);
            }
            return ldt;
        }

        public int UpdateGroup(EntityGroup entGroup)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@PKId ", DbType.Int32, entGroup.PKId);
                Commons.ADDParameter(ref lstParam, "@GroupDesc", DbType.String, entGroup.GroupDesc);
                Commons.ADDParameter(ref lstParam, "@ChangeBy", DbType.String, entGroup.ChangeBy);
                cnt = mobjDataAcces.ExecuteQuery("sp_UpdateGroup", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("GroupBLL -  UpdateGroup(EntityGroup entGroup)", ex);
            }

            return cnt;
        }

        public int DeleteGroup(EntityGroup entGroup)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@PKId", DbType.Int32, entGroup.PKId);
                cnt = mobjDataAcces.ExecuteQuery("sp_DeleteGroup", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("GroupTypeBLL - DeleteGroup(EntityGroup entGroup)", ex);
            }
            return cnt;
        }
    }
}