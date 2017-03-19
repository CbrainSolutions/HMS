using MainProjectHos.Models.DataLayer;
using MainProjectHos.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.BusinessLayer
{
    public class clsTestAllocation
    {
        public clsTestAllocation()
        {
            objData = new CriticareHospitalDataContext();
        }

        public CriticareHospitalDataContext objData { get; set; }

        public List<EntityPatientMaster> GetAdmitPatientList()
        {
            List<EntityPatientMaster> lst = null;
            try
            {
                lst = (from tbl in objData.tblPatientMasters
                       join tblAdmit in objData.tblPatientAdmitDetails
                       on tbl.PKId equals tblAdmit.PatientId
                       where tblAdmit.IsDelete == false && tblAdmit.IsDischarge == false
                       select new EntityPatientMaster
                       {
                           PatientId = Convert.ToInt32(tblAdmit.AdmitId),
                           FullName = tbl.PatientFirstName + " " + tbl.PatientMiddleName + " " + tbl.PatientLastName
                       }).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return lst;
        }
        public List<EntityPatientMaster> GetPatientListForDischarge()
        {
            List<EntityPatientMaster> lst = null;
            try
            {
                lst = (from tbl in objData.tblPatientMasters
                       join tblAdmit in objData.tblPatientAdmitDetails
                       on tbl.PKId equals tblAdmit.PatientId
                       where tblAdmit.IsDelete == false
                       select new EntityPatientMaster
                       {
                           PatientId = Convert.ToInt32(tblAdmit.AdmitId),
                           FullName = tbl.PatientFirstName + " " + tbl.PatientMiddleName + " " + tbl.PatientLastName
                       }).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return lst;
        }

        public List<EntityPatientMaster> GetAllocatedPatient()
        {
            List<EntityPatientMaster> lst = null;
            try
            {
                lst = (from tbl in objData.tblPatientMasters
                       join tblAdmit in objData.tblPatientAdmitDetails
                       on tbl.PKId equals tblAdmit.PatientId
                       where tblAdmit.IsDelete == false
                       select new EntityPatientMaster
                       {
                           PatientId = Convert.ToInt32(tblAdmit.AdmitId),
                           FullName = tbl.PatientFirstName + " " + tbl.PatientMiddleName + " " + tbl.PatientLastName
                       }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }

        public int Save(List<TestAllocation> lst, tblTestInvoice obj, bool IsCash)
        {
            int? Id = 0;
            objData.STP_Insert_tblTestInvoice(Convert.ToDateTime(DateTime.Now.Date), ref Id, Convert.ToInt32(obj.PatientId), Convert.ToDecimal(obj.Amount), Convert.ToDecimal(obj.Discount));
            foreach (TestAllocation item in lst)
            {
                tblTestInvoiceDetail objtestDetails = new tblTestInvoiceDetail()
                {
                    TestInvoiceId = Convert.ToInt32(Id),
                    TestId = item.TestId,
                    IsDelete = false,
                    Charges = Convert.ToDecimal(item.Charges)
                };
                objData.tblTestInvoiceDetails.InsertOnSubmit(objtestDetails);
            }
            int TransactionId = new PatientInvoiceBLL().GetTransactionId();
            if (IsCash)
            {
                tblCustomerTransaction objDebit = new tblCustomerTransaction()
                {
                    TransactionId = TransactionId,
                    IsCash = true,
                    TransactionDocNo = Id,
                    TransactionType = "TestInvoice",
                    BillAmount = obj.Amount,
                    PayAmount = 0,
                    PatientId = obj.PatientId,
                    IsDelete = false,
                    ReceiptDate = obj.TestInvoiceDate,
                };
                objData.tblCustomerTransactions.InsertOnSubmit(objDebit);
                //objData.SubmitChanges();
                tblCustomerTransaction objCrReceipt = new tblCustomerTransaction()
                {
                    TransactionId = TransactionId,
                    IsCash = true,
                    TransactionDocNo = Id,
                    TransactionType = "TestInvoice",
                    PayAmount = obj.Amount,
                    BillAmount = 0,
                    PatientId = obj.PatientId,
                    IsDelete = false,
                    ReceiptDate = obj.TestInvoiceDate,
                };
                objData.tblCustomerTransactions.InsertOnSubmit(objCrReceipt);
                //objData.SubmitChanges();
            }
            else
            {
                tblCustomerTransaction objDebit = new tblCustomerTransaction()
                {
                    TransactionId = TransactionId,
                    IsCash = false,
                    TransactionDocNo = Id,
                    TransactionType = "TestInvoice",
                    BillAmount = obj.Amount,
                    PatientId = obj.PatientId,
                    IsDelete = false,
                    ReceiptDate = obj.TestInvoiceDate,
                };
                objData.tblCustomerTransactions.InsertOnSubmit(objDebit);
                //objData.SubmitChanges();
            }
            objData.SubmitChanges();
            return 1;
        }

        public int Update(List<TestAllocation> lst, tblTestInvoice obj, bool IsCash)
        {
            try
            {
                tblTestInvoice objTest = (from tbl in objData.tblTestInvoices
                                          where tbl.IsDelete == false
                                          && tbl.TestInvoiceNo == obj.TestInvoiceNo
                                          select tbl).FirstOrDefault();

                List<TestAllocation> lstTemp = new List<TestAllocation>();

                List<tblTestInvoiceDetail> lstCurrent = (from tbl in objData.tblTestInvoiceDetails
                                                         where tbl.TestInvoiceId == obj.TestInvoiceNo
                                                         && tbl.IsDelete == false
                                                         select tbl).ToList();

                List<tblCustomerTransaction> objCust = (from tbl in objData.tblCustomerTransactions
                                                        where tbl.IsDelete == false
                                                        && tbl.TransactionDocNo.Equals(Convert.ToString(obj.TestInvoiceNo))
                                                        && tbl.TransactionType.Equals("TestInvoice")
                                                        select tbl).ToList();
                if (objTest != null)
                {
                    objTest.TestInvoiceDate = obj.TestInvoiceDate;
                    objTest.Amount = obj.Amount;
                    objTest.Discount = obj.Discount;
                    objTest.PatientId = obj.PatientId;
                }
                if (objCust.Count == 1)
                {
                    if (IsCash)
                    {
                        tblCustomerTransaction objC = new tblCustomerTransaction()
                        {
                            PayAmount = obj.Amount,
                            ReceiptDate = obj.TestInvoiceDate,
                            IsCash = IsCash,
                            IsDelete = false,
                            TransactionDocNo = obj.TestInvoiceNo,
                            TransactionId = objCust[0].TransactionId,
                            TransactionType = "TestInvoice",
                            PatientId = obj.PatientId,
                            BillAmount = 0
                        };
                        objData.tblCustomerTransactions.InsertOnSubmit(objC);
                        foreach (tblCustomerTransaction item in objCust)
                        {
                            item.BillAmount = obj.Amount;
                            item.ReceiptDate = obj.TestInvoiceDate;
                            item.IsCash = IsCash;
                            if (item.IsCash == false)
                            {
                                if (item.PayAmount > 0)
                                {
                                    item.IsDelete = true;
                                }
                            }
                            else
                            {
                                if (item.PayAmount > 0)
                                {
                                    item.PayAmount = obj.Amount;
                                }
                                else
                                {
                                    item.PayAmount = 0;
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (tblCustomerTransaction item in objCust)
                        {
                            item.BillAmount = obj.Amount;
                            item.ReceiptDate = obj.TestInvoiceDate;
                            item.IsCash = IsCash;
                            if (item.IsCash == false)
                            {
                                if (item.PayAmount > 0)
                                {
                                    item.IsDelete = true;
                                }
                            }
                            else
                            {
                                if (item.PayAmount > 0)
                                {
                                    item.PayAmount = obj.Amount;
                                }
                                else
                                {
                                    item.PayAmount = 0;
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (tblCustomerTransaction item in objCust)
                    {
                        item.BillAmount = obj.Amount;
                        item.ReceiptDate = obj.TestInvoiceDate;
                        item.IsCash = IsCash;
                        if (item.IsCash == false)
                        {
                            if (item.PayAmount > 0)
                            {
                                item.IsDelete = true;
                            }
                        }
                        else
                        {
                            if (item.PayAmount > 0)
                            {
                                item.PayAmount = obj.Amount;
                            }
                            else
                            {
                                item.PayAmount = 0;
                            }
                        }
                    }
                }

                foreach (TestAllocation item in lst)
                {
                    int cnt = (from tbl in objData.tblTestInvoiceDetails
                               where tbl.TestInvoiceId == obj.TestInvoiceNo
                               && tbl.TestId == item.TestId
                               && tbl.IsDelete == false
                               select tbl).ToList().Count;
                    if (cnt == 0)
                    {
                        tblTestInvoiceDetail objNewAdded = new tblTestInvoiceDetail()
                        {
                            TestInvoiceId = obj.TestInvoiceNo,
                            TestId = item.TestId,
                            Charges = item.Charges,
                            IsDelete = false,
                        };
                        objData.tblTestInvoiceDetails.InsertOnSubmit(objNewAdded);
                        objData.SubmitChanges();
                    }
                    else
                    {
                        lstTemp.Add(item);
                    }
                }


                foreach (tblTestInvoiceDetail item in lstCurrent)
                {
                    int cnt = (from tbl in lst
                               where tbl.TestInvoiceNo == item.TestInvoiceId
                               && tbl.TestId == item.TestId
                               select tbl).ToList().Count;

                    if (cnt == 0)
                    {
                        int checkExist = (from tbl in lstTemp
                                          where tbl.TestId == item.TestId
                                          select tbl).ToList().Count;
                        if (checkExist == 0)
                        {
                            item.IsDelete = true;
                        }
                    }
                }
                objData.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EntityTestInvoice> GetTestInvoiceList()
        {
            try
            {
                return (from tbl in objData.tblTestInvoices
                        join tblAdmit in objData.tblPatientAdmitDetails
                        on tbl.PatientId equals tblAdmit.AdmitId
                        join tblPatient in objData.tblPatientMasters
                        on tblAdmit.PatientId equals tblPatient.PKId
                        where tblPatient.IsDelete == false
                        select new EntityTestInvoice
                        {
                            TestInvoiceNo = tbl.TestInvoiceNo,
                            TestInvoiceDate = tbl.TestInvoiceDate,
                            Address = tblPatient.Address,
                            Amount = tbl.Amount,
                            Discount = tbl.Discount,
                            PatientName = tblPatient.PatientFirstName + " " + tblPatient.PatientMiddleName + " " + tblPatient.PatientLastName,
                            PatientId = tbl.PatientId
                        }).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EntityTestInvoice> GetTestInvoiceList(string SearchPrefix)
        {
            try
            {
                return (from tbl in GetTestInvoiceList()
                        where tbl.PatientName.ToString().ToUpper().Trim().Contains(SearchPrefix.ToString().ToUpper().Trim()) || tbl.Address.ToString().ToUpper().Trim().Contains(SearchPrefix.ToString().ToUpper().Trim())
                        select tbl).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TestAllocation> GetTestInvoiceDetails(int TestInvoiceNo)
        {
            List<TestAllocation> lst = null;
            try
            {
                lst = (from tbl in objData.tblTestInvoices
                       join tblTest in objData.tblTestInvoiceDetails
                       on tbl.TestInvoiceNo equals tblTest.TestInvoiceId
                       where tblTest.IsDelete == false
                       && tbl.TestInvoiceNo == TestInvoiceNo
                       select new TestAllocation
                       {
                           TestId = tblTest.TestId,
                           DiscountAmount = Convert.ToDecimal(tbl.Discount),
                       }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }


    }
}