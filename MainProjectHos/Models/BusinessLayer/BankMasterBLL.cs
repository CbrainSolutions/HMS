using MainProjectHos.Models.DataLayer;
using MainProjectHos.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.BusinessLayer
{
    public class BankMasterBLL
    {
        public BankMasterBLL()
        {
            objData = new CriticareHospitalDataContext();
        }

        public CriticareHospitalDataContext objData { get; set; }

        public int InsertBankMaster(EntityBankMaster bank)
        {
            int i = 0;
            try
            {
                tblBankMaster objbank = new tblBankMaster()
                {
                    AccountNo = bank.AccountNo,
                    BankAddress = bank.BankAddress,
                    BankName = bank.BankName,
                    IFSCCode = bank.IFSCCode,
                    MISCCode = bank.MISCCode,
                    BranchName = bank.BranchName,
                    City = bank.City,
                    CustomerId = bank.CustomerId,
                    MobileNo = bank.MobileNo,
                    PhNo = bank.PhNo,
                    Pin = bank.Pin,
                    IsDelete = false
                };
                objData.tblBankMasters.InsertOnSubmit(objbank);
                objData.SubmitChanges();
                i++;
            }
            catch (Exception ex)
            {
                i = 0;
                throw ex;
            }
            return i;
        }

        public int Update(EntityBankMaster bank)
        {
            int i = 0;
            try
            {
                tblBankMaster objBank = (from tbl in objData.tblBankMasters
                                         where tbl.BankId == bank.BankId
                                         && tbl.IsDelete == false
                                         select tbl).FirstOrDefault();
                if (objBank != null)
                {
                    objBank.BankAddress = bank.BankAddress;
                    objBank.BankName = bank.BankName;
                    objBank.IFSCCode = bank.IFSCCode;
                    objBank.MISCCode = bank.MISCCode;
                    objBank.BranchName = bank.BranchName;
                    objBank.City = bank.City;
                    objBank.CustomerId = bank.CustomerId;
                    objBank.MobileNo = bank.MobileNo;
                    objBank.PhNo = bank.PhNo;
                    objBank.Pin = bank.Pin;
                }
                objData.SubmitChanges();
                i++;
            }
            catch (Exception ex)
            {
                i = 0;
                throw ex;
            }
            return i;
        }

        public List<EntityBankMaster> GetBankDetails()
        {
            List<EntityBankMaster> lst = null;
            try
            {
                lst = (from bank in objData.tblBankMasters
                       where bank.IsDelete == false
                       select new EntityBankMaster
                       {
                           AccountNo = bank.AccountNo,
                           BankAddress = bank.BankAddress,
                           BankName = bank.BankName,
                           IFSCCode = bank.IFSCCode,
                           MISCCode = bank.MISCCode,
                           BranchName = bank.BranchName,
                           City = bank.City,
                           CustomerId = bank.CustomerId,
                           MobileNo = bank.MobileNo,
                           PhNo = bank.PhNo,
                           Pin = bank.Pin,
                           BankId = bank.BankId
                       }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }

        public EntityBankMaster GetBank(EntityBankMaster entSupplier)
        {
            EntityBankMaster bank = null;
            try
            {
                bank = (from tbl in objData.tblBankMasters
                        where tbl.BankId == entSupplier.BankId
                        && tbl.IsDelete == false
                        && tbl.AccountNo == entSupplier.AccountNo
                        select new EntityBankMaster { }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bank;
        }

        public List<EntityBankMaster> SelectBanks(string Prefix)
        {
            try
            {
                return (from tbl in GetBankDetails()
                        where tbl.BankName.ToUpper().Contains(Prefix.ToUpper())
                        || tbl.BankAddress.ToUpper().Contains(Prefix.ToUpper())
                        || tbl.IFSCCode.ToUpper().Contains(Prefix.ToUpper())
                        || tbl.AccountNo.ToUpper().Contains(Prefix.ToUpper())
                        select tbl).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EntityBankMaster GetBank(int BankId)
        {
            EntityBankMaster bank = null;
            try
            {
                bank = (from tbl in objData.tblBankMasters
                        where tbl.BankId == BankId
                        && tbl.IsDelete == false
                        select new EntityBankMaster
                        {
                            AccountNo = tbl.AccountNo,
                            BankAddress = tbl.BankAddress,
                            BankName = tbl.BankName,
                            IFSCCode = tbl.IFSCCode,
                            MISCCode = tbl.MISCCode,
                            BranchName = tbl.BranchName,
                            City = tbl.City,
                            CustomerId = tbl.CustomerId,
                            MobileNo = tbl.MobileNo,
                            PhNo = tbl.PhNo,
                            Pin = tbl.Pin,
                            BankId = tbl.BankId
                        }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bank;
        }

        public EntityBankMaster GetBankByAccNo(EntityBankMaster entSupplier)
        {
            EntityBankMaster bank = null;
            try
            {
                bank = (from tbl in objData.tblBankMasters
                        where tbl.IsDelete == false
                        && tbl.AccountNo == entSupplier.AccountNo
                        select new EntityBankMaster { }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bank;
        }
    }
}