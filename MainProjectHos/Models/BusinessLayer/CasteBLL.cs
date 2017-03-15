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
    public class CasteBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public CasteBLL()
        {
            objData = new CriticareHospitalDataContext();
        }

        public CriticareHospitalDataContext objData { get; set; }

        public DataTable GetNewCasteCode()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetNewCasteCode");
            }
            catch (Exception ex)
            {
                Commons.FileLog("CasteBLL - GetNewCasteCode()", ex);
            }
            return ldt;
        }

        public List<EntityCast> GetAllCaste()
        {
            List<EntityCast> ldt = null;
            try
            {
                ldt = (from tbl in objData.sp_GetAllCaste()
                       join tblRel in objData.tblReligions
                       on tbl.ReligionId equals tblRel.PKId
                       select new EntityCast
                       {
                           CastCode = tbl.CastCode,
                           CastDesc = tbl.CastDesc,
                           Religion = tblRel.PKId,
                           ReligionName = tblRel.ReligionDesc,
                       }).ToList();//mobjDataAcces.GetDataTable("sp_GetAllCaste");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }
        public DataTable GetAllReligion()
        {

            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllReligionForCaste");
            }
            catch (Exception ex)
            {
                Commons.FileLog("CasteBLL - GetAllReligion()", ex);
            }
            return ldt;
        }

        public int InsertCaste(EntityCast entCaste)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@CastCode", DbType.String, entCaste.CastCode);
                Commons.ADDParameter(ref lstParam, "@CastDesc", DbType.String, entCaste.CastDesc);
                Commons.ADDParameter(ref lstParam, "@ReligionId", DbType.Int32, entCaste.Religion);
                Commons.ADDParameter(ref lstParam, "@EntryBy", DbType.String, entCaste.EntryBy);
                cnt = mobjDataAcces.ExecuteQuery("sp_InsertCaste ", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("CasteBLL - InsertCaste(EntityCast entCaste)", ex);
            }
            return cnt;
        }

        public DataTable GetCasteForEdit(string pstrCastCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@CastCode", DbType.String, pstrCastCode);
                ldt = mobjDataAcces.GetDataTable("sp_GetCasteForEdit", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("CastBLL - GetCastForEdit(string pstrCastCode)", ex);
            }
            return ldt;
        }

        public int UpdateCaste(EntityCast entCaste)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@CastCode", DbType.String, entCaste.CastCode);
                Commons.ADDParameter(ref lstParam, "@CastDesc", DbType.String, entCaste.CastDesc);
                Commons.ADDParameter(ref lstParam, "@ReligionId", DbType.Int32, entCaste.Religion);
                Commons.ADDParameter(ref lstParam, "@ChangeBy", DbType.String, entCaste.ChangeBy);
                cnt = mobjDataAcces.ExecuteQuery("sp_UpdateCaste", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("CasteBLL -  UpdateCast(EntityCast entCaste)", ex);
            }

            return cnt;
        }

        public int DeleteCaste(EntityCast entCaste)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@CastCode", DbType.String, entCaste.CastCode);
                cnt = mobjDataAcces.ExecuteQuery("sp_DeleteCaste", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("CastBLL - DeleteCaste(EntityCast entCaste)", ex);
            }
            return cnt;
        }
    }
}