using MainProjectHos.Models.DataModels;
using MainProjectHos.Models.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.BusinessLayer
{
    public class PurchaseOrderBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();

        public PurchaseOrderBLL()
        {
            objData = new CriticareHospitalDataContext();
        }

        public CriticareHospitalDataContext objData { get; set; }

        public List<EntitySupplierMaster> GetSupplierList()
        {
            List<EntitySupplierMaster> lst = (from tbl in objData.tblSupplierMasters
                                              where tbl.IsDelete == false
                                              select new EntitySupplierMaster { PKId = tbl.PKId, SupplierName = tbl.SupplierName }).ToList();

            return lst;
        }

        public List<EntityProduct> GetProductList()
        {
            List<EntityProduct> lst = (from tbl in objData.tblProductMasters
                                       where tbl.IsDelete == false
                                       select new EntityProduct { ProductId = tbl.ProductId, ProductName = tbl.ProductName }).ToList();

            return lst;
        }

        public EntityProduct GetProductPrice(int Prod_ID)
        {
            EntityProduct lstPro = new EntityProduct();
            try
            {
                lstPro = (from tbl in objData.tblProductMasters
                          where tbl.ProductId.Equals(Prod_ID)
                          select new EntityProduct { Price = tbl.Price }).FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
            return lstPro;
        }

        public int InsertPurchaseOrder(EntityPurchaseOrder entInvoice, List<EntityPurchaseOrderDetails> lstInvoice)
        {
            try
            {
                int? PO_ID = 0;
                objData.STP_InsertPurchaseOrder(Convert.ToDateTime(entInvoice.PO_Date), ref PO_ID, Convert.ToInt32(entInvoice.VendorId), Convert.ToDecimal(entInvoice.PO_Amount));
                foreach (EntityPurchaseOrderDetails entPurchaseDetails in lstInvoice)
                {
                    tblPurchaseOrderDetail obj = new tblPurchaseOrderDetail()
                    {
                        PO_Id = PO_ID,
                        Product_Id = entPurchaseDetails.Product_Id,
                        Quantity = entPurchaseDetails.Quantity,
                        Rate = entPurchaseDetails.Rate,
                        Total = entPurchaseDetails.Total,
                        IsDelete = false
                    };
                    objData.tblPurchaseOrderDetails.InsertOnSubmit(obj);
                }
                objData.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EntityPurchaseOrder> GetPurchaseOder()
        {
            try
            {
                return (from tbl in objData.tblPurchaseOrders
                        join tblSup in objData.tblSupplierMasters
                        on tbl.VendorId equals tblSup.PKId
                        select new EntityPurchaseOrder
                        {
                            PO_Id = tbl.PO_Id,
                            VendorName = tblSup.SupplierName,
                            PO_Date = tbl.PO_Date,
                            PO_Amount = tbl.PO_Amount
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SelectPurchaseOrderStatus(int POId)
        {
            bool flag = false;
            try
            {
                tblPurchaseInvoice obj = (from tbl in objData.tblPurchaseInvoices
                                          where tbl.IsDelete == false
                                          && tbl.PONo == POId
                                          select tbl).FirstOrDefault();
                if (obj != null)
                {
                    flag = false;
                }
                else
                {
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flag;

        }

        public List<EntityPurchaseOrderDetails> SelectPurchaseOrderForEdit(int PO_ID)
        {
            List<EntityPurchaseOrderDetails> lst = new List<EntityPurchaseOrderDetails>();
            try
            {
                lst = (from tbl in objData.STP_EditPurchaseOrder(Convert.ToInt32(PO_ID))
                       select new EntityPurchaseOrderDetails
                       {
                           SR_No = Convert.ToInt32(tbl.SR_No),
                           PO_Id = Convert.ToInt32(tbl.PO_Id),
                           VendorId = Convert.ToInt32(tbl.VendorId),
                           ProductName = Convert.ToString(tbl.ProductName),
                           VendorName = Convert.ToString(tbl.SupplierName),
                           NetTotal = Convert.ToDecimal(tbl.PO_Amount),
                           Rate = Convert.ToDecimal(tbl.Rate),
                           Quantity = Convert.ToInt32(tbl.Quantity),
                           Total = Convert.ToDecimal(tbl.Rate) * Convert.ToInt32(tbl.Quantity),
                           Product_Id = Convert.ToInt32(tbl.Product_Id),
                       }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }

        public int UpdatePurchaseOrderDetails(List<EntityPurchaseOrderDetails> lstEdited, List<EntityPurchaseOrderDetails> lstUpdate)
        {
            try
            {
                tblPurchaseOrder objTest = (from tbl in objData.tblPurchaseOrders
                                            where tbl.IsDelete == false
                                            && tbl.PO_Id == lstEdited[0].PO_Id
                                            select tbl).FirstOrDefault();

                List<EntityPurchaseOrderDetails> lstTemp = new List<EntityPurchaseOrderDetails>();

                if (objTest != null)
                {
                    objTest.PO_Amount = lstEdited[0].Total;
                }
                foreach (EntityPurchaseOrderDetails item in lstEdited)
                {
                    int cnt = (from tbl in objData.tblPurchaseOrderDetails
                               where tbl.PO_Id == item.PO_Id
                               && tbl.IsDelete == false
                               select tbl).ToList().Count;
                    if (cnt == 0)
                    {
                        tblPurchaseOrderDetail objNewAdded = new tblPurchaseOrderDetail()
                        {
                            PO_Id = lstEdited[0].PO_Id,
                            Product_Id = item.Product_Id,
                            Quantity = item.Quantity,
                            Rate = item.Rate,
                            Total = item.Rate * item.Quantity,
                            IsDelete = false
                        };
                        objData.tblPurchaseOrderDetails.InsertOnSubmit(objNewAdded);
                        objData.SubmitChanges();
                    }
                    else
                    {
                        lstTemp.Add(item);
                    }
                }


                foreach (EntityPurchaseOrderDetails item in lstUpdate)
                {
                    tblPurchaseOrderDetail cnt = (from tbl in objData.tblPurchaseOrderDetails
                                                  where tbl.PO_Id == item.PO_Id
                                                  && tbl.Product_Id == item.Product_Id
                                                  select tbl).FirstOrDefault();

                    if (cnt != null)
                    {
                        int checkExist = (from tbl in lstTemp
                                          where tbl.Product_Id == item.Product_Id
                                          select tbl).ToList().Count;
                        if (checkExist == 0)
                        {
                            cnt.IsDelete = true;
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

        public List<EntityPurchaseOrder> GetPurchaseOderNotCompleted()
        {
            List<EntityPurchaseOrder> lst = new List<EntityPurchaseOrder>();
            try
            {
                lst = (from tbl in objData.tblPurchaseOrders
                       where tbl.IsDelete == false
                       && tbl.IsPOComplete == false
                       select new EntityPurchaseOrder
                       {
                           PO_Id = tbl.PO_Id,
                           VendorName = tbl.PO_Id.ToString()
                       }).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }

        public List<EntityPurchaseOrder> GetPurchaseOderNotCompleted(int? PONo)
        {
            List<EntityPurchaseOrder> lst = new List<EntityPurchaseOrder>();
            try
            {
                lst = (from tbl in objData.tblPurchaseOrders
                       where (tbl.IsDelete == false
                       && tbl.IsPOComplete == false)
                       || tbl.PO_Id == PONo
                       select new EntityPurchaseOrder
                       {
                           PO_Id = tbl.PO_Id,
                           VendorName = tbl.PO_Id.ToString()
                       }).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }

        public List<EntityPurchaseOrder> GetPurchaseOder(string prefix)
        {
            List<EntityPurchaseOrder> lst = null;
            try
            {
                lst = (from tbl in GetPurchaseOder()
                       where Convert.ToString(tbl.PO_Id).Equals(prefix)
                       || tbl.VendorName.ToUpper().Contains(prefix.ToUpper())
                       select new EntityPurchaseOrder
                       {
                           PO_Id = tbl.PO_Id,
                           VendorName = tbl.VendorName,
                           PO_Date = tbl.PO_Date,
                           PO_Amount = tbl.PO_Amount
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