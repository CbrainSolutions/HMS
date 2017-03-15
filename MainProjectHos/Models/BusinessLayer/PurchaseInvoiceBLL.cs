using MainProjectHos.Models.DataModels;
using MainProjectHos.Models.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.BusinessLayer
{
    public class PurchaseInvoiceBLL
    {
        public PurchaseInvoiceBLL()
        {
            objData = new CriticareHospitalDataContext();
        }

        public CriticareHospitalDataContext objData { get; set; }


        public List<EntityPurchaseInvoice> SelectPurchaseInvoiceForEdit(int PurchaseInvoiceNo)
        {
            List<EntityPurchaseInvoice> lst = null;
            try
            {
                lst = (from tbl in objData.tblPurchaseInvoices
                       join tblSupplier in objData.tblSupplierMasters
                       on tbl.SupplierId equals tblSupplier.PKId
                       where tbl.IsDelete == false
                       && tbl.PINo == PurchaseInvoiceNo
                       select new EntityPurchaseInvoice
                       {
                           Amount = tbl.Amount,
                           Address = tblSupplier.Address,
                           Discount = tbl.Discount,
                           PIDate = tbl.PIDate,
                           PONo = tbl.PONo,
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

        public List<EntityPurchaseInvoice> GetPurchaseInvoices()
        {
            List<EntityPurchaseInvoice> lst = new List<EntityPurchaseInvoice>();
            try
            {
                lst = (from tbl in objData.STP_Select_PurchaseInvoice()
                       select new EntityPurchaseInvoice
                       {
                           PINo = tbl.PINo,
                           PIDate = tbl.PIDate,
                           PONo = tbl.PONo,
                           SupplierName = tbl.SupplierName,
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

        #region GetPOProduct(int PONo)
        public List<EntityProduct> GetPOProduct(int PONo)
        {
            List<EntityProduct> lst = new List<EntityProduct>();
            try
            {
                lst = (from tbl in objData.STP_Select_POProduct(PONo)
                       select new EntityProduct
                       {
                           ProductId = Convert.ToInt32(tbl.Product_Id),
                           ProductName = tbl.ProductName,
                       }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }
        #endregion

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
                       select new EntityPurchaseOrderDetails { Product_Id = tbl.ProductId, ProductName = tbl.ProductName, Quantity = tblPO.Quantity, Rate = tblPO.Rate }
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
        public int InsertPurchaseInvoice(EntityPurchaseInvoice entPurchaseInvoice, List<EntityPurchaseInvoiceDetails> lstInvoice)
        {
            try
            {
                int? id = 0;
                bool flagISPOComplete = false;
                objData.STP_Insert_PurchaseInvoice(Convert.ToDateTime(entPurchaseInvoice.PIDate), Convert.ToInt32(entPurchaseInvoice.PONo), Convert.ToInt32(entPurchaseInvoice.SupplierId), false, Convert.ToDecimal(entPurchaseInvoice.Amount), Convert.ToInt32(entPurchaseInvoice.Tax1), Convert.ToInt32(entPurchaseInvoice.Tax2), Convert.ToInt32(entPurchaseInvoice.Discount), ref id);
                foreach (EntityPurchaseInvoiceDetails item in lstInvoice)
                {
                    tblPurchaseInvoiceDetail obj = new tblPurchaseInvoiceDetail()
                    {
                        PINo = id,
                        ProductCode = item.ProductCode,
                        InvoiceQty = item.InvoiceQty,
                        InvoicePrice = item.InvoicePrice,
                        Amount = item.Amount,
                        BatchNo = item.BatchNo,
                        ExpiryDate = item.ExpiryDate,
                        IsDelete = false
                    };
                    tblPurchaseOrderDetail orderItem = (from tbl in objData.tblPurchaseOrderDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.PO_Id == entPurchaseInvoice.PONo
                                                        && tbl.Product_Id == item.ProductCode
                                                        select tbl).FirstOrDefault();
                    if (orderItem != null)
                    {
                        if (orderItem.Quantity == item.InvoiceQty)
                        {
                            flagISPOComplete = true;
                        }
                        else
                        {
                            flagISPOComplete = false;
                        }
                        orderItem.InvoiceQuantity = item.InvoiceQty;
                    }
                    tblStockDetail objStock = new tblStockDetail()
                    {
                        BatchNo = item.BatchNo,
                        ExpiryDate = item.ExpiryDate,
                        InwardPrice = item.InvoicePrice,
                        InwardQty = item.InvoiceQty,
                        DocumentNo = id,
                        IsDelete = false,
                        ProductId = Convert.ToInt32(item.ProductCode),
                        TransactionType = "PInvoice",
                        InwardAmount = item.Amount
                    };
                    objData.tblStockDetails.InsertOnSubmit(objStock);
                    objData.tblPurchaseInvoiceDetails.InsertOnSubmit(obj);
                }
                int TId = new PatientInvoiceBLL().GetTransactionId();
                tblPurchaseOrder po = (from tbl in objData.tblPurchaseOrders
                                       where tbl.IsDelete == false
                                       && tbl.PO_Id == entPurchaseInvoice.PONo
                                       select tbl).FirstOrDefault();

                if (po != null)
                {
                    List<EntityProduct> lstProducts = GetPOProduct(Convert.ToInt32(entPurchaseInvoice.PONo));
                    if (lstProducts.Count == lstInvoice.Count)
                    {
                        flagISPOComplete = true;
                    }
                    else
                    {
                        flagISPOComplete = false;
                    }
                    po.IsPOComplete = flagISPOComplete;
                }
                tblCustomerTransaction objCust = new tblCustomerTransaction()
                {
                    BillAmount = entPurchaseInvoice.Amount,
                    IsCash = false,
                    IsDelete = false,
                    SupplierId = entPurchaseInvoice.SupplierId,
                    ReceiptDate = entPurchaseInvoice.PIDate,
                    TransactionDocNo = id,
                    TransactionId = TId,
                    TransactionType = "PInvoice"
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
        public List<EntityPurchaseInvoiceDetails> GetPurchaseInvoiceDetails(int purchaseInvoiceNo)
        {
            List<EntityPurchaseInvoiceDetails> lst = null;
            try
            {
                lst = (from tbl in objData.tblPurchaseInvoiceDetails
                       join tblProduct in objData.tblProductMasters
                       on tbl.ProductCode equals tblProduct.ProductId
                       where tbl.IsDelete == false
                       && tbl.PINo == purchaseInvoiceNo
                       select new EntityPurchaseInvoiceDetails
                       {
                           PurchaseInvoiceNo = purchaseInvoiceNo,
                           Amount = tbl.Amount,
                           BatchNo = tbl.BatchNo,
                           ExpiryDate = tbl.ExpiryDate,
                           InvoicePrice = tbl.InvoicePrice,
                           InvoiceQty = tbl.InvoiceQty,
                           PINoSrNo = tbl.PINoSrNo,
                           ProductCode = tbl.ProductCode,
                           ProductName = tblProduct.ProductName,
                       }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }
        #endregion

        #region GetPO Quantity
        public EntityPurchaseOrderDetails GetPOQty(int PINo, int ProductId)
        {
            EntityPurchaseOrderDetails order = null;
            try
            {
                order = (from tbl in objData.tblPurchaseOrderDetails
                         join tblPI in objData.tblPurchaseInvoices
                         on tbl.PO_Id equals tblPI.PONo
                         where tblPI.PINo == PINo
                         && tbl.Product_Id == ProductId
                         && tblPI.IsDelete == false
                         select new EntityPurchaseOrderDetails { Quantity = tbl.Quantity, InvoiceQuantity = Convert.ToInt32(tbl.InvoiceQuantity) }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return order;
        }
        #endregion

        public int Update(EntityPurchaseInvoice invoice, List<EntityPurchaseInvoiceDetails> lst)
        {
            int i = 0;
            try
            {
                tblPurchaseInvoice objExistInvoice = (from tbl in objData.tblPurchaseInvoices
                                                      where tbl.IsDelete == false
                                                      && tbl.PINo == invoice.PINo
                                                      select tbl).FirstOrDefault();
                if (objExistInvoice != null)
                {
                    bool flagISPOComplete = false;
                    if (invoice.PONo > 0)
                    {
                        if (objExistInvoice.PONo > 0)
                        {
                            List<tblPurchaseInvoiceDetail> lstExistedItems = (from tbl in objData.tblPurchaseInvoiceDetails
                                                                              where tbl.PINo == invoice.PINo
                                                                              && tbl.IsDelete == false
                                                                              select tbl).ToList();
                            if (lstExistedItems.Count > lst.Count)
                            {
                                foreach (tblPurchaseInvoiceDetail item in lstExistedItems)
                                {
                                    int cnt = (from tbl in lst
                                               where tbl.ProductCode == item.ProductCode
                                               && tbl.PurchaseInvoiceNo == item.PINo
                                               select tbl).Count();
                                    if (cnt == 0)
                                    {
                                        item.IsDelete = true;
                                    }

                                    tblPurchaseOrderDetail objOrder = (from tbl in objData.tblPurchaseOrderDetails
                                                                       join tblPurchaseInv in objData.tblPurchaseInvoices
                                                                       on tbl.PO_Id equals tblPurchaseInv.PONo
                                                                       where tblPurchaseInv.PINo == item.PINo
                                                                       && tbl.IsDelete == false
                                                                       && tbl.Product_Id == item.ProductCode
                                                                       select tbl).FirstOrDefault();
                                    if (objOrder != null)
                                    {
                                        objOrder.InvoiceQuantity = 0;
                                    }
                                }
                                objData.SubmitChanges();
                            }


                            #region Update Invoice Details
                            foreach (EntityPurchaseInvoiceDetails item in lst)
                            {

                                tblPurchaseInvoiceDetail invoiceItem = (from tbl in objData.tblPurchaseInvoiceDetails
                                                                        where tbl.PINo == invoice.PINo
                                                                        && tbl.IsDelete == false
                                                                        && tbl.ProductCode == item.ProductCode
                                                                        select tbl).FirstOrDefault();
                                if (invoiceItem != null)
                                {
                                    invoiceItem.Amount = item.Amount;
                                    invoiceItem.BatchNo = item.BatchNo;
                                    invoiceItem.ExpiryDate = item.ExpiryDate;
                                    invoiceItem.InvoicePrice = item.InvoicePrice;
                                    invoiceItem.InvoiceQty = item.InvoiceQty;
                                    invoiceItem.PINo = item.PurchaseInvoiceNo;
                                }
                                else
                                {
                                    invoiceItem = new tblPurchaseInvoiceDetail()
                                    {
                                        PINo = invoice.PINo,
                                        ProductCode = item.ProductCode,
                                        InvoiceQty = item.InvoiceQty,
                                        InvoicePrice = item.InvoicePrice,
                                        Amount = item.Amount,
                                        BatchNo = item.BatchNo,
                                        ExpiryDate = item.ExpiryDate,
                                        IsDelete = false
                                    };
                                    objData.tblPurchaseInvoiceDetails.InsertOnSubmit(invoiceItem);
                                }
                                tblPurchaseOrderDetail orderItem = (from tbl in objData.tblPurchaseOrderDetails
                                                                    where tbl.IsDelete == false
                                                                    && tbl.PO_Id == invoice.PONo
                                                                    && tbl.Product_Id == item.ProductCode
                                                                    select tbl).FirstOrDefault();
                                if (orderItem != null)
                                {
                                    if (orderItem.Quantity == item.InvoiceQty)
                                    {
                                        flagISPOComplete = true;
                                    }
                                    else
                                    {
                                        flagISPOComplete = false;
                                    }
                                    orderItem.InvoiceQuantity = item.InvoiceQty;
                                }

                                tblStockDetail stockItem = (from tbl in objData.tblStockDetails
                                                            where tbl.IsDelete == false
                                                            && tbl.TransactionType.Equals("PInvoice")
                                                            && Convert.ToInt32(tbl.DocumentNo) == invoice.PINo
                                                            && tbl.ProductId == Convert.ToInt32(item.ProductCode)
                                                            select tbl).FirstOrDefault();
                                if (stockItem != null)
                                {
                                    stockItem.ProductId = Convert.ToInt32(item.ProductCode);
                                    stockItem.InwardQty = item.InvoiceQty;
                                    stockItem.InwardPrice = item.InvoicePrice;
                                    stockItem.BatchNo = item.BatchNo;
                                    stockItem.ExpiryDate = item.ExpiryDate;
                                    stockItem.InwardAmount = item.Amount;
                                }
                                else
                                {
                                    stockItem = new tblStockDetail()
                                    {
                                        BatchNo = item.BatchNo,
                                        ExpiryDate = item.ExpiryDate,
                                        InwardPrice = item.InvoicePrice,
                                        InwardQty = item.InvoiceQty,
                                        DocumentNo = invoice.PINo,
                                        IsDelete = false,
                                        ProductId = Convert.ToInt32(item.ProductCode),
                                        TransactionType = "PInvoice",
                                        InwardAmount = item.Amount
                                    };
                                }
                            }
                            #endregion

                            #region Update Status of Order and Transaction
                            tblCustomerTransaction transact = (from tbl in objData.tblCustomerTransactions
                                                               where tbl.SupplierId == invoice.SupplierId
                                                               && tbl.IsDelete == false
                                                               && tbl.TransactionDocNo == invoice.PINo
                                                               && tbl.TransactionType.Equals("PInvoice")
                                                               select tbl).FirstOrDefault();
                            if (transact != null)
                            {
                                transact.BillAmount = invoice.Amount;
                            }
                            tblPurchaseOrder orderMain = (from tbl in objData.tblPurchaseOrders
                                                          where tbl.IsDelete == false
                                                          && tbl.PO_Id == invoice.PONo
                                                          select tbl).FirstOrDefault();
                            if (orderMain != null)
                            {
                                if (GetPOProductForEdit(Convert.ToInt32(invoice.PONo)).Count == lst.Count)
                                {
                                    flagISPOComplete = true;
                                }
                                else
                                {
                                    flagISPOComplete = false;
                                }
                                orderMain.IsPOComplete = flagISPOComplete;
                            }
                            objExistInvoice.Amount = invoice.Amount;
                            objExistInvoice.Discount = invoice.Discount;
                            objExistInvoice.PIDate = invoice.PIDate;
                            objExistInvoice.Tax1 = invoice.Tax1;
                            objExistInvoice.Tax2 = invoice.Tax2;

                            objData.SubmitChanges();
                            #endregion
                        }
                        else
                        {
                            ///Specifies that Current Invoice with PO
                            foreach (EntityPurchaseInvoiceDetails item in lst)
                            {

                            }
                        }
                    }
                    else if (objExistInvoice.PONo > 0)
                    {
                        List<tblPurchaseInvoiceDetail> lstExistedItems = (from tbl in objData.tblPurchaseInvoiceDetails
                                                                          where tbl.PINo == invoice.PINo
                                                                          && tbl.IsDelete == false
                                                                          select tbl).ToList();
                        if (lstExistedItems.Count > lst.Count)
                        {
                            foreach (tblPurchaseInvoiceDetail item in lstExistedItems)
                            {
                                int cnt = (from tbl in lst
                                           where tbl.ProductCode == item.ProductCode
                                           && tbl.PurchaseInvoiceNo == item.PINo
                                           select tbl).Count();
                                if (cnt == 0)
                                {
                                    item.IsDelete = true;
                                }
                                tblPurchaseOrderDetail objOrder = (from tbl in objData.tblPurchaseOrderDetails
                                                                   join tblPurchaseInv in objData.tblPurchaseInvoices
                                                                   on tbl.PO_Id equals tblPurchaseInv.PONo
                                                                   where tblPurchaseInv.PINo == item.PINo
                                                                   && tbl.IsDelete == false
                                                                   && tbl.Product_Id == item.ProductCode
                                                                   select tbl).FirstOrDefault();
                                if (objOrder != null)
                                {
                                    objOrder.InvoiceQuantity = 0;
                                }
                            }
                            objData.SubmitChanges();
                        }
                        if (invoice.PONo > 0)
                        {
                            #region Update Invoice Details
                            foreach (EntityPurchaseInvoiceDetails item in lst)
                            {

                                tblPurchaseInvoiceDetail invoiceItem = (from tbl in objData.tblPurchaseInvoiceDetails
                                                                        where tbl.PINo == invoice.PINo
                                                                        && tbl.IsDelete == false
                                                                        && tbl.ProductCode == item.ProductCode
                                                                        select tbl).FirstOrDefault();
                                if (invoiceItem != null)
                                {
                                    invoiceItem.Amount = item.Amount;
                                    invoiceItem.BatchNo = item.BatchNo;
                                    invoiceItem.ExpiryDate = item.ExpiryDate;
                                    invoiceItem.InvoicePrice = item.InvoicePrice;
                                    invoiceItem.InvoiceQty = item.InvoiceQty;
                                    invoiceItem.PINo = item.PurchaseInvoiceNo;
                                }
                                else
                                {
                                    invoiceItem = new tblPurchaseInvoiceDetail()
                                    {
                                        PINo = invoice.PINo,
                                        ProductCode = item.ProductCode,
                                        InvoiceQty = item.InvoiceQty,
                                        InvoicePrice = item.InvoicePrice,
                                        Amount = item.Amount,
                                        BatchNo = item.BatchNo,
                                        ExpiryDate = item.ExpiryDate,
                                        IsDelete = false
                                    };
                                    objData.tblPurchaseInvoiceDetails.InsertOnSubmit(invoiceItem);
                                }
                                tblPurchaseOrderDetail orderItem = (from tbl in objData.tblPurchaseOrderDetails
                                                                    where tbl.IsDelete == false
                                                                    && tbl.PO_Id == invoice.PONo
                                                                    && tbl.Product_Id == item.ProductCode
                                                                    select tbl).FirstOrDefault();
                                if (orderItem != null)
                                {
                                    if (orderItem.Quantity == item.InvoiceQty)
                                    {
                                        flagISPOComplete = true;
                                    }
                                    else
                                    {
                                        flagISPOComplete = false;
                                    }
                                    orderItem.InvoiceQuantity = item.InvoiceQty;
                                }

                                tblStockDetail stockItem = (from tbl in objData.tblStockDetails
                                                            where tbl.IsDelete == false
                                                            && tbl.TransactionType.Equals("PInvoice")
                                                            && Convert.ToInt32(tbl.DocumentNo) == invoice.PINo
                                                            && tbl.ProductId == Convert.ToInt32(item.ProductCode)
                                                            select tbl).FirstOrDefault();
                                if (stockItem != null)
                                {
                                    stockItem.ProductId = Convert.ToInt32(item.ProductCode);
                                    stockItem.InwardQty = item.InvoiceQty;
                                    stockItem.InwardPrice = item.InvoicePrice;
                                    stockItem.BatchNo = item.BatchNo;
                                    stockItem.ExpiryDate = item.ExpiryDate;
                                    stockItem.InwardAmount = item.Amount;
                                }
                                else
                                {
                                    stockItem = new tblStockDetail()
                                    {
                                        BatchNo = item.BatchNo,
                                        ExpiryDate = item.ExpiryDate,
                                        InwardPrice = item.InvoicePrice,
                                        InwardQty = item.InvoiceQty,
                                        DocumentNo = invoice.PINo,
                                        IsDelete = false,
                                        ProductId = Convert.ToInt32(item.ProductCode),
                                        TransactionType = "PInvoice",
                                        InwardAmount = item.Amount
                                    };
                                }
                            }
                            #endregion

                            #region Update Status of PO and Transaction Update
                            tblCustomerTransaction transact = (from tbl in objData.tblCustomerTransactions
                                                               where tbl.SupplierId == invoice.SupplierId
                                                               && tbl.IsDelete == false
                                                               && tbl.TransactionDocNo == invoice.PINo
                                                               && tbl.TransactionType.Equals("PInvoice")
                                                               select tbl).FirstOrDefault();
                            if (transact != null)
                            {
                                transact.BillAmount = invoice.Amount;
                            }

                            tblPurchaseOrder orderMain = (from tbl in objData.tblPurchaseOrders
                                                          where tbl.IsDelete == false
                                                          && tbl.PO_Id == invoice.PONo
                                                          select tbl).FirstOrDefault();
                            if (orderMain != null)
                            {
                                if (GetPOProduct(Convert.ToInt32(invoice.PONo)).Count == lst.Count)
                                {
                                    flagISPOComplete = true;
                                }
                                else
                                {
                                    flagISPOComplete = false;
                                }
                                orderMain.IsPOComplete = flagISPOComplete;
                            }

                            objExistInvoice.Amount = invoice.Amount;
                            objExistInvoice.Discount = invoice.Discount;
                            objExistInvoice.PIDate = invoice.PIDate;
                            objExistInvoice.Tax1 = invoice.Tax1;
                            objExistInvoice.Tax2 = invoice.Tax2;

                            objData.SubmitChanges();
                            #endregion
                        }
                        else
                        {
                            ///Specifies that Existing Invoice With PO and Current Not With PO

                            #region Update Invoice Details
                            foreach (EntityPurchaseInvoiceDetails item in lst)
                            {
                                tblPurchaseInvoiceDetail invoiceItem = (from tbl in objData.tblPurchaseInvoiceDetails
                                                                        where tbl.PINo == invoice.PINo
                                                                        && tbl.IsDelete == false
                                                                        && tbl.ProductCode == item.ProductCode
                                                                        select tbl).FirstOrDefault();
                                if (invoiceItem != null)
                                {
                                    invoiceItem.Amount = item.Amount;
                                    invoiceItem.BatchNo = item.BatchNo;
                                    invoiceItem.ExpiryDate = item.ExpiryDate;
                                    invoiceItem.InvoicePrice = item.InvoicePrice;
                                    invoiceItem.InvoiceQty = item.InvoiceQty;
                                    invoiceItem.PINo = item.PurchaseInvoiceNo;
                                }
                                else
                                {
                                    invoiceItem = new tblPurchaseInvoiceDetail()
                                    {
                                        PINo = invoice.PINo,
                                        ProductCode = item.ProductCode,
                                        InvoiceQty = item.InvoiceQty,
                                        InvoicePrice = item.InvoicePrice,
                                        Amount = item.Amount,
                                        BatchNo = item.BatchNo,
                                        ExpiryDate = item.ExpiryDate,
                                        IsDelete = false
                                    };
                                    objData.tblPurchaseInvoiceDetails.InsertOnSubmit(invoiceItem);
                                }
                                tblPurchaseOrderDetail orderItem = (from tbl in objData.tblPurchaseOrderDetails
                                                                    where tbl.IsDelete == false
                                                                    && tbl.PO_Id == objExistInvoice.PONo
                                                                    && tbl.Product_Id == item.ProductCode
                                                                    select tbl).FirstOrDefault();
                                if (orderItem != null)
                                {
                                    orderItem.InvoiceQuantity = 0;
                                }

                                tblStockDetail stockItem = (from tbl in objData.tblStockDetails
                                                            where tbl.IsDelete == false
                                                            && tbl.TransactionType.Equals("PInvoice")
                                                            && Convert.ToInt32(tbl.DocumentNo) == invoice.PINo
                                                            && tbl.ProductId == Convert.ToInt32(item.ProductCode)
                                                            select tbl).FirstOrDefault();
                                if (stockItem != null)
                                {
                                    stockItem.ProductId = Convert.ToInt32(item.ProductCode);
                                    stockItem.InwardQty = item.InvoiceQty;
                                    stockItem.InwardPrice = item.InvoicePrice;
                                    stockItem.BatchNo = item.BatchNo;
                                    stockItem.ExpiryDate = item.ExpiryDate;
                                    stockItem.InwardAmount = item.Amount;
                                }
                                else
                                {
                                    stockItem = new tblStockDetail()
                                    {
                                        BatchNo = item.BatchNo,
                                        ExpiryDate = item.ExpiryDate,
                                        InwardPrice = item.InvoicePrice,
                                        InwardQty = item.InvoiceQty,
                                        DocumentNo = invoice.PINo,
                                        IsDelete = false,
                                        ProductId = Convert.ToInt32(item.ProductCode),
                                        TransactionType = "PInvoice",
                                        InwardAmount = item.Amount
                                    };
                                }
                            }
                            #endregion

                            #region Update Status of PO and Transaction Update
                            tblCustomerTransaction transact = (from tbl in objData.tblCustomerTransactions
                                                               where tbl.SupplierId == invoice.SupplierId
                                                               && tbl.IsDelete == false
                                                               && tbl.TransactionDocNo == invoice.PINo
                                                               && tbl.TransactionType.Equals("PInvoice")
                                                               select tbl).FirstOrDefault();
                            if (transact != null)
                            {
                                transact.BillAmount = invoice.Amount;
                            }

                            tblPurchaseOrder orderMain = (from tbl in objData.tblPurchaseOrders
                                                          where tbl.IsDelete == false
                                                          && tbl.PO_Id == objExistInvoice.PONo
                                                          select tbl).FirstOrDefault();
                            if (orderMain != null)
                            {
                                orderMain.IsPOComplete = false;
                            }

                            objExistInvoice.Amount = invoice.Amount;
                            objExistInvoice.Discount = invoice.Discount;
                            objExistInvoice.PIDate = invoice.PIDate;
                            objExistInvoice.Tax1 = invoice.Tax1;
                            objExistInvoice.Tax2 = invoice.Tax2;
                            objExistInvoice.PONo = invoice.PONo;
                            objData.SubmitChanges();
                            #endregion
                        }
                    }
                }
                i++;
            }
            catch (Exception ex)
            {
                i = 0;
                throw ex;
            }
            return i;
        }

        #region GetPOProductForEdit(int PONo)
        public List<EntityProduct> GetPOProductForEdit(int PONo)
        {
            List<EntityProduct> lst = new List<EntityProduct>();
            try
            {
                lst = (from tbl in objData.tblPurchaseOrderDetails
                       join tblProduct in objData.tblProductMasters
                       on tbl.Product_Id equals tblProduct.ProductId
                       where tbl.IsDelete == false
                       && tbl.PO_Id == PONo
                       select new EntityProduct
                       {
                           ProductId = Convert.ToInt32(tbl.Product_Id),
                           ProductName = tblProduct.ProductName,
                       }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }
        #endregion

        public List<EntityPurchaseInvoice> GetPurchaseInvoices(string Prefix)
        {
            List<EntityPurchaseInvoice> lst = null;
            try
            {
                lst = (from tbl in GetPurchaseInvoices()
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
    }
}