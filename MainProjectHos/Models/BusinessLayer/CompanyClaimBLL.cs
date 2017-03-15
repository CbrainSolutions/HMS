using MainProjectHos.Models.DataModels;
using MainProjectHos.Models.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.BusinessLayer
{
    public class CompanyClaimBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public CompanyClaimBLL()
        {
            objData = new CriticareHospitalDataContext();
        }
        public CriticareHospitalDataContext objData { get; set; }

        public List<EntityPatientAdmit> GetPatientList()
        {
            List<EntityPatientAdmit> lst = null;
            try
            {
                lst = (from tbl in objData.tblPatientAdmitDetails
                       join tblpa in objData.tblPatientMasters
                       on tbl.PatientId equals tblpa.PKId
                       where tbl.IsCompany == true
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
                       join tblI in objData.tblCompanyMasters
                       on tbl.CompanyId equals tblI.PKId
                       where tbl.AdmitId == AdmitId
                       &&
                       tbl.IsDelete == false
                       select new EntityPatientAdmit
                       {
                           CompanyId = tbl.CompanyId,
                           CompanyName = tblI.CompanyName
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
                lst = (from tbl in objData.tblCompanyMasters
                       select new EntityPatientAdmit
                       {
                           CompanyId = tbl.PKId,
                           CompanyName = tbl.CompanyName
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
        public void Update(tblCompanyClaim obj, List<EntityinsuranceClaimDetails> lst)
        {
            try
            {
                tblCompanyClaim objcurrent = (from tbl in objData.tblCompanyClaims
                                              where tbl.ComClaimId == obj.ComClaimId
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
                    tblCompanyClaimDetail objsal = new tblCompanyClaimDetail();
                    objsal = (from tbl in objData.tblCompanyClaimDetails
                              where tbl.ComClaimDetailId == item.ComClaimDetailId
                              && tbl.IsDelete == false
                              select tbl).FirstOrDefault();
                    if (objsal != null)
                    {
                        objsal.BillNo = item.BillNo;
                        objsal.BillType = item.BillType;
                        objsal.BillDate = item.BillDate;
                        objsal.BillAmount = item.BillAmount;
                        objsal.ComClaimId = item.ComClaimId;
                        objsal.IsDelete = item.IsDelete;
                    }
                    else
                    {
                        objsal = new tblCompanyClaimDetail() { BillNo = item.BillNo, BillAmount = item.BillAmount, BillType = item.BillType, BillDate = item.BillDate, IsOtherBill = item.IsOtherBill, ComClaimId = Convert.ToInt32(obj.ComClaimId) };
                        objData.tblCompanyClaimDetails.InsertOnSubmit(objsal);

                    }
                }
                objData.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tblCompanyClaim CheckExistRecord(int AdmitId)
        {
            return (from tbl in objData.tblCompanyClaims
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
        public int? Save(tblCompanyClaim tblins, List<EntityinsuranceClaimDetails> lst)
        {
            int? ComClaimId = 0;
            try
            {
                objData.STP_Insert_tblCompanyClaim(Convert.ToInt32(tblins.AdmitId), Convert.ToInt32(tblins.CompanyId), Convert.ToDateTime(tblins.ClaimDate), Convert.ToDecimal(tblins.Total), tblins.IsApproved, ref ComClaimId);
                foreach (EntityinsuranceClaimDetails item in lst)
                {
                    tblCompanyClaimDetail tbl = new tblCompanyClaimDetail() { BillNo = item.BillNo, BillAmount = item.BillAmount, BillType = item.BillType, BillDate = item.BillDate, IsOtherBill = item.IsOtherBill, ComClaimId = Convert.ToInt32(ComClaimId) };
                    objData.tblCompanyClaimDetails.InsertOnSubmit(tbl);
                }
                objData.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ComClaimId;
        }
        public bool ValidateAllocation(tblCompanyClaim sal)
        {
            try
            {
                var res = (from tbl in objData.tblCompanyClaims
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
                List<EntityInsuranceClaim> lst = (from tbl in objData.tblCompanyClaims
                                                  join tbla in objData.tblPatientAdmitDetails
                                                  on tbl.AdmitId equals tbla.AdmitId
                                                  join tblp in objData.tblPatientMasters
                                                  on tbla.PatientId equals tblp.PKId
                                                  join tblc in objData.tblCompanyMasters
                                                  on tbl.CompanyId equals tblc.PKId
                                                  where tbl.IsDelete == false
                                                  select new EntityInsuranceClaim
                                                  {
                                                      ComClaimId = tbl.ComClaimId,
                                                      PatientName = tblp.PatientFirstName + " " + tblp.PatientMiddleName + " " + tblp.PatientLastName,
                                                      CompanyName = tblc.CompanyName,
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
            try
            {
                EntityInsuranceClaim obj = (from tbl in objData.tblCompanyClaims
                                            where tbl.IsDelete == false
                                            &&
                                            tbl.ComClaimId == ClaimId
                                            select new EntityInsuranceClaim
                                            {
                                                IsApproved = tbl.IsApproved
                                            }).FirstOrDefault();
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EntityinsuranceClaimDetails> GetClaim(int Id)
        {
            int i = 1;
            List<EntityinsuranceClaimDetails> lst = (from tbl in objData.tblCompanyClaimDetails
                                                     where tbl.ComClaimId == Id
                                                     &&
                                                     tbl.IsDelete == false
                                                     select new EntityinsuranceClaimDetails
                                                     {
                                                         BillNo = tbl.BillNo,
                                                         BillType = tbl.BillType,
                                                         BillAmount = tbl.BillAmount,
                                                         BillDate = tbl.BillDate,
                                                         IsOtherBill = tbl.IsOtherBill,
                                                         ComClaimId = tbl.ComClaimId,
                                                         ComClaimDetailId = tbl.ComClaimDetailId

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
                    tblCompanyClaimDetail objsal = new tblCompanyClaimDetail();
                    objsal = (from tbl in objData.tblCompanyClaimDetails
                              where tbl.ComClaimDetailId == item.ComClaimDetailId
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
                        objsal = new tblCompanyClaimDetail() { BillNo = item.BillNo, BillAmount = item.BillAmount, BillType = item.BillType, BillDate = item.BillDate, IsOtherBill = item.IsOtherBill, ComClaimId = Convert.ToInt32(item.ComClaimId) };
                        objData.tblCompanyClaimDetails.InsertOnSubmit(objsal);

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
                           ComClaimId = tbl.ComClaimId,
                           PatientName = tbl.PatientName,
                           CompanyName = tbl.CompanyName,
                           Category = tbl.IsApproved ? "Approved" : "Pending",
                           ClaimDate = tbl.ClaimDate,
                           Total = tbl.Total,
                           ApprovedAmount = tbl.ApprovedAmount != null ? Convert.ToDecimal(tbl.ApprovedAmount) : 0

                       }).ToList();
                return lst;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public EntityPatientAdmit GetAdmitDate(int AdmitId)
        {
            try
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}