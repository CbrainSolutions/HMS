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
    public class TestBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();

        public TestBLL()
        {
            objData = new CriticareHospitalDataContext();
        }

        public CriticareHospitalDataContext objData { get; set; }

        public List<EntityTest> GetAllTests()
        {
            try
            {
                return (from tbl in objData.tblTestMasters
                        join tblTestCat in objData.tblTestCatagories
                        on tbl.TestCatId equals tblTestCat.TestCatId
                        where tbl.IsDelete == false
                        select new EntityTest
                        {
                            TestId = tbl.TestId,
                            TestCatId = tblTestCat.TestCatId,
                            TestName = tbl.TestName,
                            IsPathology = Convert.ToBoolean(tbl.IsPathology),
                            IsRadiology = Convert.ToBoolean(tbl.IsRadiology),
                            Precautions = tbl.Precautions,
                            TestCharge = tbl.TestCharge,
                            TestCatagoryName = tblTestCat.TestCatDescription
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertTest(EntityTest entDept)
        {
            try
            {
                tblTestMaster obj = new tblTestMaster()
                {
                    Precautions = entDept.Precautions,
                    TestName = entDept.TestName,
                    TestCharge = entDept.TestCharge,
                    IsDelete = false,
                    IsPathology = entDept.IsPathology,
                    IsRadiology = entDept.IsRadiology,
                    TestCatId = entDept.TestCatId
                };
                objData.tblTestMasters.InsertOnSubmit(obj);
                objData.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertTestWithPara(EntityTestPara entDept)
        {
            try
            {
                tblTestPara obj = new tblTestPara()
                {
                    TestId = entDept.TestId,
                    ParaName = entDept.ParaName,
                    MinPara = entDept.MinPara,
                    MaxPara = entDept.MaxPara
                };
                objData.tblTestParas.InsertOnSubmit(obj);
                objData.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsRecordExists(EntityTest entDept)
        {
            bool flag = false;
            try
            {
                tblTestMaster obj = (from tbl in objData.tblTestMasters
                                     where tbl.IsDelete == false
                                     && tbl.TestName.ToUpper().ToString().Trim().Equals(entDept.TestName.ToUpper().ToString().Trim())
                                     select tbl).FirstOrDefault();
                if (obj != null)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flag;
        }

        public bool IsRecordExist(EntityTestPara entDept)
        {
            bool flag = false;
            try
            {
                tblTestPara obj = (from tbl in objData.tblTestParas
                                   where tbl.IsDelete == false
                                   && tbl.TestId.Equals(entDept.TestId)
                                  && tbl.ParaName.ToUpper().ToString().Trim().Equals(entDept.ParaName.ToUpper().ToString().Trim())
                                   select tbl).FirstOrDefault();
                if (obj != null)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flag;
        }

        public int Update(EntityTest entDept)
        {
            try
            {
                tblTestMaster test = (from tbl in objData.tblTestMasters
                                      where tbl.IsDelete == false
                                      && tbl.TestId == entDept.TestId
                                      select tbl).FirstOrDefault();
                test.TestName = entDept.TestName;
                test.TestCharge = entDept.TestCharge;
                test.Precautions = entDept.Precautions;
                test.IsPathology = entDept.IsPathology;
                test.IsRadiology = entDept.IsRadiology;
                test.TestCatId = entDept.TestCatId;
                objData.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EntityTestPara> GetAllTestsWithPara()
        {
            try
            {
                return (from tbl in objData.tblTestParas
                        join tblT in objData.tblTestMasters
                        on tbl.TestId equals tblT.TestId
                        where tbl.IsDelete == false
                        && tblT.IsDelete == false
                        select new EntityTestPara
                        {
                            TstParID = tbl.TstParID,
                            TestId = tbl.TestId,
                            TestName = tblT.TestName,
                            ParaName = tbl.ParaName,
                            MinPara = tbl.MinPara,
                            MaxPara = tbl.MaxPara
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public System.Data.DataTable GetAllTest()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllTest");
            }
            catch (Exception ex)
            {
                Commons.FileLog("TestBLL - GetAllTest()", ex);
            }
            return ldt;
        }

        public DataTable SelectTestParaForEdit(string id)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@TstParID", DbType.String, id);
                ldt = mobjDataAcces.GetDataTable("sp_TestPara", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("TestBLL - SelectTestParaForEdit()", ex);
            }
            return ldt;
        }

        public int UpdatePara(EntityTestPara entDept)
        {
            try
            {
                tblTestPara test = (from tbl in objData.tblTestParas
                                    where tbl.IsDelete == false
                                    && tbl.TstParID == entDept.TstParID
                                    select tbl).FirstOrDefault();
                test.TestId = entDept.TestId;
                test.ParaName = entDept.ParaName;
                test.MinPara = entDept.MinPara;
                test.MaxPara = entDept.MaxPara;
                objData.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<tblTestCatagory> GetAllTestCatagory()
        {
            try
            {
                return (from tbl in objData.tblTestCatagories
                        where tbl.IsDelete == false
                        select tbl).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EntityTest> GetAllTests(string SearchText)
        {
            return (from tbl in GetAllTests()
                    where tbl.TestName.Contains(SearchText) || tbl.TestCatagoryName.Contains(SearchText)
                    select tbl).ToList();
        }
    }
}