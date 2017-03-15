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
    public class InitialsBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public InitialsBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataTable GetNewInitialCode()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetNewInitialsCode");
            }
            catch (Exception ex)
            {
                Commons.FileLog("InitialsBLL - GetNewInitialCode()", ex);
            }
            return ldt;
        }
        public DataTable GetAllInitials()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllInitials");
            }
            catch (Exception ex)
            {
                Commons.FileLog("InitialsBLL - GetAllInitials()", ex);
            }
            return ldt;
        }
        public int InsertInitial(EntityInitials entInitial)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@InitialCode", DbType.String, entInitial.InitialCode);
                Commons.ADDParameter(ref lstParam, "@InitialDesc", DbType.String, entInitial.InitialDesc);
                Commons.ADDParameter(ref lstParam, "@EntryBy", DbType.String, entInitial.EntryBy);
                cnt = mobjDataAcces.ExecuteQuery("sp_InsertInitial", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("InitialsBLL -  InsertInitial(EntityInitials entInitial)", ex);
            }

            return cnt;
        }

        //public int InsertInitial(EntityDepartment entInitial)
        //{
        //    throw new NotImplementedException();
        //}

        public DataTable GetInitialForEdit(string pstrInitialCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@InitialCode", DbType.String, pstrInitialCode);
                ldt = mobjDataAcces.GetDataTable("sp_GetInitialForEdit", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("InitialBLL - GetInitialForEdit(string pstrInitialCode)", ex);
            }
            return ldt;
        }

        public int UpdateInitial(EntityInitials entInitial)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@InitialCode", DbType.String, entInitial.InitialCode);
                Commons.ADDParameter(ref lstParam, "@InitialDesc", DbType.String, entInitial.InitialDesc);
                Commons.ADDParameter(ref lstParam, "@ChangeBy", DbType.String, entInitial.ChangeBy);
                cnt = mobjDataAcces.ExecuteQuery("sp_UpdateInitial", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("InitialsBLL -  UpdateInitial(EntityInitials entInitial)", ex);
            }

            return cnt;
        }

        public int DeleteInitial(EntityInitials entInitial)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@InitialCode", DbType.String, entInitial.InitialCode);
                cnt = mobjDataAcces.ExecuteQuery("sp_DeleteInitial", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("InitialsBLL - DeleteInitial(EntityInitials entInitial)", ex);
            }
            return cnt;
        }
    }
}