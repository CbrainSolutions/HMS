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
    public class PathologyBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();

        public PathologyBLL()
        {
            objData = new CriticareHospitalDataContext();
        }

        public CriticareHospitalDataContext objData { get; set; }


        public List<TestAllocation> GetPatientList()
        {
            try
            {
                return (from tbl in objData.tblTestInvoices
                        join tblAdmit in objData.tblPatientAdmitDetails
                        on tbl.PatientId equals tblAdmit.AdmitId
                        join tblPat in objData.tblPatientMasters
                        on tblAdmit.PatientId equals tblPat.PKId
                        where tbl.IsDelete == false
                        && tblAdmit.IsDischarge == false
                        select new TestAllocation { AdmitId = tblAdmit.AdmitId, PatientName = tblPat.PatientFirstName + " " + tblPat.PatientMiddleName + " " + tblPat.PatientLastName }).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TestAllocation> GetAllocatedTests(int PatientId)
        {
            try
            {
                return (from tbl in objData.tblTestInvoiceDetails
                        join tbltest in objData.tblTestInvoices
                          on tbl.TestInvoiceId equals tbltest.TestInvoiceNo
                        join tblmaster in objData.tblTestMasters
                        on tbl.TestId equals tblmaster.TestId
                        where tbl.IsDelete == false
                        && tbltest.PatientId == PatientId
                        select new TestAllocation { TestId = tbl.TestId, TestName = tblmaster.TestName }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EntityPathology> GetAllocatedTestsToPatient(int PatientId, int TestId)
        {
            try
            {
                return (from tbl in objData.tblTestInvoiceDetails
                        join tbltest in objData.tblTestParas
                          on tbl.TestId equals tbltest.TestId
                        join tbltestin in objData.tblTestInvoices
                        on tbl.TestInvoiceId equals tbltestin.TestInvoiceNo
                        where tbl.IsDelete == false
                        && tbltestin.PatientId == PatientId
                        && tbl.TestId == TestId
                        select new EntityPathology
                        {
                            TestId = Convert.ToInt32(tbl.TestId),
                            TestParaId = tbltest.TstParID,
                            TestParaName = tbltest.ParaName,
                            Min = tbltest.MinPara,
                            Max = tbltest.MaxPara
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<EntityPathology> GetUnit()
        {
            try
            {
                return (from tbl in objData.tblUnitMasters
                        where tbl.IsDelete == false
                        select new EntityPathology { Unit = tbl.PKId, unitname = tbl.UnitDesc }).ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<EntityPathology> GetTestMethod()
        {
            try
            {
                return (from tbl in objData.tblTestMethods
                        where tbl.IsDelete == false
                        select new EntityPathology { TestMethodId = tbl.TestMethodId, TestMethodName = tbl.TestMethodName }).ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int Save(tblPathology pathology, List<tblPathologyDetail> lst)
        {
            int? Id = 0;
            objData.STP_Insert_tblPathology(Convert.ToInt32(pathology.PatientId), Convert.ToInt32(pathology.TestId), Convert.ToDateTime(DateTime.Now.Date), pathology.FinalComment, pathology.TestDoneById, ref Id);

            foreach (tblPathologyDetail item in lst)
            {
                tblPathologyDetail objtestDetails = new tblPathologyDetail()
                {
                    LabId = Convert.ToInt32(Id),
                    TestParaId = item.TestParaId,
                    UnitId = item.UnitId,
                    TestMethodId = item.TestMethodId,
                    Result = item.Result,
                    Comment = item.Comment,
                    Isdelete = false,

                };
                objData.tblPathologyDetails.InsertOnSubmit(objtestDetails);
            }
            objData.SubmitChanges();
            return 1;
        }

        public DataTable GetPathologyTests()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("STP_SelectPathology");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable SelectPatientTest(int id)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@LabId", DbType.String, id);
                ldt = mobjDataAcces.GetDataTable("STP_PathologyEdit", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("EntityPathology - SelectPatientTest()", ex);
            }
            return ldt;
        }

        public void Update(List<tblPathologyDetail> lst, tblPathology tblpath)
        {
            try
            {
                tblPathology path = (from tbl in objData.tblPathologies
                                     where tbl.PatientId == tblpath.PatientId
                                     select tbl).FirstOrDefault();
                if (path != null)
                {
                    path.FinalComment = tblpath.FinalComment;
                }
                foreach (tblPathologyDetail item in lst)
                {
                    tblPathologyDetail tblpat = (from tbl in objData.tblPathologyDetails
                                                 where tbl.LabId == item.LabId
                                                 && tbl.TestParaId == item.TestParaId
                                                 && tbl.Isdelete == false
                                                 select tbl).FirstOrDefault();
                    if (tblpat != null)
                    {
                        tblpat.Result = item.Result;
                        tblpat.UnitId = item.UnitId;
                        tblpat.TestMethodId = item.TestMethodId;
                        tblpat.Comment = item.Comment;
                    }
                }
                objData.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EntityPathology> SearchPathologyDetails(string Prefix)
        {
            List<EntityPathology> lst = null;
            try
            {
                lst = (from tbl in objData.tblPathologies
                       join tblA in objData.tblPatientAdmitDetails
                       on tbl.PatientId equals tblA.AdmitId
                       join tblp in objData.tblPatientMasters
                       on tblA.PatientId equals tblp.PKId
                       join tblt in objData.tblTestMasters
                       on tbl.TestId equals tblt.TestId
                       where tbl.IsDelete == false &&
                       (tbl.PatientId.ToString().Contains(Prefix.ToString()) || tblp.PatientFirstName.ToUpper().ToString().Contains(Prefix.ToString().ToUpper()) || tblp.PatientMiddleName.ToUpper().ToString().Contains(Prefix.ToUpper().ToString()) || tblp.PatientLastName.ToUpper().ToString().Contains(Prefix.ToUpper().ToString()) || tblt.TestName.ToUpper().ToString().Contains(Prefix.ToUpper().ToString()))
                       select new EntityPathology
                       {
                           PatientId = tbl.PatientId,
                           PatientName = tblp.PatientFirstName + ' ' + tblp.PatientMiddleName + ' ' + tblp.PatientLastName,
                           TestDate = Convert.ToDateTime(tbl.TestDate),
                           TestDoneById = Convert.ToInt32(tbl.TestDoneById),
                           LabId = tbl.LabId,
                           TestName = tblt.TestName,
                           FinalComment = tbl.FinalComment,

                       }).ToList();
                return lst;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GetTestStatus(int TestID)
        {
            bool flag = false;
            tblTestMaster tblTest = new tblTestMaster();
            tblTest = (from tbl in objData.tblTestMasters
                       where tbl.TestId.Equals(TestID)
                       && tbl.IsRadiology == true
                       select tbl).FirstOrDefault();
            if (tblTest != null)
            {
                flag = true;
            }
            return flag;
        }

        public int GetLabId()
        {
            int TId = 0;
            try
            {
                int Cnt = (from tbl in objData.tblPathologies
                           select tbl).Count();
                if (Cnt == 0)
                {
                    TId = 1;
                }
                else
                {
                    TId = Convert.ToInt32((from tbl in objData.tblPathologies
                                           select tbl).Max(e => e.LabId));

                    TId++;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return TId;
        }

        public int CheckPatientDischarge(int PatientId)
        {
            int ID = 0;
            try
            {
                ID = (from tbl in objData.tblPatientAdmitDetails
                      where tbl.AdmitId.Equals(PatientId)
                      && tbl.IsDischarge == true
                      select tbl).Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ID;
        }


    }

}