using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MainProjectHos.Models.DataModels;
using MainProjectHos.Models.DataLayer;

namespace MainProjectHos.Models.BusinessLayer
{
    public class BookAppoinmentBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public BookAppoinmentBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataTable GetNewAppoinmentCode()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetNewAppoinmentCode");
            }
            catch (Exception ex)
            {
                Commons.FileLog("BookAppoinmentBLL - GetNewAppoinmentCode()", ex);
            }
            return ldt;
        }

        public DataTable GetAllAppoinment()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllAppointments");
            }
            catch (Exception ex)
            {
                Commons.FileLog("BookAppoinmentBLL - GetAllAppoinment()", ex);
            }
            return ldt;
        }


        public int InsertAppoinment(EntityBookAppoinment entAppoinment)
        {

            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@PatientCode", DbType.String, entAppoinment.PatientCode);
                Commons.ADDParameter(ref lstParam, "@FullName", DbType.String, entAppoinment.FullName);
                Commons.ADDParameter(ref lstParam, "@DeptId", DbType.Int32, entAppoinment.DepartmentId);
                Commons.ADDParameter(ref lstParam, "@OPD", DbType.Int32, entAppoinment.OPDRoom);
                Commons.ADDParameter(ref lstParam, "@ConsutingDr", DbType.Int32, entAppoinment.ConsultingDr);
                Commons.ADDParameter(ref lstParam, "@VisitType", DbType.String, entAppoinment.VisitType);
                Commons.ADDParameter(ref lstParam, "@Shift", DbType.String, entAppoinment.Shift);
                cnt = mobjDataAcces.ExecuteQuery("sp_InsertAppoinment ", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("BookAppoinmentBLL - int InsertAppoinment(EntityBookAppoinment entAppoinment)", ex);
            }
            return cnt;
        }

        public int DeleteAppointment(int pintPKId)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@PKId", DbType.Int32, pintPKId);
                cnt = mobjDataAcces.ExecuteQuery("sp_DeleteAppointment", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("PatientMasterBLL - DeleteAppointment(int pintPKId)", ex);
            }
            return cnt;
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
                Commons.FileLog("PatientMasterBLL - GetAllOPD()", ex);
            }
            return ldt;
        }

        public DataTable GetConsultants()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetConsultants");
            }
            catch (Exception ex)
            {
                Commons.FileLog("PatientMasterBLL - GetConsultants()", ex);
            }
            return ldt;
        }

        public DataTable GetMedicalDepartments()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetMedicalDepartments");
            }
            catch (Exception ex)
            {
                Commons.FileLog("PatientMasterBLL - GetMedicalDepartments()", ex);
            }
            return ldt;
        }
        public DataTable GetPatientName(string pstrPatientCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@PatientCode", DbType.String, pstrPatientCode);
                ldt = mobjDataAcces.GetDataTable("sp_GetPatientName", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("PatientMasterBLL - GetPatientName(string pstrPatientCode)", ex);
            }
            return ldt;
        }
    }
}