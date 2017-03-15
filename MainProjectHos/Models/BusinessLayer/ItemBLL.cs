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
    public class ItemBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public ItemBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataTable GetNewItemCode()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetNewItemCode");

            }
            catch (Exception ex)
            {

                Commons.FileLog("ItemBLL - GetNewItemCode()", ex);
            }
            return ldt;
        }

        public DataTable GetAllItem()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllItem");

            }
            catch (Exception ex)
            {

                Commons.FileLog("ItemBLL - GetAllItem()", ex);
            }
            return ldt;
        }

        public DataTable GetUnit()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetUnitForItem");
            }
            catch (Exception ex)
            {

                Commons.FileLog("ItemBLL - GetUnit()", ex);
            }
            return ldt;
        }

        public DataTable GetSupplier()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetSupplierForItem");

            }
            catch (Exception ex)
            {

                Commons.FileLog("ItemBLL - GetSupplier()", ex);
            }
            return ldt;
        }

        public DataTable GetGroup()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetGroupForItem");
            }
            catch (Exception ex)
            {
                Commons.FileLog("ItemBLL - GetGroup()", ex);
            }
            return ldt;
        }


        public int InsertItem(EntityItem entItem)
        {
            int cnt = 0;
            try
            {
                if (entItem.IsCheked == 0)
                {
                    List<SqlParameter> lstParam = new List<SqlParameter>();
                    Commons.ADDParameter(ref lstParam, "@ItemCode", DbType.String, entItem.ItemCode);
                    Commons.ADDParameter(ref lstParam, "@ItemDesc", DbType.String, entItem.ItemDesc);
                    Commons.ADDParameter(ref lstParam, "@ReorderLevel", DbType.Int32, entItem.ReorderLevel);
                    Commons.ADDParameter(ref lstParam, "@ReorderMaxLevel", DbType.Int32, entItem.ReorderMaxLevel);
                    Commons.ADDParameter(ref lstParam, "@UnitCode", DbType.String, entItem.UnitCode);
                    Commons.ADDParameter(ref lstParam, "@SupplierCode", DbType.String, entItem.SupplierCode);
                    Commons.ADDParameter(ref lstParam, "@OpenningBalance", DbType.Decimal, entItem.OpeningBalance);
                    Commons.ADDParameter(ref lstParam, "@Rate", DbType.Decimal, entItem.Rate);
                    Commons.ADDParameter(ref lstParam, "@GroupId", DbType.Int32, entItem.GroupId);
                    Commons.ADDParameter(ref lstParam, "@EntryBy", DbType.String, entItem.EntryBy);
                    cnt = mobjDataAcces.ExecuteQuery("sp_InsertItem ", lstParam);
                }
                else
                {
                    List<SqlParameter> lstParam = new List<SqlParameter>();
                    Commons.ADDParameter(ref lstParam, "@ItemCode", DbType.String, entItem.ItemCode);
                    Commons.ADDParameter(ref lstParam, "@ItemDesc", DbType.String, entItem.ItemDesc);
                    Commons.ADDParameter(ref lstParam, "@ReorderLevel", DbType.Int32, entItem.ReorderLevel);
                    Commons.ADDParameter(ref lstParam, "@ReorderMaxLevel", DbType.Int32, entItem.ReorderMaxLevel);
                    Commons.ADDParameter(ref lstParam, "@UnitCode", DbType.String, entItem.UnitCode);
                    Commons.ADDParameter(ref lstParam, "@SupplierCode", DbType.String, entItem.SupplierCode);
                    Commons.ADDParameter(ref lstParam, "@OpenningBalance", DbType.Decimal, entItem.OpeningBalance);
                    Commons.ADDParameter(ref lstParam, "@Rate", DbType.Decimal, entItem.Rate);
                    Commons.ADDParameter(ref lstParam, "@GroupId", DbType.Int32, entItem.GroupId);
                    Commons.ADDParameter(ref lstParam, "@ManifacturingDate", DbType.DateTime, entItem.ManifacturingDate);
                    Commons.ADDParameter(ref lstParam, "@ExpiryDate", DbType.DateTime, entItem.ExpiryDate);
                    Commons.ADDParameter(ref lstParam, "@EntryBy", DbType.String, entItem.EntryBy);
                    cnt = mobjDataAcces.ExecuteQuery("sp_InsertItem ", lstParam);
                }
            }
            catch (Exception ex)
            {

                Commons.FileLog("ItemBLL - InsertItem(EntityItem entItem)", ex);
            }
            return cnt;
        }



        public DataTable GetItemForEdit(string pstrItemCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@ItemCode", DbType.String, pstrItemCode);
                ldt = mobjDataAcces.GetDataTable("sp_GetItemForEdit", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("ItemBLL - GetItemForEdit(string pstrItemCode)", ex);
            }
            return ldt;
        }

        public int UpdateItem(EntityItem entItem)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@ItemCode", DbType.String, entItem.ItemCode);
                Commons.ADDParameter(ref lstParam, "@ItemDesc", DbType.String, entItem.ItemDesc);
                Commons.ADDParameter(ref lstParam, "@ReorderLevel", DbType.Int32, entItem.ReorderLevel);
                Commons.ADDParameter(ref lstParam, "@ReorderMaxLevel", DbType.Int32, entItem.ReorderMaxLevel);
                Commons.ADDParameter(ref lstParam, "@UnitCode", DbType.String, entItem.UnitCode);
                Commons.ADDParameter(ref lstParam, "@SupplierCode", DbType.String, entItem.SupplierCode);
                Commons.ADDParameter(ref lstParam, "@OpenningBalance", DbType.Decimal, entItem.OpeningBalance);
                Commons.ADDParameter(ref lstParam, "@Rate", DbType.Decimal, entItem.Rate);
                Commons.ADDParameter(ref lstParam, "@GroupId", DbType.Int32, entItem.GroupId);
                Commons.ADDParameter(ref lstParam, "@ManifacturingDate", DbType.DateTime, entItem.ManifacturingDate);
                Commons.ADDParameter(ref lstParam, "@ExpiryDate", DbType.DateTime, entItem.ExpiryDate);
                Commons.ADDParameter(ref lstParam, "@ChangeBy", DbType.String, entItem.ChangeBy);
                cnt = mobjDataAcces.ExecuteQuery("sp_UpdateItem", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("ItemBLL -  UpdateItem(EntityItem entItem)", ex);
            }

            return cnt;
        }

        private List<SqlParameter> CreateParameterDeleteItem(EntityItem entItem)
        {
            List<SqlParameter> lstParam = new List<SqlParameter>();
            Commons.ADDParameter(ref lstParam, "@ItemCode", DbType.String, entItem.ItemCode);
            Commons.ADDParameter(ref lstParam, "@Discontinued", DbType.Boolean, entItem.DisContinued);
            Commons.ADDParameter(ref lstParam, "@DisContRemark", DbType.String, entItem.DisContRemark);
            return lstParam;
        }

        public int DeleteItem(List<EntityItem> lstentItem)
        {
            int cnt = 0;
            List<string> lstspNames = new List<string>();
            List<List<SqlParameter>> lstParamVals = new List<List<SqlParameter>>();
            try
            {
                foreach (EntityItem entItem in lstentItem)
                {
                    lstspNames.Add("sp_DeleteItem");
                    lstParamVals.Add(CreateParameterDeleteItem(entItem));
                }
                cnt = mobjDataAcces.ExecuteTransaction(lstspNames, lstParamVals);
            }
            catch (Exception ex)
            {
                Commons.FileLog("ItemBLL - DeleteItem(List<EntityItem> lstEntItem) ", ex);
            }
            return cnt;
        }


    }
}