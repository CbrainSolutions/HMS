using MainProjectHos.Models.DataLayer;
using MainProjectHos.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.BusinessLayer
{
    public class AllowanceDeductionBLL
    {
        public AllowanceDeductionBLL()
        {
            objData = new CriticareHospitalDataContext();
        }

        public CriticareHospitalDataContext objData { get; set; }

        public List<EntityAllowanceDeduction> GetAllowDed()
        {
            List<EntityAllowanceDeduction> lst = (from tbl in objData.tblAllowanceDeductions
                                                  where tbl.IsDelete == false
                                                  orderby tbl.IsDeduction
                                                  select new EntityAllowanceDeduction
                                                  {
                                                      AllowDedId = tbl.AllowDedId,
                                                      Description = tbl.Description,
                                                      Percentage = tbl.Percentage,
                                                      Amount = tbl.IsPercentage ? tbl.Percentage : tbl.Amount,
                                                      IsPercentage = tbl.IsPercentage,
                                                      IsFixed = tbl.IsFixed,
                                                      IsFlexible = tbl.IsFlexible,
                                                      IsAllowance = tbl.IsAllowance,
                                                      IsDeduction = tbl.IsDeduction,
                                                      CategoryDesc = tbl.IsAllowance ? "Allowance" : "Deduction",
                                                      Type = tbl.IsFixed ? "Fixed" : tbl.IsFlexible ? "Flexible" : tbl.IsBasic ? "Basic" : "Percentage"
                                                  }).ToList();

            return lst;
        }
        public List<EntityAllowanceDeduction> GetAllowance()
        {
            try
            {

                return (from tbl in objData.tblAllowanceDeductions
                        where tbl.IsDelete == false
                        && tbl.IsAllowance == true
                        select new EntityAllowanceDeduction { AllowDedId = tbl.AllowDedId, Description = tbl.Description }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EntityAllowanceDeduction> GetDeduction()
        {
            try
            {

                return (from tbl in objData.tblAllowanceDeductions
                        where tbl.IsDelete == false
                        && tbl.IsDeduction == true
                        select new EntityAllowanceDeduction { AllowDedId = tbl.AllowDedId, Description = tbl.Description }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public EntityAllowanceDeduction GetDetailsAllownce(int AllownceId)
        {
            EntityAllowanceDeduction lst = (from tbl in objData.tblAllowanceDeductions
                                            where tbl.IsDelete == false
                                            && tbl.IsAllowance == true
                                            && tbl.AllowDedId == AllownceId
                                            select new EntityAllowanceDeduction
                                            {
                                                AllowDedId = tbl.AllowDedId,
                                                Percentage = tbl.Percentage,
                                                IsPercentage = tbl.IsPercentage,
                                                IsFixed = tbl.IsFixed,
                                                IsFlexible = tbl.IsFlexible,
                                                Amount = tbl.Amount


                                            }).FirstOrDefault();

            return lst;
        }
        public EntityAllowanceDeduction GetDetailsDeduction(int deductionId)
        {
            EntityAllowanceDeduction lst = (from tbl in objData.tblAllowanceDeductions
                                            where tbl.IsDelete == false
                                            && tbl.IsDeduction == true
                                            && tbl.AllowDedId == deductionId
                                            select new EntityAllowanceDeduction
                                            {
                                                AllowDedId = tbl.AllowDedId,
                                                Percentage = tbl.Percentage,
                                                Amount = tbl.Amount,
                                                IsPercentage = tbl.IsPercentage,
                                                IsFixed = tbl.IsFixed,
                                                IsFlexible = tbl.IsFlexible,


                                            }).FirstOrDefault();

            return lst;
        }
        public EntityAllowanceDeduction ValidateDescName(EntityAllowanceDeduction objInfo)
        {
            try
            {


                tblAllowanceDeduction objBatch = (from tbl in objData.tblAllowanceDeductions
                                                  where tbl.Description.ToUpper().Equals(objInfo.Description)
                                                  && tbl.AllowDedId == objInfo.AllowDedId
                                                  && tbl.IsDelete == false
                                                  select tbl).FirstOrDefault();

                if (objBatch != null)
                {
                    return objInfo;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EntityAllowanceDeduction ValidateByName(EntityAllowanceDeduction objInfo)
        {
            try
            {


                tblAllowanceDeduction objBatch = (from tbl in objData.tblAllowanceDeductions
                                                  where tbl.Description.ToUpper().Equals(objInfo.Description)
                                                  && tbl.IsDelete == false
                                                  select tbl).FirstOrDefault();

                if (objBatch != null)
                {
                    return objInfo;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Save(EntityAllowanceDeduction objInfo)
        {
            try
            {

                tblAllowanceDeduction objBatch = new tblAllowanceDeduction();
                objBatch.Description = objInfo.Description;
                objBatch.IsFlexible = objInfo.IsFlexible;
                objBatch.IsFixed = objInfo.IsFixed;
                objBatch.IsPercentage = objInfo.IsPercentage;
                objBatch.Amount = objInfo.Amount;
                objBatch.Percentage = objInfo.Percentage;
                objBatch.IsAllowance = objInfo.IsAllowance;
                objBatch.IsDeduction = objInfo.IsDeduction;
                objBatch.IsBasic = objInfo.IsBasic.Value;
                objData.tblAllowanceDeductions.InsertOnSubmit(objBatch);
                objData.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal EntityAllowanceDeduction GetAllowID(int LedgerId)
        {
            EntityAllowanceDeduction lst = (from tbl in objData.tblAllowanceDeductions
                                            where tbl.IsDelete == false
                                            && tbl.AllowDedId == LedgerId
                                            select new EntityAllowanceDeduction
                                            {
                                                AllowDedId = tbl.AllowDedId,
                                                Description = tbl.Description,
                                                Percentage = tbl.Percentage,
                                                Amount = tbl.Amount,
                                                IsPercentage = tbl.IsPercentage,
                                                IsFixed = tbl.IsFixed,
                                                IsFlexible = tbl.IsFlexible,
                                                IsAllowance = tbl.IsAllowance,
                                                IsDeduction = tbl.IsDeduction,
                                            }).FirstOrDefault();

            return lst;
        }

        public void Update(EntityAllowanceDeduction objT)
        {
            try
            {

                tblAllowanceDeduction obj = (from tbl in objData.tblAllowanceDeductions
                                             where tbl.AllowDedId == objT.AllowDedId
                                             && tbl.IsDelete == false
                                             select tbl).FirstOrDefault();
                obj.Description = objT.Description;
                obj.Amount = objT.Amount;
                obj.Percentage = objT.Percentage;
                obj.IsFlexible = objT.IsFlexible;
                obj.IsFixed = objT.IsFixed;
                obj.IsPercentage = objT.IsPercentage;
                obj.IsAllowance = objT.IsAllowance;
                obj.IsDeduction = objT.IsDeduction;
                objData.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EntityAllowanceDeduction SelectFlags(int Allowdedid)
        {
            return (from tbl in objData.tblAllowanceDeductions
                    where tbl.IsDelete == false
                    &&
                    tbl.AllowDedId.Equals(Allowdedid)
                    select new EntityAllowanceDeduction
                    {
                        IsFixed = tbl.IsFixed,
                        IsFlexible = tbl.IsFlexible,
                        IsPercentage = tbl.IsPercentage,
                        IsDeduction = tbl.IsDeduction,
                        IsAllowance = tbl.IsAllowance
                    }).FirstOrDefault();
        }

        public EntityAllowanceDeduction GetAllowDed(EntityAllowanceDeduction ent)
        {
            EntityAllowanceDeduction cnt = (from tbl in objData.tblAllowanceDeductions
                                            where tbl.Description.Equals(ent.Description)
                                            select new EntityAllowanceDeduction { Description = tbl.Description }).FirstOrDefault();
            return cnt;
        }
    }
}