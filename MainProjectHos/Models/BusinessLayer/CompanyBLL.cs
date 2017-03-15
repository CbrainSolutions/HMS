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
    public class CompanyBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public CompanyBLL()
        {
            objData = new CriticareHospitalDataContext();
            //
            // TODO: Add constructor logic here
            //
        }
        public CriticareHospitalDataContext objData { get; set; }
        public DataTable GetNewCompanyCode()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetNewCompanyCode");

            }
            catch (Exception ex)
            {

                Commons.FileLog("CompanyBLL - GetNewCompanyCode()", ex);
            }
            return ldt;
        }

        public DataTable GetAllCompany()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllCompany");

            }
            catch (Exception ex)
            {

                Commons.FileLog("CompanyBLL - GetAllCompany()", ex);
            }
            return ldt;
        }
        public int InsertCompany(EntityCompany entCompany)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@CompanyCode", DbType.String, entCompany.CompanyCode);
                Commons.ADDParameter(ref lstParam, "@CompanyName", DbType.String, entCompany.CompanyName);
                Commons.ADDParameter(ref lstParam, "@Address", DbType.String, entCompany.Address);
                Commons.ADDParameter(ref lstParam, "@PhoneNo", DbType.String, entCompany.PhoneNo);
                Commons.ADDParameter(ref lstParam, "@MobileNo", DbType.String, entCompany.MobileNo);
                Commons.ADDParameter(ref lstParam, "@VATCSTNo", DbType.String, entCompany.VATCSTNo);
                Commons.ADDParameter(ref lstParam, "@ExciseNo", DbType.String, entCompany.ExciseNo);
                Commons.ADDParameter(ref lstParam, "@Email", DbType.String, entCompany.Email);
                Commons.ADDParameter(ref lstParam, "@ServiceTaxNo", DbType.String, entCompany.ServiceTaxNo);
                Commons.ADDParameter(ref lstParam, "@EntryBy", DbType.String, entCompany.EntryBy);
                cnt = mobjDataAcces.ExecuteQuery("sp_InsertCompany ", lstParam);
            }
            catch (Exception ex)
            {

                Commons.FileLog("CompanyBLL - InsertCompany(EntityCompany entCompany)", ex);
            }
            return cnt;
        }

        public DataTable GetCompanyForEdit(string pstrCompanyCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@CompanyCode", DbType.String, pstrCompanyCode);
                ldt = mobjDataAcces.GetDataTable("sp_GetCompanyForEdit", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("CompanyBLL - GetCompanyForEdit(string pstrCompanyCode)", ex);
            }
            return ldt;
        }

        public int UpdateCompany(EntityCompany entCompany)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@CompanyCode", DbType.String, entCompany.CompanyCode);
                Commons.ADDParameter(ref lstParam, "@CompanyName", DbType.String, entCompany.CompanyName);
                Commons.ADDParameter(ref lstParam, "@Address", DbType.String, entCompany.Address);
                Commons.ADDParameter(ref lstParam, "@PhoneNo", DbType.String, entCompany.PhoneNo);
                Commons.ADDParameter(ref lstParam, "@MobileNo", DbType.String, entCompany.MobileNo);
                Commons.ADDParameter(ref lstParam, "@VATCSTNo", DbType.String, entCompany.VATCSTNo);
                Commons.ADDParameter(ref lstParam, "@ExciseNo", DbType.String, entCompany.ExciseNo);
                Commons.ADDParameter(ref lstParam, "@Email", DbType.String, entCompany.Email);
                Commons.ADDParameter(ref lstParam, "@ServiceTaxNo", DbType.String, entCompany.ServiceTaxNo);
                Commons.ADDParameter(ref lstParam, "@ChangeBy", DbType.String, entCompany.ChangeBy);
                cnt = mobjDataAcces.ExecuteQuery("sp_UpdateCompany", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("CompanyBLL -  UpdateCompany(EntityCompany entCompany)", ex);
            }

            return cnt;
        }

        public int DeleteCompany(EntityCompany entCompany)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@CompanyCode", DbType.String, entCompany.CompanyCode);
                cnt = mobjDataAcces.ExecuteQuery("sp_DeleteCompany", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("CompanyBLL - DeleteCompany(EntityCompany entCompany)", ex);
            }
            return cnt;
        }

        public EntityCompany GetCompany(EntityCompany entCompany)
        {
            EntityCompany ent = (from tbl in objData.tblCompanyMasters
                                 where tbl.VATCSTNo.Equals(entCompany.VATCSTNo)
                                 select new EntityCompany { VATCSTNo = tbl.VATCSTNo }).FirstOrDefault();
            return ent;
        }

        public List<EntityCompany> SelectCompanyDetails(string Prefix)
        {
            List<EntityCompany> lst = null;
            try
            {
                lst = (from tbl in objData.sp_GetAllCompany()
                       where
                       tbl.CompanyCode.ToString().ToUpper().Contains(Prefix.ToUpper()) || tbl.CompanyName.ToString().ToUpper().Contains(Prefix.ToUpper()) ||
                       tbl.VATCSTNo.ToString().ToUpper().Contains(Prefix.ToUpper()) || tbl.ExciseNo.ToString().ToUpper().Contains(Prefix.ToUpper())
                       select new EntityCompany
                       {
                           CompanyCode = tbl.CompanyCode,
                           CompanyName = tbl.CompanyName,
                           Address = tbl.Address,
                           Email = tbl.Email,
                           ExciseNo = tbl.ExciseNo,
                           VATCSTNo = tbl.VATCSTNo,
                           PhoneNo = tbl.PhoneNo,
                           MobileNo = tbl.MobileNo,
                           ServiceTaxNo = tbl.ServiceTaxNo
                       }).ToList();
                return lst;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}