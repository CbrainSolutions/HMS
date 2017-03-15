using MainProjectHos.Models.DataLayer;
using MainProjectHos.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.BusinessLayer
{
    public class RequisitionDetailsBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public RequisitionDetailsBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public DataTable GetAllRequisitionDetails()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllRequisitionDetails");
            }
            catch (Exception ex)
            {
                Commons.FileLog("RequisitionDetailsBLL - GetAllRequisitionDetails()", ex);
            }
            return ldt;
        }
        private List<SqlParameter> createParameterList(EntityMaterialRequisition entRequisition)
        {
            List<SqlParameter> lstParam = new List<SqlParameter>();
            Commons.ADDParameter(ref lstParam, "@RequisitionCode", DbType.String, entRequisition.RequisitionCode);
            Commons.ADDParameter(ref lstParam, "@ItemCode", DbType.String, entRequisition.ItemCode);
            Commons.ADDParameter(ref lstParam, "@Quantity", DbType.Decimal, entRequisition.Qty);
            Commons.ADDParameter(ref lstParam, "@RequisitionStatus", DbType.String, entRequisition.RequisitionStatus);
            return lstParam;
        }


        public DataTable GetRequisitionForEdit(string pstrRequisitionCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@RequisitionCode", DbType.String, pstrRequisitionCode);
                ldt = mobjDataAcces.GetDataTable("sp_GetRequisitionForEdit", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("RequisitionDetailsBLL - GetRequisitionForEdit(string pstrRequisitionCode)", ex);
            }
            return ldt;
        }



        public int UpdateRequisition(List<EntityMaterialRequisition> pstentMaterialReq, EntityMaterialRequisition entRequisition)
        {
            int cnt = 0;
            List<string> lstspName = new List<string>();
            List<List<SqlParameter>> lstParamVals = new List<List<SqlParameter>>();
            List<SqlParameter> lstParam;
            try
            {
                foreach (EntityMaterialRequisition entMaterialReq in pstentMaterialReq)
                {
                    lstspName.Add("sp_UpdateRerquisitonDT");
                    lstParamVals.Add(createParameterList(entMaterialReq));

                    lstParam = new List<SqlParameter>();
                    lstspName.Add("sp_UpdateItemByReqDetails");
                    Commons.ADDParameter(ref lstParam, "@ItemCode", DbType.String, entRequisition.ItemCode);
                    Commons.ADDParameter(ref lstParam, "@Quantity", DbType.Decimal, entRequisition.Qty);
                    lstParamVals.Add(lstParam);
                }

                lstspName.Add("sp_UpdateRerquisitonMT");
                lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@RequisitionCode", DbType.String, entRequisition.RequisitionCode);
                Commons.ADDParameter(ref lstParam, "@RequisitionStatus", DbType.String, entRequisition.RequisitionStatus);
                lstParamVals.Add(lstParam);

                cnt = mobjDataAcces.ExecuteTransaction(lstspName, lstParamVals);
            }
            catch (Exception ex)
            {
                Commons.FileLog("RequisitionDetailsBLL - InsertRequisition(EntityRequisition entRequisition)", ex);
            }
            return cnt;
        }
    }
}