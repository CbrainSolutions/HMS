using MainProjectHos.Models.DataLayer;
using MainProjectHos.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.BusinessLayer
{
    public class AllocConsultDoctorToPatientBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public AllocConsultDoctorToPatientBLL()
        {
            objData = new CriticareHospitalDataContext();
        }
        public CriticareHospitalDataContext objData { get; set; }

        public tblAllocConsultDoctor GetPrescriptionInfo(int SrNo)
        {
            tblAllocConsultDoctor obj = null;
            try
            {
                obj = (from tbl in objData.tblAllocConsultDoctors
                       where tbl.IsDelete == false
                       && tbl.SrNo == SrNo
                       select tbl).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

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
                lst = (from tbl in objData.tblBedAllocationToPatients
                       join tblad in objData.tblPatientAdmitDetails
                       on tbl.PatientId equals tblad.AdmitId
                       join tblpa in objData.tblPatientMasters
                       on tblad.PatientId equals tblpa.PKId
                       orderby tblpa.PatientFirstName
                       where tbl.IsDelete == false
                       select new EntityPatientAdmit
                       {
                           AdmitId = Convert.ToInt32(tbl.PatientId),
                           PatientFirstName = tblpa.PatientFirstName + ' ' + tblpa.PatientMiddleName + ' ' + tblpa.PatientLastName
                       }).ToList();
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EntityPatientAdmit> GetPatientListNursing()
        {
            List<EntityPatientAdmit> lst = null;
            try
            {
                lst = (from tbl in objData.tblBedAllocationToPatients
                       join tblad in objData.tblPatientAdmitDetails
                       on tbl.PatientId equals tblad.AdmitId
                       join tblpa in objData.tblPatientMasters
                       on tblad.PatientId equals tblpa.PKId
                       where tbl.IsDelete == false
                       select new EntityPatientAdmit
                       {
                           AdmitId = Convert.ToInt32(tblpa.PKId),
                           PatientFirstName = tblpa.PatientFirstName + ' ' + tblpa.PatientMiddleName + ' ' + tblpa.PatientLastName
                       }).ToList();
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(tblAllocConsultDoctor obj, List<EntityAllocaConDocDetails> lst)
        {
            try
            {
                tblAllocConsultDoctor objcurrent = (from tbl in objData.tblAllocConsultDoctors
                                                    where tbl.SrNo == obj.SrNo
                                                    select tbl).FirstOrDefault();
                if (objcurrent != null)
                {
                    objcurrent.CategoryId = obj.CategoryId;
                    objcurrent.ConsultDocId = obj.ConsultDocId;
                    objcurrent.Consult_Date = obj.Consult_Date;
                    objcurrent.ConsultCharges = obj.ConsultCharges;
                }

                foreach (EntityAllocaConDocDetails item in lst)
                {
                    tblAllocConsultDoctorDetail objsal = new tblAllocConsultDoctorDetail();
                    objsal = (from tbl in objData.tblAllocConsultDoctorDetails
                              where tbl.SrDetailId == item.SrDetailId
                              && tbl.SrNo == item.SrNo && tbl.AdmitId == item.AdmitId
                              && tbl.IsDelete == false
                              select tbl).FirstOrDefault();
                    if (objsal != null)
                    {
                        objsal.AdmitId = Convert.ToInt32(item.AdmitId);
                        objsal.CategoryId = obj.CategoryId;
                        objsal.ConsultDocId = obj.ConsultDocId;
                        objsal.Consult_Date = obj.Consult_Date;
                        objsal.ConsultCharges = obj.ConsultCharges;
                        objsal.IsDelete = item.IsDelete;
                    }
                    else
                    {
                        objsal = new tblAllocConsultDoctorDetail()
                        {
                            AdmitId = item.AdmitId,
                            CategoryId = obj.CategoryId,
                            ConsultDocId = obj.ConsultDocId,
                            Consult_Date = obj.Consult_Date,
                            ConsultCharges = obj.ConsultCharges,
                            SrNo = Convert.ToInt32(obj.SrNo),
                            IsDelete = false
                        };
                        objData.tblAllocConsultDoctorDetails.InsertOnSubmit(objsal);
                    }
                }
                objData.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int? Save(tblAllocConsultDoctor tblins, List<EntityAllocaConDocDetails> lst)
        {
            int? SrNo = 0;
            try
            {
                objData.STP_Insert_tblAllocaConsultDoc(Convert.ToInt32(tblins.CategoryId), Convert.ToInt32(tblins.ConsultDocId), Convert.ToDecimal(tblins.ConsultCharges), Convert.ToDateTime(tblins.Consult_Date), ref SrNo);
                foreach (EntityAllocaConDocDetails item in lst)
                {
                    tblAllocConsultDoctorDetail tbl = new tblAllocConsultDoctorDetail()
                    {
                        AdmitId = Convert.ToInt32(item.AdmitId),
                        CategoryId = Convert.ToInt32(tblins.CategoryId),
                        ConsultDocId = Convert.ToInt32(tblins.ConsultDocId),
                        Consult_Date = Convert.ToDateTime(tblins.Consult_Date),
                        ConsultCharges = Convert.ToDecimal(tblins.ConsultCharges),
                        SrNo = Convert.ToInt32(SrNo),
                        IsDelete = false
                    };
                    objData.tblAllocConsultDoctorDetails.InsertOnSubmit(tbl);
                }
                objData.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return SrNo;
        }

        public bool ValidateAllocation(tblOTMedicineBill sal)
        {
            try
            {
                var res = (from tbl in objData.tblOTMedicineBills
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

        public List<EntityAllocaConDoc> GetAllocatedPatientList()
        {
            List<EntityAllocaConDoc> lst = (from tbl in objData.tblAllocConsultDoctors
                                            join tbla in objData.tblOperationCategories
                                            on tbl.CategoryId equals tbla.CategoryId
                                            join tblp in objData.tblAnaesthetists
                                            on tbl.ConsultDocId equals tblp.PKId
                                            where tbl.IsDelete == false
                                            select new EntityAllocaConDoc
                                            {
                                                SrNo = tbl.SrNo,
                                                CategoryName = tbla.CategoryName,
                                                CategoryId = Convert.ToInt32(tbl.CategoryId),
                                                ConsultName = tblp.AnaesthetistName,
                                                ConsultDocId = Convert.ToInt32(tbl.ConsultDocId),
                                                Consult_Date = Convert.ToDateTime(tbl.Consult_Date),
                                                ConsultCharges = Convert.ToDecimal(tbl.ConsultCharges)
                                            }).ToList();
            return lst;
        }

        public List<EntityAllocaConDocDetails> GetDocForPatientAllocate(int Id)
        {
            int i = 0;
            List<EntityAllocaConDocDetails> lst = (from tbl in objData.tblAllocConsultDoctorDetails
                                                   join tbla in objData.tblPatientAdmitDetails
                                                   on tbl.AdmitId equals tbla.AdmitId
                                                   join tblp in objData.tblPatientMasters
                                                   on tbla.PatientId equals tblp.PKId
                                                   where tbl.SrNo == Id
                                                   &&
                                                   tbl.IsDelete == false
                                                   select new EntityAllocaConDocDetails
                                                   {
                                                       PatientName = tblp.PatientFirstName + " " + tblp.PatientMiddleName + " " + tblp.PatientLastName,
                                                       AdmitId = Convert.ToInt32(tbl.AdmitId),
                                                       SrNo = tbl.SrNo,
                                                       SrDetailId = tbl.SrDetailId
                                                   }).ToList();
            foreach (EntityAllocaConDocDetails item in lst)
            {
                item.TempId = i++;
            }
            return lst;
        }

        public void Update(List<EntityAllocaConDocDetails> lst)
        {
            try
            {
                foreach (EntityAllocaConDocDetails item in lst)
                {
                    tblAllocConsultDoctorDetail objsal = new tblAllocConsultDoctorDetail();
                    objsal = (from tbl in objData.tblAllocConsultDoctorDetails
                              where tbl.SrDetailId == item.SrDetailId
                              && tbl.IsDelete == false
                              select tbl).FirstOrDefault();
                    if (objsal != null)
                    {
                        objsal.AdmitId = item.AdmitId;
                        objsal.IsDelete = item.IsDelete;
                    }
                    else
                    {
                        objsal = new tblAllocConsultDoctorDetail() { AdmitId = Convert.ToInt32(item.AdmitId), SrNo = Convert.ToInt32(item.SrNo) };
                        objData.tblAllocConsultDoctorDetails.InsertOnSubmit(objsal);
                    }
                }
                objData.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<EntityAllocaConDoc> GetInsurance(string Prefix)
        {
            List<EntityAllocaConDoc> lst = null;
            try
            {
                lst = (from tbl in GetAllocatedPatientList()
                       where tbl.SrNo.ToString().ToUpper().Contains(Prefix.ToUpper())
                       || tbl.CategoryName.ToString().ToUpper().Contains(Prefix.ToUpper())
                       || tbl.ConsultName.ToString().ToUpper().Contains(Prefix.ToUpper())
                       select new EntityAllocaConDoc()
                       {
                           SrNo = tbl.SrNo,
                           CategoryName = tbl.CategoryName,
                           ConsultName = tbl.ConsultName,
                           Consult_Date = tbl.Consult_Date,
                           ConsultCharges = tbl.ConsultCharges,
                       }).ToList();
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetConsultDoctor()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllAnaesthetist");
            }
            catch (Exception ex)
            {
                Commons.FileLog("AllocConsultDoctorToPatientBLL - GetConsultDoctor()", ex);
            }
            return ldt;
        }

        public decimal GetConsultCharge(int CategoryId, int ConsultDocId)
        {
            decimal FinalAmount = 0;
            try
            {


                List<tblAnaesthetist> lstTrans = (from tbl in objData.tblAnaesthetists
                                                  where tbl.CategoryId == Convert.ToInt32(CategoryId)
                                                  && tbl.PKId == Convert.ToInt32(ConsultDocId)
                                                  select tbl).ToList();

                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.ConsultCharges));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }
    }
}