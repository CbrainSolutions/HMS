using MainProjectHos.Models.DataModels;
using MainProjectHos.Models.DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.BusinessLayer
{
    public class PrescriptionBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public PrescriptionBLL()
        {
            objData = new CriticareHospitalDataContext();
        }
        public CriticareHospitalDataContext objData { get; set; }

        public tblPrescription GetPrescriptionInfo(int PrescriptionId)
        {
            tblPrescription obj = null;
            try
            {
                obj = (from tbl in objData.tblPrescriptions
                       where tbl.IsDelete == false
                       && tbl.Prescription_Id == PrescriptionId
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

        public List<EntityPatientAdmit> GetPatientList(int CategoryId)
        {
            List<EntityPatientAdmit> lst = null;
            try
            {
                lst = (from tbl in objData.tblPatientAdmitDetails
                       join tblpa in objData.tblPatientMasters
                       on tbl.PatientId equals tblpa.PKId
                       join tblDo in objData.tblDocCategories
                       on tblpa.DeptCategory equals tblDo.OperaCatId
                       where tbl.IsDischarge == false && tblDo.OperaCatId == CategoryId
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

        public void Update(tblPrescription obj, List<EntityPrescriptionDetails> lst)
        {
            try
            {
                tblPrescription objcurrent = (from tbl in objData.tblPrescriptions
                                              where tbl.Prescription_Id == obj.Prescription_Id
                                              select tbl).FirstOrDefault();
                if (objcurrent != null)
                {
                    objcurrent.AdmitId = obj.AdmitId;
                    objcurrent.DeptCategory = obj.DeptCategory;
                    objcurrent.DeptDoctor = obj.DeptDoctor;
                    objcurrent.Prescription_Date = obj.Prescription_Date;
                    objcurrent.InjectionName = obj.InjectionName;
                    objcurrent.IsDressing = obj.IsDressing;
                    objcurrent.IsInjection = obj.IsInjection;
                    objcurrent.Investigation = obj.Investigation;
                    objcurrent.Impression = obj.Impression;
                    objcurrent.AdviceNote = obj.AdviceNote;
                    objcurrent.Remarks = obj.Remarks;
                }

                foreach (EntityPrescriptionDetails item in lst)
                {
                    tblPrescriptionDetail objsal = new tblPrescriptionDetail();
                    objsal = (from tbl in objData.tblPrescriptionDetails
                              where tbl.PrescriptionDetailId == item.PrescriptionDetailId
                              && tbl.Prescription_Id == item.Prescription_Id && tbl.ProductId == item.ProductId
                              && tbl.IsDelete == false
                              select tbl).FirstOrDefault();
                    if (objsal != null)
                    {
                        objsal.ProductId = Convert.ToInt32(item.ProductId);
                        objsal.Morning = item.Morning;
                        objsal.Afternoon = item.Afternoon;
                        objsal.Night = item.Night;
                        objsal.NoOfDays = item.NoOfDays;
                        objsal.Quantity = item.Quantity;
                        objsal.IsDelete = item.IsDelete;
                    }
                    else
                    {
                        objsal = new tblPrescriptionDetail()
                        {
                            ProductId = item.ProductId,
                            Morning = item.Morning,
                            Afternoon = item.Afternoon,
                            Night = item.Night,
                            NoOfDays = item.NoOfDays,
                            Quantity = item.Quantity,
                            Prescription_Id = Convert.ToInt32(obj.Prescription_Id),
                            IsDelete = false
                        };
                        objData.tblPrescriptionDetails.InsertOnSubmit(objsal);
                    }

                    tblStockDetail stock = (from tbl in objData.tblStockDetails
                                            where tbl.IsDelete == false
                                            && tbl.DocumentNo == item.Prescription_Id
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
                        stock.OutwardQty = Convert.ToInt32(item.Quantity);
                        stock.IsDelete = item.IsDelete;
                    }
                    else
                    {
                        tblStockDetail stock1 = new tblStockDetail()
                        {
                            ProductId = Convert.ToInt32(item.ProductId),
                            OpeningQty = 0,
                            InwardQty = 0,
                            InwardPrice = 0,
                            InwardAmount = 0,
                            OutwardQty = Convert.ToInt32(item.Quantity),
                            TransactionType = "Issue",
                            IsDelete = false,
                        };
                        objData.tblStockDetails.InsertOnSubmit(stock1);

                    }
                }
                objData.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int? Save(tblPrescription tblins, List<EntityPrescriptionDetails> lst)
        {
            int? PrescriptionId = 0;
            try
            {
                objData.STP_Insert_tblPrescription(Convert.ToInt32(tblins.AdmitId), Convert.ToInt32(tblins.DeptCategory), tblins.DeptDoctor, Convert.ToDateTime(tblins.Prescription_Date),
                    tblins.IsDressing, tblins.IsInjection, tblins.InjectionName, tblins.Investigation, tblins.Impression, tblins.AdviceNote, tblins.Remarks, ref PrescriptionId);
                foreach (EntityPrescriptionDetails item in lst)
                {
                    tblPrescriptionDetail tbl = new tblPrescriptionDetail()
                    {
                        ProductId = Convert.ToInt32(item.ProductId),
                        Morning = item.Morning,
                        Afternoon = item.Afternoon,
                        Night = item.Night,
                        NoOfDays = item.NoOfDays,
                        Quantity = item.Quantity,
                        Prescription_Id = Convert.ToInt32(PrescriptionId),
                        IsDelete = false
                    };
                    objData.tblPrescriptionDetails.InsertOnSubmit(tbl);
                    tblStockDetail stock = new tblStockDetail()
                    {
                        ProductId = Convert.ToInt32(item.ProductId),
                        OpeningQty = 0,
                        InwardQty = 0,
                        InwardPrice = 0,
                        InwardAmount = 0,
                        OutwardQty = Convert.ToInt32(item.Quantity),
                        TransactionType = "Issue",
                        DocumentNo = PrescriptionId,
                    };
                    objData.tblStockDetails.InsertOnSubmit(stock);
                }
                objData.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return PrescriptionId;
        }

        public bool ValidateAllocation(tblPrescription sal)
        {
            try
            {
                var res = (from tbl in objData.tblPrescriptions
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

        public List<EntityPrescription> GetInsurance()
        {
            List<EntityPrescription> lst = (from tbl in objData.tblPrescriptions
                                            join tbla in objData.tblPatientAdmitDetails
                                            on tbl.AdmitId equals tbla.AdmitId
                                            join tblp in objData.tblPatientMasters
                                            on tbla.PatientId equals tblp.PKId
                                            join tblDoc in objData.tblOperationCategories
                                            on tbl.DeptCategory equals tblDoc.CategoryId
                                            where tbl.IsDelete == false
                                            select new EntityPrescription
                                            {
                                                Prescription_Id = tbl.Prescription_Id,
                                                PatientName = tblp.PatientFirstName + " " + tblp.PatientMiddleName + " " + tblp.PatientLastName,
                                                Prescription_Date = tbl.Prescription_Date,
                                                AdmitId = tbl.AdmitId,
                                                DeptCategory = Convert.ToInt32(tbl.DeptCategory),
                                                DeptDoctor = tbl.DeptDoctor,
                                                CategoryName = tblDoc.CategoryName
                                            }).ToList();
            return lst;
        }

        public List<EntityPrescriptionDetails> GetPrescription(int Id)
        {
            int i = 0;
            List<EntityPrescriptionDetails> lst = (from tbl in objData.tblPrescriptionDetails
                                                   join tblTab in objData.tblProductMasters
                                                   on tbl.ProductId equals tblTab.ProductId
                                                   where tbl.Prescription_Id == Id
                                                   &&
                                                   tbl.IsDelete == false
                                                   select new EntityPrescriptionDetails
                                                   {
                                                       ProductName = tblTab.ProductName,
                                                       ProductId = Convert.ToInt32(tbl.ProductId),
                                                       Morning = tbl.Morning,
                                                       Afternoon = tbl.Afternoon,
                                                       Night = tbl.Night,
                                                       NoOfDays = tbl.NoOfDays,
                                                       Quantity = tbl.Quantity,
                                                       Prescription_Id = tbl.Prescription_Id,
                                                       IsDressing = tbl.IsDressing,
                                                       IsInjection = tbl.IsInjection,
                                                       InjectionName = tbl.InjectionName,
                                                       PrescriptionDetailId = tbl.PrescriptionDetailId
                                                   }).ToList();
            foreach (EntityPrescriptionDetails item in lst)
            {
                item.TempId = i++;
            }
            return lst;
        }

        public void Update(List<EntityPrescriptionDetails> lst)
        {
            try
            {
                foreach (EntityPrescriptionDetails item in lst)
                {
                    tblPrescriptionDetail objsal = new tblPrescriptionDetail();
                    objsal = (from tbl in objData.tblPrescriptionDetails
                              where tbl.PrescriptionDetailId == item.PrescriptionDetailId
                              && tbl.IsDelete == false
                              select tbl).FirstOrDefault();
                    if (objsal != null)
                    {
                        objsal.ProductId = item.ProductId;
                        objsal.Quantity = item.Quantity;
                        objsal.Morning = item.Morning;
                        objsal.Afternoon = item.Afternoon;
                        objsal.Night = item.Night;
                        objsal.NoOfDays = item.NoOfDays;
                        objsal.IsDelete = item.IsDelete;
                    }
                    else
                    {
                        objsal = new tblPrescriptionDetail() { ProductId = Convert.ToInt32(item.ProductId), Quantity = item.Quantity, Morning = item.Morning, Afternoon = item.Afternoon, Night = item.Night, NoOfDays = item.NoOfDays, Prescription_Id = Convert.ToInt32(item.Prescription_Id) };
                        objData.tblPrescriptionDetails.InsertOnSubmit(objsal);
                    }
                }
                objData.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<EntityPrescription> GetInsurance(string Prefix)
        {
            List<EntityPrescription> lst = null;
            try
            {
                lst = (from tbl in GetInsurance()
                       where tbl.Prescription_Id.ToString().ToUpper().Contains(Prefix.ToUpper())
                       || tbl.PatientName.ToString().ToUpper().Contains(Prefix.ToUpper())
                       select new EntityPrescription()
                       {
                           Prescription_Id = tbl.Prescription_Id,
                           PatientName = tbl.PatientName,
                           Prescription_Date = tbl.Prescription_Date,
                       }).ToList();
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetTablet()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetTabletForPresc");
            }
            catch (Exception ex)
            {
                Commons.FileLog("PrescriptionBLL - GetTablet()", ex);
            }
            return ldt;
        }
    }

}