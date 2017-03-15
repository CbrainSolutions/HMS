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
    public class SurgeryMasterBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public SurgeryMasterBLL()
        {
            objData = new CriticareHospitalDataContext();
        }

        public CriticareHospitalDataContext objData { get; set; }
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
                ldt = mobjDataAcces.GetDataTable("sp_GetAllSurgeryMaster");

            }
            catch (Exception ex)
            {

                Commons.FileLog("TabletMasterBLL - GetAllNurse()", ex);
            }
            return ldt;
        }

        public int InsertReligion(EntitySurgeryMaster entReligion)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@NameOfSurgery", DbType.String, entReligion.NameOfSurgery);
                Commons.ADDParameter(ref lstParam, "@OperationalProcedure", DbType.String, entReligion.OperationalProcedure);
                cnt = mobjDataAcces.ExecuteQuery("sp_InsertSurgeryMaster ", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("TabletMasterBLL - InsertReligion(EntityReligion entReligion)", ex);
            }
            return cnt;
        }

        public DataTable GetReligionForEdit(int pstrReligionCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@PKId", DbType.Int32, pstrReligionCode);
                ldt = mobjDataAcces.GetDataTable("sp_GetSurgeryMasterForEdit", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("TabletMasterBLL  - GetNurseForEdit(string pstrReligionCode)", ex);
            }
            return ldt;
        }

        public int UpdateReligion(EntitySurgeryMaster entReligion)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@PKId", DbType.Int32, entReligion.PKId);
                Commons.ADDParameter(ref lstParam, "@NameOfSurgery", DbType.String, entReligion.NameOfSurgery);
                Commons.ADDParameter(ref lstParam, "@OperationalProcedure", DbType.String, entReligion.OperationalProcedure);
                cnt = mobjDataAcces.ExecuteQuery("sp_UpdateSurgeryMaster", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("TabletMasterBLL -  UpdateNurse(EntityNurse entReligion)", ex);
            }

            return cnt;
        }

        public int DeleteReligion(EntitySurgeryMaster entReligion)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@PKId", DbType.Int32, entReligion.PKId);
                cnt = mobjDataAcces.ExecuteQuery("sp_DeleteSurgeryMaster", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("TabletMasterBLL - DeleteNurse(EntityNurse entReligion)", ex);
            }
            return cnt;
        }

        public List<EntitySurgeryMaster> GetAllSurgeryName()
        {
            List<EntitySurgeryMaster> lst = null;
            try
            {
                lst = (from tbl in objData.tblSurgeryMasters
                       where tbl.PKId >= 0
                       select new EntitySurgeryMaster
                       {
                           PKId = Convert.ToInt32(tbl.PKId),
                           NameOfSurgery = tbl.NameOfSurgery
                       }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }

        public EntitySurgeryMaster GetOperationalProcedure(int Val)
        {
            try
            {
                EntitySurgeryMaster objPat = (from tbl in objData.tblSurgeryMasters
                                              where tbl.PKId.Equals(Val)
                                              select new EntitySurgeryMaster { OperationalProcedure = Convert.ToString(tbl.OperationalProcedure) }).FirstOrDefault();
                return objPat;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }

}