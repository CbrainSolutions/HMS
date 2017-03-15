using MainProjectHos.Models.DataLayer;
using MainProjectHos.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.BusinessLayer
{
    public class BedMasterBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();

        public BedMasterBLL()
        {
            objData = new CriticareHospitalDataContext();
        }

        public CriticareHospitalDataContext objData { get; set; }

        public DataTable GetAllBedMasters()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_SelectAllBeds");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public int InsertBedMaster(EntityBedMaster entBedMaster)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@BedNo", DbType.String, entBedMaster.BedNo);
                Commons.ADDParameter(ref lstParam, "@RoomId", DbType.Int32, entBedMaster.RoomId);
                Commons.ADDParameter(ref lstParam, "@FloorNo", DbType.Int32, entBedMaster.FloorNo);
                Commons.ADDParameter(ref lstParam, "@CategoryId", DbType.Int32, entBedMaster.CategoryId);
                cnt = mobjDataAcces.ExecuteQuery("sp_InsertBedMaster", lstParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return cnt;
        }

        public int UpdateBedMaster(EntityBedMaster entBedMaster)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@BedId", DbType.Int32, entBedMaster.BedId);
                Commons.ADDParameter(ref lstParam, "@BedNo", DbType.Int32, entBedMaster.BedNo);
                Commons.ADDParameter(ref lstParam, "@RoomId", DbType.Int32, entBedMaster.RoomId);
                Commons.ADDParameter(ref lstParam, "@FloorNo", DbType.Int32, entBedMaster.FloorNo);
                Commons.ADDParameter(ref lstParam, "@CategoryId", DbType.Int32, entBedMaster.CategoryId);
                cnt = mobjDataAcces.ExecuteQuery("sp_UpdateBedMaster", lstParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return cnt;
        }

        public DataTable GetAllRooms()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllRooms");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public EnitiyRoomMaster GetFloorAndCategory(int RoomId)
        {
            try
            {
                EnitiyRoomMaster obj = (from tbl in objData.tblRoomMasters
                                        join tblCat in objData.tblRoomCategories
                                        on tbl.CategoryId equals tblCat.PKId
                                        join tblFloor in objData.tblFloorMasters
                                        on tbl.FloorNo equals tblFloor.FloorId
                                        where tbl.IsDelete == false
                                        && tbl.RoomId == RoomId
                                        select new EnitiyRoomMaster
                                        {
                                            RoomId = tbl.RoomId,
                                            CategoryId = tbl.CategoryId,
                                            CategoryName = tblCat.CategoryDesc,
                                            FloorName = tblFloor.FloorName,
                                            FloorNo = tbl.FloorNo
                                        }).FirstOrDefault();

                return obj;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataTable SelectBedForEdit(int p)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@BedId", DbType.Int32, p);
                ldt = mobjDataAcces.GetDataTable("sp_SelectBedForEdit", lstParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public List<EntityBedMaster> SelectBedMaster(string Prefix)
        {
            List<EntityBedMaster> lst = null;
            try
            {
                lst = (from tbl in objData.sp_SelectAllBeds()
                       where
                       tbl.BedId.ToString().ToUpper().Contains(Prefix.ToUpper()) || tbl.BedNo.ToString().ToUpper().Contains(Prefix.ToUpper()) || tbl.FloorName.ToString().ToUpper().Contains(Prefix.ToUpper())
                       || tbl.RoomNo.ToString().ToUpper().Contains(Prefix.ToUpper())
                       select new EntityBedMaster
                       {
                           BedId = tbl.BedId,
                           BedNo = tbl.BedNo,
                           RoomNo = tbl.RoomNo,
                           FloorName = tbl.FloorName,
                           CategoryDesc = tbl.CategoryDesc
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