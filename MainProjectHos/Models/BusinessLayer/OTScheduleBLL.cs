using MainProjectHos.Models.DataLayer;
using MainProjectHos.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.BusinessLayer
{
    public class OTScheduleBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();

        public OTScheduleBLL()
        {
            objData = new CriticareHospitalDataContext();
        }

        public CriticareHospitalDataContext objData { get; set; }

        public List<sp_GetAllBedAllocOTResult> GetAllBedAllocOT()
        {
            try
            {
                List<sp_GetAllBedAllocOTResult> lstFinal = new List<sp_GetAllBedAllocOTResult>();
                List<sp_GetAllBedAllocOTResult> lst = new CriticareHospitalDataContext().sp_GetAllBedAllocOT().ToList();
                foreach (sp_GetAllBedAllocOTResult item in lst)
                {

                    if (item.OTBedAllocId == null)
                    {
                        lstFinal.Add(item);
                    }
                    else
                    {
                        if (item.DischargeDate != null)
                        {

                            if (item.DischargeDate.Value.CompareTo(DateTime.Now) >= -1)
                            {
                                int cnt = (from tbl in lstFinal
                                           where tbl.BedId == item.BedId
                                           select tbl).ToList().Count;
                                if (cnt == 0)
                                {
                                    lstFinal.Add(item);
                                }
                                //else
                                //{
                                //    int indexc = 0;
                                //    bool flag = false;
                                //    foreach (sp_GetAllBedAllocOTResult finalBeds in lstFinal)
                                //    {
                                //        if (finalBeds.BedId==item.BedId)
                                //        {
                                //            flag = true;
                                //            break;    
                                //        }
                                //        indexc++;
                                //    }
                                //    if (flag)
                                //    {
                                //        lstFinal.RemoveAt(indexc);
                                //    }
                                //}
                            }
                            else
                            {
                                int cnt = (from tbl in lstFinal
                                           where tbl.BedId == item.BedId
                                           select tbl).ToList().Count;
                                if (cnt == 0)
                                {
                                    lstFinal.Add(item);
                                }
                                else
                                {
                                    int indexc = 0;
                                    bool flag = false;
                                    foreach (sp_GetAllBedAllocOTResult finalBeds in lstFinal)
                                    {
                                        if (finalBeds.BedId == item.BedId)
                                        {
                                            flag = true;
                                            break;
                                        }
                                        indexc++;
                                    }
                                    if (flag)
                                    {
                                        lstFinal.RemoveAt(indexc);
                                    }
                                }
                            }
                        }
                    }
                }
                return lstFinal;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int InsertOTAllocBed(EntityOTBedAlloc entDept)
        {
            int cnt = 0;
            try
            {
                tblOTBedAlloc objDaySch = new tblOTBedAlloc()
                {
                    BedId = entDept.BedId,
                    FloorId = entDept.FloorId,
                    RoomId = entDept.RoomId,
                    PatientId = entDept.PatientId,
                    OperCatId = entDept.OperCatId,
                    DocId = entDept.DocId,
                    OperId = entDept.OperationId,
                    AllocationDate = Convert.ToDateTime(entDept.AllocationDate),
                    DischargeDate = Convert.ToDateTime(entDept.DischargeDate),
                    IsDelete = false,
                    AnestheticId = entDept.AnestheticId,
                    AssistantDocId = entDept.AssistantId,
                    TypeOfAnaesthesia = entDept.TypeOfAnaesthetist,
                    Implant = entDept.Implant,
                    MaterialForHPE = entDept.MaterialHPE,
                    Surgeon = entDept.Surgeon,
                    AnaestheticNote = entDept.AnaestheticNote,
                    SurgeryNote = entDept.SurgeryNote
                };
                objData.tblOTBedAllocs.InsertOnSubmit(objDaySch);
                objData.SubmitChanges();
                cnt = 1;
            }
            catch (Exception ex)
            {
                Commons.FileLog("BedStatusBLL -  InsertBedMaster(EntityBedMaster entBedMaster)", ex);
            }

            return cnt;
        }

        public List<EntityDocCategory> GetAllDoctors(int OperaId)
        {
            List<EntityDocCategory> lstDoc = (from tblD in objData.tblDocCategories
                                              join tblE in objData.tblEmployees
                                              on tblD.DocId equals tblE.PKId
                                              join tblO in objData.tblOperationCategories
                                              on tblD.OperaCatId equals tblO.CategoryId
                                              where tblD.OperaCatId.Equals(OperaId)
                                              select new EntityDocCategory { DocId = tblD.DocId, Doc_Name = tblE.EmpFirstName + ' ' + tblE.EmpMiddleName + ' ' + tblE.EmpLastName }).ToList();
            return lstDoc;
        }

        public List<EntityDocCategory> GetAllDoctors()
        {
            List<EntityDocCategory> lstDoc = (from tblD in objData.tblDocCategories
                                              join tblE in objData.tblEmployees
                                              on tblD.DocId equals tblE.PKId
                                              join tblO in objData.tblOperationCategories
                                              on tblD.OperaCatId equals tblO.CategoryId
                                              join tblDesig in objData.tblDesignationMasters
                                              on tblE.DesignationId equals tblDesig.PKId
                                              where tblDesig.DesignationDesc.Contains("Doctor")
                                              select new EntityDocCategory
                                              {
                                                  DocId = tblD.DocId,
                                                  Doc_Name = tblE.EmpFirstName + ' ' + tblE.EmpMiddleName + ' ' + tblE.EmpLastName
                                              }).ToList();
            return lstDoc;
        }

        public List<sp_GetAllBedAllocOTForPatientResult> SearchBedConsumption()
        {
            try
            {
                return (objData.sp_GetAllBedAllocOTForPatient()).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<sp_GetAllBedAllocOTForPatientDatewiseResult> SearchDatewiseOTConsumption(DateTime fromdate, DateTime todate)
        {
            try
            {
                return (objData.sp_GetAllBedAllocOTForPatientDatewise(fromdate, todate)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}