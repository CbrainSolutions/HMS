using MainProjectHos.Models.DataModels;
using MainProjectHos.Models.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.BusinessLayer
{
    public class ICUDischargeBilling
    {
        public ICUDischargeBilling()
        {
            objData = new CriticareHospitalDataContext();
        }

        public CriticareHospitalDataContext objData { get; set; }

        public List<EntityChargeCategory> GetOtherChargeList()
        {
            try
            {
                List<EntityChargeCategory> lst = new List<EntityChargeCategory>();

                return lst = (from tbl in objData.tblChargeCategories
                              where tbl.IsOther == true
                              select new EntityChargeCategory { ChargesId = tbl.ChargesId, ChargeCategoryName = tbl.ChargeCategoryName }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<EntityBedMaster> FreeBedsofIPD()
        {
            return (from tbl in objData.STP_BindIPDBedForShifting()
                    select new EntityBedMaster
                    {
                        BedId = tbl.bedid,
                        BedNo = tbl.bedno
                    }).ToList();
        }

        public EntityBedAllocToPatient GetICUAllocatedBed(int BedAllocId)
        {
            EntityBedAllocToPatient obj = null;
            try
            {
                obj = (from tbl in objData.tblBedAllocationToPatients
                       join tblbed in objData.tblBedMasters
                       on tbl.BedId equals tblbed.BedId
                       join tblRoomtype in objData.tblRoomCategories
                       on tblbed.CategoryId equals tblRoomtype.PKId
                       where tbl.IsDelete == false
                       && tbl.DischargeDate == null
                       select new EntityBedAllocToPatient
                       {
                           BedId = tbl.BedId,
                           BedNo = tblbed.BedNo
                       }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

        public List<EntityBedAllocToPatient> GetICUAllocatedBedDetails(int PatientAdmitId)
        {
            List<EntityBedAllocToPatient> lstAllocation = null;
            try
            {
                lstAllocation = (from tbl in objData.tblBedAllocationToPatients
                                 join tblbed in objData.tblBedMasters
                                 on tbl.BedId equals tblbed.BedId
                                 join tblRoomtype in objData.tblRoomCategories
                                 on tblbed.CategoryId equals tblRoomtype.PKId
                                 where tbl.IsDelete == false
                                 && tbl.PatientId == PatientAdmitId
                                 && tbl.DischargeDate == null
                                 select new EntityBedAllocToPatient
                                 {
                                     BedAllocId = tbl.BedAllocId,
                                     BedId = tbl.BedId,
                                     BedNo = tblbed.BedNo,
                                     AllocationDate = tbl.AllocationDate,
                                     DischargeDate = tbl.DischargeDate,
                                     CategoryDesc = Convert.ToBoolean(tblRoomtype.IsICU) ? "ICU" : "IPD",
                                     ShiftDate = tbl.ShiftDate,
                                     Charges = tblRoomtype.Rate
                                 }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstAllocation;
        }

        public int InsertInvoice(EntityICUInvoice entInvoice, List<EntityICUInvoiceDetail> lstInvoice, bool IsCash)
        {
            try
            {
                int? ICUInvoiceNo = 0;
                objData.STP_InserttblICUInvoiceDischarge(Convert.ToDateTime(entInvoice.AllocationDate), Convert.ToInt32(entInvoice.PatientId)
                    , Convert.ToDecimal(entInvoice.NetAmount), Convert.ToDecimal(entInvoice.TotalAmount),
                    Convert.ToInt32(entInvoice.Discount), Convert.ToInt32(entInvoice.Tax1), Convert.ToInt32(entInvoice.Tax2), false, Convert.ToBoolean(entInvoice.Is_ShiftIPD),
                    Convert.ToDateTime(entInvoice.ShiftDate), ref ICUInvoiceNo);
                foreach (EntityICUInvoiceDetail item in lstInvoice)
                {
                    tblICUInvoiceDischargeDetail obj = new tblICUInvoiceDischargeDetail()
                    {
                        ICUBedAllocId = entInvoice.ICUBedAllocId,
                        IsDelete = false,
                        ChargesId = item.ChargesId,
                        Amount = item.Amount,
                        NoofDays = item.NoofDays,
                        Charge = item.Charges,
                        Quantity = item.Quantity,
                        ICUInvoiceNo = ICUInvoiceNo,
                    };
                    objData.tblICUInvoiceDischargeDetails.InsertOnSubmit(obj);
                }
                int TransactionId = new PatientInvoiceBLL().GetTransactionId();
                if (IsCash)
                {
                    tblCustomerTransaction objDebit = new tblCustomerTransaction()
                    {
                        TransactionId = TransactionId,
                        IsCash = true,
                        TransactionDocNo = ICUInvoiceNo,
                        TransactionType = "ICUInvoice",
                        BillAmount = entInvoice.NetAmount,
                        PayAmount = 0,
                        PatientId = entInvoice.PatientId,
                        IsDelete = false,
                        ReceiptDate = entInvoice.AllocationDate,
                    };
                    objData.tblCustomerTransactions.InsertOnSubmit(objDebit);
                    //objData.SubmitChanges();
                    tblCustomerTransaction objCrReceipt = new tblCustomerTransaction()
                    {
                        TransactionId = TransactionId,
                        IsCash = true,
                        TransactionDocNo = ICUInvoiceNo,
                        TransactionType = "ICUInvoice",
                        PayAmount = entInvoice.NetAmount,
                        BillAmount = 0,
                        PatientId = entInvoice.PatientId,
                        IsDelete = false,
                        ReceiptDate = entInvoice.AllocationDate,
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
                        TransactionDocNo = ICUInvoiceNo,
                        TransactionType = "ICUInvoice",
                        BillAmount = entInvoice.NetAmount,
                        PatientId = entInvoice.PatientId,
                        IsDelete = false,
                        ReceiptDate = entInvoice.AllocationDate,
                    };
                    objData.tblCustomerTransactions.InsertOnSubmit(objDebit);
                    //objData.SubmitChanges();
                }
                objData.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EntitySelectICUInvoiceDetail> GetPatientInvoice()
        {
            return (from tbl in objData.STP_SelectICUInvoiceDetails()
                    select new EntitySelectICUInvoiceDetail
                    {
                        ICUInvoiceNo = tbl.ICUInvoiceNo,
                        ICUInvoiceDate = tbl.ICUInvoiceDate,
                        PatientAdmitId = tbl.PatientAdmitId,
                        FullName = tbl.FullName,
                        NetAmount = tbl.NetAmount,
                        ServiceTax = Convert.ToDecimal(tbl.TotalAmount * tbl.ServiceTax / 100),
                        Vat = Convert.ToDecimal(tbl.TotalAmount * tbl.Vat / 100),
                        Discount = Convert.ToDecimal(tbl.TotalAmount * tbl.Discount / 100),
                        TotalAmount = tbl.TotalAmount
                    }).ToList();
        }

        public List<EntityICUInvoiceDetail> SelectICUDischargeInvoiceDetails(int InvoiceNo)
        {
            List<EntityICUInvoiceDetail> lst = null;
            List<EntityICUInvoiceDetail> lstFinal = null;
            try
            {
                lst = (from tbl in objData.tblICUInvoiceDischargeDetails
                       join tblCharges in objData.tblChargeCategories
                       on tbl.ChargesId equals tblCharges.ChargesId
                       where tbl.IsDelete == false
                       && tbl.ICUInvoiceNo == InvoiceNo
                       select new EntityICUInvoiceDetail
                       {
                           ICUSRlNo = tbl.ICUSRlNo,
                           ICUBedAllocId = tbl.ICUBedAllocId,
                           ChargesName = tblCharges.ChargeCategoryName,
                           NoofDays = tbl.NoofDays.Value,
                           Quantity = tbl.Quantity.Value,
                           Charges = tbl.Charge.Value,
                           Amount = tbl.Amount,
                           ChargesId = tbl.ChargesId,
                           IsDelete = tbl.IsDelete
                       }).ToList();
                int i = 1;
                lstFinal = new List<EntityICUInvoiceDetail>();
                foreach (EntityICUInvoiceDetail item in lst)
                {
                    item.TempId = i++;
                    lstFinal.Add(item);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstFinal;
        }

        public int UpdateInvoice(List<EntityICUInvoiceDetail> lstUpdate, EntityICUInvoice entInvoice)
        {
            int count = 0;
            try
            {
                tblICUInvoiceDischarge obj = (from tbl in objData.tblICUInvoiceDischarges
                                              where tbl.IsDelete == false
                                              && tbl.ICUInvoiceNo == entInvoice.InvoiceNo
                                              select tbl).FirstOrDefault();
                if (obj != null)
                {
                    obj.Discount = entInvoice.Discount;
                    obj.Vat = Convert.ToInt32(entInvoice.Tax1);
                    obj.ServiceTax = Convert.ToInt32(entInvoice.Tax2);
                    obj.TotalAmount = entInvoice.TotalAmount;
                    obj.NetAmount = entInvoice.NetAmount;
                }
                foreach (EntityICUInvoiceDetail item in lstUpdate)
                {
                    if (item.ICUSRlNo == 0)
                    {
                        tblICUInvoiceDischargeDetail objDetails = new tblICUInvoiceDischargeDetail()
                        {
                            ICUBedAllocId = entInvoice.ICUBedAllocId,
                            IsDelete = false,
                            ChargesId = item.ChargesId,
                            Amount = item.Amount,
                            NoofDays = item.NoofDays,
                            Charge = item.Charges,
                            Quantity = item.Quantity,
                            ICUInvoiceNo = entInvoice.InvoiceNo,
                        };
                        objData.tblICUInvoiceDischargeDetails.InsertOnSubmit(objDetails);
                    }
                    else
                    {
                        tblICUInvoiceDischargeDetail objDetails = (from tbl in objData.tblICUInvoiceDischargeDetails
                                                                   where tbl.IsDelete == false
                                                                   && tbl.ICUSRlNo == item.ICUSRlNo
                                                                   select tbl).FirstOrDefault();
                        if (objDetails != null)
                        {
                            objDetails.Amount = item.Amount;
                            objDetails.Charge = item.Charges;
                            objDetails.ChargesId = item.ChargesId;
                            objDetails.NoofDays = item.NoofDays;
                            objDetails.Quantity = item.Quantity;
                            objDetails.IsDelete = item.IsDelete;
                            objDetails.ICUBedAllocId = item.ICUBedAllocId;
                        }
                    }
                }

                List<tblCustomerTransaction> objCust = (from tbl in objData.tblCustomerTransactions
                                                        where tbl.IsDelete == false
                                                        && tbl.TransactionDocNo == entInvoice.InvoiceNo
                                                        && tbl.TransactionType == "ICUInvoice"
                                                        select tbl).ToList();
                if (obj != null && objCust != null)
                {
                    if (objCust.Count == 1)
                    {
                        if (entInvoice.IsCash.Value)
                        {
                            tblCustomerTransaction objC = new tblCustomerTransaction()
                            {
                                PayAmount = entInvoice.NetAmount,
                                ReceiptDate = entInvoice.AllocationDate,
                                IsCash = entInvoice.IsCash,
                                IsDelete = false,
                                TransactionDocNo = entInvoice.ICUBedAllocId,
                                TransactionId = objCust[0].TransactionId,
                                TransactionType = "ICUInvoice",
                                PatientId = entInvoice.PatientId,
                            };
                            objData.tblCustomerTransactions.InsertOnSubmit(objC);
                            foreach (tblCustomerTransaction item in objCust)
                            {
                                item.BillAmount = entInvoice.NetAmount;
                                item.ReceiptDate = entInvoice.AllocationDate;
                                item.IsCash = entInvoice.IsCash;
                                if (item.IsCash == false)
                                {
                                    if (item.PayAmount > 0)
                                    {
                                        item.IsDelete = true;
                                    }
                                }
                                else
                                {
                                    item.PayAmount = entInvoice.NetAmount;
                                }
                            }
                        }
                        else
                        {
                            foreach (tblCustomerTransaction item in objCust)
                            {
                                item.BillAmount = entInvoice.NetAmount;
                                item.ReceiptDate = entInvoice.AllocationDate;
                                item.IsCash = entInvoice.IsCash;
                                if (item.IsCash == false)
                                {
                                    if (item.PayAmount > 0)
                                    {
                                        item.IsDelete = true;
                                    }
                                }
                                else
                                {
                                    item.PayAmount = entInvoice.NetAmount;
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (tblCustomerTransaction item in objCust)
                        {
                            item.BillAmount = entInvoice.NetAmount;
                            item.ReceiptDate = entInvoice.AllocationDate;
                            item.IsCash = entInvoice.IsCash;
                            if (item.IsCash == false)
                            {
                                if (item.PayAmount > 0)
                                {
                                    item.IsDelete = true;
                                }
                            }
                            else
                            {
                                item.PayAmount = entInvoice.NetAmount;
                            }
                        }
                    }
                }
                objData.SubmitChanges();
                count++;
            }
            catch (Exception ex)
            {
                count = 0;
                throw ex;
            }
            return count;
        }

        public List<STP_GetICUInvoiceBedsFinalResult> GetFinalICUInvoice(int InvoiceNo)
        {
            List<STP_GetICUInvoiceBedsFinalResult> lst = (from tbl in GetPatientInvoiceDetails()
                                                          where tbl.ICUInvoiceNo == InvoiceNo
                                                          select tbl).ToList();
            return lst;
        }

        private List<STP_GetICUInvoiceBedsFinalResult> GetPatientInvoiceDetails()
        {
            return objData.STP_GetICUInvoiceBedsFinal().ToList();
        }

        public List<EntityICUInvoiceDischargeDetails> SelectFinalICUInvoiceDetails(int InvoiceNo)
        {
            List<EntityICUInvoiceDischargeDetails> lst = (from tbl in objData.tblICUInvoiceDischarges
                                                          join tblICUdetails in objData.tblICUInvoiceDischargeDetails
                                                          on tbl.ICUInvoiceNo equals tblICUdetails.ICUInvoiceNo
                                                          join tblCharges in objData.tblChargeCategories
                                                          on tblICUdetails.ChargesId equals tblCharges.ChargesId
                                                          where tblICUdetails.IsDelete == false
                                                          && tblICUdetails.ICUInvoiceNo == InvoiceNo
                                                          select new EntityICUInvoiceDischargeDetails
                                                          {
                                                              ChargesName = tblCharges.ChargeCategoryName,
                                                              ICUSRlNo = tblICUdetails.ICUSRlNo,
                                                              Amount = tblICUdetails.Amount,
                                                              NetAmount = Convert.ToDecimal(tbl.NetAmount),
                                                              Charge = tblICUdetails.Charge,
                                                              ChargesId = tblCharges.ChargesId,
                                                              Discount = Convert.ToInt32(tbl.Discount),
                                                              Vat = Convert.ToInt32(tbl.Vat),
                                                              Service = Convert.ToInt32(tbl.ServiceTax),
                                                              NoofDays = tblICUdetails.NoofDays,
                                                              Quantity = tblICUdetails.Quantity
                                                          }).ToList();
            return lst;
        }


    }

}