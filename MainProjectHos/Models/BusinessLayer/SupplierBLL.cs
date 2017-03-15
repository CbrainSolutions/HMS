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
    public class SupplierBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public SupplierBLL()
        {
            objData = new CriticareHospitalDataContext();
            //
            // TODO: Add constructor logic here
            //
        }
        public CriticareHospitalDataContext objData { get; set; }
        public DataTable GetNewSupplierCode()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetNewSupplierCode");

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public List<EntitySupplierMaster> GetAllSupplier()
        {
            List<EntitySupplierMaster> ldt = null;
            try
            {
                ldt = (from tbl in objData.sp_GetAllSupplier()
                       select new EntitySupplierMaster
                       {
                           PKId = tbl.PKId,
                           SupplierCode = tbl.SupplierCode,
                           SupplierName = tbl.SupplierName,
                           VATCSTNo = tbl.VATCSTNo,
                           ExciseNo = tbl.ExciseNo,
                           Address = tbl.Address,
                           PhoneNo = tbl.PhoneNo,
                           MobileNo = tbl.MobileNo,
                           ServiceTaxNo = tbl.ServiceTaxNo,
                           Email = tbl.Email
                       }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }
        public int InsertSupplier(EntitySupplier entSupplier)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@SupplierCode", DbType.String, entSupplier.SupplierCode);
                Commons.ADDParameter(ref lstParam, "@SupplierName", DbType.String, entSupplier.SupplierName);
                Commons.ADDParameter(ref lstParam, "@Address", DbType.String, entSupplier.Address);
                Commons.ADDParameter(ref lstParam, "@PhoneNo", DbType.String, entSupplier.PhoneNo);
                Commons.ADDParameter(ref lstParam, "@MobileNo", DbType.String, entSupplier.MobileNo);
                Commons.ADDParameter(ref lstParam, "@VATCSTNo", DbType.String, entSupplier.VATCSTNo);
                Commons.ADDParameter(ref lstParam, "@ExciseNo", DbType.String, entSupplier.ExciseNo);
                Commons.ADDParameter(ref lstParam, "@Email", DbType.String, entSupplier.Email);
                Commons.ADDParameter(ref lstParam, "@ServiceTaxNo", DbType.String, entSupplier.ServiceTaxNo);
                Commons.ADDParameter(ref lstParam, "@EntryBy", DbType.String, entSupplier.EntryBy);
                cnt = mobjDataAcces.ExecuteQuery("sp_InsertSupplier ", lstParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cnt;
        }

        public DataTable GetSupplierForEdit(string pstrSupplierCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@SupplierCode", DbType.String, pstrSupplierCode);
                ldt = mobjDataAcces.GetDataTable("sp_GetSupplierForEdit", lstParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public int UpdateSupplier(EntitySupplier entSupplier)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@SupplierCode", DbType.String, entSupplier.SupplierCode);
                Commons.ADDParameter(ref lstParam, "@SupplierName", DbType.String, entSupplier.SupplierName);
                Commons.ADDParameter(ref lstParam, "@Address", DbType.String, entSupplier.Address);
                Commons.ADDParameter(ref lstParam, "@PhoneNo", DbType.String, entSupplier.PhoneNo);
                Commons.ADDParameter(ref lstParam, "@MobileNo", DbType.String, entSupplier.MobileNo);
                Commons.ADDParameter(ref lstParam, "@VATCSTNo", DbType.String, entSupplier.VATCSTNo);
                Commons.ADDParameter(ref lstParam, "@ExciseNo", DbType.String, entSupplier.ExciseNo);
                Commons.ADDParameter(ref lstParam, "@Email", DbType.String, entSupplier.Email);
                Commons.ADDParameter(ref lstParam, "@ServiceTaxNo", DbType.String, entSupplier.ServiceTaxNo);
                Commons.ADDParameter(ref lstParam, "@ChangeBy", DbType.String, entSupplier.ChangeBy);
                cnt = mobjDataAcces.ExecuteQuery("sp_UpdateSupplier", lstParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return cnt;
        }

        public int DeleteSupplier(EntitySupplier entSupplier)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@SupplierCode", DbType.String, entSupplier.SupplierCode);
                cnt = mobjDataAcces.ExecuteQuery("sp_DeleteSupplier", lstParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cnt;
        }

        public EntitySupplier GetSupplier(EntitySupplier entSupplier)
        {
            EntitySupplier ent = (from tbl in objData.tblSupplierMasters
                                  where tbl.VATCSTNo.Equals(entSupplier.VATCSTNo)
                                  select new EntitySupplier { VATCSTNo = tbl.VATCSTNo }).FirstOrDefault();
            return ent;

        }

        public List<EntitySupplierMaster> SelectSupplier(string Prefix)
        {
            List<EntitySupplierMaster> lst = null;
            try
            {
                lst = (from tbl in objData.sp_GetAllSupplier()
                       where tbl.IsDelete = false
                       &&
                       tbl.SupplierCode.ToString().ToUpper().Contains(Prefix.ToUpper()) || tbl.SupplierName.ToString().ToUpper().Contains(Prefix.ToUpper()) ||
                       tbl.VATCSTNo.ToString().ToUpper().Contains(Prefix.ToUpper()) || tbl.ExciseNo.ToString().ToUpper().Contains(Prefix.ToUpper())
                       select new EntitySupplierMaster
                       {
                           SupplierCode = tbl.SupplierCode,
                           SupplierName = tbl.SupplierName,
                           VATCSTNo = tbl.VATCSTNo,
                           ExciseNo = tbl.ExciseNo,
                           Address = tbl.Address,
                           PhoneNo = tbl.PhoneNo,
                           MobileNo = tbl.MobileNo,
                           ServiceTaxNo = tbl.ServiceTaxNo,
                           Email = tbl.Email
                       }).ToList();
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}