using MainProjectHos.Models.DataLayer;
using MainProjectHos.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.BusinessLayer
{
    public class ICUInvoiceBLL
    {
        public ICUInvoiceBLL()
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

        public List<EntityPatientMaster> GetPatientList(bool IsDischarge)
        {
            List<EntityPatientMaster> lst = null;
            if (IsDischarge)
            {
                lst = (from tbl in objData.tblPatientMasters
                       join tblAdmint in objData.tblPatientAdmitDetails
                       on tbl.PKId equals tblAdmint.PatientId
                       where tblAdmint.IsDischarge == false
                       orderby tblAdmint.AdmitId descending
                       select new EntityPatientMaster { PatientId = tblAdmint.AdmitId, PatientFirstName = tbl.PatientFirstName + ' ' + tbl.PatientMiddleName + ' ' + tbl.PatientLastName }).ToList();
            }
            else
            {
                lst = (from tbl in objData.tblPatientMasters
                       join tblAdmint in objData.tblPatientAdmitDetails
                       on tbl.PKId equals tblAdmint.PatientId
                       orderby tblAdmint.AdmitId descending
                       select new EntityPatientMaster { PatientId = tblAdmint.AdmitId, PatientFirstName = tbl.PatientFirstName + ' ' + tbl.PatientMiddleName + ' ' + tbl.PatientLastName }).ToList();
            }
            return lst;
        }

        public List<EntityPatientMaster> GetICUPatientList(bool IsDischarge)
        {
            List<EntityPatientMaster> lst = null;
            if (IsDischarge)
            {
                lst = (from tbl in objData.tblPatientMasters
                       join tblAdmint in objData.tblPatientAdmitDetails
                       on tbl.PKId equals tblAdmint.PatientId
                       where tblAdmint.IsDischarge == false
                       && tblAdmint.IsIPD == true
                       orderby tblAdmint.AdmitId descending
                       select new EntityPatientMaster { PatientId = tblAdmint.AdmitId, PatientFirstName = tbl.PatientFirstName + ' ' + tbl.PatientMiddleName + ' ' + tbl.PatientLastName }).ToList();
            }
            else
            {
                lst = (from tbl in objData.tblPatientMasters
                       join tblAdmint in objData.tblPatientAdmitDetails
                       on tbl.PKId equals tblAdmint.PatientId
                       where tblAdmint.IsIPD == true
                       orderby tblAdmint.AdmitId descending
                       select new EntityPatientMaster { PatientId = tblAdmint.AdmitId, PatientFirstName = tbl.PatientFirstName + ' ' + tbl.PatientMiddleName + ' ' + tbl.PatientLastName }).ToList();
            }
            return lst;
        }

        public List<EntityInvoiceDetails> SelectPatientInvoiceForEdit(int InvoiceNo)
        {
            return new List<EntityInvoiceDetails>();
        }

        public List<sp_GetAllBedAllocICUResult> GetPatientInvoice()
        {
            return objData.sp_GetAllBedAllocICU().ToList();
        }

        public int InsertInvoice(EntityICUInvoice entInvoice, List<EntityICUInvoiceDetail> lstInvoice, bool IsCash)
        {
            try
            {
                int? Id = 0;
                objData.STP_InserttblICUBedAlloc(Convert.ToInt32(entInvoice.BedId), Convert.ToInt32(0), Convert.ToInt32(0), Convert.ToInt32(0), Convert.ToInt32(entInvoice.PatientId), Convert.ToDateTime(entInvoice.AllocationDate), (entInvoice.DischargeDate), Convert.ToDecimal(entInvoice.NetAmount), Convert.ToDecimal(entInvoice.TotalAmount), Convert.ToInt32(entInvoice.Discount), Convert.ToInt32(entInvoice.Tax1), Convert.ToInt32(entInvoice.Tax2), entInvoice.IsDischarge, ref Id);
                foreach (EntityICUInvoiceDetail item in lstInvoice)
                {
                    tblICUInvoiceDetail obj = new tblICUInvoiceDetail()
                    {
                        ICUBedAllocId = Id,
                        IsDelete = false,
                        ChargesId = item.ChargesId,
                        Amount = item.Amount
                    };
                    objData.tblICUInvoiceDetails.InsertOnSubmit(obj);
                }
                int TransactionId = new PatientInvoiceBLL().GetTransactionId();
                if (IsCash)
                {
                    tblCustomerTransaction objDebit = new tblCustomerTransaction()
                    {
                        TransactionId = TransactionId,
                        IsCash = true,
                        TransactionDocNo = Id,
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
                        TransactionDocNo = Id,
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
                        TransactionDocNo = Id,
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

        public List<EntityBedMaster> GetICUBeds()
        {
            try
            {
                return (from tbl in objData.tblBedMasters
                        join tblRoom in objData.tblRoomMasters
                        on tbl.RoomId equals tblRoom.RoomId
                        join tblRoomCat in objData.tblRoomCategories
                        on tblRoom.CategoryId equals tblRoomCat.PKId
                        where tbl.IsDelete == false
                        && tblRoomCat.IsICU == true
                        select new EntityBedMaster { BedId = tbl.BedId, BedNo = tbl.BedNo }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public decimal GetICUCharges(decimal BedId)
        {
            tblRoomCategory objCat = (from tbl in objData.tblBedMasters
                                      join tblRoom in objData.tblRoomMasters
                                      on tbl.RoomId equals tblRoom.RoomId
                                      join tblRoomCat in objData.tblRoomCategories
                                      on tblRoom.CategoryId equals tblRoomCat.PKId
                                      where tbl.BedId == BedId
                                      && tbl.IsDelete == false
                                      select tblRoomCat).FirstOrDefault();
            return objCat.Rate;
        }

        public List<EntityICUInvoiceDetail> SelectICUInvoiceDetails(int ICUInvoiceNo)
        {
            List<EntityICUInvoiceDetail> lst = (from tbl in objData.tblICUBedAllocs
                                                join tblICUdetails in objData.tblICUInvoiceDetails
                                                on tbl.ICUBedAllocId equals tblICUdetails.ICUBedAllocId
                                                join tblCharges in objData.tblChargeCategories
                                                on tblICUdetails.ChargesId equals tblCharges.ChargesId
                                                where tblICUdetails.IsDelete == false
                                                && tblICUdetails.ICUBedAllocId == ICUInvoiceNo
                                                select new EntityICUInvoiceDetail
                                                {
                                                    ChargesName = tblCharges.ChargeCategoryName,
                                                    ICUSRlNo = tbl.ICUBedAllocId,
                                                    Amount = tblICUdetails.Amount,
                                                    NetAmount = tbl.NetAmount,
                                                    ChargesId = tblCharges.ChargesId,
                                                    Discount = tbl.Discount,
                                                    Vat = tbl.Tax1,
                                                    Service = tbl.Tax2
                                                }).ToList();
            return lst;
        }

        public int UpdateInvoice(List<EntityICUInvoiceDetail> lstEdited, List<EntityICUInvoiceDetail> lstUpdate, EntityICUInvoice entInvoice)
        {
            try
            {
                tblICUBedAlloc objTest = (from tbl in objData.tblICUBedAllocs
                                          where tbl.IsDelete == false
                                          && tbl.ICUBedAllocId == entInvoice.ICUBedAllocId
                                          select tbl).FirstOrDefault();

                List<EntityICUInvoiceDetail> lstTemp = new List<EntityICUInvoiceDetail>();

                List<tblCustomerTransaction> objCust = (from tbl in objData.tblCustomerTransactions
                                                        where tbl.IsDelete == false
                                                        && tbl.TransactionDocNo == entInvoice.ICUBedAllocId
                                                        && tbl.TransactionType == "ICUInvoice"
                                                        select tbl).ToList();
                if (objTest != null && objCust != null)
                {
                    if (objCust.Count == 1)
                    {
                        if (entInvoice.IsCash.Value)
                        {
                            tblCustomerTransaction objC = new tblCustomerTransaction()
                            {
                                PayAmount = lstEdited[0].NetAmount,
                                ReceiptDate = entInvoice.AllocationDate,
                                IsCash = entInvoice.IsCash,
                                IsDelete = false,
                                TransactionDocNo = entInvoice.ICUBedAllocId,
                                TransactionId = objCust[0].TransactionId,
                                TransactionType = "Invoice",
                                PatientId = entInvoice.PatientId,
                            };
                            objData.tblCustomerTransactions.InsertOnSubmit(objC);
                            foreach (tblCustomerTransaction item in objCust)
                            {
                                item.BillAmount = lstEdited[0].NetAmount;
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
                                    item.PayAmount = lstEdited[0].NetAmount;
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
                                    item.PayAmount = lstEdited[0].NetAmount;
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (tblCustomerTransaction item in objCust)
                        {
                            item.BillAmount = lstEdited[0].NetAmount;
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
                                item.PayAmount = lstEdited[0].NetAmount;
                            }
                        }
                    }

                    objTest.AllocationDate = entInvoice.AllocationDate;
                    objTest.TotalAmount = entInvoice.TotalAmount;
                    objTest.NetAmount = lstEdited[0].NetAmount;
                    objTest.Tax1 = entInvoice.Tax1;
                    objTest.Discount = entInvoice.Discount;
                    objTest.Tax2 = entInvoice.Tax2;
                }
                foreach (EntityICUInvoiceDetail item in lstEdited)
                {
                    int cnt = (from tbl in objData.tblICUInvoiceDetails
                               where tbl.ICUBedAllocId == entInvoice.ICUBedAllocId
                               && tbl.ChargesId == item.ChargesId
                               && tbl.IsDelete == false
                               select tbl).ToList().Count;
                    if (cnt == 0)
                    {
                        tblICUInvoiceDetail objNewAdded = new tblICUInvoiceDetail()
                        {
                            ICUBedAllocId = entInvoice.ICUBedAllocId,
                            ChargesId = item.ChargesId,
                            Amount = item.Amount,
                            IsDelete = false
                        };
                        objData.tblICUInvoiceDetails.InsertOnSubmit(objNewAdded);
                        objData.SubmitChanges();
                    }
                    else
                    {
                        lstTemp.Add(item);
                    }
                }

                foreach (EntityICUInvoiceDetail item in lstUpdate)
                {
                    tblICUInvoiceDetail cnt = (from tbl in objData.tblICUInvoiceDetails
                                               where tbl.ICUBedAllocId == entInvoice.ICUBedAllocId
                                               && tbl.ChargesId == item.ChargesId
                                               select tbl).FirstOrDefault();

                    if (cnt != null)
                    {
                        int checkExist = (from tbl in lstTemp
                                          where tbl.ChargesId == item.ChargesId
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

        public List<sp_GetAllBedAllocICUResult> GetICUInvoice(int ICUInvoiceNo)
        {
            List<sp_GetAllBedAllocICUResult> lst = (from tbl in GetPatientInvoice()
                                                    where tbl.ICUBedAllocId == ICUInvoiceNo
                                                    select tbl).ToList();
            return lst;
        }

        public int IsBedAlloc(int BedId)
        {
            int Check = 0;
            tblICUBedAlloc objICU = (from tbl in objData.tblICUBedAllocs
                                     where tbl.BedId.Equals(BedId)
                                     && tbl.IsDischargeDone.Equals(true)
                                     select tbl).FirstOrDefault();

            tblICUBedAlloc objIc = (from tbl in objData.tblICUBedAllocs
                                    select tbl).FirstOrDefault();

            if (objICU != null && objIc == null)
            {
                Check = 1;
            }
            else
            {
                if (objIc == null)
                {
                    Check = 1;
                }
            }

            return Check;
        }


        public List<STP_BindIPDBedForShiftingResult> GetIPDBeds()
        {
            List<STP_BindIPDBedForShiftingResult> entBedMass = new List<STP_BindIPDBedForShiftingResult>();
            try
            {
                entBedMass = (from tbl in objData.STP_BindIPDBedForShifting()
                              select new STP_BindIPDBedForShiftingResult { bedid = tbl.bedid, bedno = tbl.bedno }).ToList();

                return entBedMass;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<STP_BindICUBedForShiftingResult> GetICUBedsForShifting()
        {
            List<STP_BindICUBedForShiftingResult> entBedMass = new List<STP_BindICUBedForShiftingResult>();
            try
            {
                entBedMass = (from tbl in objData.STP_BindICUBedForShifting()
                              select new STP_BindICUBedForShiftingResult { bedid = tbl.bedid, bedno = tbl.bedno }).ToList();

                return entBedMass;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

}