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
    public class InsuranceComBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public InsuranceComBLL()
        {
            objData = new CriticareHospitalDataContext();
        }

        public CriticareHospitalDataContext objData { get; set; }

        public DataTable GetNewInsuranceCode()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetNewInsuranceCode");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public List<sp_GetAllInsuranceResult> GetAllInsurance()
        {
            List<sp_GetAllInsuranceResult> lst = null;
            try
            {
                lst = objData.sp_GetAllInsurance().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }

        public DataTable GetAllCountry()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetCountryForInsurance");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable GetState()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetStates");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable GetCity()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetCities");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }


        public DataTable GetAllState(EntityInsuranceCom entInsurance)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@CountryId", DbType.Int32, entInsurance.Country);
                ldt = mobjDataAcces.GetDataTable("sp_GetStateForInsurance", lstParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable GetAllCity(EntityInsuranceCom entInsurance)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@StateId", DbType.Int32, entInsurance.State);
                ldt = mobjDataAcces.GetDataTable("sp_GetCityForInsurance", lstParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public int InsertInsurance(EntityInsuranceCom entInsurance)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@InsuranceCode", DbType.String, entInsurance.InsuranceCode);
                Commons.ADDParameter(ref lstParam, "@InsuranceDesc", DbType.String, entInsurance.InsuranceDesc);
                Commons.ADDParameter(ref lstParam, "@Address", DbType.String, entInsurance.Address);
                Commons.ADDParameter(ref lstParam, "@Country", DbType.String, entInsurance.Country);
                Commons.ADDParameter(ref lstParam, "@State", DbType.String, entInsurance.State);
                Commons.ADDParameter(ref lstParam, "@City", DbType.String, entInsurance.City);
                Commons.ADDParameter(ref lstParam, "@EmailID", DbType.String, entInsurance.EmailID);
                Commons.ADDParameter(ref lstParam, "@ContactNo", DbType.String, entInsurance.ContactNo);
                Commons.ADDParameter(ref lstParam, "@PostalCode", DbType.String, entInsurance.PostalCode);
                Commons.ADDParameter(ref lstParam, "@FaxNumber", DbType.String, entInsurance.FaxNumber);
                Commons.ADDParameter(ref lstParam, "@ContactPerson", DbType.String, entInsurance.ContactPerson);
                Commons.ADDParameter(ref lstParam, "@ContactPhNo", DbType.String, entInsurance.ContactPhNo);
                Commons.ADDParameter(ref lstParam, "@MobileNo", DbType.String, entInsurance.MobileNo);
                Commons.ADDParameter(ref lstParam, "@ContactEmailID", DbType.String, entInsurance.ContactEmail);
                Commons.ADDParameter(ref lstParam, "@Notes", DbType.String, entInsurance.Notes);
                Commons.ADDParameter(ref lstParam, "@EntryBy", DbType.String, entInsurance.EntryBy);
                cnt = mobjDataAcces.ExecuteQuery("sp_InsertInsurance", lstParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cnt;
        }

        public DataTable GetInsuranceForEdit(string pstrInsuranceCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@InsuranceCode", DbType.String, pstrInsuranceCode);
                ldt = mobjDataAcces.GetDataTable("sp_GetInsuranceForEdit", lstParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public int UpdateInsurance(EntityInsuranceCom entInsurance)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@InsuranceCode", DbType.String, entInsurance.InsuranceCode);
                Commons.ADDParameter(ref lstParam, "@InsuranceDesc", DbType.String, entInsurance.InsuranceDesc);
                Commons.ADDParameter(ref lstParam, "@Address", DbType.String, entInsurance.Address);
                Commons.ADDParameter(ref lstParam, "@Country", DbType.String, entInsurance.Country);
                Commons.ADDParameter(ref lstParam, "@State", DbType.String, entInsurance.State);
                Commons.ADDParameter(ref lstParam, "@City", DbType.String, entInsurance.City);
                Commons.ADDParameter(ref lstParam, "@EmailID", DbType.String, entInsurance.EmailID);
                Commons.ADDParameter(ref lstParam, "@ContactNo", DbType.String, entInsurance.ContactNo);
                Commons.ADDParameter(ref lstParam, "@PostalCode", DbType.String, entInsurance.PostalCode);
                Commons.ADDParameter(ref lstParam, "@ContactPerson", DbType.String, entInsurance.ContactPerson);
                Commons.ADDParameter(ref lstParam, "@ContactPhNo", DbType.String, entInsurance.ContactPhNo);
                Commons.ADDParameter(ref lstParam, "@MobileNo", DbType.String, entInsurance.MobileNo);
                Commons.ADDParameter(ref lstParam, "@ContactEmailID", DbType.String, entInsurance.ContactEmail);
                Commons.ADDParameter(ref lstParam, "@Notes", DbType.String, entInsurance.Notes);
                Commons.ADDParameter(ref lstParam, "@ChangeBy", DbType.String, entInsurance.ChangeBy);
                cnt = mobjDataAcces.ExecuteQuery("sp_UpdateInsurance", lstParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cnt;
        }

        public int DeleteInsurance(EntityInsuranceCom entInsurance)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@InsuranceCode", DbType.String, entInsurance.InsuranceCode);
                cnt = mobjDataAcces.ExecuteQuery("sp_DeleteInsurance", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("InsuranceComBLL - DeleteInsurance(EntityInsurance entInsurance)", ex);
            }
            return cnt;
        }

        public EntityInsuranceCom GetInsuranceById(int Id)
        {
            EntityInsuranceCom obj = null;
            try
            {
                obj = (from tbl in objData.tblInsuranceComMasters
                       where tbl.PKId == Id
                       select new EntityInsuranceCom
                       {
                           Address = tbl.Address,
                           City = tbl.City,
                           ContactEmail = tbl.ContactEmailID,
                           ContactNo = tbl.ContactNo,
                           ContactPerson = tbl.ContactPerson,
                           ContactPhNo = tbl.ContactPhNo,
                           Country = tbl.ContactPhNo,
                           EmailID = tbl.EmailID,
                           FaxNumber = tbl.FaxNumber,
                           InsuranceCode = tbl.InsuranceCode,
                           InsuranceDesc = tbl.InsuranceDesc,
                           MobileNo = tbl.MobileNo,
                           Notes = tbl.Notes,
                           PostalCode = tbl.PostalCode,
                           State = tbl.State
                       }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

        public List<sp_GetAllInsuranceResult> GetAllInsurance(string prefix)
        {
            List<sp_GetAllInsuranceResult> lst = null;
            try
            {
                lst = (from tbl in GetAllInsurance()
                       where tbl.InsuranceDesc.ToUpper().Contains(prefix.ToUpper())
                       || tbl.City.ToUpper().Contains(prefix.ToUpper())
                       || tbl.State.ToUpper().Contains(prefix.ToUpper())
                       || tbl.ContactNo.ToUpper().Contains(prefix.ToUpper())
                       || tbl.ContactPerson.ToUpper().Contains(prefix.ToUpper())
                       select tbl).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }
    }
}