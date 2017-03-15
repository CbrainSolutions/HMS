using MainProjectHos.Models.DataModels;
using MainProjectHos.Models.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.BusinessLayer
{
    public class ChargeCategoryBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();

        public ChargeCategoryBLL()
        {
            objData = new CriticareHospitalDataContext();
        }

        public CriticareHospitalDataContext objData { get; set; }

        public bool CheckRecordExists(EntityChargeCategory entDept)
        {
            bool flag1 = false;
            try
            {
                tblChargeCategory objCharge = (from tbl in objData.tblChargeCategories
                                               where tbl.ChargeCategoryName.ToUpper().ToString().Trim().Equals(entDept.ChargeCategoryName.ToUpper().ToString().Trim())
                                               && tbl.ChargesId == entDept.ChargesId
                                               select tbl).FirstOrDefault();
                if (objCharge != null)
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

        public bool IsRecordExists(EntityChargeCategory entDept)
        {
            bool flag1 = false;
            try
            {
                tblChargeCategory objCharge = (from tbl in objData.tblChargeCategories
                                               where tbl.ChargeCategoryName.ToUpper().ToString().Trim().Equals(entDept.ChargeCategoryName.ToUpper().ToString().Trim())
                                               select tbl).FirstOrDefault();
                if (objCharge != null)
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

        public int InsertChargeCategory(EntityChargeCategory entDept)
        {
            try
            {
                tblChargeCategory obj = new tblChargeCategory()
                {
                    ChargeCategoryName = entDept.ChargeCategoryName,
                    IsOperation = entDept.IsOperation,
                    IsBed = entDept.IsBed,
                    IsConsulting = entDept.IsConsulting,
                    IsOther = entDept.IsOther,
                    IsICU = entDept.IsICU
                };
                objData.tblChargeCategories.InsertOnSubmit(obj);
                objData.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EntityChargeCategory> GetChargeDetail()
        {
            try
            {
                return (from tbl in objData.tblChargeCategories
                        where (tbl.IsBed == true
                        || tbl.IsConsulting == true
                        || tbl.IsOperation == true || tbl.IsICU == true)
                        || tbl.IsOther == true
                        select new EntityChargeCategory
                        {
                            ChargesId = tbl.ChargesId,
                            ChargeCategoryName = tbl.ChargeCategoryName,
                            IsOperation = tbl.IsOperation,
                            IsBed = tbl.IsBed,
                            IsConsulting = tbl.IsConsulting,
                            IsOther = tbl.IsOther,
                            IsICU = tbl.IsICU
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EntityChargeCategory> GetICUChargeDetail()
        {
            try
            {
                return (from tbl in objData.tblChargeCategories
                        where tbl.IsICU == true
                        select new EntityChargeCategory
                        {
                            ChargesId = tbl.ChargesId,
                            ChargeCategoryName = tbl.ChargeCategoryName,
                            IsOperation = tbl.IsOperation,
                            IsBed = tbl.IsBed,
                            IsConsulting = tbl.IsConsulting,
                            IsOther = tbl.IsOther,
                            IsICU = tbl.IsICU
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EntityChargeCategory GetChargeCategory(int Charge_ID)
        {
            try
            {
                return (from tbl in objData.tblChargeCategories
                        where tbl.IsDelete == false
                        && tbl.ChargesId.Equals(Charge_ID)
                        select new EntityChargeCategory
                        {
                            ChargesId = tbl.ChargesId,
                            ChargeCategoryName = tbl.ChargeCategoryName,
                            IsOperation = tbl.IsOperation,
                            IsBed = tbl.IsBed,
                            IsConsulting = tbl.IsConsulting,
                            IsOther = tbl.IsOther,
                            IsICU = tbl.IsICU
                        }).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int Update(EntityChargeCategory entDept)
        {
            try
            {
                tblChargeCategory test = (from tbl in objData.tblChargeCategories
                                          where tbl.IsDelete == false
                                          && tbl.ChargesId == entDept.ChargesId
                                          select tbl).FirstOrDefault();
                test.ChargesId = entDept.ChargesId;
                test.ChargeCategoryName = entDept.ChargeCategoryName;
                test.IsOperation = entDept.IsOperation;
                test.IsBed = entDept.IsBed;
                test.IsConsulting = entDept.IsConsulting;
                test.IsOther = entDept.IsOther;
                test.IsICU = entDept.IsICU;
                objData.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetCount(string CatagoryType)
        {
            int cnt = 0;
            try
            {
                if (CatagoryType.ToUpper().Equals("O"))
                {
                    List<tblChargeCategory> lst = (from tbl in objData.tblChargeCategories
                                                   where tbl.IsDelete == false
                                                   && tbl.IsOperation == true
                                                   select tbl).ToList();
                    cnt = lst.Count;
                }
                if (CatagoryType.ToUpper().Equals("B"))
                {
                    List<tblChargeCategory> lst = (from tbl in objData.tblChargeCategories
                                                   where tbl.IsDelete == false
                                                   && tbl.IsBed == true
                                                   select tbl).ToList();
                    cnt = lst.Count;
                }
                if (CatagoryType.ToUpper().Equals("C"))
                {
                    List<tblChargeCategory> lst = (from tbl in objData.tblChargeCategories
                                                   where tbl.IsDelete == false
                                                   && tbl.IsConsulting == true
                                                   select tbl).ToList();
                    cnt = lst.Count;
                }
                if (CatagoryType.ToUpper().Equals("I"))
                {
                    List<tblChargeCategory> lst = (from tbl in objData.tblChargeCategories
                                                   where tbl.IsDelete == false
                                                   && tbl.IsICU == true
                                                   select tbl).ToList();
                    cnt = lst.Count;
                }
                if (CatagoryType.ToUpper().Equals("I"))
                {
                    List<tblChargeCategory> lst = (from tbl in objData.tblChargeCategories
                                                   where tbl.IsDelete == false
                                                   && tbl.IsICU == true
                                                   select tbl).ToList();
                    cnt = lst.Count;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cnt;
        }

        public List<tblChargeCategory> GetChargeCategoryDetail()
        {
            return null;
        }

        public int GetCount(string ChargeType, int ChargeId)
        {
            int cnt = 0;
            try
            {
                if (ChargeType.ToUpper().Equals("O"))
                {
                    List<tblChargeCategory> lst = (from tbl in objData.tblChargeCategories
                                                   where tbl.IsDelete == false
                                                   && tbl.IsOperation == true
                                                   && tbl.ChargesId == ChargeId
                                                   select tbl).ToList();
                    cnt = lst.Count;
                }
                if (ChargeType.ToUpper().Equals("B"))
                {
                    List<tblChargeCategory> lst = (from tbl in objData.tblChargeCategories
                                                   where tbl.IsDelete == false
                                                   && tbl.IsBed == true
                                                   && tbl.ChargesId == ChargeId
                                                   select tbl).ToList();
                    cnt = lst.Count;
                }
                if (ChargeType.ToUpper().Equals("C"))
                {
                    List<tblChargeCategory> lst = (from tbl in objData.tblChargeCategories
                                                   where tbl.IsDelete == false
                                                   && tbl.IsConsulting == true
                                                   && tbl.ChargesId == ChargeId
                                                   select tbl).ToList();
                    cnt = lst.Count;
                }
                if (ChargeType.ToUpper().Equals("I"))
                {
                    List<tblChargeCategory> lst = (from tbl in objData.tblChargeCategories
                                                   where tbl.IsDelete == false
                                                   && tbl.IsICU == true
                                                   && tbl.ChargesId == ChargeId
                                                   select tbl).ToList();
                    cnt = lst.Count;
                }
                if (ChargeType.ToUpper().Equals("I"))
                {
                    List<tblChargeCategory> lst = (from tbl in objData.tblChargeCategories
                                                   where tbl.IsDelete == false
                                                   && tbl.IsICU == true
                                                   select tbl).ToList();
                    cnt = lst.Count;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cnt;
        }
    }
}