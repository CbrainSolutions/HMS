using MainProjectHos.Models.DataLayer;
using MainProjectHos.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.BusinessLayer
{
    public class PatientInvoiceBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();

        public PatientInvoiceBLL()
        {
            objData = new CriticareHospitalDataContext();
        }

        public CriticareHospitalDataContext objData { get; set; }

        public List<EntityPatientMaster> GetPatientList(bool IsDischarge)
        {
            try
            {
                List<EntityPatientMaster> lst = null;
                if (IsDischarge)
                {
                    lst = (from tbl in objData.tblPatientMasters
                           join tblAdmint in objData.tblPatientAdmitDetails
                           on tbl.PKId equals tblAdmint.PatientId
                           orderby tblAdmint.AdmitId descending
                           select new EntityPatientMaster { PatientId = tblAdmint.AdmitId, PatientFirstName = tbl.PatientFirstName + ' ' + tbl.PatientMiddleName + ' ' + tbl.PatientLastName }).ToList();
                }
                else
                {
                    lst = (from tbl in objData.tblPatientMasters
                           join tblAdmint in objData.tblPatientAdmitDetails
                           on tbl.PKId equals tblAdmint.PatientId
                           where tblAdmint.IsDischarge == false
                           orderby tblAdmint.AdmitId descending
                           select new EntityPatientMaster { PatientId = tblAdmint.AdmitId, PatientFirstName = tbl.PatientFirstName + ' ' + tbl.PatientMiddleName + ' ' + tbl.PatientLastName }).ToList();
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EntityPatientMaster> GetPatientList1()
        {
            try
            {
                List<EntityPatientMaster> lst = null;

                lst = (from tbl in objData.tblPatientMasters
                       join tblAdmint in objData.tblPatientAdmitDetails
                       on tbl.PKId equals tblAdmint.PatientId
                       orderby tblAdmint.AdmitId descending
                       select new EntityPatientMaster { PatientId = tblAdmint.AdmitId, PatientFirstName = tbl.PatientFirstName + ' ' + tbl.PatientMiddleName + ' ' + tbl.PatientLastName }).ToList();

                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EntityChargeCategory> GetOtherChargeList()
        {
            try
            {
                List<EntityChargeCategory> lst = (from tbl in objData.tblChargeCategories
                                                  select new EntityChargeCategory { ChargesId = tbl.ChargesId, ChargeCategoryName = tbl.ChargeCategoryName }).ToList();

                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EntityPatientInvoice> GetRegistrationCharges()
        {
            try
            {
                List<EntityPatientInvoice> lst = (from tbl in objData.tblRoomCategories
                                                  where tbl.CategoryDesc == "Registration Fee"
                                                  select new EntityPatientInvoice { Description = tbl.CategoryDesc, Amount = tbl.Rate }).ToList();

                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EntityPatientInvoice> GetBedCharges(int PatID)
        {
            List<EntityPatientInvoice> lst = (from tbl in objData.tblBedAllocationToPatients
                                              join tblR in objData.tblRoomMasters
                                              on tbl.RoomId equals tblR.RoomId
                                              join tblC in objData.tblRoomCategories
                                              on tblR.CategoryId equals tblC.PKId
                                              join tblP in objData.tblPatientAdmitDetails
                                              on tbl.PatientId equals tblP.AdmitId
                                              where tbl.PatientId.Equals(PatID)
                                              && tbl.DischargeDate == null
                                              orderby tbl.BedAllocId ascending
                                              select new EntityPatientInvoice { BedAllocId = tbl.BedAllocId, Amount = tblC.Rate, AllocDate = tbl.AllocationDate.Value }).ToList();
            return lst;
        }

        public List<EntityPatientInvoice> GetOTBedCharges(int PatID)
        {
            List<EntityPatientInvoice> lst = (from tbl in objData.tblBedAllocationToPatients
                                              join tblbed in objData.tblBedMasters
                                              on tbl.BedId equals tblbed.BedId
                                              join tblR in objData.tblRoomMasters
                                              on tbl.RoomId equals tblR.RoomId
                                              join tblC in objData.tblRoomCategories
                                              on tblR.CategoryId equals tblC.PKId
                                              join tblP in objData.tblPatientAdmitDetails
                                              on tbl.PatientId equals tblP.AdmitId
                                              where tbl.PatientId.Equals(PatID)
                                              && tbl.DischargeDate == null
                                              && tblC.IsOT == false
                                              && tblC.IsICU == false
                                              //&& tbl.ShiftDate==null
                                              orderby tbl.BedAllocId ascending
                                              select new EntityPatientInvoice { BedAllocId = tbl.BedAllocId, Amount = tblC.Rate, AllocDate = tbl.AllocationDate.Value, Description = tblbed.BedNo }).ToList();
            return lst;
        }

        public List<EntityPatientInvoice> GetConsultChargesIPD(int PatID)
        {
            List<EntityPatientInvoice> lst = (from tbl in objData.tblAllocConsultDoctorDetails
                                                  //join tblB in objData.tblBedAllocationToPatients
                                                  //on tbl.Admi equals tblB.PatientId
                                              where tbl.AdmitId.Equals(PatID)
                                              select new EntityPatientInvoice { DocAllocId = Convert.ToInt32(tbl.AdmitId), Amount = tbl.ConsultCharges, AllocDate = tbl.Consult_Date.Value }).ToList();
            return lst;
        }

        public List<EntityPatientInvoice> GetOperaCharges(int PatID)
        {
            List<EntityPatientInvoice> lst = (from tbl in objData.tblOTBedAllocs
                                              join tblCat in objData.tblOperationCategories
                                                  on tbl.OperCatId equals tblCat.CategoryId
                                              join tblOpera in objData.tblOperationMasters
                                              on tbl.OperId equals tblOpera.OperationId
                                              where tbl.PatientId.Equals(PatID)
                                              select new EntityPatientInvoice { OTBedAllocId = tbl.OTBedAllocId, Amount = tblOpera.Price, Description = tblOpera.OperationName }).ToList();
            return lst;
        }

        public EntityPatientMaster GetPatientCate(int Pat)
        {
            EntityPatientMaster cate = (from tbl in objData.tblPatientAdmitDetails
                                        where tbl.AdmitId.Equals(Pat) &&
                                         tbl.IsDischarge == false
                                        select new EntityPatientMaster { PatientId = tbl.AdmitId, PatientType = tbl.IsIPD.Value ? "IPD" : "OPD" }).SingleOrDefault();
            return cate;
        }

        public EntityPatientInvoice GetWardName(int PatID)
        {
            EntityPatientInvoice lst = (from tbl in objData.tblBedAllocationToPatients
                                        join tblbed in objData.tblBedMasters
                                        on tbl.BedId equals tblbed.BedId
                                        join tblR in objData.tblRoomMasters
                                        on tbl.RoomId equals tblR.RoomId
                                        join tblC in objData.tblRoomCategories
                                        on tblR.CategoryId equals tblC.PKId
                                        join tblP in objData.tblPatientAdmitDetails
                                        on tbl.PatientId equals tblP.AdmitId
                                        where tbl.PatientId.Equals(PatID)
                                        && tbl.DischargeDate == null
                                        && tblC.IsOT == false
                                        && tblC.IsICU == false
                                        && tbl.ShiftDate == null
                                        orderby tbl.BedAllocId ascending
                                        select new EntityPatientInvoice { Description = tblC.CategoryDesc }).FirstOrDefault();
            return lst;
        }

        public EntityPatientInvoice GetChargesForCate(int charge)
        {
            EntityPatientInvoice cate = (from tbl in objData.tblChargeCategories
                                         where tbl.ChargesId.Equals(charge)
                                         && tbl.IsDelete == false
                                         select new EntityPatientInvoice { Amount = tbl.Charges }).SingleOrDefault();
            return cate;
        }

        public List<EntityPatientInvoice> GetConsultChargesOPD(int PatID)
        {
            List<EntityPatientInvoice> lst = (from tbl in objData.tblPatientMasters
                                              join tblAdmit in objData.tblPatientAdmitDetails
                                              on tbl.PKId equals tblAdmit.PatientId
                                              join tblDocCat in objData.tblDocCategories
                                              on tbl.DeptCategory equals tblDocCat.OperaCatId
                                              //join tblDocCat1 in objData.tblDocCategories
                                              //on tbl.DeptDoctorId equals tblDocCat1.DocId
                                              where tblAdmit.AdmitId.Equals(PatID)
                                              //&& tbl.DeptCategory.Equals(tblDocCat.OperaCatId)
                                              //&& tbl.DeptDoctorId.Equals(tblDocCat.DocId)
                                              select new EntityPatientInvoice { DocAllocId = Convert.ToInt32(tblAdmit.AdmitId), Amount = tblDocCat.Charges }).ToList();
            return lst;
        }

        public List<EntityPatientInvoice> GetOtherCharges()
        {
            List<EntityPatientInvoice> lst = (from tbl in objData.tblChargeCategories
                                              select new EntityPatientInvoice { OtherID = tbl.ChargesId }).ToList();
            return lst;
        }

        #region GetTransactionId()
        public int GetTransactionId()
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
                                           select tbl).Max(e => e.TransactionId).Value);

                    TId++;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return TId;
        }
        #endregion

        public int InsertInvoice(EntityPatientInvoice entInvoice, List<EntityInvoiceDetails> lstInvoice, bool IsCash)
        {
            try
            {
                int? BillNo = 0;
                objData.STP_InsertInvoice(Convert.ToDateTime(entInvoice.BillDate),
                    ref BillNo, Convert.ToString(entInvoice.BillType), Convert.ToInt32(entInvoice.PatientId), Convert.ToString(entInvoice.PreparedByName),
                    Convert.ToDecimal(entInvoice.Amount), Convert.ToDecimal(entInvoice.Discount),
                    //Convert.ToDecimal(entInvoice.Vat), Convert.ToDecimal(entInvoice.Service), 
                    Convert.ToDecimal(entInvoice.NetAmount), Convert.ToDecimal(entInvoice.TotalAdvance),
                    Convert.ToDecimal(entInvoice.BalanceAmount), Convert.ToDecimal(entInvoice.ReceivedAmount),
                    Convert.ToDecimal(entInvoice.RefundAmount),
                    true, Convert.ToDecimal(entInvoice.FixedDiscount));
                foreach (EntityInvoiceDetails entInvoiceDetails in lstInvoice)
                {
                    tblPatientInvoiceDetail obj = new tblPatientInvoiceDetail()
                    {
                        BillNo = BillNo,
                        BedAllocId = entInvoiceDetails.BedAllocId,
                        OTAllocId = entInvoiceDetails.OTBedAllocId,
                        DocAllocId = entInvoiceDetails.DocAllocationId,
                        OtherChargesId = entInvoiceDetails.OtherChargesId,
                        Amount = entInvoiceDetails.Amount,
                        NetAmount = entInvoice.NetAmount,
                        ChargePerDay = entInvoiceDetails.PerDayCharge,
                        Remarks = entInvoiceDetails.Remarks,
                        NoOfDays = entInvoiceDetails.NoOfDays,
                        Quantity = entInvoiceDetails.Quantity,
                        IsDelete = false
                    };
                    objData.tblPatientInvoiceDetails.InsertOnSubmit(obj);
                    if (entInvoice.BillType == "Original")
                    {
                        if (entInvoiceDetails.BedAllocId > 0)
                        {
                            int i = new BedStatusBLL().DischargePatient(entInvoice.PatientId, entInvoice.BillDate);
                        }
                    }
                }
                //int i = new BedStatusBLL().DischargePatient(entInvoiceDetails.PatientID, entInvoice.BillDate);
                if (entInvoice.BillType != "Estimated")
                {
                    int TransactionId = GetTransactionId();
                    if (IsCash)
                    {
                        tblCustomerTransaction objDebit = new tblCustomerTransaction()
                        {
                            TransactionId = TransactionId,
                            IsCash = true,
                            TransactionDocNo = BillNo,
                            TransactionType = "Invoice",
                            BillAmount = entInvoice.NetAmount,
                            PayAmount = entInvoice.NetAmount,
                            Discount = entInvoice.FixedDiscount,
                            PatientId = entInvoice.PatientId,
                            PreparedByName = entInvoice.PreparedByName,
                            IsDelete = false,
                            ReceiptDate = entInvoice.BillDate,
                        };
                        objData.tblCustomerTransactions.InsertOnSubmit(objDebit);
                        //objData.SubmitChanges();
                        //tblCustomerTransaction objCrReceipt = new tblCustomerTransaction()
                        //{
                        //    TransactionId = TransactionId,
                        //    IsCash = true,
                        //    TransactionDocNo = BillNo,
                        //    TransactionType = "Invoice",
                        //    PayAmount = entInvoice.NetAmount,
                        //    BillAmount=0,
                        //    PatientId = entInvoice.PatientId,
                        //    IsDelete = false,
                        //    ReceiptDate = entInvoice.BillDate,
                        //};
                        //objData.tblCustomerTransactions.InsertOnSubmit(objCrReceipt);
                        //objData.SubmitChanges();
                    }
                    else
                    {
                        tblCustomerTransaction objDebit = new tblCustomerTransaction()
                        {
                            TransactionId = TransactionId,
                            IsCash = false,
                            TransactionDocNo = BillNo,
                            TransactionType = "Invoice",
                            BillAmount = entInvoice.NetAmount,
                            Discount = entInvoice.FixedDiscount,
                            PatientId = entInvoice.PatientId,
                            PreparedByName = entInvoice.PreparedByName,
                            IsDelete = false,
                            ReceiptDate = entInvoice.BillDate,
                        };
                        objData.tblCustomerTransactions.InsertOnSubmit(objDebit);
                        //objData.SubmitChanges();
                    }
                }

                tblPatientAdmitDetail admit = (from tbl in objData.tblPatientAdmitDetails
                                               where tbl.IsDelete == false
                                               && tbl.IsDischarge == false
                                               && tbl.AdmitId == entInvoice.PatientId
                                               select tbl).FirstOrDefault();
                if (entInvoice.BillType == "Estimated")
                {
                    if (admit != null)
                    {
                        admit.IsDischarge = false;

                    }
                }

                if (entInvoice.BillType == "Original")
                {
                    if (admit != null)
                    {
                        admit.IsDischarge = true;

                    }
                    //if (admit != null && entInvoice.PatientType == "OPD")
                    //{
                    //    admit.IsDischarge = false;

                    //}
                }
                objData.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<STP_GetPatientInvoiceResult> GetPatientInvoice()
        {
            List<STP_GetPatientInvoiceResult> lst = null;
            try
            {
                lst = objData.STP_GetPatientInvoice().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }

        public List<EntityInvoiceDetails> SelectPatientInvoiceForEdit(int BillNo)
        {
            List<EntityInvoiceDetails> lst = new List<EntityInvoiceDetails>();
            try
            {
                lst = (from tbl in objData.STP_EditPatientInvoice(Convert.ToInt32(BillNo))
                       select new EntityInvoiceDetails
                       {
                           BillSRNo = Convert.ToInt32(tbl.BillSRNo),
                           BillNo = Convert.ToInt32(tbl.BillNo),
                           BillType = Convert.ToString(tbl.BillType),
                           PreparedByName = Convert.ToString(tbl.PreparedByName),
                           PatientID = Convert.ToInt32(tbl.PatientId),
                           PatientName = Convert.ToString(tbl.FullName),
                           Total = Convert.ToDecimal(tbl.Total),
                           Amount = Convert.ToDecimal(tbl.Amount),
                           TotalAdvance = Convert.ToDecimal(tbl.TotalAdvance),
                           BalanceAmount = Convert.ToDecimal(tbl.BalanceAmount),
                           ReceivedAmount = Convert.ToDecimal(tbl.ReceivedAmount),
                           RefundAmount = Convert.ToDecimal(tbl.RefundAmount),
                           Discount = Convert.ToDecimal(tbl.FixedDiscount),
                           //Vat = Convert.ToDecimal(tbl.Vat),
                           //Service = Convert.ToDecimal(tbl.Service),
                           NetAmount = Convert.ToDecimal(tbl.NetAmount),
                           BedNo = tbl.BedNo,
                           ChargesName = tbl.ChargesName,
                           OtherId = Convert.ToInt32(tbl.OtherChargesId),
                           OtherChargesId = Convert.ToInt32(tbl.OtherChargesId),
                           Quantity = Convert.ToInt32(tbl.Quantity),
                           NoOfDays = Convert.ToInt32(tbl.NoOfDays),
                           Remarks = Convert.ToString(tbl.Remarks),
                           PerDayCharge = Convert.ToDecimal(tbl.ChargePerDay),
                           IsDelete = false,
                           BedAllocId = Convert.ToInt32(tbl.BedAllocId),
                           OTBedAllocId = Convert.ToInt32(tbl.OTAllocId),
                           DocAllocationId = Convert.ToInt32(tbl.DocAllocId),
                           PatientType = Convert.ToString(tbl.PatientType)
                       }).ToList();

                int i = 1;
                foreach (EntityInvoiceDetails item in lst)
                {
                    item.TempId = i;
                    i++;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }

        public int UpdateInvoice(List<EntityInvoiceDetails> lstEdited, List<EntityInvoiceDetails> lstUpdate)
        {
            try
            {
                tblPatientInvoice objTest = (from tbl in objData.tblPatientInvoices
                                             where tbl.IsDelete == false
                                             && tbl.BillNo == lstEdited[0].BillNo
                                             select tbl).FirstOrDefault();

                List<EntityInvoiceDetails> lstTemp = new List<EntityInvoiceDetails>();

                List<tblCustomerTransaction> objCust = (from tbl in objData.tblCustomerTransactions
                                                        where tbl.IsDelete == false
                                                        && tbl.TransactionDocNo == lstEdited[0].BillNo
                                                        && tbl.TransactionType == "Invoice"
                                                        select tbl).ToList();
                if (objTest != null && objCust != null)
                {
                    if (objCust.Count == 1)
                    {
                        if (lstEdited[0].IsCash)
                        {
                            tblCustomerTransaction objC = new tblCustomerTransaction()
                            {
                                PayAmount = lstEdited[0].NetAmount,
                                ReceiptDate = lstEdited[0].BillDate,
                                IsCash = lstEdited[0].IsCash,
                                IsDelete = false,
                                TransactionDocNo = lstEdited[0].BillNo,
                                TransactionId = objCust[0].TransactionId,
                                TransactionType = "Invoice",
                                PatientId = lstEdited[0].PatientID,
                            };
                            objData.tblCustomerTransactions.InsertOnSubmit(objC);
                            foreach (tblCustomerTransaction item in objCust)
                            {
                                item.BillAmount = lstEdited[0].NetAmount;
                                item.ReceiptDate = lstEdited[0].BillDate;
                                item.IsCash = lstEdited[0].IsCash;
                                if (item.IsCash == false)
                                {
                                    if (item.PayAmount > 0)
                                    {
                                        item.IsDelete = true;
                                    }
                                }
                                else
                                {
                                    item.PayAmount = 0;
                                }
                            }
                        }
                        else
                        {
                            foreach (tblCustomerTransaction item in objCust)
                            {
                                item.BillAmount = lstEdited[0].NetAmount;
                                item.ReceiptDate = lstEdited[0].BillDate;
                                item.IsCash = lstEdited[0].IsCash;
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
                                        item.PayAmount = lstEdited[0].NetAmount;
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
                            item.BillAmount = lstEdited[0].NetAmount;
                            item.ReceiptDate = lstEdited[0].BillDate;
                            item.IsCash = lstEdited[0].IsCash;
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
                                    item.PayAmount = lstEdited[0].NetAmount;
                                }
                                else
                                {
                                    item.PayAmount = 0;
                                }
                            }
                        }
                    }

                    objTest.BillDate = lstEdited[0].BillDate;
                    objTest.Amount = lstEdited[0].Total;
                    objTest.NetAmount = lstEdited[0].NetAmount;
                    //objTest.Vat = lstEdited[0].Vat;
                    objTest.Discount = lstEdited[0].Discount;
                    //objTest.Service = lstEdited[0].Service;
                }
                foreach (EntityInvoiceDetails item in lstEdited)
                {
                    int cnt = (from tbl in objData.tblPatientInvoiceDetails
                               where tbl.BillNo == item.BillNo
                               && tbl.OtherChargesId == item.OtherChargesId
                               && tbl.IsDelete == false
                               select tbl).ToList().Count;
                    if (cnt == 0)
                    {
                        tblPatientInvoiceDetail objNewAdded = new tblPatientInvoiceDetail()
                        {
                            BillNo = lstEdited[0].BillNo,
                            BedAllocId = item.BedAllocId,
                            OTAllocId = item.OTBedAllocId,
                            DocAllocId = item.DocAllocationId,
                            OtherChargesId = item.OtherChargesId,
                            Amount = item.Amount,
                            NetAmount = item.NetAmount,
                            IsDelete = false
                        };
                        objData.tblPatientInvoiceDetails.InsertOnSubmit(objNewAdded);
                        objData.SubmitChanges();
                    }
                    else
                    {
                        lstTemp.Add(item);
                    }
                }

                foreach (EntityInvoiceDetails item in lstUpdate)
                {
                    tblPatientInvoiceDetail cnt = (from tbl in objData.tblPatientInvoiceDetails
                                                   where tbl.BillNo == item.BillNo
                                                   && tbl.OtherChargesId == item.OtherChargesId
                                                   select tbl).FirstOrDefault();

                    if (cnt != null)
                    {
                        int checkExist = (from tbl in lstTemp
                                          where tbl.OtherChargesId == item.OtherChargesId
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

        public int GetBillNo(int Patient_ID)
        {
            EntityPatientInvoice entIn = new EntityPatientInvoice();

            entIn = (from tbl in objData.tblPatientInvoices
                     where tbl.PatientId.Equals(Patient_ID) && tbl.BillType != "Estimated"
                     select new EntityPatientInvoice { BillNo = tbl.BillNo }).FirstOrDefault();

            if (entIn != null)
            {
                return entIn.BillNo;
            }
            else
            {
                return 0;
            }
        }

        public int UpdateInvoice(EntityPatientInvoice entInvoice, List<EntityInvoiceDetails> lst)
        {
            int i = 0;
            try
            {
                tblPatientInvoice objTest = (from tbl in objData.tblPatientInvoices
                                             where tbl.IsDelete == false
                                             && tbl.BillNo == entInvoice.BillNo
                                             select tbl).FirstOrDefault();
                if (objTest != null)
                {
                    objTest.Amount = entInvoice.Amount;
                    objTest.BillType = entInvoice.BillType;
                    objTest.NetAmount = entInvoice.NetAmount;
                    objTest.TotalAdvance = entInvoice.TotalAdvance;
                    objTest.BalanceAmount = entInvoice.BalanceAmount;
                    objTest.ReceivedAmount = entInvoice.ReceivedAmount;
                    objTest.RefundAmount = entInvoice.RefundAmount;
                    objTest.FixedDiscount = entInvoice.FixedDiscount;
                    objTest.PreparedByName = entInvoice.PreparedByName;
                    //objTest.Service = entInvoice.Service;
                    //objTest.Vat = entInvoice.Vat;
                    if (entInvoice.BillType != "Estimated")
                    {
                        int TransactionId = GetTransactionId();
                        if (entInvoice.IsCash)
                        {
                            tblCustomerTransaction objDebit = new tblCustomerTransaction()
                            {
                                TransactionId = TransactionId,
                                IsCash = true,
                                TransactionDocNo = entInvoice.BillNo,
                                TransactionType = "Invoice",
                                BillAmount = entInvoice.NetAmount,
                                PayAmount = entInvoice.NetAmount,
                                Discount = entInvoice.FixedDiscount,
                                PatientId = entInvoice.PatientId,
                                PreparedByName = entInvoice.PreparedByName,
                                IsDelete = false,
                                ReceiptDate = entInvoice.BillDate,
                            };
                            objData.tblCustomerTransactions.InsertOnSubmit(objDebit);

                        }
                        else
                        {
                            tblCustomerTransaction objDebit = new tblCustomerTransaction()
                            {
                                TransactionId = TransactionId,
                                IsCash = false,
                                TransactionDocNo = entInvoice.BillNo,
                                TransactionType = "Invoice",
                                BillAmount = entInvoice.NetAmount,
                                Discount = entInvoice.FixedDiscount,
                                PatientId = entInvoice.PatientId,
                                PreparedByName = entInvoice.PreparedByName,
                                IsDelete = false,
                                ReceiptDate = entInvoice.BillDate,
                            };
                            objData.tblCustomerTransactions.InsertOnSubmit(objDebit);
                            //objData.SubmitChanges();
                        }
                    }
                    else
                    {
                        List<tblCustomerTransaction> objCust = (from tbl in objData.tblCustomerTransactions
                                                                where tbl.IsDelete == false
                                                                && tbl.TransactionDocNo == entInvoice.BillNo
                                                                && tbl.TransactionType == "Invoice"
                                                                select tbl).ToList();
                        if (objCust != null)
                        {
                            if (objCust.Count == 1)
                            {
                                if (entInvoice.IsCash)
                                {
                                    tblCustomerTransaction objC = new tblCustomerTransaction()
                                    {
                                        PayAmount = entInvoice.NetAmount,
                                        BillAmount = entInvoice.NetAmount,
                                        Discount = entInvoice.FixedDiscount,
                                        ReceiptDate = entInvoice.BillDate,
                                        IsCash = entInvoice.IsCash,
                                        IsDelete = false,
                                        TransactionDocNo = entInvoice.BillNo,
                                        TransactionId = objCust[0].TransactionId,
                                        TransactionType = "Invoice",
                                        PatientId = entInvoice.PatientId,
                                        PreparedByName = entInvoice.PreparedByName,
                                    };
                                    objData.tblCustomerTransactions.InsertOnSubmit(objC);
                                    foreach (tblCustomerTransaction item in objCust)
                                    {
                                        item.BillAmount = entInvoice.NetAmount;
                                        item.ReceiptDate = entInvoice.BillDate;
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
                                            item.PayAmount = 0;
                                        }
                                    }
                                }
                                else
                                {
                                    foreach (tblCustomerTransaction item in objCust)
                                    {
                                        item.BillAmount = entInvoice.NetAmount;
                                        item.ReceiptDate = entInvoice.BillDate;
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
                                            if (item.PayAmount > 0)
                                            {
                                                item.PayAmount = entInvoice.NetAmount;
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
                                    item.BillAmount = entInvoice.NetAmount;
                                    item.ReceiptDate = entInvoice.BillDate;
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
                                        if (item.PayAmount > 0)
                                        {
                                            item.PayAmount = entInvoice.NetAmount;
                                        }
                                        else
                                        {
                                            item.PayAmount = 0;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    foreach (EntityInvoiceDetails item in lst)
                    {
                        if (item.BillSRNo > 0)
                        {
                            tblPatientInvoiceDetail cnt = (from tbl in objData.tblPatientInvoiceDetails
                                                           where tbl.BillNo == item.BillNo
                                                           && tbl.BillSRNo == item.BillSRNo
                                                           && tbl.IsDelete == false
                                                           select tbl).FirstOrDefault();
                            if (cnt != null)
                            {
                                cnt.BillNo = entInvoice.BillNo;
                                cnt.Quantity = item.Quantity;
                                cnt.NoOfDays = item.NoOfDays;
                                cnt.Remarks = item.Remarks;
                                cnt.Amount = item.Amount;
                                cnt.ChargePerDay = item.PerDayCharge;
                                cnt.OtherChargesId = item.OtherChargesId;
                                cnt.IsDelete = item.IsDelete;
                            }
                        }
                        else
                        {
                            tblPatientInvoiceDetail objNewAdded = new tblPatientInvoiceDetail()
                            {
                                BillNo = entInvoice.BillNo,
                                BedAllocId = item.BedAllocId,
                                OTAllocId = item.OTBedAllocId,
                                DocAllocId = item.DocAllocationId,
                                OtherChargesId = item.OtherChargesId,
                                Amount = item.Amount,
                                ChargePerDay = item.PerDayCharge,
                                NetAmount = item.NetAmount,
                                Remarks = item.Remarks,
                                NoOfDays = item.NoOfDays,
                                Quantity = item.Quantity,
                                IsDelete = false
                            };
                            objData.tblPatientInvoiceDetails.InsertOnSubmit(objNewAdded);
                            if (entInvoice.BillType == "Original")
                            {
                                if (objNewAdded.BedAllocId > 0)
                                {
                                    int p = new BedStatusBLL().DischargePatient(entInvoice.PatientId, entInvoice.BillDate);
                                }
                            }
                        }
                    }

                    tblPatientAdmitDetail admit = (from tbl in objData.tblPatientAdmitDetails
                                                   where tbl.IsDelete == false
                                                   && tbl.AdmitId == entInvoice.PatientId
                                                   select tbl).FirstOrDefault();
                    if (entInvoice.BillType == "Estimated")
                    {
                        if (admit != null)
                        {
                            admit.IsDischarge = false;
                        }
                    }

                    if (entInvoice.BillType == "Original")
                    {
                        if (admit != null)
                        {
                            admit.IsDischarge = true;
                        }
                    }

                    objData.SubmitChanges();
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

        public int UpdateOPDRefund(EntityPatientInvoice entInvoice, List<EntityInvoiceDetails> lst)
        {
            int i = 0;
            try
            {
                tblPatientInvoice objTest = (from tbl in objData.tblPatientInvoices
                                             where tbl.IsDelete == false
                                             && tbl.BillNo == entInvoice.BillNo
                                             select tbl).FirstOrDefault();
                if (objTest != null)
                {
                    objTest.Amount = 0;
                    objTest.NetAmount = 0;
                    objTest.ReceivedAmount = 0;
                    objTest.RefundAmount = entInvoice.NetAmount;
                    objTest.TotalAdvance = 0;

                    tblCustomerTransaction objCust = (from tbl in objData.tblCustomerTransactions
                                                      where tbl.IsDelete == false
                                                      && tbl.TransactionDocNo == entInvoice.BillNo
                                                      && tbl.TransactionType == "Invoice"
                                                      select tbl).FirstOrDefault();


                    if (objCust != null)
                    {
                        objCust.PayAmount = 0;
                        objCust.BillAmount = 0;
                    }

                    foreach (EntityInvoiceDetails item in lst)
                    {
                        if (item.BillSRNo > 0)
                        {
                            tblPatientInvoiceDetail cnt = (from tbl in objData.tblPatientInvoiceDetails
                                                           where tbl.BillNo == item.BillNo
                                                           && tbl.BillSRNo == item.BillSRNo
                                                           && tbl.IsDelete == false
                                                           select tbl).FirstOrDefault();
                            if (cnt != null)
                            {
                                cnt.BillNo = entInvoice.BillNo;
                                cnt.ChargePerDay = 0;
                                cnt.NetAmount = 0;
                                cnt.Amount = 0;
                                cnt.OtherChargesId = item.OtherChargesId;
                                cnt.IsDelete = item.IsDelete;
                            }
                            objData.SubmitChanges();
                        }
                    }
                    objData.SubmitChanges();
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
    }
}