using MainProjectHos.Models.DataModels;
using MainProjectHos.Models.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.BusinessLayer
{
    public class IssueMaterialBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();

        public IssueMaterialBLL()
        {
            objData = new CriticareHospitalDataContext();
        }

        public CriticareHospitalDataContext objData { get; set; }

        public List<EntityEmployee> GetEmployeeList()
        {
            List<EntityEmployee> lst = (from tbl in objData.tblEmployees
                                        where tbl.IsDelete == false
                                        select new EntityEmployee { PKId = tbl.PKId, EmpName = tbl.EmpFirstName + " " + tbl.EmpMiddleName + " " + tbl.EmpLastName }).ToList();

            return lst;
        }

        public List<EntityPatientMaster> GetPatientList()
        {
            List<EntityPatientMaster> lst = (from tbl in objData.tblPatientMasters
                                             where tbl.IsDelete == false
                                             select new EntityPatientMaster { PKId = tbl.PKId, FullName = tbl.PatientFirstName + " " + tbl.PatientMiddleName + " " + tbl.PatientLastName }).ToList();

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
            EntityProduct lstPro = null;
            try
            {
                lstPro = (from tbl in objData.tblStockDetails
                          where tbl.ProductId.Equals(Prod_ID)
                          && tbl.IsDelete == false
                          && tbl.TransactionType.Equals("PInvoice")
                          orderby tbl.StockId descending
                          select new EntityProduct { Price = Convert.ToDecimal(tbl.InwardPrice) }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstPro;
        }

        public int InsertPurchaseOrder(EntityIssueMaterial entInvoice, List<EntityIssueMaterialDetails> lstInvoice)
        {
            try
            {
                int? PO_ID = 0;
                objData.STP_InsertIssueMaterial(Convert.ToDateTime(entInvoice.IssueDate), ref PO_ID, Convert.ToInt32(entInvoice.PatientId), Convert.ToDecimal(entInvoice.TotalAmount), Convert.ToInt32(entInvoice.EmpId));
                foreach (EntityIssueMaterialDetails entPurchaseDetails in lstInvoice)
                {
                    tblIssueDetail obj = new tblIssueDetail()
                    {
                        IssueId = PO_ID,
                        ProductId = Convert.ToInt32(entPurchaseDetails.ProductId),
                        Quantity = entPurchaseDetails.Quantity,
                        Rate = entPurchaseDetails.Rate,
                        Total = Convert.ToDecimal(entPurchaseDetails.Total),
                        IsDelete = false,
                        BatchNo = entPurchaseDetails.BatchNo,
                        ExpiryDate = entPurchaseDetails.ExpiryDate
                    };
                    objData.tblIssueDetails.InsertOnSubmit(obj);
                    tblStockDetail stock = new tblStockDetail()
                    {
                        ProductId = Convert.ToInt32(entPurchaseDetails.ProductId),
                        OpeningQty = 0,
                        InwardQty = 0,
                        InwardPrice = 0,
                        InwardAmount = 0,
                        BatchNo = entPurchaseDetails.BatchNo,
                        ExpiryDate = entPurchaseDetails.ExpiryDate,
                        OutwardQty = entPurchaseDetails.Quantity,
                        OutwardPrice = entPurchaseDetails.Rate,
                        OutwardAmount = entPurchaseDetails.Total,
                        TransactionType = "Issue",
                        DocumentNo = PO_ID,
                    };
                    objData.tblStockDetails.InsertOnSubmit(stock);
                }
                objData.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EntityIssueMaterial> GetMaterialIssue()
        {
            try
            {
                return (from tbl in objData.tblIssueMasters
                        join tblSup in objData.tblEmployees
                        on tbl.EmpId equals tblSup.PKId
                        join tblAdmit in objData.tblPatientAdmitDetails
                        on tbl.PatientId equals tblAdmit.AdmitId
                        join tblPatient in objData.tblPatientMasters
                        on tblAdmit.PatientId equals tblPatient.PKId
                        where tblAdmit.IsDischarge == false
                        && tbl.IsDelete == false
                        select new EntityIssueMaterial
                        {
                            IssueId = tbl.IssueId,
                            EmployeeName = tblSup.EmpFirstName + " " + tblSup.EmpMiddleName + " " + tblSup.EmpLastName,
                            PatientName = tblPatient.PatientFirstName + " " + tblPatient.PatientMiddleName + " " + tblPatient.PatientLastName,
                            IssueDate = tbl.IssueDate,
                            TotalAmount = tbl.TotalAmount
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EntityIssueMaterialDetails> SelectPurchaseOrderForEdit(int PO_ID)
        {
            int i = 1;
            List<EntityIssueMaterialDetails> lst = new List<EntityIssueMaterialDetails>();
            try
            {
                lst = (from tbl in objData.STP_EditIssueMaterial(Convert.ToInt32(PO_ID))
                       select new EntityIssueMaterialDetails
                       {
                           SR_No = Convert.ToInt32(tbl.SR_No),
                           IssueId = Convert.ToInt32(tbl.IssueId),
                           ProductName = Convert.ToString(tbl.ProductName),
                           Total = Convert.ToDecimal(tbl.Total),
                           Rate = Convert.ToDecimal(tbl.Rate),
                           Quantity = Convert.ToInt32(tbl.Quantity),
                           ProductId = Convert.ToInt32(tbl.ProductId),
                           BatchNo = tbl.BatchNo,
                           ExpiryDate = tbl.ExpiryDate,
                           IsDelete = false,
                       }).ToList();
                foreach (EntityIssueMaterialDetails item in lst)
                {
                    item.TempId = i++;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }

        public int UpdateMaterialIssueDetails(EntityIssueMaterial lstEdited, List<EntityIssueMaterialDetails> lstUpdate)
        {
            try
            {
                tblIssueMaster objTest = (from tbl in objData.tblIssueMasters
                                          where tbl.IsDelete == false
                                          && tbl.IssueId == lstEdited.IssueId
                                          select tbl).FirstOrDefault();

                if (objTest != null)
                {
                    objTest.IssueId = Convert.ToInt32(lstEdited.IssueId);
                    objTest.TotalAmount = lstEdited.TotalAmount;
                    objTest.PatientId = lstEdited.PatientId;
                    objTest.EmpId = lstEdited.EmpId;
                    objTest.IssueDate = lstEdited.IssueDate;
                }
                foreach (EntityIssueMaterialDetails item in lstUpdate)
                {
                    if (item.SR_No == 0)
                    {
                        tblIssueDetail objNewAdded = new tblIssueDetail()
                        {
                            IssueId = Convert.ToInt32(lstEdited.IssueId),
                            ProductId = Convert.ToInt32(item.ProductId),
                            Quantity = item.Quantity,
                            Rate = item.Rate,
                            Total = Convert.ToDecimal(item.Total),
                            IsDelete = false,
                            BatchNo = item.BatchNo,
                            ExpiryDate = item.ExpiryDate,
                        };
                        objData.tblIssueDetails.InsertOnSubmit(objNewAdded);
                        tblStockDetail stock = new tblStockDetail()
                        {
                            ProductId = Convert.ToInt32(item.ProductId),
                            OpeningQty = 0,
                            InwardQty = 0,
                            InwardPrice = 0,
                            InwardAmount = 0,
                            BatchNo = item.BatchNo,
                            ExpiryDate = item.ExpiryDate,
                            OutwardQty = item.Quantity,
                            OutwardPrice = item.Rate,
                            OutwardAmount = item.Total,
                            TransactionType = "Issue",
                            IsDelete = false,
                        };
                        objData.tblStockDetails.InsertOnSubmit(stock);
                    }
                    else
                    {
                        tblIssueDetail cnt = (from tbl in objData.tblIssueDetails
                                              where tbl.IsDelete == false
                                              && tbl.SR_No == item.SR_No
                                              select tbl).FirstOrDefault();
                        if (cnt != null)
                        {
                            cnt.IssueId = Convert.ToInt32(item.IssueId);
                            cnt.BatchNo = item.BatchNo;
                            cnt.ExpiryDate = item.ExpiryDate;
                            cnt.ProductId = Convert.ToInt32(item.ProductId);
                            cnt.Quantity = item.Quantity;
                            cnt.Rate = item.Rate;
                            cnt.Total = Convert.ToDecimal(item.Total);
                            cnt.IsDelete = item.IsDelete;
                        }
                        tblStockDetail stock = (from tbl in objData.tblStockDetails
                                                where tbl.IsDelete == false
                                                && tbl.DocumentNo == lstEdited.IssueId
                                                && tbl.ProductId == item.ProductId
                                                && tbl.TransactionType.Equals("Issue")
                                                select tbl).FirstOrDefault();
                        if (stock != null)
                        {
                            stock.ProductId = Convert.ToInt32(item.ProductId);
                            stock.OpeningQty = 0;
                            stock.InwardQty = 0;
                            stock.InwardPrice = 0;
                            stock.InwardAmount = 0;
                            stock.BatchNo = item.BatchNo;
                            stock.ExpiryDate = item.ExpiryDate;
                            stock.OutwardQty = item.Quantity;
                            stock.OutwardPrice = item.Rate;
                            stock.OutwardAmount = item.Total;
                            stock.IsDelete = item.IsDelete;
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

        public List<EntityIssueMaterialDetails> GetBatchNoandExpiryDatesByProduct(int ProductId)
        {
            List<EntityIssueMaterialDetails> lst = null;
            try
            {
                lst = (from tbl in objData.tblStockDetails
                       where tbl.ProductId.Equals(ProductId)
                       && tbl.IsDelete == false
                       && tbl.TransactionType.Equals("PInvoice")
                       select new EntityIssueMaterialDetails { ProductId = tbl.ProductId, BatchNo = tbl.BatchNo, ExpiryDate = tbl.ExpiryDate }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }

        public List<EntityIssueMaterial> GetMaterialIssue(string prefix)
        {
            List<EntityIssueMaterial> lst = null;
            try
            {
                lst = (from tbl in GetMaterialIssue()
                       where tbl.PatientName.ToUpper().Contains(prefix.ToUpper())
                       || tbl.EmployeeName.ToUpper().Contains(prefix.ToUpper())
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