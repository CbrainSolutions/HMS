using MainProjectHos.Models.DataModels;
using MainProjectHos.Models.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.BusinessLayer
{
    public class ClaimApprovalBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public ClaimApprovalBLL()
        {
            objData = new CriticareHospitalDataContext();
        }
        public CriticareHospitalDataContext objData { get; set; }

        public List<EntityBankMaster> GetBankName()
        {
            List<EntityBankMaster> lst = null;
            try
            {
                lst = (from tbl in objData.tblBankMasters
                       where tbl.IsDelete == false
                       select new EntityBankMaster
                       {
                           BankId = tbl.BankId,
                           BankName = tbl.BankName
                       }).ToList();
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertData(tblInsuranceClaim tblins, EntityCustomerTransaction objPatient, EntityCustomerTransaction objBank, bool IsCash, bool IsNeft, bool IsCheque)
        {
            try
            {
                tblInsuranceClaim objcurrent = (from tbl in objData.tblInsuranceClaims
                                                where tbl.ClaimId == tblins.ClaimId
                                                select tbl).FirstOrDefault();
                if (objcurrent != null)
                {
                    objcurrent.ApprovedAmount = tblins.ApprovedAmount;
                    objcurrent.ApprovedDate = tblins.ApprovedDate;
                    objcurrent.IsApproved = tblins.IsApproved;
                    objcurrent.TDS = tblins.TDS;
                    //objcurrent.Discount = tblins.Discount;
                    int TransactionId = new PatientInvoiceBLL().GetTransactionId();
                    if (IsCash)
                    {

                        tblCustomerTransaction tblcust = new tblCustomerTransaction()
                        {
                            TransactionId = TransactionId,
                            IsCash = true,
                            ISCheque = false,
                            TransactionDocNo = objPatient.TransactionDocNo,
                            TransactionType = "Claim",
                            BillAmount = 0,
                            PayAmount = objPatient.PayAmount,
                            PatientId = objPatient.PatientId,
                            IsDelete = false,
                            ReceiptDate = objPatient.ReceiptDate,
                        };
                        objData.tblCustomerTransactions.InsertOnSubmit(tblcust);
                    }
                    else if (IsNeft)
                    {
                        tblCustomerTransaction tblcust = new tblCustomerTransaction()
                        {

                            TransactionId = TransactionId,
                            IsCash = false,
                            ISCheque = false,
                            TransactionDocNo = objPatient.TransactionDocNo,
                            TransactionType = "Claim",
                            BillAmount = 0,
                            PayAmount = objPatient.PayAmount,
                            PatientId = objPatient.PatientId,
                            IsDelete = false,
                            ReceiptDate = objPatient.ReceiptDate,
                        };
                        objData.tblCustomerTransactions.InsertOnSubmit(tblcust);
                        tblCustomerTransaction tblcustBank = new tblCustomerTransaction()
                        {
                            TransactionId = TransactionId,
                            IsCash = false,
                            ISCheque = false,
                            TransactionDocNo = objBank.TransactionDocNo,
                            TransactionType = "Claim",
                            BillAmount = objBank.BillAmount,
                            PayAmount = 0,
                            PatientId = 0,
                            IsDelete = false,
                            ReceiptDate = objBank.ReceiptDate,
                            BankId = objBank.BankId

                        };
                        objData.tblCustomerTransactions.InsertOnSubmit(tblcustBank);
                    }
                    else if (IsCheque)
                    {
                        tblCustomerTransaction tblcust = new tblCustomerTransaction()
                        {

                            TransactionId = TransactionId,
                            IsCash = false,
                            ISCheque = true,
                            TransactionDocNo = objPatient.TransactionDocNo,
                            TransactionType = "Claim",
                            BillAmount = 0,
                            PayAmount = objPatient.PayAmount,
                            PatientId = objPatient.PatientId,
                            IsDelete = false,
                            ReceiptDate = objPatient.ReceiptDate,
                        };
                        objData.tblCustomerTransactions.InsertOnSubmit(tblcust);
                        tblCustomerTransaction tblcustnew = new tblCustomerTransaction()
                        {
                            TransactionId = TransactionId,
                            IsCash = false,
                            ISCheque = true,
                            TransactionDocNo = objBank.TransactionDocNo,
                            TransactionType = "Claim",
                            BillAmount = objBank.BillAmount,
                            PayAmount = 0,
                            PatientId = 0,
                            IsDelete = false,
                            ReceiptDate = objBank.ReceiptDate,
                            BankName = objBank.BankName,
                            ChequeDate = objBank.ChequeDate,
                            ChequeNo = objBank.ChequeNo
                        };
                        objData.tblCustomerTransactions.InsertOnSubmit(tblcustnew);
                    }
                }

                objData.SubmitChanges();
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}