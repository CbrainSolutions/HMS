using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MainProjectHos.Models.DataModels;
using MainProjectHos.Models.DataLayer;
using System.Data;
using System.Data.SqlClient;

namespace MainProjectHos.Models.BusinessLayer
{
    public class UserAuthenticationBLL
    {
        public UserAuthenticationBLL()
        {
            objData = new CriticareHospitalDataContext();
        }

        public CriticareHospitalDataContext objData { get; set; }

        public List<EntityDesignation> GetDesignation()
        {
            try
            {
                return (from tbl in objData.tblDesignationMasters
                        orderby tbl.PKId ascending
                        select new EntityDesignation { PKID = tbl.PKId, DesignationDesc = tbl.DesignationDesc }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EntityEmployee> GetEmployee(int Designation_ID)
        {
            try
            {
                return (from tbl in objData.tblEmployees
                        where tbl.DesignationId.Equals(Designation_ID)
                        select new EntityEmployee { PKId = tbl.PKId, EmpName = tbl.EmpFirstName + ' ' + tbl.EmpMiddleName + ' ' + tbl.EmpLastName }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EntityFormMaster> GetAllForms()
        {
            try
            {
                List<EntityFormMaster> lst = (from tbl in objData.tblFormMasters
                                              where tbl.IsDelete == false
                                              select new EntityFormMaster
                                              {
                                                  FormId = tbl.FormId,
                                                  FormTitle = tbl.FormTitle,
                                                  FormName = tbl.FormName
                                              }).ToList();
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<EntityFormMaster> GetAllocatedForms(int EmpId)
        {
            try
            {
                List<EntityFormMaster> lst = (from tbl in objData.tblUserAuthorizations
                                              join tblForm in objData.tblFormMasters
                                              on tbl.FormId equals tblForm.FormId
                                              where (tbl.IsDelete == false && tblForm.IsDelete == false && tbl.EmpId.Equals(EmpId))
                                              orderby tbl.FormId ascending
                                              select new EntityFormMaster
                                              {
                                                  FormId = tbl.FormId,
                                                  FormTitle = tblForm.FormTitle
                                              }).ToList();
                return lst;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool GetAllocFormOnEmp(int EmpId, int FormId)
        {
            bool flag1 = false;
            tblUserAuthorization obj = new tblUserAuthorization();
            try
            {
                obj = (from tbl in objData.tblUserAuthorizations
                       where tbl.IsDelete == false
                       && tbl.EmpId.Equals(EmpId)
                       && tbl.FormId.Equals(FormId)
                       select tbl).FirstOrDefault();
                if (obj != null)
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

        public bool Save(List<tblUserAuthorization> lstUser)
        {
            try
            {
                foreach (tblUserAuthorization item in lstUser)
                {
                    objData.tblUserAuthorizations.InsertOnSubmit(item);
                }
                objData.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EntityFormMaster> ListofForms(string UserName)
        {
            List<EntityFormMaster> lstForm = new List<EntityFormMaster>();

            try
            {
                lstForm = (from tblForm in objData.tblFormMasters
                           join tblUser in objData.tblUserAuthorizations
                           on tblForm.FormId equals tblUser.FormId
                           join tblEmp in objData.tblEmployees
                           on tblUser.EmpId equals tblEmp.PKId
                           where (tblEmp.EmpFirstName + ' ' + tblEmp.EmpMiddleName + ' ' + tblEmp.EmpLastName).Equals(UserName)
                           select new EntityFormMaster { FormName = tblForm.FormName }).ToList();

                return lstForm;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}