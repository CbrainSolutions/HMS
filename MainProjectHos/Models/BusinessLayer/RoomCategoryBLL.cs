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
    public class RoomCategoryBLL
    {

        clsDataAccess mobjDataAcces = new clsDataAccess();

        public RoomCategoryBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataTable GetNewCategoryCode()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetNewCategoryCode");
            }
            catch (Exception ex)
            {
                Commons.FileLog("RoomCategoryBLL - GetNewCategoryCode()", ex);
            }
            return ldt;
        }

        public DataTable GetAllCategories()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("GetALLCategories");
            }
            catch (Exception ex)
            {
                Commons.FileLog("RoomCategoryBLL - GetAllCategories()", ex);
            }
            return ldt;
        }

        public DataTable GetCategoriesForEdit(string pstrCategoryCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@CategoryCode", DbType.String, pstrCategoryCode);
                ldt = mobjDataAcces.GetDataTable("sp_GetCategoriesForEdit", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("RoomCategoryBLL - GetCategoriesForEdit(string pstrCategoryCode)", ex);
            }
            return ldt;
        }

        public int InsertCategory(EntityRoomCategory entRoomCat)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@CategoryCode", DbType.String, entRoomCat.CategoryCode);
                Commons.ADDParameter(ref lstParam, "@CategoryDesc", DbType.String, entRoomCat.CategoryDesc);
                Commons.ADDParameter(ref lstParam, "@Rate", DbType.Decimal, entRoomCat.Rate);
                Commons.ADDParameter(ref lstParam, "@EntryBy", DbType.String, entRoomCat.EntryBy);
                Commons.ADDParameter(ref lstParam, "@IsICU", DbType.Boolean, entRoomCat.IsICU);
                Commons.ADDParameter(ref lstParam, "@IsOT", DbType.Boolean, entRoomCat.IsOT);
                cnt = mobjDataAcces.ExecuteQuery("sp_InsertCategory", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("RoomCategoryBLL -  InsertCategory(EntityRoomCategory entRoomCat)", ex);
            }

            return cnt;
        }

        public int UpdateCategory(EntityRoomCategory entRoomCat)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@CategoryCode", DbType.String, entRoomCat.CategoryCode);
                Commons.ADDParameter(ref lstParam, "@CategoryDesc", DbType.String, entRoomCat.CategoryDesc);
                Commons.ADDParameter(ref lstParam, "@Rate", DbType.Decimal, entRoomCat.Rate);
                Commons.ADDParameter(ref lstParam, "@ChangeBy", DbType.String, entRoomCat.ChangeBy);
                Commons.ADDParameter(ref lstParam, "@IsICU", DbType.Boolean, entRoomCat.IsICU);
                Commons.ADDParameter(ref lstParam, "@IsOT", DbType.Boolean, entRoomCat.IsOT);
                cnt = mobjDataAcces.ExecuteQuery("sp_UpdateCategory", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("RoomCategoryBLL -  UpdateCategory(EntityRoomCategory entRoomCat)", ex);
            }

            return cnt;
        }

        public int DeleteCategory(EntityRoomCategory entRoomCat)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@CategoryCode", DbType.String, entRoomCat.CategoryCode);
                cnt = mobjDataAcces.ExecuteQuery("sp_DeleteCategory", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("DepartmentBLL - DeleteDepartment(EntityDepartment entDept)", ex);
            }
            return cnt;
        }
    }

}