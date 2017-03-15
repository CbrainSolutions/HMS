using MainProjectHos.Models.DataLayer;
using MainProjectHos.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.BusinessLayer
{
    public class OpeartionMasterBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();

        public OpeartionMasterBLL()
        {
            objData = new CriticareHospitalDataContext();
        }

        public CriticareHospitalDataContext objData { get; set; }


        public System.Data.DataTable GetAllCategoryName()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllOperationCategoryName");
            }
            catch (Exception ex)
            {
                Commons.FileLog("OpeartionCategoryBLL - GetAllCategoryName()", ex);
            }
            return ldt;
        }

        public int InsertOperationName(EntityOperationMaster entOpera)
        {
            try
            {
                tblOperationMaster obj = new tblOperationMaster()
                {
                    OperationCategoryId = entOpera.OperationCategoryId,
                    OperationName = entOpera.OperationName,
                    Price = entOpera.Price
                };
                objData.tblOperationMasters.InsertOnSubmit(obj);
                objData.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public bool IsRecordExists(string OperationName)
        {
            bool flag1 = false;
            try
            {
                tblOperationMaster objOpera = (from tbl in objData.tblOperationMasters
                                               where tbl.OperationName.ToUpper().ToString().Trim().Equals(OperationName.ToUpper().ToString().Trim())
                                               select tbl).FirstOrDefault();
                if (objOpera != null)
                {
                    flag1 = true;
                }
                else
                {
                    flag1 = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flag1;
        }

        public List<EntityOperationMaster> GetAllOperationDetails()
        {
            try
            {
                return (from tbl in objData.tblOperationMasters
                        join tblCat in objData.tblOperationCategories
                        on tbl.OperationCategoryId equals tblCat.CategoryId
                        select new EntityOperationMaster
                        {
                            OperationId = tbl.OperationId,
                            CatName = tblCat.CategoryName,
                            OperationName = tbl.OperationName,
                            OperationCategoryId = tbl.OperationCategoryId,
                            Price = tbl.Price
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EntityOperationMaster SelectOperation(int OperaId)
        {
            try
            {
                return (from tbl in objData.tblOperationMasters
                        join tblCat in objData.tblOperationCategories
                        on tbl.OperationCategoryId equals tblCat.CategoryId
                        where tbl.IsDelete == false
                        && tbl.OperationId.Equals(OperaId)
                        select new EntityOperationMaster
                        {
                            OperationCategoryId = tbl.OperationCategoryId,
                            OperationName = tbl.OperationName,
                            CatName = tblCat.CategoryName,
                            Price = tbl.Price
                        }).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int Update(EntityOperationMaster entOpera)
        {
            try
            {
                tblOperationMaster test = (from tbl in objData.tblOperationMasters
                                           where tbl.IsDelete == false
                                           && tbl.OperationId == entOpera.OperationId
                                           select tbl).FirstOrDefault();
                test.OperationId = entOpera.OperationId;
                test.OperationName = entOpera.OperationName;
                test.OperationCategoryId = entOpera.OperationCategoryId;
                test.Price = entOpera.Price;
                objData.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EntityOperationMaster> GetAllOperationDetails(string prefix)
        {
            return (from tbl in GetAllOperationDetails()
                    where tbl.CatName.ToString().ToUpper().Contains(prefix.ToUpper()) || tbl.OperationName.ToString().ToUpper().Contains(prefix.ToUpper())
                    select new EntityOperationMaster
                    {
                        OperationId = tbl.OperationId,
                        CatName = tbl.CatName,
                        OperationName = tbl.OperationName,
                        OperationCategoryId = tbl.OperationCategoryId,
                        Price = tbl.Price
                    }).ToList();
        }
    }
}