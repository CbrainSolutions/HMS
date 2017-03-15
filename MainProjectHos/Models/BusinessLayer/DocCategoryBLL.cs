using MainProjectHos.Models.DataLayer;
using MainProjectHos.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.BusinessLayer
{
    public class DocCategoryBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();

        public DocCategoryBLL()
        {
            objData = new CriticareHospitalDataContext();
        }

        public CriticareHospitalDataContext objData { get; set; }



        //SELECT
        //tblEmployee.DesignationId,
        //tblDesignationMaster.DesignationDesc,
        //tblEmployee.EmpFirstName,
        //tblEmployee.EmpMiddleName,
        //tblEmployee.EmpLastName
        //FROM
        //tblEmployee
        //INNER JOIN
        //tblDesignationMaster
        //ON
        //tblEmployee.DesignationId = tblDesignationMaster.PKId
        //WHERE
        //tblEmployee.DesignationId=1 or tblEmployee.DesignationId=9
        //and tblEmployee.PKId
        //not in
        //(select DocId from tblDocCategory
        //where IsDelete='False')


        public DataTable GetAllNonAllocDoc()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("STP_NotAllocatedDocToOperation");
            }
            catch (Exception ex)
            {
                Commons.FileLog("ShiftBLL - GetAllShift()", ex);
            }
            return ldt;
        }



        public bool IsRecordExists(int Doctor_Id)
        {
            bool flag1 = false;
            try
            {
                tblDocCategory objOpera = (from tbl in objData.tblDocCategories
                                           where tbl.DocId.Equals(Doctor_Id)
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

        public int InsertDoctorDetails(EntityDocCategory entOpera)
        {
            try
            {
                tblDocCategory obj = new tblDocCategory()
                {
                    OperaCatId = entOpera.OperaCatId,
                    DocId = entOpera.DocId
                };
                objData.tblDocCategories.InsertOnSubmit(obj);
                objData.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EntityDocCategory> GetAlDoctorDetails()
        {
            try
            {
                return (from tbl in objData.tblDocCategories
                        join tblE in objData.tblEmployees
                        on tbl.DocId equals tblE.PKId
                        join tblC in objData.tblOperationCategories
                        on tbl.OperaCatId equals tblC.CategoryId
                        select new EntityDocCategory
                        {
                            DocAllocId = tbl.DocAllocId,
                            DocId = tbl.DocId,
                            OperaCatId = tbl.OperaCatId,
                            Opera_Name = tblC.CategoryName,
                            Doc_Name = tblE.EmpFirstName + ' ' + tblE.EmpMiddleName + ' ' + tblE.EmpLastName
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EntityDocCategory SelectDoc(int AllocID)
        {
            try
            {
                return (from tbl in objData.tblDocCategories
                        join tblE in objData.tblEmployees
                        on tbl.DocId equals tblE.PKId
                        join tblC in objData.tblOperationCategories
                        on tbl.OperaCatId equals tblC.CategoryId
                        where tbl.IsDelete == false
                        && tbl.DocAllocId.Equals(AllocID)
                        select new EntityDocCategory
                        {
                            DocAllocId = tbl.DocAllocId,
                            DocId = tbl.DocId,
                            OperaCatId = tbl.OperaCatId,
                            Opera_Name = tblC.CategoryName,
                            Doc_Name = tblE.EmpFirstName + ' ' + tblE.EmpMiddleName + ' ' + tblE.EmpLastName
                        }).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<EntityDocCategory> GetAllDoctors()
        {
            List<EntityDocCategory> lstDoc = (from tbl in objData.tblEmployees
                                              join tblD in objData.tblDesignationMasters
                                              on tbl.DesignationId equals tblD.PKId
                                              where tblD.PKId == 1 || tblD.PKId == 9
                                              select new EntityDocCategory { DocId = tbl.PKId, Doc_Name = tbl.EmpFirstName + ' ' + tbl.EmpMiddleName + ' ' + tbl.EmpLastName }).ToList();
            return lstDoc;
        }

        public int Update(EntityDocCategory entOpera)
        {
            try
            {
                tblDocCategory test = (from tbl in objData.tblDocCategories
                                       where tbl.IsDelete == false
                                       && tbl.DocAllocId == entOpera.DocAllocId
                                       select tbl).FirstOrDefault();
                test.DocAllocId = entOpera.DocAllocId;
                test.DocId = entOpera.DocId;
                test.OperaCatId = entOpera.OperaCatId;
                objData.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EntityDocCategory> SelectDoctCategory(string Prefix)
        {
            List<EntityDocCategory> lst = null;
            try
            {
                lst = (from tbl in GetAlDoctorDetails()
                       where
                       tbl.DocAllocId.ToString().ToUpper().Contains(Prefix.ToUpper()) || tbl.Doc_Name.ToString().ToUpper().Contains(Prefix.ToUpper()) ||
                       tbl.Opera_Name.ToString().ToUpper().Contains(Prefix.ToUpper())
                       select new EntityDocCategory
                       {
                           DocAllocId = tbl.DocAllocId,
                           Doc_Name = tbl.Doc_Name,
                           Opera_Name = tbl.Opera_Name
                       }).ToList();
                return lst;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}