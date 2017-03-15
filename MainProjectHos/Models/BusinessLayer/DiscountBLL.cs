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
    public class DiscountBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();

        public DiscountBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataTable GetNewDiscountCode()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetNewDiscountCode");

            }
            catch (Exception ex)
            {

                Commons.FileLog("DiscountBLL - GetNewDiscountCode()", ex);
            }
            return ldt;
        }

        public DataTable GetAllDiscount()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllDiscount");
            }
            catch (Exception ex)
            {
                Commons.FileLog("DiscountBLL - GetAllDiscount()", ex);
            }
            return ldt;
        }

        public int InsertDiscount(EntityDiscount entDiscount)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@DiscountCode", DbType.String, entDiscount.DiscountCode);
                Commons.ADDParameter(ref lstParam, "@DiscountDesc", DbType.String, entDiscount.DiscountDesc);
                Commons.ADDParameter(ref lstParam, "@Discount", DbType.Decimal, entDiscount.Discount);
                Commons.ADDParameter(ref lstParam, "@EntryBy", DbType.String, entDiscount.EntryBy);
                cnt = mobjDataAcces.ExecuteQuery("sp_InsertDiscount ", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("DiscountBLL -  InsertDiscount(EntityDiscount entDiscount)", ex);
            }
            return cnt;
        }

        public DataTable GetDiscountForEdit(string pstrDiscountCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@DiscountCode", DbType.String, pstrDiscountCode);
                ldt = mobjDataAcces.GetDataTable("sp_GetDiscountForEdit", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("DiscountBLL  - GetDiscountForEdit(string pstrDiscountCode)", ex);
            }
            return ldt;
        }

        public int UpdateDiscount(EntityDiscount entDiscount)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@DiscountCode", DbType.String, entDiscount.DiscountCode);
                Commons.ADDParameter(ref lstParam, "@DiscountDesc", DbType.String, entDiscount.DiscountDesc);
                Commons.ADDParameter(ref lstParam, "@Discount", DbType.Decimal, entDiscount.Discount);
                Commons.ADDParameter(ref lstParam, "@ChangeBy", DbType.String, entDiscount.ChangeBy);
                cnt = mobjDataAcces.ExecuteQuery("sp_UpdateDiscount", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("DiscountBLL -  UpdateDiscount(EntityDiscount entDiscount)", ex);
            }
            return cnt;
        }

        public int DeleteDiscount(EntityDiscount entDiscount)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@DiscountCode", DbType.String, entDiscount.DiscountCode);
                cnt = mobjDataAcces.ExecuteQuery("sp_DeleteDiscount", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("DiscountBLL - DeleteDiscount(EntityDiscount entDiscount)", ex);
            }
            return cnt;
        }
    }
}