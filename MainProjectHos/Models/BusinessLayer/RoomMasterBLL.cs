using MainProjectHos.Models.DataLayer;
using MainProjectHos.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace MainProjectHos.Models.BusinessLayer
{
    public class RoomMasterBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();

        public RoomMasterBLL()
        {
            objData = new CriticareHospitalDataContext();
        }

        public CriticareHospitalDataContext objData { get; set; }

        public System.Data.DataTable GetAllRoomDetails()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllRoomDetails");
            }
            catch (Exception ex)
            {
                Commons.FileLog("RoomMasterBLL - GetAllRoomDetails()", ex);
            }
            return ldt;
        }

        public DataTable GetRoomForEdit(string lstrRoomId)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@RoomId", DbType.String, lstrRoomId);
                ldt = mobjDataAcces.GetDataTable("sp_GetRoomForEdit", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("RoomMasterBLL - GetRoomForEdit(string lstrRoomId)", ex);
            }
            return ldt;
        }

        public int UpdateRooms(EnitiyRoomMaster entRoom)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                //Commons.ADDParameter(ref lstParam, "@RoomId", DbType.String, entRoom.RoomId);
                Commons.ADDParameter(ref lstParam, "@RoomNo", DbType.String, entRoom.RoomNo);
                Commons.ADDParameter(ref lstParam, "@FloorNo", DbType.Int32, entRoom.FloorNo);
                Commons.ADDParameter(ref lstParam, "@CategoryId", DbType.String, entRoom.CategoryId);
                cnt = mobjDataAcces.ExecuteQuery("sp_UpdateRoom", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("RoomMasterBLL -  UpdateRooms(EnitiyRoomMaster entRoom)", ex);
            }

            return cnt;
        }

        public int InsertRoom(EnitiyRoomMaster entRoom)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@RoomNo", DbType.String, entRoom.RoomNo);
                Commons.ADDParameter(ref lstParam, "@CategoryId", DbType.Int32, entRoom.CategoryId);
                Commons.ADDParameter(ref lstParam, "@FloorNo", DbType.String, entRoom.FloorNo);
                cnt = mobjDataAcces.ExecuteQuery("sp_InsertRoom", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("RoomMasterBLL -  InsertRoom(EnitiyRoomMaster entRoom)", ex);
            }

            return cnt;
        }

        public DataTable GetNewRoomId()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetNewRoomId");
            }
            catch (Exception ex)
            {
                Commons.FileLog("RoomMasterBLL - GetNewRoomId()", ex);
            }
            return ldt;
        }

        public DataTable GetAllCategory()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllCategory");
            }
            catch (Exception ex)
            {
                Commons.FileLog("RoomMasterBLL - GetAllCategory()", ex);
            }
            return ldt;
        }

        public DataTable GetAllFloor()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllFloor");
            }
            catch (Exception ex)
            {
                Commons.FileLog("RoomMasterBLL - GetAllFloor()", ex);
            }
            return ldt;
        }

        public DataTable SelectRoomForEdit(int id)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@RoomId", DbType.String, id);
                ldt = mobjDataAcces.GetDataTable("sp_SelectRoomForEdit", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("EmployeeBLL - SelectAllEmployee()", ex);
            }
            return ldt;
        }

        public tblRoomMaster GetRoomByNameAndId(string RoomNo, int LedgerId)
        {
            try
            {

                tblRoomMaster objOther = (from tbl in objData.tblRoomMasters
                                          where tbl.RoomId == LedgerId
                                          && tbl.RoomNo.Equals(RoomNo)
                                          select tbl).FirstOrDefault();
                return objOther;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(tblRoomMaster objT)
        {
            try
            {

                tblRoomMaster obj = (from tbl in objData.tblRoomMasters
                                     where tbl.RoomId.Equals(objT.RoomId)
                                     && tbl.IsDelete == false
                                     select tbl).FirstOrDefault();
                obj.RoomNo = objT.RoomNo;
                obj.FloorNo = objT.FloorNo;
                obj.CategoryId = objT.CategoryId;
                objData.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tblRoomMaster GetRoomByName(string RoomNo)
        {
            try
            {

                tblRoomMaster objOther = (from tbl in objData.tblRoomMasters
                                          where tbl.RoomNo.Equals(RoomNo)
                                          && tbl.IsDelete == false
                                          select tbl).FirstOrDefault();
                return objOther;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}