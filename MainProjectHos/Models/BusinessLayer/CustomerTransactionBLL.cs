using MainProjectHos.Models.DataModels;
using MainProjectHos.Models.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.BusinessLayer
{
    public class CustomerTransactionBLL
    {
        public CustomerTransactionBLL()
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
                       where tbl.IsDelete == false
                       select new EntityPatientMaster
                       {
                           PatientId = tbl.PKId,
                           FullName = tbl.PatientFirstName + " " + tbl.PatientMiddleName + " " + tbl.PatientLastName
                       }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }
        //
        public List<EntityPatientMaster> GetAllocatedPatientInfo()
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

        //
        public int Save(EntityCustomerTransaction entCust, EntityCustomerTransaction transact, bool IsCash)
        {
            tblPatientAdmitDetail admit = (from tbl in objData.tblPatientAdmitDetails
                                           where tbl.IsDelete == false
                                           && tbl.PatientId == entCust.PatientId
                                           orderby tbl.AdmitId descending
                                           select tbl).FirstOrDefault();
            int TransactionId = new PatientInvoiceBLL().GetTransactionId();
            int ReceiptNo = GetReceiptNo();
            if (IsCash)
            {
                tblCustomerTransaction objDebit = new tblCustomerTransaction()
                {
                    ReceiptDate = entCust.ReceiptDate,
                    PatientId = admit.AdmitId,
                    TransactionId = TransactionId,
                    TransactionType = "Receipt",
                    TransactionDocNo = ReceiptNo,
                    IsCash = true,
                    ISCheque = false,
                    PayAmount = entCust.PayAmount,
                    IsDelete = false,
                };
                objData.tblCustomerTransactions.InsertOnSubmit(objDebit);
            }
            else
            {
                if (transact != null)
                {
                    if (Convert.ToBoolean(transact.ISCheque))
                    {
                        tblCustomerTransaction objCredit = new tblCustomerTransaction()
                        {
                            ReceiptDate = entCust.ReceiptDate,
                            PatientId = admit.AdmitId,
                            TransactionId = TransactionId,
                            TransactionType = "Receipt",
                            TransactionDocNo = ReceiptNo,
                            IsCash = false,
                            PayAmount = entCust.PayAmount,
                            ISCheque = true,
                            IsDelete = false,
                        };
                        objData.tblCustomerTransactions.InsertOnSubmit(objCredit);
                        tblCustomerTransaction objDebit = new tblCustomerTransaction()
                        {
                            ReceiptDate = transact.ReceiptDate,
                            PatientId = transact.PatientId,
                            BankId = transact.BankId,
                            TransactionId = TransactionId,
                            TransactionType = "Receipt",
                            TransactionDocNo = ReceiptNo,
                            ISCheque = true,
                            PayAmount = transact.PayAmount,
                            ChequeDate = transact.ReceiptDate,
                            ChequeNo = transact.ChequeNo,
                            BankName = transact.BankName,
                            IsDelete = false,
                            IsCash = false,
                        };
                        objData.tblCustomerTransactions.InsertOnSubmit(objDebit);
                    }
                    else
                    {
                        tblCustomerTransaction objCredit = new tblCustomerTransaction()
                        {
                            ReceiptDate = entCust.ReceiptDate,
                            PatientId = admit.AdmitId,
                            TransactionId = TransactionId,
                            TransactionType = "Receipt",
                            TransactionDocNo = ReceiptNo,
                            IsCash = false,
                            PayAmount = entCust.PayAmount,
                            ISCheque = false,
                            IsDelete = false,
                            ChequeNo = entCust.ChequeNo,
                            BankName = entCust.BankName,
                            BillAmount = entCust.BillAmount,
                        };
                        objData.tblCustomerTransactions.InsertOnSubmit(objCredit);
                        tblCustomerTransaction objDebit = new tblCustomerTransaction()
                        {
                            ReceiptDate = transact.ReceiptDate,
                            PatientId = transact.PatientId,
                            BankId = transact.BankId,
                            TransactionId = TransactionId,
                            TransactionType = "Receipt",
                            TransactionDocNo = ReceiptNo,
                            ISCheque = false,
                            PayAmount = transact.PayAmount,
                            ChequeNo = transact.ChequeNo,
                            BankName = transact.BankName,
                            IsDelete = false,
                            IsCash = false,
                            BillAmount = transact.BillAmount,
                        };
                        objData.tblCustomerTransactions.InsertOnSubmit(objDebit);
                    }
                }
                else
                {
                    tblCustomerTransaction objCredit = new tblCustomerTransaction()
                    {
                        ReceiptDate = entCust.ReceiptDate,
                        PatientId = admit.AdmitId,
                        TransactionId = TransactionId,
                        TransactionType = "Receipt",
                        TransactionDocNo = ReceiptNo,
                        IsCash = false,
                        PayAmount = entCust.PayAmount,
                        ISCheque = false,
                        IsDelete = false,
                        ChequeNo = entCust.ChequeNo,
                        ChequeDate = entCust.ChequeDate,
                        BankName = entCust.BankName,
                        BillAmount = entCust.BillAmount,
                    };
                    objData.tblCustomerTransactions.InsertOnSubmit(objCredit);
                    tblCustomerTransaction objDebit = new tblCustomerTransaction()
                    {
                        ReceiptDate = transact.ReceiptDate,
                        PatientId = transact.PatientId,
                        BankId = transact.BankId,
                        TransactionId = TransactionId,
                        TransactionType = "Receipt",
                        TransactionDocNo = ReceiptNo,
                        ISCheque = false,
                        PayAmount = transact.PayAmount,
                        ChequeDate = transact.ReceiptDate,
                        ChequeNo = transact.ChequeNo,
                        BankName = transact.BankName,
                        IsDelete = false,
                        IsCash = false,
                        BillAmount = transact.BillAmount,
                    };
                    objData.tblCustomerTransactions.InsertOnSubmit(objDebit);
                }
            }
            objData.SubmitChanges();
            return 1;
        }

        public int SaveSupplierTransaction(EntityCustomerTransaction entCust, EntityCustomerTransaction transact, bool IsCash)
        {
            int TransactionId = new PatientInvoiceBLL().GetTransactionId();
            int ReceiptNo = GetReceiptNo();
            if (IsCash)
            {
                tblCustomerTransaction objDebit = new tblCustomerTransaction()
                {
                    ReceiptDate = entCust.ReceiptDate,
                    SupplierId = entCust.SupplierId,
                    TransactionId = TransactionId,
                    TransactionType = "SupplierPayment",
                    TransactionDocNo = ReceiptNo,
                    IsCash = true,
                    ISCheque = false,
                    PayAmount = entCust.PayAmount,
                    IsDelete = false,
                };
                objData.tblCustomerTransactions.InsertOnSubmit(objDebit);
            }
            else
            {
                if (transact != null)
                {
                    if (Convert.ToBoolean(transact.ISCheque))
                    {
                        tblCustomerTransaction objCredit = new tblCustomerTransaction()
                        {
                            ReceiptDate = entCust.ReceiptDate,
                            SupplierId = entCust.SupplierId,
                            TransactionId = TransactionId,
                            TransactionType = "SupplierPayment",
                            TransactionDocNo = ReceiptNo,
                            IsCash = false,
                            PayAmount = entCust.PayAmount,
                            ISCheque = true,
                            IsDelete = false,
                        };
                        objData.tblCustomerTransactions.InsertOnSubmit(objCredit);
                        tblCustomerTransaction objDebit = new tblCustomerTransaction()
                        {
                            ReceiptDate = transact.ReceiptDate,
                            PatientId = transact.PatientId,
                            BankId = transact.BankId,
                            TransactionId = TransactionId,
                            TransactionType = "SupplierPayment",
                            TransactionDocNo = ReceiptNo,
                            ISCheque = true,
                            PayAmount = transact.PayAmount,
                            ChequeDate = transact.ReceiptDate,
                            ChequeNo = transact.ChequeNo,
                            BankName = transact.BankName,
                            IsDelete = false,
                            IsCash = false,
                        };
                        objData.tblCustomerTransactions.InsertOnSubmit(objDebit);
                    }
                    else
                    {
                        tblCustomerTransaction objCredit = new tblCustomerTransaction()
                        {
                            ReceiptDate = entCust.ReceiptDate,
                            SupplierId = entCust.SupplierId,
                            TransactionId = TransactionId,
                            TransactionType = "SupplierPayment",
                            TransactionDocNo = ReceiptNo,
                            IsCash = false,
                            PayAmount = entCust.PayAmount,
                            ISCheque = false,
                            IsDelete = false,
                            ChequeNo = entCust.ChequeNo,
                            BankName = entCust.BankName,
                            BillAmount = entCust.BillAmount,
                        };
                        objData.tblCustomerTransactions.InsertOnSubmit(objCredit);
                        tblCustomerTransaction objDebit = new tblCustomerTransaction()
                        {
                            ReceiptDate = transact.ReceiptDate,
                            PatientId = 0,
                            BankId = transact.BankId,
                            TransactionId = TransactionId,
                            TransactionType = "Receipt",
                            TransactionDocNo = ReceiptNo,
                            ISCheque = false,
                            PayAmount = transact.PayAmount,
                            ChequeNo = transact.ChequeNo,
                            BankName = transact.BankName,
                            IsDelete = false,
                            IsCash = false,
                            BillAmount = transact.BillAmount,
                        };
                        objData.tblCustomerTransactions.InsertOnSubmit(objDebit);
                    }
                }
                else
                {
                    tblCustomerTransaction objCredit = new tblCustomerTransaction()
                    {
                        ReceiptDate = entCust.ReceiptDate,
                        SupplierId = entCust.SupplierId,
                        TransactionId = TransactionId,
                        TransactionType = "Receipt",
                        TransactionDocNo = ReceiptNo,
                        IsCash = false,
                        PayAmount = entCust.PayAmount,
                        ISCheque = false,
                        IsDelete = false,
                        ChequeNo = entCust.ChequeNo,
                        ChequeDate = entCust.ChequeDate,
                        BankName = entCust.BankName,
                        BillAmount = entCust.BillAmount,
                    };
                    objData.tblCustomerTransactions.InsertOnSubmit(objCredit);
                    tblCustomerTransaction objDebit = new tblCustomerTransaction()
                    {
                        ReceiptDate = transact.ReceiptDate,
                        PatientId = transact.PatientId,
                        BankId = transact.BankId,
                        TransactionId = TransactionId,
                        TransactionType = "Receipt",
                        TransactionDocNo = ReceiptNo,
                        ISCheque = false,
                        PayAmount = transact.PayAmount,
                        ChequeDate = transact.ReceiptDate,
                        ChequeNo = transact.ChequeNo,
                        BankName = transact.BankName,
                        IsDelete = false,
                        IsCash = false,
                        BillAmount = transact.BillAmount,
                    };
                    objData.tblCustomerTransactions.InsertOnSubmit(objDebit);
                }
            }
            //objData.sp_InsertReceipt(Convert.ToInt32(obj.PatientId), Convert.ToDateTime(DateTime.Now.Date), Convert.ToDecimal(obj.Amount), Convert.ToDateTime(DateTime.Now.Date), Convert.ToString(obj.ChequeNo), Convert.ToString(obj.BankName), Convert.ToDecimal(obj.PayAmount), Convert.ToDecimal(obj.Amount), Convert.ToInt32(obj.TransactionId), Convert.ToString(obj.IsCash), Convert.ToString(obj.ISCheque), ref Id);
            objData.SubmitChanges();
            return 1;
        }

        private int GetReceiptNo()
        {
            int TId = 0;
            try
            {
                CriticareHospitalDataContext objData = new CriticareHospitalDataContext();
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
        //

        public decimal GetSupplierTrans(int SupplierId)
        {
            decimal FinalAmount = 0;
            try
            {


                List<tblCustomerTransaction> lstTrans = (from tbl in objData.tblCustomerTransactions
                                                         where tbl.SupplierId == Convert.ToInt32(SupplierId)
                                                         && tbl.IsDelete == false
                                                         select tbl).ToList();

                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.BillAmount)) - Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetPatientTrans(int Pat_Id)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.PatientId == Pat_Id
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                      && tbl.TransactionType.Equals("Refund") == false
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.BillAmount)) - Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public List<tblCustomerTransaction> GetPatientTransByPatientId(int Pat_Id)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.PatientId == Pat_Id
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                      && tbl.IsDelete == false
                                      select tbl);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstTrans;
        }
        //
        public List<tblCustomerTransaction> GetPatientTransByPatientIdInfo(int Pat_Id)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.AdmitId == Pat_Id
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                      && tbl.IsDelete == false
                                      select tbl);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstTrans;
        }
        //
        public decimal GetPatientBillAmount(int Pat_Id)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                lstTrans = (from tbl in objData.tblCustomerTransactions
                            where tbl.PatientId == Pat_Id
                            select tbl).ToList();
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.BillAmount));
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
                        && tbl.PayAmount > 0
                        && tbl.TransactionType.Equals("Receipt")
                        select new EntityCustomerTransaction
                        {
                            ReceiptNo = tbl.ReceiptNo,
                            ReceiptDate = tbl.ReceiptDate,
                            PatientName = tblPat.PatientFirstName + ' ' + tblPat.PatientMiddleName + ' ' + tblPat.PatientLastName,
                            Address = tblPat.Address,
                            Amount = tbl.PayAmount
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EntityCustomerTransaction GetReceiptDetails(int ReceiptNo)
        {
            EntityCustomerTransaction transact = null;
            try
            {
                EntityCustomerTransaction objTransactId = (from tbl in objData.tblCustomerTransactions
                                                           where tbl.ReceiptNo == ReceiptNo
                                                           && tbl.IsDelete == false
                                                           select new EntityCustomerTransaction { TransactionId = tbl.TransactionId, PatientId = tbl.PatientId }).FirstOrDefault();
                if (objTransactId != null)
                {
                    transact = (from tbl in objData.tblCustomerTransactions
                                join tblBank in objData.tblBankMasters
                                on tbl.BankId equals tblBank.BankId
                                where tbl.TransactionId == objTransactId.TransactionId
                                && tbl.IsDelete == false
                                && tbl.BillAmount > 0
                                select new EntityCustomerTransaction
                                {
                                    PatientName = tblBank.BankName,
                                    ChequeDate = tbl.ChequeDate,
                                    ChequeNo = tbl.ChequeNo,
                                    BankName = tbl.BankName,
                                    ISCheque = tbl.ISCheque
                                }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return transact;
        }

        public int Update(tblCustomerTransaction entPatient, EntityCustomerTransaction transact, bool IsCash)
        {
            int i = 0;
            try
            {
                tblPatientAdmitDetail admit = (from tbl in objData.tblPatientAdmitDetails
                                               where tbl.IsDelete == false
                                               && tbl.PatientId == entPatient.PatientId
                                               orderby tbl.AdmitId descending
                                               select tbl).FirstOrDefault();

                tblCustomerTransaction cust = (from tbl in objData.tblCustomerTransactions
                                               where tbl.ReceiptNo == entPatient.ReceiptNo
                                               && tbl.IsDelete == false
                                               select tbl).FirstOrDefault();
                if (entPatient != null)
                {
                    List<tblCustomerTransaction> lst = (from tbl in objData.tblCustomerTransactions
                                                        where tbl.TransactionId == cust.TransactionId
                                                        select tbl).ToList();
                    if (IsCash)
                    {
                        if (lst.Count == 2)
                        {
                            #region Change Card Payment to Cash Payment
                            foreach (tblCustomerTransaction item in lst)
                            {
                                if (Convert.ToDecimal(item.PayAmount) > 0)
                                {
                                    item.IsCash = false;
                                    item.TransactionType = "Receipt";
                                    item.BankName = entPatient.BankName;
                                    item.ChequeDate = entPatient.ChequeDate;
                                    item.ChequeNo = entPatient.ChequeNo;
                                    item.ISCheque = entPatient.ISCheque;
                                    item.PayAmount = entPatient.PayAmount;
                                    item.BillAmount = entPatient.BillAmount;
                                    item.TransactionDocNo = cust.TransactionDocNo;
                                    item.ReceiptDate = entPatient.ReceiptDate;
                                    item.BankId = entPatient.BankId;
                                }
                                else
                                {
                                    item.IsCash = false;
                                    item.IsDelete = true;
                                    item.TransactionType = "Receipt";
                                    item.BillAmount = entPatient.BillAmount;
                                    item.BankName = entPatient.BankName;
                                    item.ChequeDate = entPatient.ChequeDate;
                                    //item.BankId = ent.BankId;
                                    item.ChequeNo = entPatient.ChequeNo;
                                    item.PatientId = admit.AdmitId;
                                    item.ISCheque = entPatient.ISCheque;
                                    item.PayAmount = entPatient.PayAmount;
                                    item.TransactionDocNo = item.TransactionDocNo;
                                    item.ReceiptDate = entPatient.ReceiptDate;
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            #region Transaction
                            if (transact != null)
                            {
                                tblCustomerTransaction objDebit = new tblCustomerTransaction()
                                {
                                    ReceiptDate = transact.ReceiptDate,
                                    PatientId = admit.AdmitId,
                                    TransactionId = cust.TransactionId,
                                    TransactionDocNo = cust.TransactionDocNo,
                                    TransactionType = "Receipt",
                                    ISCheque = false,
                                    PayAmount = transact.PayAmount,
                                    ChequeDate = transact.ReceiptDate,
                                    ChequeNo = transact.ChequeNo,
                                    BankName = transact.BankName,
                                    IsDelete = false,
                                    IsCash = false,
                                    BillAmount = transact.BillAmount,
                                    BankId = transact.BankId,
                                };
                                objData.tblCustomerTransactions.InsertOnSubmit(objDebit);
                            }
                            else
                            {
                                if (lst.Count == 1)
                                {
                                    #region Change Card Payment to Cash Payment
                                    foreach (tblCustomerTransaction item in lst)
                                    {
                                        if (Convert.ToDecimal(item.PayAmount) > 0)
                                        {
                                            item.IsCash = false;
                                            item.TransactionType = "Receipt";
                                            item.BankName = entPatient.BankName;
                                            item.ChequeDate = entPatient.ChequeDate;
                                            item.ChequeNo = entPatient.ChequeNo;
                                            item.ISCheque = entPatient.ISCheque;
                                            item.PayAmount = entPatient.PayAmount;
                                            item.BillAmount = entPatient.BillAmount;
                                            item.TransactionDocNo = cust.TransactionDocNo;
                                            item.ReceiptDate = entPatient.ReceiptDate;
                                            item.BankId = entPatient.BankId;
                                        }
                                        else
                                        {
                                            item.IsCash = false;
                                            item.IsDelete = true;
                                            item.TransactionType = "Receipt";
                                            item.BillAmount = entPatient.BillAmount;
                                            item.BankName = entPatient.BankName;
                                            item.ChequeDate = entPatient.ChequeDate;
                                            //item.BankId = ent.BankId;
                                            item.ChequeNo = entPatient.ChequeNo;
                                            item.PatientId = admit.AdmitId;
                                            item.ISCheque = entPatient.ISCheque;
                                            item.PayAmount = entPatient.PayAmount;
                                            item.TransactionDocNo = item.TransactionDocNo;
                                            item.ReceiptDate = entPatient.ReceiptDate;
                                        }
                                    }
                                    #endregion
                                }
                            }
                            #endregion
                        }
                    }
                    else
                    {
                        if (lst.Count == 2)
                        {
                            foreach (tblCustomerTransaction item in lst)
                            {
                                if (Convert.ToDecimal(item.PayAmount) > 0)
                                {
                                    item.IsCash = false;
                                    item.IsDelete = true;
                                    item.TransactionType = "Receipt";
                                    item.BankName = transact.BankName;
                                    item.ChequeDate = transact.ChequeDate;
                                    item.ChequeNo = transact.ChequeNo;
                                    item.ISCheque = transact.ISCheque;
                                    item.PayAmount = transact.PayAmount;
                                    item.BillAmount = transact.BillAmount;
                                    item.TransactionDocNo = item.TransactionDocNo;
                                    item.ReceiptDate = transact.ReceiptDate;
                                    item.BankId = transact.BankId;
                                }
                                else
                                {
                                    item.IsCash = false;
                                    item.TransactionType = "Receipt";
                                    item.BillAmount = entPatient.BillAmount;
                                    item.BankName = entPatient.BankName;
                                    item.ChequeDate = entPatient.ChequeDate;
                                    item.ChequeNo = entPatient.ChequeNo;
                                    item.ISCheque = entPatient.ISCheque;
                                    item.PayAmount = entPatient.PayAmount;
                                    item.TransactionDocNo = item.TransactionDocNo;
                                    item.PatientId = admit.AdmitId;
                                    item.ReceiptDate = entPatient.ReceiptDate;
                                    //item.BankId = transact.BankId;
                                }
                            }
                        }
                        else
                        {
                            if (transact != null)
                            {
                                tblCustomerTransaction objDebit = new tblCustomerTransaction()
                                {
                                    ReceiptDate = transact.ReceiptDate,
                                    //PatientId = admit.AdmitId,
                                    TransactionId = cust.TransactionId,
                                    TransactionDocNo = cust.TransactionDocNo,
                                    TransactionType = "Receipt",
                                    ISCheque = false,
                                    PayAmount = transact.PayAmount,
                                    ChequeDate = transact.ChequeDate,
                                    ChequeNo = transact.ChequeNo,
                                    BankName = transact.BankName,
                                    IsDelete = false,
                                    IsCash = false,
                                    BillAmount = transact.BillAmount,
                                    BankId = transact.BankId,
                                };
                                objData.tblCustomerTransactions.InsertOnSubmit(objDebit);
                                foreach (tblCustomerTransaction item in lst)
                                {
                                    if (Convert.ToDecimal(item.PayAmount) > 0)
                                    {
                                        item.PayAmount = entPatient.PayAmount;
                                        item.IsCash = false;
                                        item.ISCheque = entPatient.ISCheque;
                                    }
                                }
                            }
                            else
                            {
                                foreach (tblCustomerTransaction item in lst)
                                {
                                    if (Convert.ToDecimal(item.PayAmount) > 0)
                                    {
                                        item.IsCash = false;
                                        item.IsDelete = true;
                                        item.TransactionType = "Receipt";
                                        item.BankName = transact.BankName;
                                        item.ChequeDate = transact.ChequeDate;
                                        item.ChequeNo = transact.ChequeNo;
                                        item.ISCheque = transact.ISCheque;
                                        item.PayAmount = transact.PayAmount;
                                        item.BillAmount = transact.BillAmount;
                                        item.TransactionDocNo = item.TransactionDocNo;
                                        item.ReceiptDate = transact.ReceiptDate;
                                        item.BankId = transact.BankId;
                                    }
                                    else
                                    {
                                        item.IsCash = false;
                                        item.TransactionType = "Receipt";
                                        item.BillAmount = entPatient.BillAmount;
                                        item.BankName = entPatient.BankName;
                                        item.ChequeDate = entPatient.ChequeDate;
                                        item.ChequeNo = entPatient.ChequeNo;
                                        item.ISCheque = entPatient.ISCheque;
                                        item.PayAmount = entPatient.PayAmount;
                                        item.TransactionDocNo = item.TransactionDocNo;
                                        item.ReceiptDate = entPatient.ReceiptDate;
                                        item.BankId = entPatient.BankId;
                                    }
                                }
                            }
                        }
                    }
                }
                objData.SubmitChanges();
                i++;
            }
            catch (Exception ex)
            {
                i = 0;
                throw ex;
            }
            return i;
        }

        public int UpdateSupplierTransaction(tblCustomerTransaction entSupplier, EntityCustomerTransaction transact, bool IsCash)
        {
            int i = 0;
            try
            {
                tblCustomerTransaction cust = (from tbl in objData.tblCustomerTransactions
                                               where tbl.ReceiptNo == entSupplier.ReceiptNo
                                               && tbl.IsDelete == false
                                               select tbl).FirstOrDefault();
                if (entSupplier != null)
                {
                    List<tblCustomerTransaction> lst = (from tbl in objData.tblCustomerTransactions
                                                        where tbl.TransactionId == cust.TransactionId
                                                        select tbl).ToList();
                    if (IsCash)
                    {
                        if (lst.Count == 2)
                        {
                            #region Change Card Payment to Cash Payment
                            foreach (tblCustomerTransaction item in lst)
                            {
                                if (Convert.ToInt32(item.BankId) > 0)
                                {
                                    item.IsCash = false;
                                    item.IsDelete = true;
                                    item.TransactionType = "SupplierPayment";
                                    item.BillAmount = entSupplier.BillAmount;
                                    item.BankName = entSupplier.BankName;
                                    item.ChequeDate = entSupplier.ChequeDate;
                                    item.BankId = entSupplier.BankId;
                                    item.ChequeNo = entSupplier.ChequeNo;
                                    item.SupplierId = entSupplier.SupplierId;
                                    item.ISCheque = entSupplier.ISCheque;
                                    item.PayAmount = entSupplier.PayAmount;
                                    item.TransactionDocNo = item.TransactionDocNo;
                                    item.ReceiptDate = entSupplier.ReceiptDate;
                                }
                                else
                                {
                                    item.IsCash = false;
                                    item.TransactionType = "SupplierPayment";
                                    item.BankName = entSupplier.BankName;
                                    item.ChequeDate = entSupplier.ChequeDate;
                                    item.ChequeNo = entSupplier.ChequeNo;
                                    item.ISCheque = entSupplier.ISCheque;
                                    item.PayAmount = entSupplier.PayAmount;
                                    item.BillAmount = entSupplier.BillAmount;
                                    item.TransactionDocNo = cust.TransactionDocNo;
                                    item.ReceiptDate = entSupplier.ReceiptDate;
                                    item.SupplierId = entSupplier.SupplierId;
                                    //item.BankId = ent.BankId;
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            if (transact != null)
                            {
                                tblCustomerTransaction objDebit = new tblCustomerTransaction()
                                {
                                    ReceiptDate = transact.ReceiptDate,
                                    SupplierId = entSupplier.SupplierId,
                                    TransactionId = cust.TransactionId,
                                    TransactionDocNo = cust.TransactionDocNo,
                                    TransactionType = "SupplierPayment",
                                    ISCheque = false,
                                    PayAmount = transact.PayAmount,
                                    ChequeDate = transact.ReceiptDate,
                                    ChequeNo = transact.ChequeNo,
                                    BankName = transact.BankName,
                                    IsDelete = false,
                                    IsCash = false,
                                    BillAmount = transact.BillAmount,
                                    BankId = transact.BankId,
                                };
                                objData.tblCustomerTransactions.InsertOnSubmit(objDebit);
                            }
                        }
                    }
                    else
                    {
                        if (lst.Count == 2)
                        {
                            foreach (tblCustomerTransaction item in lst)
                            {
                                if (Convert.ToInt32(item.BankId) > 0)
                                {
                                    item.IsCash = false;
                                    item.IsDelete = true;
                                    item.TransactionType = "SupplierPayment";
                                    item.BankName = transact.BankName;
                                    item.ChequeDate = transact.ChequeDate;
                                    item.ChequeNo = transact.ChequeNo;
                                    item.ISCheque = transact.ISCheque;
                                    item.PayAmount = transact.PayAmount;
                                    item.BillAmount = transact.BillAmount;
                                    item.TransactionDocNo = item.TransactionDocNo;
                                    item.ReceiptDate = transact.ReceiptDate;
                                    item.BankId = transact.BankId;
                                }
                                else
                                {
                                    item.IsCash = false;
                                    item.TransactionType = "SupplierPayment";
                                    item.BillAmount = entSupplier.BillAmount;
                                    item.BankName = entSupplier.BankName;
                                    item.ChequeDate = entSupplier.ChequeDate;
                                    item.ChequeNo = entSupplier.ChequeNo;
                                    item.ISCheque = entSupplier.ISCheque;
                                    item.PayAmount = entSupplier.PayAmount;
                                    item.TransactionDocNo = item.TransactionDocNo;
                                    item.SupplierId = entSupplier.SupplierId;
                                    item.ReceiptDate = entSupplier.ReceiptDate;
                                    //item.BankId = transact.BankId;
                                }
                            }
                        }
                        else
                        {
                            if (transact != null)
                            {
                                tblCustomerTransaction objDebit = new tblCustomerTransaction()
                                {
                                    ReceiptDate = transact.ReceiptDate,
                                    //PatientId = admit.AdmitId,
                                    TransactionId = cust.TransactionId,
                                    TransactionDocNo = cust.TransactionDocNo,
                                    TransactionType = "SupplierPayment",
                                    ISCheque = false,
                                    PayAmount = transact.PayAmount,
                                    ChequeDate = transact.ChequeDate,
                                    ChequeNo = transact.ChequeNo,
                                    BankName = transact.BankName,
                                    IsDelete = false,
                                    IsCash = false,
                                    BillAmount = transact.BillAmount,
                                    BankId = transact.BankId,
                                };
                                objData.tblCustomerTransactions.InsertOnSubmit(objDebit);
                                foreach (tblCustomerTransaction item in lst)
                                {
                                    if (Convert.ToDecimal(item.PayAmount) > 0)
                                    {
                                        item.SupplierId = entSupplier.SupplierId;
                                        item.PayAmount = entSupplier.PayAmount;
                                        item.BillAmount = entSupplier.BillAmount;
                                        item.IsCash = false;
                                        item.ISCheque = entSupplier.ISCheque;
                                    }
                                }
                            }
                            else
                            {
                                foreach (tblCustomerTransaction item in lst)
                                {
                                    if (Convert.ToInt32(item.BankId) > 0)
                                    {
                                        item.IsCash = false;
                                        item.IsDelete = true;
                                        item.TransactionType = "SupplierPayment";
                                        item.BankName = transact.BankName;
                                        item.ChequeDate = transact.ChequeDate;
                                        item.ChequeNo = transact.ChequeNo;
                                        item.ISCheque = transact.ISCheque;
                                        item.PayAmount = transact.PayAmount;
                                        item.BillAmount = transact.BillAmount;
                                        item.TransactionDocNo = item.TransactionDocNo;
                                        item.ReceiptDate = transact.ReceiptDate;
                                        item.BankId = transact.BankId;
                                    }
                                    else
                                    {
                                        item.IsCash = false;
                                        item.SupplierId = entSupplier.SupplierId;
                                        item.TransactionType = "SupplierPayment";
                                        item.BillAmount = entSupplier.BillAmount;
                                        item.BankName = entSupplier.BankName;
                                        item.ChequeDate = entSupplier.ChequeDate;
                                        item.ChequeNo = entSupplier.ChequeNo;
                                        item.ISCheque = entSupplier.ISCheque;
                                        item.PayAmount = entSupplier.PayAmount;
                                        item.TransactionDocNo = item.TransactionDocNo;
                                        item.ReceiptDate = entSupplier.ReceiptDate;
                                        item.BankId = entSupplier.BankId;
                                    }
                                }
                            }
                        }
                    }
                }
                objData.SubmitChanges();
                i++;
            }
            catch (Exception ex)
            {
                i = 0;
                throw ex;
            }
            return i;
        }

        public List<EntityCustomerTransaction> GetCustomerTransactionList(string prefix)
        {
            List<EntityCustomerTransaction> lst = null;
            try
            {
                lst = (from tbl in GetCustomerTransactionList()
                       where tbl.PatientName.Contains(prefix)
                       || tbl.Address.Contains(prefix)
                       select tbl).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }

        public List<EntityCustomerTransaction> GetSupplierTransactionList()
        {
            return (from tbl in objData.STP_Select_SupplierTranactions()
                    select new EntityCustomerTransaction
                    {
                        ReceiptNo = tbl.ReceiptNo,
                        ReceiptDate = tbl.ReceiptDate,
                        SupplierName = tbl.SupplierName,
                        Address = tbl.Address,
                        Amount = tbl.PayAmount
                    }).ToList();
        }

        public List<EntityCustomerTransaction> GetSupplierTransactionList(string Prefix)
        {
            return (from tbl in GetSupplierTransactionList()
                    where tbl.SupplierName.ToUpper().Contains(Prefix.ToUpper())
                    || tbl.Address.ToUpper().Contains(Prefix.ToUpper())
                    select tbl).ToList();
        }

        public List<EntitySupplierMaster> GetAllocatedSupplier()
        {
            List<EntitySupplierMaster> lst = null;
            try
            {
                lst = (from tbl in objData.tblSupplierMasters
                       where tbl.IsDelete == false
                       select new EntitySupplierMaster
                       {
                           PKId = tbl.PKId,
                           SupplierName = tbl.SupplierName
                       }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }

        public List<tblCustomerTransaction> GetSupplierTransBySupplierId(int SupplierId)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            try
            {
                List<tblSupplierMaster> lstAdmit = (from tbl in objData.tblSupplierMasters
                                                    where tbl.IsDelete == false
                                                    && tbl.PKId == SupplierId
                                                    select tbl).ToList();
                foreach (tblSupplierMaster item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.SupplierId == item.PKId
                                      && tbl.IsDelete == false
                                      select tbl);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstTrans;
        }



        public List<EntityProduct> GetAllocatedProduct()
        {
            List<EntityProduct> lst = null;
            try
            {
                lst = (from tbl in objData.tblProductMasters
                       where tbl.IsDelete == false
                       select new EntityProduct
                       {
                           ProductId = tbl.ProductId,
                           ProductName = tbl.ProductName
                       }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }

        public List<tblStockDetail> GetProductTransByProductId(int ProductId)
        {
            List<tblStockDetail> lstTrans = new List<tblStockDetail>();
            try
            {
                List<tblProductMaster> lstAdmit = (from tbl in objData.tblProductMasters
                                                   where tbl.IsDelete == false
                                                   && tbl.ProductId == ProductId
                                                   select tbl).ToList();
                foreach (tblProductMaster item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblStockDetails
                                      where tbl.ProductId == item.ProductId
                                      && tbl.IsDelete == false
                                      select tbl);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstTrans;
        }

        public EntityProduct GetUnitOfMeasurement(string ProductId)
        {
            EntityProduct lst = null;
            try
            {
                lst = (from tbl in objData.tblProductMasters
                       where tbl.IsDelete == false
                       && tbl.ProductId.Equals(ProductId)
                       select new EntityProduct
                       {
                           ProductId = tbl.ProductId,
                           ProductName = tbl.ProductName,
                           UOM = tbl.UOM
                       }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }

        public List<tblCustomerTransaction> GetPatientTransByPatientAdmitId(int AdmitId)
        {
            List<tblCustomerTransaction> lst = null;
            tblPatientAdmitDetail admit = (from tbl in objData.tblPatientAdmitDetails
                                           where tbl.AdmitId == AdmitId
                                           select tbl).FirstOrDefault();
            if (admit != null)
            {
                lst = GetPatientTransByPatientId(Convert.ToInt32(admit.PatientId));
            }
            return lst;
        }
    }
}