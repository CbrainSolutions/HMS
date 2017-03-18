using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using MainProjectHos.Models.DataLayer;
using MainProjectHos.Models.DataModels;

namespace MainProjectHos.Models.BusinessLayer
{
    public class AnaesthetistBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public AnaesthetistBLL()
        {
            objData = new CriticareHospitalDataContext();
        }

        public CriticareHospitalDataContext objData { get; set; }

        public DataTable GetNewReligionCode()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetNewAnaesthetistCode");

            }
            catch (Exception ex)
            {

                Commons.FileLog("AnaesthetistBLL - GetNewAnaesthetistCode()", ex);
            }
            return ldt;
        }

        public DataTable GetAllReligion()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllAnaesthetist");

            }
            catch (Exception ex)
            {

                Commons.FileLog("AnaesthetistBLL - GetAllAnaesthetist()", ex);
            }
            return ldt;
        }

        public int InsertReligion(EntityAnaesthetist entReligion)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@AnaesthetistCode", DbType.String, entReligion.AnaesthetistCode);
                Commons.ADDParameter(ref lstParam, "@AnaesthetistName", DbType.String, entReligion.AnaesthetistName);
                Commons.ADDParameter(ref lstParam, "@TypeOfAnaesthetist", DbType.String, entReligion.TypeOfAnaesthetist);
                Commons.ADDParameter(ref lstParam, "@CategoryName", DbType.String, entReligion.CategoryName);
                Commons.ADDParameter(ref lstParam, "@CategoryId", DbType.Int32, entReligion.CategoryId);
                Commons.ADDParameter(ref lstParam, "@ConsultCharges", DbType.Decimal, entReligion.ConsultCharges);

                cnt = mobjDataAcces.ExecuteQuery("sp_InsertAnaesthetist ", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("AnaesthetistBLL - InsertAnaesthetist(EntityAnaesthetist entReligion)", ex);
            }
            return cnt;
        }

        public DataTable GetReligionForEdit(string pstrReligionCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@AnaesthetistCode", DbType.String, pstrReligionCode);
                ldt = mobjDataAcces.GetDataTable("sp_GetAnaesthetistForEdit", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("AnaesthetistBLL  - GetAnaesthetistForEdit(string pstrReligionCode)", ex);
            }
            return ldt;
        }

        public int UpdateReligion(EntityAnaesthetist entReligion)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@AnaesthetistCode", DbType.String, entReligion.AnaesthetistCode);
                Commons.ADDParameter(ref lstParam, "@AnaesthetistName", DbType.String, entReligion.AnaesthetistName);
                Commons.ADDParameter(ref lstParam, "@TypeOfAnaesthetist", DbType.String, entReligion.TypeOfAnaesthetist);
                Commons.ADDParameter(ref lstParam, "@CategoryName", DbType.String, entReligion.CategoryName);
                Commons.ADDParameter(ref lstParam, "@CategoryId", DbType.Int32, entReligion.CategoryId);
                Commons.ADDParameter(ref lstParam, "@ConsultCharges", DbType.Decimal, entReligion.ConsultCharges);
                cnt = mobjDataAcces.ExecuteQuery("sp_UpdateAnaesthetist", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("AnaesthetistBLL -  UpdateAnaesthetist(EntityAnaesthetist entReligion)", ex);
            }

            return cnt;
        }

        public int DeleteReligion(EntityAnaesthetist entReligion)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@AnaesthetistCode", DbType.String, entReligion.AnaesthetistCode);
                cnt = mobjDataAcces.ExecuteQuery("sp_DeleteAnaesthetist", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("AnaesthetistBLL - DeleteAnaesthetist(EntityAnaesthetist entReligion)", ex);
            }
            return cnt;
        }

        public EntityAnaesthetist SelectOperation(int OperaId)
        {
            try
            {
                return (from tbl in objData.tblAnaesthetists
                        where tbl.PKId.Equals(OperaId)
                        select new EntityAnaesthetist
                        {
                            AnaesthetistName = tbl.AnaesthetistName,
                            TypeOfAnaesthetist = tbl.TypeOfAnaesthesia,
                        }).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}