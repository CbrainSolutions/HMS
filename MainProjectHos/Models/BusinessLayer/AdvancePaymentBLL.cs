using MainProjectHos.Models.DataLayer;
using MainProjectHos.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.BusinessLayer
{
    public class AdvancePaymentBLL
    {
        public AdvancePaymentBLL()
        {
            objData = new CriticareHospitalDataContext();
        }

        public CriticareHospitalDataContext objData { get; set; }

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

        public int Save(EntityCustomerTransaction entCust, bool IsCash)
        {
            int TransactionId = new PatientInvoiceBLL().GetTransactionId();
            int ReceiptNo = GetReceiptNo();

            if (IsCash)
            {
                tblCustomerTransaction objDebit = new tblCustomerTransaction()
                {
                    ReceiptDate = entCust.ReceiptDate,
                    PatientId = entCust.PatientId,
                    TransactionId = TransactionId,
                    TransactionType = "Refund",
                    TransactionDocNo = ReceiptNo,
                    IsCash = true,
                    BillAmount = entCust.BillAmount,
                    PayAmount = 0,
                    IsDelete = false,

                };
                objData.tblCustomerTransactions.InsertOnSubmit(objDebit);
            }
            else
            {
                tblCustomerTransaction objDebit = new tblCustomerTransaction()
                {
                    ReceiptDate = entCust.ReceiptDate,
                    PatientId = entCust.PatientId,
                    TransactionId = TransactionId,
                    TransactionType = "Refund",
                    TransactionDocNo = ReceiptNo,
                    ISCheque = true,
                    BillAmount = entCust.BillAmount,
                    PayAmount = 0,
                    ChequeDate = entCust.ReceiptDate,
                    ChequeNo = entCust.ChequeNo,
                    BankName = entCust.BankName,
                    IsDelete = false,

                };
                objData.tblCustomerTransactions.InsertOnSubmit(objDebit);
            }
            objData.SubmitChanges();
            return 1;
        }

        private int GetReceiptNo()
        {
            int TId = 0;
            try
            {
                int Cnt = (from tbl in objData.tblCustomerTransactions
                           select tbl).Count();
                if (Cnt == 0)
                {
                    TId = 1;
                }
                else
                {
                    TId = Convert.ToInt32((from tbl in objData.tblCustomerTransactions
                                           select tbl).Max(e => e.ReceiptNo));

                    TId++;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return TId;
        }

        public int Update(List<EntityCustomerTransaction> lst, tblCustomerTransaction obj)
        {
            try
            {
                tblCustomerTransaction objTest = (from tbl in objData.tblCustomerTransactions
                                                  where tbl.IsDelete == false
                                                  && tbl.ReceiptNo == obj.ReceiptNo
                                                  select tbl).FirstOrDefault();

                List<EntityCustomerTransaction> lstTemp = new List<EntityCustomerTransaction>();

                List<tblTestInvoice> lstCurrent = (from tbl in objData.tblTestInvoices
                                                   where tbl.PatientId == obj.PatientId
                                                   && tbl.IsDelete == false
                                                   select tbl).ToList();

                if (objTest != null)
                {
                    objTest.ReceiptDate = obj.ReceiptDate;
                    objTest.BillAmount = obj.BillAmount;
                    objTest.PayAmount = obj.PayAmount;
                    objTest.Discount = obj.Discount;
                    objTest.PatientId = obj.PatientId;
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
                        join tblPatient in objData.tblPatientMasters
                        on tbl.PatientId equals tblPatient.PKId
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
                        where tbl.PatientName.Contains(SearchPrefix) || tbl.Address.Contains(SearchPrefix)
                        select tbl).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EntityCustomerTransaction> GetTestInvoiceDetails(int PatientId)
        {
            List<EntityCustomerTransaction> lst = null;
            try
            {
                lst = (from tbl in objData.tblCustomerTransactions
                       join tblTest in objData.tblTestInvoices
                       on tbl.PatientId equals tblTest.PatientId
                       where tblTest.IsDelete == false
                       && tbl.PatientId == PatientId
                       select new EntityCustomerTransaction
                       {
                           PatientId = tblTest.PatientId,
                           Amount = Convert.ToDecimal(tbl.BillAmount)
                       }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }

        public decimal GetPatientTrans(int Pat_Id)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            List<tblCustomerTransaction> lstRefund = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                tblPatientAdmitDetail admit = (from tbl in objData.tblPatientAdmitDetails
                                               where tbl.AdmitId == Pat_Id
                                               select tbl).FirstOrDefault();

                if (admit != null)
                {
                    List<tblPatientAdmitDetail> lst = (from tbl in objData.tblPatientAdmitDetails
                                                       where tbl.PatientId == admit.PatientId
                                                       select tbl).ToList();

                    foreach (tblPatientAdmitDetail item in lst)
                    {
                        lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                          where tbl.PatientId == item.PatientId
                                          && tbl.TransactionType != "Refund"
                                          select tbl);
                    }
                    foreach (tblPatientAdmitDetail item in lst)
                    {
                        lstRefund.AddRange(from tbl in objData.tblCustomerTransactions
                                           where tbl.PatientId == item.AdmitId
                                           && tbl.TransactionType.Equals("Refund")
                                           select tbl);
                    }
                    if (lstTrans.Sum(p => p.BillAmount) > 0)
                    {
                        FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.BillAmount)) - Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount)) - Convert.ToDecimal(lstRefund.Sum(p => p.PayAmount));
                    }
                    else
                    {
                        FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount) - Convert.ToDecimal(lstRefund.Sum(p => p.PayAmount)));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetPatientBillAmount(int Pat_Id)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                tblPatientAdmitDetail admit = (from tbl in objData.tblPatientAdmitDetails
                                               where tbl.AdmitId == Pat_Id
                                               select tbl).FirstOrDefault();

                if (admit != null)
                {
                    List<tblPatientAdmitDetail> lst = (from tbl in objData.tblPatientAdmitDetails
                                                       where tbl.PatientId == admit.PatientId
                                                       select tbl).ToList();

                    foreach (tblPatientAdmitDetail item in lst)
                    {
                        lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                          where tbl.PatientId == item.PatientId
                                          select tbl);
                    }
                    FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.BillAmount));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public List<EntityCustomerTransaction> GetCustomerTransactionList()
        {
            try
            {
                return (from tbl in objData.tblCustomerTransactions
                        join tblAdmit in objData.tblPatientAdmitDetails
                        on tbl.PatientId equals tblAdmit.AdmitId
                        join tblPat in objData.tblPatientMasters
                        on tblAdmit.PatientId equals tblPat.PKId
                        where tbl.IsDelete == false
                        && tbl.BillAmount > 0
                        && tbl.TransactionType.Equals("Refund")

                        select new EntityCustomerTransaction
                        {
                            ReceiptNo = tbl.ReceiptNo,
                            ReceiptDate = tbl.ReceiptDate,
                            PatientName = tblPat.PatientFirstName + ' ' + tblPat.PatientMiddleName + ' ' + tblPat.PatientLastName,
                            Address = tblPat.Address,
                            Amount = tbl.BillAmount
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}