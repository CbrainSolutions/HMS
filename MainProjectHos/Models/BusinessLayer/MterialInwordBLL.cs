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
    public class MterialInwordBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public MterialInwordBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataTable GetNewInwardNo()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetNewInwardNo");
            }
            catch (Exception ex)
            {

                Commons.FileLog("MterialInwardBLL - GetNewInwardNo()", ex); ;
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

                Commons.FileLog("MterialInwardBLL - GetGroup()", ex);
            }
            return ldt;
        }
        public DataTable GetSupplier()
        {
            DataTable ldtSupplier = new DataTable();
            try
            {
                ldtSupplier = mobjDataAcces.GetDataTable("sp_GetSuppliers");
            }
            catch (Exception ex)
            {

                Commons.FileLog("MterialInwardBLL - GetSupplier()", ex);
            }
            return ldtSupplier;
        }

        public DataTable GetInwardDTStatus(string pstrInwardNo)
        {
            DataTable ldt = new DataTable();
            DataTable ldtInward = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@InwardNo", DbType.String, pstrInwardNo);
                ldt = mobjDataAcces.GetDataTable("sp_GetInwardDTStatus", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("MterialInwardBLL - GetInwardStatus(string pstrInwardNo)", ex);
            }
            return ldt;
        }


        public DataTable GetInwardStatus(string pstrInwardNo)
        {
            DataTable ldt = new DataTable();

            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@InwardNo", DbType.String, pstrInwardNo);
                ldt = mobjDataAcces.GetDataTable("sp_GetInwardStatus", lstParam);

            }
            catch (Exception ex)
            {
                Commons.FileLog("MterialInwardBLL - GetInwardStatus(string pstrInwardNo)", ex);
            }
            return ldt;
        }



        public DataTable GetMaterialInward()
        {
            DataTable ldtMaterialInword = new DataTable();
            try
            {
                ldtMaterialInword = mobjDataAcces.GetDataTable("sp_GetAllMterialInward");
            }
            catch (Exception ex)
            {
                Commons.FileLog("MterialInwardBLL - GetMaterialInword()", ex);
            }
            return ldtMaterialInword;
        }

        private List<SqlParameter> CreateParameterMaterialInward(EntityMatrialInword entInward)
        {
            List<SqlParameter> lstParam = new List<SqlParameter>();
            Commons.ADDParameter(ref lstParam, "@InwardNo", DbType.String, entInward.InwardNo);
            Commons.ADDParameter(ref lstParam, "@InwardDate", DbType.DateTime, entInward.InwardDate);
            Commons.ADDParameter(ref lstParam, "@SupplierCode", DbType.String, entInward.SupplierCode);
            Commons.ADDParameter(ref lstParam, "@SupplierName", DbType.String, entInward.SupplierName);
            Commons.ADDParameter(ref lstParam, "@GroupId", DbType.Int32, entInward.Group);
            Commons.ADDParameter(ref lstParam, "@ItemCode", DbType.String, entInward.ItemCode);
            Commons.ADDParameter(ref lstParam, "@ItemDesc", DbType.String, entInward.ItemDesc);
            Commons.ADDParameter(ref lstParam, "@Quantity", DbType.Decimal, entInward.Quantity);
            Commons.ADDParameter(ref lstParam, "@UnitCode", DbType.String, entInward.unit);
            Commons.ADDParameter(ref lstParam, "@TotalAmount", DbType.Decimal, entInward.TotalAmount);
            Commons.ADDParameter(ref lstParam, "@OtherCharges", DbType.Decimal, entInward.OtherCharges);
            Commons.ADDParameter(ref lstParam, "@TotalProductAmount", DbType.Decimal, entInward.TotalProductAmount);
            Commons.ADDParameter(ref lstParam, "@TotalInwardAmount", DbType.Decimal, entInward.TotalInwardAmount);
            Commons.ADDParameter(ref lstParam, "@EntryBy", DbType.String, entInward.EntryBy);
            return lstParam;
        }


        public int InsertMaterialInward(List<EntityMatrialInword> lstentInward, EntityMatrialInword entMaterialInward)
        {
            int cnt = 0;
            List<string> lstspName = new List<string>();
            List<List<SqlParameter>> lstParamVals = new List<List<SqlParameter>>();
            List<SqlParameter> lstParam;
            try
            {
                foreach (EntityMatrialInword entInward in lstentInward)
                {
                    lstspName.Add("sp_InsertInwardDT");
                    lstParamVals.Add(CreateParameterMaterialInward(entInward));

                    lstParam = new List<SqlParameter>();
                    lstspName.Add("sp_UpdateItemByInwardDetails");
                    Commons.ADDParameter(ref lstParam, "@ItemCode", DbType.String, entInward.ItemCode);
                    Commons.ADDParameter(ref lstParam, "@Quantity", DbType.Decimal, entInward.Quantity);
                    lstParamVals.Add(lstParam);
                }
                if (!Commons.IsRecordExists("tblMaterialInwardMT", "InwardNo", entMaterialInward.InwardNo))
                {
                    lstspName.Add("sp_InsertInwardMT");
                    lstParam = new List<SqlParameter>();
                    Commons.ADDParameter(ref lstParam, "@InwardNo", DbType.String, entMaterialInward.InwardNo);
                    Commons.ADDParameter(ref lstParam, "@InwardDate", DbType.DateTime, entMaterialInward.InwardDate);
                    Commons.ADDParameter(ref lstParam, "@SupplierCode", DbType.String, entMaterialInward.SupplierCode);
                    Commons.ADDParameter(ref lstParam, "@SupplierName", DbType.String, entMaterialInward.SupplierName);
                    Commons.ADDParameter(ref lstParam, "@TotalInwardAmount", DbType.Decimal, entMaterialInward.TotalInwardAmount);
                    Commons.ADDParameter(ref lstParam, "@EntryBy", DbType.String, entMaterialInward.EntryBy);
                    lstParamVals.Add(lstParam);
                }

                lstParam = new List<SqlParameter>();
                lstspName.Add("sp_UpdateSupplierMasterOnInward");
                Commons.ADDParameter(ref lstParam, "@SupplierCode", DbType.String, entMaterialInward.SupplierCode);
                Commons.ADDParameter(ref lstParam, "@InwardAmnt", DbType.Decimal, entMaterialInward.TotalInwardAmount);
                lstParamVals.Add(lstParam);

                cnt = mobjDataAcces.ExecuteTransaction(lstspName, lstParamVals);
            }

            catch (Exception ex)
            {
                Commons.FileLog("MterialInwardBLL - insertMaterialInward(EntityMatrialInward entMaterialInward)", ex);
            }
            return cnt;
        }


        public int DeleteMaterialInward(List<EntityItem> lstentItemMaster, EntityMatrialInword entInward)
        {
            int cnt = 0;
            List<string> lstspName = new List<string>();
            List<List<SqlParameter>> lstParamVals = new List<List<SqlParameter>>();
            List<SqlParameter> lstParam;
            try
            {
                lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@InwardNo", DbType.String, entInward.InwardNo);
                lstspName.Add("sp_DeleteInwardDT");
                lstParamVals.Add(lstParam);

                lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@InwardNo", DbType.String, entInward.InwardNo);
                lstspName.Add("sp_DeleteInwardMT");
                lstParamVals.Add(lstParam);


                foreach (EntityItem entItemMaster in lstentItemMaster)
                {
                    lstspName.Add("sp_UpdateItemByReqDetails");
                    lstParam = new List<SqlParameter>();
                    Commons.ADDParameter(ref lstParam, "@ItemCode", DbType.String, entItemMaster.ItemCode);
                    Commons.ADDParameter(ref lstParam, "@Quantity", DbType.Decimal, entItemMaster.OpeningBalance);
                    lstParamVals.Add(lstParam);
                }

                lstParam = new List<SqlParameter>();
                lstspName.Add("sp_UpdateSupplierMasterAmntOnInwardDelete");
                Commons.ADDParameter(ref lstParam, "@SupplierCode", DbType.String, entInward.SupplierCode);
                Commons.ADDParameter(ref lstParam, "@InwardAmnt", DbType.Decimal, entInward.TotalInwardAmount);
                lstParamVals.Add(lstParam);
                cnt = mobjDataAcces.ExecuteTransaction(lstspName, lstParamVals);
            }

            catch (Exception ex)
            {
                Commons.FileLog("MterialInwardBLL - DeleteMaterialInward(EntityMatrialInword entMaterialInward)", ex);
            }
            return cnt;
        }


        public DataTable GetFilteredInward(DateTime ldtStart, DateTime ldtEnd, string lstrInwardNo)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@StartDate", DbType.DateTime, ldtStart);
                Commons.ADDParameter(ref lstParam, "@EndDate", DbType.DateTime, ldtEnd);
                Commons.ADDParameter(ref lstParam, "@InwardNo", DbType.String, lstrInwardNo);
                ldt = mobjDataAcces.GetDataTable("sp_GetInward", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("MaterialInwardBLL - GetFilteredInward(DateTime ldtStart, DateTime ldtEnd, string lstrInwardNo)", ex);
            }
            return ldt;
        }


        public DataTable GetItem(EntityMatrialInword entInward)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@GroupId", DbType.String, entInward.Group);
                ldt = mobjDataAcces.GetDataTable("sp_GetItemForInward", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("MaterialInwardBLL - GetItem()", ex);
            }
            return ldt;
        }

        public DataTable GetItemByGroup(EntityMatrialInword entInward)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@GroupId", DbType.String, entInward.Group);
                ldt = mobjDataAcces.GetDataTable("sp_GetItemForInward", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("MaterialInwardBLL - GetItem()", ex);
            }
            return ldt;
        }

        public DataTable GetItemCode(string pstrInwardNo)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@InwardNo", DbType.String, pstrInwardNo);
                ldt = mobjDataAcces.GetDataTable("sp_GetInwardDTStatus", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("MaterialInwardBLL - GetItem()", ex);
            }
            return ldt;
        }

        public DataTable GetFilteredInwardDetail(DateTime StartDate, DateTime EndDate, string pstrInwardNo, string pstrSupplierCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@StartDate", DbType.Date, StartDate);
                Commons.ADDParameter(ref lstParam, "@EndDate", DbType.Date, EndDate);
                Commons.ADDParameter(ref lstParam, "@InwardNo", DbType.String, pstrInwardNo);
                Commons.ADDParameter(ref lstParam, "@SupplierCode", DbType.String, pstrSupplierCode);
                ldt = mobjDataAcces.GetDataTable("sp_GetFilteredInwardDetail", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("PatientMasterBLL - GetFilteredInwardDetail(DateTime StartDate, DateTime EndDate, string pstrInwardNo,string pstrSupplierCode)", ex);
            }
            return ldt;
        }

    }
}