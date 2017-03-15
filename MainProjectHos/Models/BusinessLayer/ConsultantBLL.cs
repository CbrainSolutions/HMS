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
    public class ConsultantBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public ConsultantBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataTable GetNewConsultantCode()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetNewConsultantCode");
            }
            catch (Exception ex)
            {
                Commons.FileLog("ConsultantBLL - GetNewConsultantCode()", ex);
            }
            return ldt;
        }


        public DataTable SelectAllConsultant()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_SelectAllConsultant");
            }
            catch (Exception ex)
            {
                Commons.FileLog("ConsultantBLL - SelectAllConsultant()", ex);
            }
            return ldt;
        }

        public DataTable GetAllWards()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetWardForConsultant");
            }
            catch (Exception ex)
            {
                Commons.FileLog("ConsultantBLL - GetAllWards()", ex);
            }
            return ldt;
        }

        public DataTable SelectConsultantForEdit(string pstrConsCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@ConsCode", DbType.String, pstrConsCode);
                ldt = mobjDataAcces.GetDataTable("sp_SelectConsultantForEdit", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("ConsultantBLL - SelectAllConsultant()", ex);
            }
            return ldt;
        }

        //public DataTable GetConsultantName(string pstrConsCode)
        //{
        //    DataTable ldt = new DataTable();
        //    try
        //    {
        //        List<SqlParameter> lstParam = new List<SqlParameter>();
        //        Commons.ADDParameter(ref lstParam, "@ConsCode", DbType.String, pstrConsCode);
        //        ldt = mobjDataAcces.GetDataTable("GetConsultantName", lstParam);
        //    }
        //    catch (Exception ex)
        //    {
        //        Commons.FileLog("ConsultantBLL - GetConsultantName(string pstrEmpCode)", ex);
        //    }
        //    return ldt;
        //}

        private List<SqlParameter> CreateParameterInsertConsultant(EntityConsultant entConsultant)
        {
            List<SqlParameter> lstParam = new List<SqlParameter>();
            Commons.ADDParameter(ref lstParam, "@ConsCode", DbType.String, entConsultant.ConsultantCode);
            Commons.ADDParameter(ref lstParam, "@ConsFirstName", DbType.String, entConsultant.FirstName);
            Commons.ADDParameter(ref lstParam, "@ConsMiddleName", DbType.String, entConsultant.MiddleName);
            Commons.ADDParameter(ref lstParam, "@ConsLastName", DbType.String, entConsultant.LastName);
            Commons.ADDParameter(ref lstParam, "@ConsAddress", DbType.String, entConsultant.Address);
            Commons.ADDParameter(ref lstParam, "@ConsDOB", DbType.DateTime, entConsultant.DOB);
            Commons.ADDParameter(ref lstParam, "@ConsDOJ", DbType.DateTime, entConsultant.DOJ);

            return lstParam;
        }

        private List<SqlParameter> CreateParameterDeleteCons(EntityConsultant entConsultant)
        {
            List<SqlParameter> lstParam = new List<SqlParameter>();
            Commons.ADDParameter(ref lstParam, "@ConsCode", DbType.String, entConsultant.ConsultantCode);
            Commons.ADDParameter(ref lstParam, "@Discontinued", DbType.Boolean, entConsultant.DisContinued);
            Commons.ADDParameter(ref lstParam, "@DiscontRemark", DbType.String, entConsultant.DisContRemark);
            return lstParam;
        }

        public int UpdateConsultant(EntityConsultant entConsultant)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@ConsCode", DbType.String, entConsultant.ConsultantCode);
                Commons.ADDParameter(ref lstParam, "@ConsFirstName", DbType.String, entConsultant.FirstName);
                Commons.ADDParameter(ref lstParam, "@ConsMiddleName", DbType.String, entConsultant.MiddleName);
                Commons.ADDParameter(ref lstParam, "@ConsLastName", DbType.String, entConsultant.LastName);
                Commons.ADDParameter(ref lstParam, "@ConsAddress", DbType.String, entConsultant.Address);
                Commons.ADDParameter(ref lstParam, "@ConsDOB", DbType.DateTime, entConsultant.DOB);
                Commons.ADDParameter(ref lstParam, "@ConsDOJ", DbType.DateTime, entConsultant.DOJ);
                Commons.ADDParameter(ref lstParam, "@ChangeBy", DbType.String, entConsultant.ChaneBy);
                Commons.ADDParameter(ref lstParam, "@WardNo", DbType.String, entConsultant.WardNo);
                Commons.ADDParameter(ref lstParam, "@Fees", DbType.String, entConsultant.Fees);
                cnt = mobjDataAcces.ExecuteQuery("sp_UpdateConsultant", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("ConsultantBLL - UpdateConsultant(EntityConsultant entConsultant)", ex);
            }
            return cnt;
        }

        public int InsertConsultant(EntityConsultant entConsultant)
        {
            int cnt = 0;

            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@ConsCode", DbType.String, entConsultant.ConsultantCode);
                Commons.ADDParameter(ref lstParam, "@ConsFirstName", DbType.String, entConsultant.FirstName);
                Commons.ADDParameter(ref lstParam, "@ConsMiddleName", DbType.String, entConsultant.MiddleName);
                Commons.ADDParameter(ref lstParam, "@ConsLastName", DbType.String, entConsultant.LastName);
                Commons.ADDParameter(ref lstParam, "@ConsAddress", DbType.String, entConsultant.Address);
                Commons.ADDParameter(ref lstParam, "@ConsDOB", DbType.DateTime, entConsultant.DOB);
                Commons.ADDParameter(ref lstParam, "@ConsDOJ", DbType.DateTime, entConsultant.DOJ);
                Commons.ADDParameter(ref lstParam, "@EntryBy", DbType.String, entConsultant.EntryBy);
                Commons.ADDParameter(ref lstParam, "@WardNo", DbType.String, entConsultant.WardNo);
                Commons.ADDParameter(ref lstParam, "@Fees", DbType.String, entConsultant.Fees);
                cnt = mobjDataAcces.ExecuteQuery("sp_InsertConsultant", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("ConsultantBLL - InsertConsultant(EntityConsultant entConsultant) ", ex);
            }
            return cnt;
        }

        public int DeleteConsultant(List<EntityConsultant> lstEntConsultant)
        {
            int cnt = 0;
            List<string> lstspNames = new List<string>();
            List<List<SqlParameter>> lstParamVals = new List<List<SqlParameter>>();
            try
            {
                foreach (EntityConsultant entConsultant in lstEntConsultant)
                {
                    lstspNames.Add("sp_DeleteConsultant");
                    lstParamVals.Add(CreateParameterDeleteCons(entConsultant));

                    lstspNames.Add("sp_DeleteConsultantLogin");
                    List<SqlParameter> lstParam = new List<SqlParameter>();
                    Commons.ADDParameter(ref lstParam, "@ConsCode", DbType.String, entConsultant.ConsultantCode);
                    Commons.ADDParameter(ref lstParam, "@Discontinued", DbType.Boolean, entConsultant.DisContinued);
                    lstParamVals.Add(lstParam);
                }
                cnt = mobjDataAcces.ExecuteTransaction(lstspNames, lstParamVals);
            }
            catch (Exception ex)
            {
                Commons.FileLog("ConsultantBLL - DeleteConsultant(List<EntityConsultant> lstEntConsultant) ", ex);
            }
            return cnt;
        }
    }
}