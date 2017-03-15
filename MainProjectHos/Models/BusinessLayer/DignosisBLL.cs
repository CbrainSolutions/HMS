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
    public class DignosisBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public DignosisBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataTable GetNewDignosisCode()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetNewDignosisCode");

            }
            catch (Exception ex)
            {

                Commons.FileLog("DignosisBLL - GetNewDignosisCode()", ex);
            }
            return ldt;
        }

        public DataTable GetAllDignosis()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllDignosis");

            }
            catch (Exception ex)
            {

                Commons.FileLog("DignosisBLL - GetAllDignosis()", ex);
            }
            return ldt;
        }
        public int InsertDignosis(EntityDignosis entDignosis)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@DignosisCode", DbType.String, entDignosis.DignosisCode);
                Commons.ADDParameter(ref lstParam, "@DignosisDesc", DbType.String, entDignosis.DignosisDesc);
                Commons.ADDParameter(ref lstParam, "@EntryBy", DbType.String, entDignosis.EntryBy);
                cnt = mobjDataAcces.ExecuteQuery("sp_InsertDignosis ", lstParam);
            }
            catch (Exception ex)
            {

                Commons.FileLog("DignosisBLL - InsertDignosis(EntityDignosis entDignosis)", ex);
            }
            return cnt;
        }



        public DataTable GetDignosisForEdit(string pstrDignosisCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@DignosisCode", DbType.String, pstrDignosisCode);
                ldt = mobjDataAcces.GetDataTable("sp_GetDignosisForEdit", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("DignosisBLL - GetDignosisForEdit(string pstrDignosisCode)", ex);
            }
            return ldt;
        }

        public int UpdateDignosis(EntityDignosis entDignosis)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@DignosisCode", DbType.String, entDignosis.DignosisCode);
                Commons.ADDParameter(ref lstParam, "@DignosisDesc", DbType.String, entDignosis.DignosisDesc);
                Commons.ADDParameter(ref lstParam, "@ChangeBy", DbType.String, entDignosis.ChangeBy);
                cnt = mobjDataAcces.ExecuteQuery("sp_UpdateDignosis", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("DignosisBLL -  UpdateDignosis(EntityDignosis entDignosis)", ex);
            }

            return cnt;
        }

        public int DeleteDignosis(EntityDignosis entDignosis)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@DignosisCode", DbType.String, entDignosis.DignosisCode);
                cnt = mobjDataAcces.ExecuteQuery("sp_DeleteDignosis", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("DignosisBLL - DeleteDignosis(EntityDignosis entDignosis)", ex);
            }
            return cnt;
        }
    }
}