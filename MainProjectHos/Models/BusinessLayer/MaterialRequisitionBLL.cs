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
    public class MaterialRequisitionBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public MaterialRequisitionBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataTable GetNewRequisitionNo()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetNewRequisitionNo");

            }
            catch (Exception ex)
            {

                Commons.FileLog("MaterialRequisitionBLL - GetNewRequisitionNo()", ex);
            }
            return ldt;
        }

        public DataTable GetRequisitionNo()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetRequisitionNo");
            }
            catch (Exception ex)
            {

                Commons.FileLog("MaterialRequisitionBLL - GetRequisitionNo()", ex);
            }
            return ldt;
        }


        public DataTable GetAllRequisition()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllRequisition");

            }
            catch (Exception ex)
            {

                Commons.FileLog("MaterialRequisitionBLL - GetAllRequisition()", ex);
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
                Commons.FileLog("MaterialRequisitionBLL - GetGroup()", ex);
            }
            return ldt;
        }


        public DataTable GetItem(EntityMaterialRequisition entRequisition)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@GroupId", DbType.String, entRequisition.Group);
                ldt = mobjDataAcces.GetDataTable("sp_GetItemForInward", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("MaterialRequisitionBLL - GetItem()", ex);
            }
            return ldt;
        }


        private List<SqlParameter> CreateParameterMaterialReceipt(EntityMaterialRequisition entRequisition)
        {
            List<SqlParameter> lstParam = new List<SqlParameter>();
            Commons.ADDParameter(ref lstParam, "@RequisitionCode", DbType.String, entRequisition.RequisitionCode);
            Commons.ADDParameter(ref lstParam, "@Quantity", DbType.Decimal, entRequisition.Qty);
            Commons.ADDParameter(ref lstParam, "@Unit", DbType.String, entRequisition.Unit);
            Commons.ADDParameter(ref lstParam, "@RequisitionBy", DbType.String, entRequisition.RequisitionBy);
            Commons.ADDParameter(ref lstParam, "@RequisitionStatus", DbType.String, entRequisition.RequisitionStatus);
            Commons.ADDParameter(ref lstParam, "@GroupId", DbType.Int32, entRequisition.Group);
            Commons.ADDParameter(ref lstParam, "@ItemCode", DbType.String, entRequisition.ItemCode);
            Commons.ADDParameter(ref lstParam, "@ItemDesc", DbType.String, entRequisition.Item);
            Commons.ADDParameter(ref lstParam, "@EntryBy", DbType.String, entRequisition.EntryBy);
            return lstParam;
        }



        public int InsertRequisition(List<EntityMaterialRequisition> lstentRequisition, EntityMaterialRequisition entRequisition)
        {
            int cnt = 0;
            List<string> lstspNames = new List<string>();
            List<List<SqlParameter>> lstParamVals = new List<List<SqlParameter>>();
            try
            {
                foreach (EntityMaterialRequisition entMaterialReq in lstentRequisition)
                {
                    lstspNames.Add("sp_InsertRequisitionDT");
                    lstParamVals.Add(CreateParameterMaterialReceipt(entMaterialReq));
                }

                if (!Commons.IsRecordExists("tblMaterialRequisitionMT", "RequisitionCode", entRequisition.RequisitionCode))
                {
                    lstspNames.Add("sp_InsertRequisitionMT");
                    List<SqlParameter> lstParam = new List<SqlParameter>();
                    Commons.ADDParameter(ref lstParam, "@RequisitionCode", DbType.String, entRequisition.RequisitionCode);
                    Commons.ADDParameter(ref lstParam, "@RequisitionBy", DbType.String, entRequisition.RequisitionBy);
                    Commons.ADDParameter(ref lstParam, "@RequisitionStatus", DbType.String, entRequisition.RequisitionStatus);
                    Commons.ADDParameter(ref lstParam, "@GroupId", DbType.Int32, entRequisition.Group);
                    Commons.ADDParameter(ref lstParam, "@EntryBy", DbType.String, entRequisition.EntryBy);
                    lstParamVals.Add(lstParam);
                }
                cnt = mobjDataAcces.ExecuteTransaction(lstspNames, lstParamVals);
            }
            catch (Exception ex)
            {
                Commons.FileLog("CustomerReceiptBLL -  InsertRequisitionDT(List<EntityMaterialRequisition> lstentRequisition, EntityMaterialRequisition entRequisition)", ex);
            }
            return cnt;
        }


        public int DeleteRequisition(EntityMaterialRequisition entRequisition)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@RequisitionCode", DbType.String, entRequisition.RequisitionCode);
                cnt = mobjDataAcces.ExecuteQuery("sp_DeleteRequisitionDT", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("RequisitionBLL - DeleteRequisition(EntityRequisition entRequisition)", ex);
            }
            return cnt;
        }

        public DataTable GetFilteredRequisition(DateTime pdtStartDate, DateTime pdtEndDate, string pstrRequisitionNo)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@StartDate", DbType.DateTime, pdtStartDate);
                Commons.ADDParameter(ref lstParam, "@EndDate", DbType.DateTime, pdtEndDate);
                Commons.ADDParameter(ref lstParam, "@RequisitionCode", DbType.String, pstrRequisitionNo);
                ldt = mobjDataAcces.GetDataTable("sp_GetRequisition", lstParam);

            }
            catch (Exception ex)
            {

                Commons.FileLog("MaterialRequisitionBLL - GetRequisition()", ex);
            }
            return ldt;
        }

        public DataTable GetEmpName(EntityMaterialRequisition entRequisition)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@EmpCode", DbType.String, entRequisition.EntryBy);
                ldt = mobjDataAcces.GetDataTable("sp_GetEmployeeName", lstParam);
            }
            catch (Exception ex)
            {

                Commons.FileLog("MaterialRequisitionBLL -  GetUsername(EntityMaterialRequisition entRequisition)", ex);
            }
            return ldt;
        }
    }
}