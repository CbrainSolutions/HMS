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
    public class ConsultantChargeBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public ConsultantChargeBLL()
        {
            //
            // TODO: Add constructor logic here
            //
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
                Commons.FileLog("ConsultantChargeBLL - GetConsultants()", ex);
            }
            return ldt;
        }

        public DataTable GetWard()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetWardForConsultant");
            }
            catch (Exception ex)
            {
                Commons.FileLog("ConsultantChargeBLL - GetWard()", ex);
            }
            return ldt;
        }

        public DataTable GetChargeDetailForEdit(int pintPKId)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@PKId", DbType.Int32, pintPKId);
                ldt = mobjDataAcces.GetDataTable("sp_GetConsultantChargeForEdit", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("ConsultantChargeBLL - GetChargeDetailForEdit()", ex);
            }
            return ldt;
        }

        public DataTable GetConsultantCharges()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetConsultantCharges");
            }
            catch (Exception ex)
            {
                Commons.FileLog("ConsultantChargeBLL - GetConsultantCharges()", ex);
            }
            return ldt;
        }

        public int InsertConsultantCharges(EntityConsultantChargeMaster entChargeMaster)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@ConsultantId", DbType.Int32, entChargeMaster.ConsultantId);
                Commons.ADDParameter(ref lstParam, "@Ward", DbType.Int32, entChargeMaster.WardNo);
                Commons.ADDParameter(ref lstParam, "@Charge", DbType.Decimal, entChargeMaster.Charge);
                Commons.ADDParameter(ref lstParam, "@EntryBy", DbType.String, entChargeMaster.UserName);
                cnt = mobjDataAcces.ExecuteQuery("sp_InsertConsultantCharge", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("ConsultantChargeBLL - InsertConsultantCharges(EntityConsultantChargeMaster entChargeMaster)", ex);
            }
            return cnt;
        }

        public int UpdateConsultantCharges(EntityConsultantChargeMaster entChargeMaster)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@PKId", DbType.Int32, entChargeMaster.PKId);
                Commons.ADDParameter(ref lstParam, "@ConsultantId", DbType.Int32, entChargeMaster.ConsultantId);
                Commons.ADDParameter(ref lstParam, "@Ward", DbType.Int32, entChargeMaster.WardNo);
                Commons.ADDParameter(ref lstParam, "@Charge", DbType.Decimal, entChargeMaster.Charge);
                Commons.ADDParameter(ref lstParam, "@ChangeBy", DbType.String, entChargeMaster.UserName);
                cnt = mobjDataAcces.ExecuteQuery("sp_UpdateConsultantChargeMaster", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("ConsultantChargeBLL - UpdateConsultantCharges(EntityConsultantChargeMaster entChargeMaster)", ex);
            }
            return cnt;
        }

        public int DeleteConsultantCharges(int pintPKId)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@PKId", DbType.Int32, pintPKId);
                cnt = mobjDataAcces.ExecuteQuery("sp_DeleteConsultantChargeMaster", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("ConsultantChargeBLL - DeleteConsultantCharges(int pintPKId)", ex);
            }
            return cnt;
        }
    }

}