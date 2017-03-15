using MainProjectHos.Models.DataModels;
using MainProjectHos.Models.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.BusinessLayer
{
    public class InsuranceClaimBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public InsuranceClaimBLL()
        {
            objData = new CriticareHospitalDataContext();
        }
        public CriticareHospitalDataContext objData { get; set; }

        public EntityPatientAdmit GetAdmitDate(int AdmitId)
        {
            EntityPatientAdmit lst = (from tbl in objData.tblPatientAdmitDetails
                                      where tbl.IsDelete == false
                                      &&
                                      tbl.AdmitId == AdmitId
                                      select new EntityPatientAdmit
                                      {
                                          AdmitDate = tbl.AdmitDate
                                      }).FirstOrDefault();
            return lst;
        }
        public List<EntityPatientAdmit> GetPatientList()
        {
            List<EntityPatientAdmit> lst = null;
            try
            {
                lst = (from tbl in objData.tblPatientAdmitDetails
                       join tblpa in objData.tblPatientMasters
                       on tbl.PatientId equals tblpa.PKId
                       where tbl.IsInsured == true
                       &&
                       tbl.IsDelete == false
                       select new EntityPatientAdmit
                       {
                           AdmitId = tbl.AdmitId,
                           PatientFirstName = tblpa.PatientFirstName + ' ' + tblpa.PatientMiddleName + ' ' + tblpa.PatientLastName
                       }).ToList();
                return lst;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public EntityPatientAdmit GetinsuranceComName(int AdmitId)
        {
            EntityPatientAdmit lst = null;
            try
            {
                lst = (from tbl in objData.tblPatientAdmitDetails
                       join tblI in objData.tblInsuranceComMasters
                       on tbl.InsuranceComId equals tblI.PKId
                       where tbl.AdmitId == AdmitId
                       &&
                       tbl.IsDelete == false
                       select new EntityPatientAdmit
                       {
                           InsuranceComId = tbl.InsuranceComId,
                           CompanyName = tblI.InsuranceDesc
                       }).FirstOrDefault();
                return lst;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<EntityPatientAdmit> GetInsuranceName()
        {
            List<EntityPatientAdmit> lst = null;
            try
            {
                lst = (from tbl in objData.tblInsuranceComMasters
                       select new EntityPatientAdmit
                       {
                           CompanyId = tbl.PKId,
                           CompanyName = tbl.InsuranceDesc
                       }).ToList();
                return lst;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EntityinsuranceClaimDetails> GetDetails(int AdmitId)
        {
            List<EntityinsuranceClaimDetails> lst = null;
            int i = 1;
            try
            {
                lst = (from tbl in objData.tblPatientAdmitDetails
                       join tbla in objData.tblCustomerTransactions
                       on tbl.AdmitId equals tbla.PatientId
                       where tbla.PatientId == AdmitId
                       &&
                       tbla.IsDelete == false
                       &&
                       tbla.BillAmount > 0
                       &&
                       (
                       tbla.TransactionType == "ICUInvoice" || tbla.TransactionType == "TestInvoice" || tbla.TransactionType == "Invoice"
                       )
                       select new EntityinsuranceClaimDetails()
                       {
                           BillNo = Convert.ToInt32(tbla.TransactionDocNo),
                           BillDate = Convert.ToDateTime(tbla.ReceiptDate),
                           BillType = tbla.TransactionType,
                           BillAmount = Convert.ToDecimal(tbla.BillAmount),
                           IsOtherBill = false
                       }).ToList();
                foreach (EntityinsuranceClaimDetails item in lst)
                {
                    item.TempId = i++;
                }
                return lst;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Update(tblInsuranceClaim obj, List<EntityinsuranceClaimDetails> lst)
        {
            try
            {
                tblInsuranceClaim objcurrent = (from tbl in objData.tblInsuranceClaims
                                                where tbl.ClaimId == obj.ClaimId
                                                select tbl).FirstOrDefault();
                if (objcurrent != null)
                {
                    objcurrent.AdmitId = obj.AdmitId;
                    objcurrent.CompanyId = obj.CompanyId;
                    objcurrent.ClaimDate = obj.ClaimDate;
                    objcurrent.Total = obj.Total;

                }

                foreach (EntityinsuranceClaimDetails item in lst)
                {
                    tblInsuranceClaimDetail objsal = new tblInsuranceClaimDetail();
                    objsal = (from tbl in objData.tblInsuranceClaimDetails
                              where tbl.ClaimDetailId == item.ClaimDetailId
                              && tbl.IsDelete == false
                              select tbl).FirstOrDefault();
                    if (objsal != null)
                    {
                        objsal.BillNo = item.BillNo;
                        objsal.BillType = item.BillType;
                        objsal.BillDate = item.BillDate;
                        objsal.BillAmount = item.BillAmount;
                        objsal.ClaimId = item.ClaimId;
                        objsal.IsDelete = item.IsDelete;
                    }
                    else
                    {
                        objsal = new tblInsuranceClaimDetail() { BillNo = item.BillNo, BillAmount = item.BillAmount, BillType = item.BillType, BillDate = item.BillDate, IsOtherBill = item.IsOtherBill, ClaimId = Convert.ToInt32(obj.ClaimId) };
                        objData.tblInsuranceClaimDetails.InsertOnSubmit(objsal);

                    }
                }
                objData.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tblInsuranceClaim CheckExistRecord(int AdmitId)
        {
            return (from tbl in objData.tblInsuranceClaims
                    where tbl.AdmitId == AdmitId

                    select tbl).FirstOrDefault();
        }
        public EntityinsuranceClaimDetails GetData(int BillId)
        {
            return (from tbl in objData.tblInsuranceClaimDetails
                    where tbl.BillNo == BillId
                    select new EntityinsuranceClaimDetails
                    {
                        BillNo = tbl.BillNo,
                        BillDate = tbl.BillDate,
                        BillAmount = tbl.BillAmount,
                        BillType = tbl.BillType,
                        IsOtherBill = tbl.IsOtherBill
                    }).FirstOrDefault();

        }
        public int? Save(tblInsuranceClaim tblins, List<EntityinsuranceClaimDetails> lst)
        {
            int? ClaimId = 0;
            try
            {
                objData.STP_Insert_tblInsuranceClaim(Convert.ToInt32(tblins.AdmitId), Convert.ToInt32(tblins.CompanyId), Convert.ToDateTime(tblins.ClaimDate), Convert.ToDecimal(tblins.Total), tblins.IsApproved, ref ClaimId);
                foreach (EntityinsuranceClaimDetails item in lst)
                {
                    tblInsuranceClaimDetail tbl = new tblInsuranceClaimDetail() { BillNo = item.BillNo, BillAmount = item.BillAmount, BillType = item.BillType, BillDate = item.BillDate, IsOtherBill = item.IsOtherBill, ClaimId = Convert.ToInt32(ClaimId) };
                    objData.tblInsuranceClaimDetails.InsertOnSubmit(tbl);
                }
                objData.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ClaimId;
        }
        public bool ValidateAllocation(tblInsuranceClaim sal)
        {
            try
            {
                var res = (from tbl in objData.tblInsuranceClaims
                           where tbl.AdmitId == sal.AdmitId
                           select tbl).FirstOrDefault();
                if (res != null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public tblPatientAdmitDetail GetEmployee(int AdmitId)
        {
            return (from tbl in objData.tblPatientAdmitDetails
                    where tbl.AdmitId == AdmitId
                    select tbl).FirstOrDefault();
        }

        public List<EntityInsuranceClaim> GetInsurance()
        {
            try
            {
                List<EntityInsuranceClaim> lst = (from tbl in objData.tblInsuranceClaims
                                                  join tbla in objData.tblPatientAdmitDetails
                                                  on tbl.AdmitId equals tbla.AdmitId
                                                  join tblp in objData.tblPatientMasters
                                                  on tbla.PatientId equals tblp.PKId
                                                  join tblc in objData.tblInsuranceComMasters
                                                  on tbl.CompanyId equals tblc.PKId
                                                  where tbl.IsDelete == false
                                                  select new EntityInsuranceClaim
                                                  {
                                                      ClaimId = tbl.ClaimId,
                                                      PatientName = tblp.PatientFirstName + " " + tblp.PatientMiddleName + " " + tblp.PatientLastName,
                                                      CompanyName = tblc.InsuranceDesc,
                                                      ClaimDate = tbl.ClaimDate,
                                                      Total = tbl.Total,
                                                      IsApproved = Convert.ToBoolean(tbl.IsApproved),
                                                      Category = Convert.ToBoolean(tbl.IsApproved) ? "Approved" : "Pending",
                                                      ApprovedAmount = tbl.ApprovedAmount != null ? Convert.ToDecimal(tbl.ApprovedAmount) : 0,
                                                      AdmitId = tbl.AdmitId
                                                  }).ToList();
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EntityInsuranceClaim GetApproval(int ClaimId)
        {
            EntityInsuranceClaim obj = (from tbl in objData.tblInsuranceClaims
                                        where tbl.IsDelete == false
                                        &&
                                        tbl.ClaimId == ClaimId
                                        select new EntityInsuranceClaim
                                        {
                                            IsApproved = tbl.IsApproved
                                        }).FirstOrDefault();
            return obj;
        }

        public List<EntityinsuranceClaimDetails> GetClaim(int Id)
        {
            int i = 1;
            List<EntityinsuranceClaimDetails> lst = (from tbl in objData.tblInsuranceClaimDetails
                                                     where tbl.ClaimId == Id
                                                     &&
                                                     tbl.IsDelete == false
                                                     select new EntityinsuranceClaimDetails
                                                     {
                                                         BillNo = tbl.BillNo,
                                                         BillType = tbl.BillType,
                                                         BillAmount = tbl.BillAmount,
                                                         BillDate = tbl.BillDate,
                                                         IsOtherBill = tbl.IsOtherBill,
                                                         ClaimId = tbl.ClaimId,
                                                         ClaimDetailId = tbl.ClaimDetailId

                                                     }).ToList();
            foreach (EntityinsuranceClaimDetails item in lst)
            {
                item.TempId = i++;
            }
            return lst;
        }

        public void Update(List<EntityinsuranceClaimDetails> lst)
        {
            try
            {
                foreach (EntityinsuranceClaimDetails item in lst)
                {
                    tblInsuranceClaimDetail objsal = new tblInsuranceClaimDetail();
                    objsal = (from tbl in objData.tblInsuranceClaimDetails
                              where tbl.ClaimDetailId == item.ClaimDetailId
                              && tbl.IsDelete == false
                              select tbl).FirstOrDefault();
                    if (objsal != null)
                    {
                        objsal.BillNo = item.BillNo;
                        objsal.BillType = item.BillType;
                        objsal.BillDate = item.BillDate;
                        objsal.BillAmount = item.BillAmount;
                        objsal.IsDelete = item.IsDelete;
                    }
                    else
                    {
                        objsal = new tblInsuranceClaimDetail() { BillNo = item.BillNo, BillAmount = item.BillAmount, BillType = item.BillType, BillDate = item.BillDate, IsOtherBill = item.IsOtherBill, ClaimId = Convert.ToInt32(item.ClaimId) };
                        objData.tblInsuranceClaimDetails.InsertOnSubmit(objsal);

                    }
                }
                objData.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<EntityInsuranceClaim> GetInsurance(string Prefix)
        {
            List<EntityInsuranceClaim> lst = null;
            try
            {
                lst = (from tbl in GetInsurance()
                       where tbl.ClaimId.ToString().ToUpper().Contains(Prefix.ToUpper())
                       || tbl.PatientName.ToString().ToUpper().Contains(Prefix.ToUpper()) ||
                       tbl.CompanyName.ToString().ToUpper().Contains(Prefix.ToUpper()) ||
                       tbl.Category.ToString().ToUpper().Contains(Prefix.ToUpper())
                       select new EntityInsuranceClaim()
                       {
                           ClaimId = tbl.ClaimId,
                           PatientName = tbl.PatientName,
                           CompanyName = tbl.CompanyName,
                           Category = tbl.IsApproved ? "Approved" : "Pending",
                           ClaimDate = tbl.ClaimDate,
                           Total = tbl.Total,
                           ApprovedAmount = 0

                       }).ToList();
                return lst;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}