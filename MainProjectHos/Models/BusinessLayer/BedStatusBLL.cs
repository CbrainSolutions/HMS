using MainProjectHos.Models.BusinessLayer;
using MainProjectHos.Models.DataLayer;
using MainProjectHos.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.BusinessLayer
{
    public class BedStatusBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();

        public BedStatusBLL()
        {
            objData = new CriticareHospitalDataContext();
        }

        public CriticareHospitalDataContext objData { get; set; }

        public DataTable GetAllBedMasters()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllOccupiedBeds");
            }
            catch (Exception ex)
            {
                Commons.FileLog("BedStatusBLL - GetAllBedMasters()", ex);
            }
            return ldt;
        }

        public DataTable GetAllBedMasterDetail()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_SelectAllBeds");
            }
            catch (Exception ex)
            {
                Commons.FileLog("BedStatusBLL - GetAllBedMasters()", ex);
            }
            return ldt;
        }



        public List<sp_GetAllBedAllocResult> GetAllBedAlloc()
        {
            try
            {
                List<sp_GetAllBedAllocResult> lstFinal = new List<sp_GetAllBedAllocResult>();
                List<sp_GetAllBedAllocResult> lstSemiFinal = (from tbl in objData.sp_GetAllBedAlloc()
                                                              select new sp_GetAllBedAllocResult
                                                              {
                                                                  AdmitId = tbl.AdmitId == null ? 0 : tbl.AdmitId,
                                                                  AllocationDate = tbl.AllocationDate,
                                                                  BedAllocId = tbl.BedAllocId,
                                                                  BedId = tbl.BedId,
                                                                  BedNo = tbl.BedNo,
                                                                  DischargeDate = tbl.DischargeDate,
                                                                  FloorName = tbl.FloorName,
                                                                  FloorNo = tbl.FloorNo,
                                                                  FullName = tbl.FullName,
                                                                  IsDischarge = tbl.IsDischarge,
                                                                  PatientId = tbl.PatientId,
                                                                  IsShiftTo_ICU = tbl.IsShiftTo_ICU,
                                                                  IsShiftTo_IPD = tbl.IsShiftTo_IPD,
                                                                  RoomId = tbl.RoomId,
                                                                  ShiftDate = tbl.ShiftDate,
                                                                  RoomNo = tbl.RoomNo,
                                                              }).ToList();

                List<tblBedMaster> lstbeds = (from tbl in objData.tblBedMasters
                                              join tblRoomC in objData.tblRoomCategories
                                              on tbl.CategoryId equals tblRoomC.PKId
                                              where tblRoomC.IsOT == false
                                              //&& tblRoomC.IsICU == false
                                              select tbl).ToList();
                foreach (tblBedMaster item in lstbeds)
                {
                    sp_GetAllBedAllocResult obj = (from tbl in lstSemiFinal
                                                   where tbl.BedId == item.BedId
                                                   orderby tbl.BedAllocId descending
                                                   select tbl).FirstOrDefault();
                    if (obj != null)
                    {
                        if (Convert.ToBoolean(obj.IsDischarge) || obj.ShiftDate != null)
                        {
                            obj.FullName = string.Empty;
                            obj.AllocationDate = null;
                            obj.DischargeDate = null;
                            obj.ShiftDate = null;
                        }
                        lstFinal.Add(obj);
                    }
                }

                return lstFinal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public DataTable GetAllPatient()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllIPDPatientList");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public EntityBedMaster GetFloorRoomBed(object Bed_Id)
        {
            try
            {
                EntityBedMaster lobjBed = (from tbl in objData.tblBedMasters
                                           where tbl.BedId.Equals(Bed_Id)
                                           && tbl.IsDelete == false
                                           select new EntityBedMaster { FloorNo = tbl.FloorNo, RoomId = tbl.RoomId, BedNo = tbl.BedNo }).FirstOrDefault();
                return lobjBed;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public int Update(EntityBedAllocToPatient entDept)
        {
            try
            {
                tblBedAllocationToPatient obj = (from tbl in objData.tblBedAllocationToPatients
                                                 where tbl.BedId.Equals(entDept.BedId)
                                                 && tbl.IsDelete == false
                                                 select new tblBedAllocationToPatient
                                                 {
                                                     RoomId = entDept.RoomId,
                                                     FloorId = entDept.FloorId,
                                                     PatientId = entDept.PatientId,
                                                     AllocationDate = entDept.AllocationDate
                                                 }).FirstOrDefault();
                objData.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertAllocBed(EntityBedAllocToPatient entDept)
        {
            int cnt = 0;
            try
            {
                tblBedAllocationToPatient objDaySch = new tblBedAllocationToPatient()
                {
                    BedId = entDept.BedId,
                    FloorId = entDept.FloorId,
                    RoomId = entDept.RoomId,
                    PatientId = entDept.PatientId,
                    AllocationDate = Convert.ToDateTime(entDept.AllocationDate),
                    IsDelete = false
                };
                objData.tblBedAllocationToPatients.InsertOnSubmit(objDaySch);
                objData.SubmitChanges();
                cnt = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return cnt;
        }

        public int DischargePatient(int PatientID, DateTime BillDate)
        {
            try
            {
                List<EntityBedAllocToPatient> lst = new ICUDischargeBilling().GetICUAllocatedBedDetails(PatientID);

                foreach (var item in lst)
                {
                    tblBedAllocationToPatient obj = (from tbl in objData.tblBedAllocationToPatients
                                                     where tbl.BedAllocId.Equals(item.BedAllocId)
                                                     select tbl).FirstOrDefault();

                    if (obj != null)
                    {
                        obj.DischargeDate = BillDate;
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



        public List<sp_GetAllBedAllocResult> SelectBedAlloc(string Prefix)
        {
            List<sp_GetAllBedAllocResult> lst = GetAllBedAlloc();
            List<sp_GetAllBedAllocResult> lstFinal = null;
            try
            {
                lstFinal = (from tbl in lst
                            where tbl.BedNo.ToString().ToUpper().Contains(Prefix.ToUpper()) || tbl.FloorName.ToString().ToUpper().Contains(Prefix.ToUpper())
                            select new sp_GetAllBedAllocResult
                            {
                                BedId = Convert.ToInt32(tbl.BedId),
                                BedNo = tbl.BedNo,
                                FloorName = tbl.FloorName,
                                RoomNo = tbl.RoomNo,
                                FullName = tbl.FullName,
                                AllocationDate = tbl.AllocationDate,
                                DischargeDate = tbl.DischargeDate
                            }).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstFinal;
        }

        public int GetBedTye(int BedID)
        {
            int BedCount = 0;

            BedCount = (from tbl in objData.tblBedMasters
                        join tblCat in objData.tblRoomCategories
                        on tbl.CategoryId equals tblCat.PKId
                        where tbl.BedId.Equals(BedID)
                        && tblCat.IsICU == true
                        select tbl).Count();

            return BedCount;
        }

        public int GetPatientId(int Bed_ID)
        {
            int ID;
            tblBedAllocationToPatient tblBed = null;
            tblBed = (from tbl in objData.tblBedAllocationToPatients
                      where tbl.DischargeDate == null
                      && tbl.BedId.Equals(Bed_ID)
                      select tbl).FirstOrDefault();

            return ID = Convert.ToInt32(tblBed.PatientId);
        }


        public int UpdateShiftBed(EntityBedAllocToPatient entBedAlloc)
        {
            try
            {
                tblBedAllocationToPatient objDaySch = new tblBedAllocationToPatient()
                {
                    BedId = entBedAlloc.BedId,
                    FloorId = entBedAlloc.FloorId,
                    RoomId = entBedAlloc.RoomId,
                    PatientId = entBedAlloc.PatientId,
                    AllocationDate = Convert.ToDateTime(entBedAlloc.AllocationDate),
                    IsDelete = false
                };
                objData.tblBedAllocationToPatients.InsertOnSubmit(objDaySch);

                tblBedAllocationToPatient obj = (from tbl in objData.tblBedAllocationToPatients
                                                 where tbl.BedId.Equals(entBedAlloc.ShiftBedId)
                                                 && tbl.PatientId.Equals(entBedAlloc.PatientId)
                                                 && tbl.DischargeDate == null
                                                 && tbl.IsDelete == false
                                                 orderby tbl.BedAllocId descending
                                                 select tbl).FirstOrDefault();
                if (obj != null)
                {
                    obj.ShiftDate = entBedAlloc.ShiftDate;
                    obj.IsShiftTo_ICU = entBedAlloc.Is_ShiftTo_ICU;
                    obj.IsShiftTo_IPD = entBedAlloc.Is_ShiftTo_IPD;
                    objData.SubmitChanges();
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        public EntityRoomCategory GetRoomCategoryByBedID(int BedId)
        {
            EntityRoomCategory objCat = null;
            try
            {
                objCat = (from tbl in objData.tblBedMasters
                          join tblRoomCat in objData.tblRoomCategories
                          on tbl.CategoryId equals tblRoomCat.PKId
                          where tbl.BedId == BedId
                          && tbl.IsDelete == false
                          select new EntityRoomCategory
                          {
                              PKId = tblRoomCat.PKId,
                              IsICU = Convert.ToBoolean(tblRoomCat.IsICU),
                          }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objCat;
        }
    }
}