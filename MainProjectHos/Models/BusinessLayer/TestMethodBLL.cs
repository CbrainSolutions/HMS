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
    public class TestMethodBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();

        public TestMethodBLL()
        {
            objData = new CriticareHospitalDataContext();
        }

        public CriticareHospitalDataContext objData { get; set; }
        public int GetNewTestMethodId()
        {
            EntityTestMethod ldt = new EntityTestMethod();
            try
            {
                ldt = (from tbl in objData.tblTestMethods
                       where tbl.IsDelete == false
                       orderby tbl.TestMethodId descending
                       select new EntityTestMethod { TestMethodId = (tbl.TestMethodId + 1) }).FirstOrDefault();
            }
            catch (Exception ex)
            {

                Commons.FileLog("TestMethodBLL - GetNewTestMethodId()", ex);
            }
            if (ldt != null)
            {
                return ldt.TestMethodId;
            }
            else
            {
                return 1;
            }
        }



        public DataTable GetAllTests()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllTests");
            }
            catch (Exception ex)
            {
                Commons.FileLog("TestMethodBLL - GetAllTests()", ex);
            }
            return ldt;
        }

        public DataTable GetTestMethodForEdit(string pstrTestMethodId)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@TestMethodId", DbType.String, pstrTestMethodId);
                ldt = mobjDataAcces.GetDataTable("sp_GetTestMethodForEdit", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("TestMethodBLL - GetTestForEditMethod(string pstrTestMethodId)", ex);
            }
            return ldt;
        }


        public int InsertTestMethod(EntityTestMethod entTest)
        {
            try
            {
                tblTestMethod obj = new tblTestMethod()
                {
                    TestMethodName = entTest.TestMethodDesc,
                    IsDelete = false

                };
                objData.tblTestMethods.InsertOnSubmit(obj);
                objData.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int UpdateTestMethod(EntityTestMethod entTest)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@TestMethodId", DbType.String, entTest.TestMethodId);
                Commons.ADDParameter(ref lstParam, "@TestMethodDesc", DbType.String, entTest.TestMethodDesc);

                cnt = mobjDataAcces.ExecuteQuery("sp_UpdateTestMethod", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("TestMethodBLL -  UpdateTestMethod(EntityTestMethod entTest)", ex);
            }

            return cnt;
        }

        public int DeleteTestMethod(EntityTestMethod entTest)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@TestMethodId", DbType.String, entTest.TestMethodId);
                cnt = mobjDataAcces.ExecuteQuery("sp_DeleteTestMethod", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("TestMethodBLL - DeleteTestMethod(EntityTestMethod entTest)", ex);
            }
            return cnt;
        }
    }
}