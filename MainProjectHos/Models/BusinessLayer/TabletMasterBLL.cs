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
    public class TabletMasterBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public TabletMasterBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataTable GetNewReligionCode()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetNewTabletCode");

            }
            catch (Exception ex)
            {

                Commons.FileLog("TabletMasterBLL - GetNewReligionCode()", ex);
            }
            return ldt;
        }

        public DataTable GetAllReligion()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllTablet");

            }
            catch (Exception ex)
            {

                Commons.FileLog("TabletMasterBLL - GetAllNurse()", ex);
            }
            return ldt;
        }

        public int InsertReligion(EntityTabletMaster entReligion)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@TabletCode", DbType.String, entReligion.TabletCode);
                Commons.ADDParameter(ref lstParam, "@MedicineName", DbType.String, entReligion.MedicineName);
                cnt = mobjDataAcces.ExecuteQuery("sp_InsertTablet ", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("TabletMasterBLL - InsertReligion(EntityReligion entReligion)", ex);
            }
            return cnt;
        }

        public DataTable GetReligionForEdit(string pstrReligionCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@TabletCode", DbType.String, pstrReligionCode);
                ldt = mobjDataAcces.GetDataTable("sp_GetTabletForEdit", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("TabletMasterBLL  - GetNurseForEdit(string pstrReligionCode)", ex);
            }
            return ldt;
        }

        public int UpdateReligion(EntityTabletMaster entReligion)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@TabletCode", DbType.String, entReligion.TabletCode);
                Commons.ADDParameter(ref lstParam, "@MedicineName", DbType.String, entReligion.MedicineName);
                cnt = mobjDataAcces.ExecuteQuery("sp_UpdateTablet", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("TabletMasterBLL -  UpdateNurse(EntityNurse entReligion)", ex);
            }

            return cnt;
        }

        public int DeleteReligion(EntityTabletMaster entReligion)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@TabletCode", DbType.String, entReligion.TabletCode);
                cnt = mobjDataAcces.ExecuteQuery("sp_DeleteTablet", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("TabletMasterBLL - DeleteNurse(EntityNurse entReligion)", ex);
            }
            return cnt;
        }
    }

}