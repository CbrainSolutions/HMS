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
    public class PatientTypeBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public PatientTypeBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataTable GetNewPatientCode()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetNewPatientTypeCode");

            }
            catch (Exception ex)
            {

                Commons.FileLog("PatientTypeBLL - GetNewPatientCode()", ex);
            }
            return ldt;
        }

        public DataTable GetAllPatient()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllPatientType");

            }
            catch (Exception ex)
            {

                Commons.FileLog("PatientTypeBLL - GetAllPatient()", ex);
            }
            return ldt;
        }

        public int InsertPatient(EntityPatientType entPatient)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@PatientCode", DbType.String, entPatient.PatientCode);
                Commons.ADDParameter(ref lstParam, "@PatientDesc", DbType.String, entPatient.PatientDesc);
                Commons.ADDParameter(ref lstParam, "@EntryBy", DbType.String, entPatient.EntryBy);
                cnt = mobjDataAcces.ExecuteQuery("sp_InsertPatient ", lstParam);
            }
            catch (Exception ex)
            {

                Commons.FileLog("PatientTypeBLL -InsertPatient(EntityPatientType entPatient)", ex);
            }
            return cnt;
        }

        public DataTable GetPatientForEdit(string pstrPatientCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@PatientCode", DbType.String, pstrPatientCode);
                ldt = mobjDataAcces.GetDataTable("sp_GetPatientForEdit", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("PatientTypeBLL  - GetPatientForEdit(string pstrPatientCode)", ex);
            }
            return ldt;
        }

        public int UpdatePatient(EntityPatientType entPatient)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@PatientCode", DbType.String, entPatient.PatientCode);
                Commons.ADDParameter(ref lstParam, "@PatientDesc", DbType.String, entPatient.PatientDesc);
                Commons.ADDParameter(ref lstParam, "@ChangeBy", DbType.String, entPatient.ChangeBy);
                cnt = mobjDataAcces.ExecuteQuery("sp_UpdatePatient", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("PatientTypeBLL -   UpdatePatient(EntityPatientType entPatient)", ex);
            }

            return cnt;
        }

        public int DeletePatient(EntityPatientType entPatient)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@PatientCode", DbType.String, entPatient.PatientCode);
                cnt = mobjDataAcces.ExecuteQuery("sp_DeletePatient", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("PatientTypeBLL - DeletePatient(EntityPatientType entPatient)", ex);
            }
            return cnt;
        }
    }
}