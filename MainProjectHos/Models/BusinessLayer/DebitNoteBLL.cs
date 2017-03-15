using MainProjectHos.Models.DataModels;
using MainProjectHos.Models.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.BusinessLayer
{
    public class DebitNoteBLL
    {
        public DebitNoteBLL()
        {
            objData = new CriticareHospitalDataContext();
        }

        public CriticareHospitalDataContext objData { get; set; }


        public List<EntityDebitNote> SelectDebitNoteForEdit(int DebitNoteNo)
        {
            List<EntityDebitNote> lst = null;
            try
            {
                lst = (from tbl in objData.tblDebitNotes
                       join tblSupplier in objData.tblSupplierMasters
                       on tbl.SupplierId equals tblSupplier.PKId
                       where tbl.IsDelete == false
                       && tbl.DNNo == DebitNoteNo
                       select new EntityDebitNote
                       {
                           Amount = tbl.Amount,
                           NetAmount = tbl.NetAmount,
                           Address = tblSupplier.Address,
                           Discount = tbl.Discount,
                           DNDate = tbl.DNDate,
                           SupplierId = tbl.SupplierId,
                           SupplierName = tblSupplier.SupplierName,
                           Tax1 = tbl.Tax1,
                           Tax2 = tbl.Tax2

                       }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }

        public List<EntityDebitNote> GetDebitNotes()
        {
            List<EntityDebitNote> lst = new List<EntityDebitNote>();
            try
            {
                lst = (from tbl in objData.STP_Select_DebitNote()
                       select new EntityDebitNote
                       {
                           DNNo = tbl.DNNo,
                           DNDate = tbl.DNDate,
                           SupplierName = tbl.SupplierName,
                           NetAmount = tbl.NetAmount,
                           Amount = tbl.Amount,
                           Address = tbl.Address
                       }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }


        #region GetSupplierByPO(int PONo)
        public EntitySupplierMaster GetSupplierByPO(int PONo)
        {
            EntitySupplierMaster obj = new EntitySupplierMaster();
            try
            {
                obj = (from tbl in objData.tblSupplierMasters
                       join tblPO in objData.tblPurchaseOrders
                       on tbl.PKId equals tblPO.VendorId
                       where tblPO.IsDelete == false
                       && tblPO.PO_Id == PONo
                       select new EntitySupplierMaster { PKId = tbl.PKId, SupplierName = tbl.SupplierName }
                     ).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }
        #endregion

        #region GetItemDetails
        public EntityPurchaseOrderDetails GetItemDetails(int PONo, int ProductId)
        {
            EntityPurchaseOrderDetails obj = new EntityPurchaseOrderDetails();
            try
            {
                obj = (from tbl in objData.tblProductMasters
                       join tblPO in objData.tblPurchaseOrderDetails
                       on tbl.ProductId equals tblPO.Product_Id
                       where tblPO.IsDelete == false
                       && tblPO.PO_Id == PONo
                       && tblPO.Product_Id == ProductId
                       select new EntityPurchaseOrderDetails { Product_Id = tbl.ProductId, ProductName = tbl.ProductName, Quantity = tblPO.Quantity, Rate = tblPO.Rate / tblPO.Quantity }
                     ).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }
        #endregion

        #region Insert Purchase Invoice
        public int InsertPurchaseInvoice(EntityDebitNote entPurchaseInvoice, List<EntityDebitNoteDetails> lstInvoice)
        {
            try
            {
                int? id = 0;
                objData.STP_Insert_DebitNote(Convert.ToDateTime(entPurchaseInvoice.DNDate), Convert.ToInt32(entPurchaseInvoice.SupplierId), false, Convert.ToDecimal(entPurchaseInvoice.Amount), Convert.ToDecimal(entPurchaseInvoice.NetAmount), Convert.ToInt32(entPurchaseInvoice.Tax1), Convert.ToInt32(entPurchaseInvoice.Tax2), Convert.ToInt32(entPurchaseInvoice.Discount), ref id);
                foreach (EntityDebitNoteDetails item in lstInvoice)
                {
                    tblDebitNoteDetail obj = new tblDebitNoteDetail()
                    {
                        DNNo = id,
                        ProductCode = item.ProductCode,
                        Quantity = item.Quantity,
                        Price = item.Price,
                        Amount = item.Amount,
                        BatchNo = item.BatchNo,
                        ExpiryDate = item.ExpiryDate,
                        IsDelete = false
                    };
                    tblStockDetail objStock = new tblStockDetail()
                    {
                        OpeningQty = 0,
                        InwardQty = 0,
                        InwardPrice = 0,
                        InwardAmount = 0,
                        BatchNo = item.BatchNo,
                        ExpiryDate = item.ExpiryDate,
                        OutwardQty = item.Quantity,
                        OutwardPrice = item.Price,
                        DocumentNo = id,
                        IsDelete = false,
                        ProductId = Convert.ToInt32(item.ProductCode),
                        TransactionType = "DebitNote",
                        OutwardAmount = item.Amount
                    };
                    objData.tblStockDetails.InsertOnSubmit(objStock);
                    objData.tblDebitNoteDetails.InsertOnSubmit(obj);
                }
                int TId = new PatientInvoiceBLL().GetTransactionId();

                tblCustomerTransaction objCust = new tblCustomerTransaction()
                {
                    PayAmount = entPurchaseInvoice.NetAmount,
                    IsCash = false,
                    IsDelete = false,
                    SupplierId = entPurchaseInvoice.SupplierId,
                    ReceiptDate = entPurchaseInvoice.DNDate,
                    TransactionDocNo = id,
                    TransactionId = TId,
                    TransactionType = "DebitNote"
                };
                objData.tblCustomerTransactions.InsertOnSubmit(objCust);
                objData.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return 1;
        }
        #endregion

        #region Get Purchase Invoice Details
        public List<EntityDebitNoteDetails> GetDebitNoteDetails(int DebitNoteNo)
        {
            List<EntityDebitNoteDetails> lst = null;
            try
            {
                lst = (from tbl in objData.tblDebitNoteDetails
                       join tblProduct in objData.tblProductMasters
                       on tbl.ProductCode equals tblProduct.ProductId
                       where tbl.IsDelete == false
                       && tbl.DNNo == DebitNoteNo
                       select new EntityDebitNoteDetails
                       {
                           DebitNoteNo = tbl.DNNo,
                           Amount = tbl.Amount,
                           BatchNo = tbl.BatchNo,
                           ExpiryDate = tbl.ExpiryDate,
                           Price = tbl.Price,
                           Quantity = tbl.Quantity,
                           DNSrNo = tbl.DNSrNo,
                           ProductCode = tbl.ProductCode,
                           ProductName = tblProduct.ProductName,
                           IsDelete = tbl.IsDelete,
                       }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }
        #endregion

        public int Update(EntityDebitNote lstEdited, List<EntityDebitNoteDetails> lst)
        {
            int i = 0;
            try
            {
                tblDebitNote objTest = (from tbl in objData.tblDebitNotes
                                        where tbl.IsDelete == false
                                        && tbl.DNNo == lstEdited.DNNo
                                        select tbl).FirstOrDefault();

                if (objTest != null)
                {
                    objTest.Amount = lstEdited.Amount;
                    objTest.NetAmount = lstEdited.NetAmount;
                    objTest.SupplierId = lstEdited.SupplierId;
                    objTest.DNDate = lstEdited.DNDate;
                    objTest.Discount = lstEdited.Discount;
                    objTest.Tax1 = lstEdited.Tax1;
                    objTest.Tax2 = lstEdited.Tax2;
                }
                foreach (EntityDebitNoteDetails item in lst)
                {
                    if (item.DNSrNo == 0)
                    {
                        tblDebitNoteDetail objNewAdded = new tblDebitNoteDetail()
                        {
                            DNNo = lstEdited.DNNo,
                            ProductCode = item.ProductCode,
                            Quantity = item.Quantity,
                            Price = item.Price,
                            Amount = item.Amount,
                            BatchNo = item.BatchNo,
                            ExpiryDate = item.ExpiryDate,
                            IsDelete = false
                        };
                        objData.tblDebitNoteDetails.InsertOnSubmit(objNewAdded);
                        tblStockDetail stock = new tblStockDetail()
                        {
                            ProductId = Convert.ToInt32(item.ProductCode),
                            OpeningQty = 0,
                            InwardQty = 0,
                            InwardPrice = 0,
                            InwardAmount = 0,
                            BatchNo = item.BatchNo,
                            ExpiryDate = item.ExpiryDate,
                            OutwardQty = item.Quantity,
                            OutwardPrice = item.Price,
                            OutwardAmount = item.Amount,
                            TransactionType = "DebitNote",
                            IsDelete = false,
                        };
                        objData.tblStockDetails.InsertOnSubmit(stock);
                    }
                    else
                    {
                        tblDebitNoteDetail cnt = (from tbl in objData.tblDebitNoteDetails
                                                  where tbl.IsDelete == false
                                                  && tbl.DNSrNo == item.DNSrNo
                                                  select tbl).FirstOrDefault();
                        if (cnt != null)
                        {
                            cnt.BatchNo = item.BatchNo;
                            cnt.ExpiryDate = item.ExpiryDate;
                            cnt.ProductCode = Convert.ToInt32(item.ProductCode);
                            cnt.Quantity = item.Quantity;
                            cnt.Price = item.Price;
                            cnt.Amount = Convert.ToDecimal(item.Amount);
                            cnt.IsDelete = item.IsDelete;
                        }
                        tblStockDetail stock = (from tbl in objData.tblStockDetails
                                                where tbl.IsDelete == false
                                                && tbl.DocumentNo == lstEdited.DNNo
                                                && tbl.ProductId == item.ProductCode
                                                && tbl.TransactionType.Equals("DebitNote")
                                                select tbl).FirstOrDefault();
                        if (stock != null)
                        {
                            stock.ProductId = Convert.ToInt32(item.ProductCode);
                            stock.OpeningQty = 0;
                            stock.InwardQty = 0;
                            stock.InwardPrice = 0;
                            stock.InwardAmount = 0;
                            stock.BatchNo = item.BatchNo;
                            stock.ExpiryDate = item.ExpiryDate;
                            stock.OutwardQty = item.Quantity;
                            stock.OutwardPrice = item.Price;
                            stock.OutwardAmount = item.Amount;
                            stock.IsDelete = Convert.ToBoolean(item.IsDelete);
                        }
                    }
                }

                #region Update Status of Order and Transaction
                tblCustomerTransaction transact = (from tbl in objData.tblCustomerTransactions
                                                   where tbl.SupplierId == lstEdited.SupplierId
                                                   && tbl.IsDelete == false
                                                   && tbl.TransactionDocNo == lstEdited.DNNo
                                                   && tbl.TransactionType.Equals("DebitNote")
                                                   select tbl).FirstOrDefault();
                if (transact != null)
                {
                    transact.PayAmount = lstEdited.NetAmount;
                    transact.ReceiptDate = lstEdited.DNDate;
                }



                objData.SubmitChanges();
                #endregion

                i++;
            }
            catch (Exception ex)
            {
                i = 0;
                throw ex;
            }
            return i;
        }

        public List<EntityDebitNote> GetDebitNotes(string Prefix)
        {
            List<EntityDebitNote> lst = null;
            try
            {
                lst = (from tbl in GetDebitNotes()
                       where tbl.SupplierName.ToUpper().Contains(Prefix.ToUpper())
                       || tbl.Address.ToUpper().Contains(Prefix.ToUpper())
                       select tbl).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }


        public List<EntityDebitNoteDetails> GetBatchNoandExpiryDatesByProduct(int ProductId)
        {
            List<EntityDebitNoteDetails> lst = null;
            try
            {
                lst = (from tbl in objData.tblStockDetails
                       where tbl.ProductId.Equals(ProductId)
                       && tbl.IsDelete == false
                       && tbl.TransactionType.Equals("PInvoice")
                       select new EntityDebitNoteDetails { ProductId = tbl.ProductId, BatchNo = tbl.BatchNo, ExpiryDate = tbl.ExpiryDate }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }

        public int? debitNoteNo
        {
            get;
            set;
        }
    }
}