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
    public class OPDPatientMasterBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();

        public OPDPatientMasterBLL()
        {
            objData = new CriticareHospitalDataContext();
        }

        public CriticareHospitalDataContext objData { get; set; }

        public List<EntityPatientMaster> GetPatientList()
        {
            List<EntityPatientMaster> ldt = null;
            try
            {
                ldt = (from tbl in objData.tblPatientMasters
                       where tbl.IsDelete == false
                       select new EntityPatientMaster
                       {
                           FullName = tbl.PatientFirstName + " " + tbl.PatientMiddleName + " " + tbl.PatientLastName,
                           Age = Convert.ToInt32(tbl.Age),
                           BirthDate = Convert.ToDateTime(tbl.BirthDate),
                           GenderDesc = tbl.Gender == 1 ? "Male" : "Female",
                           PatientCode = tbl.PatientCode,
                           PatientAddress = tbl.Address,
                           PatientContactNo = tbl.ContactNo
                       }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public List<sp_GetAllPatientListForRegisteredResult> GetPatientListForRegistered()
        {
            List<sp_GetAllPatientListForRegisteredResult> ldt = null;
            try
            {
                ldt = objData.sp_GetAllPatientListForRegistered().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable GetNewOPDBillNo()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetNewOPDBillNo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable GetNewDiagnosisCode()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetNewDiagnosisCode");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable GetOPDChargesForConsultant(int pintConsultanatId)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@ConsultantId", DbType.String, pintConsultanatId);
                ldt = mobjDataAcces.GetDataTable("sp_GetOPdChargesForConsultanat", lstParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable GetXRayTestList()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllXRayTest");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable GetAllBloodTest()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllBloodTest");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public decimal GetRate(string pstrChangeDescruption)
        {
            decimal ldc = 0.0M;
            try
            {
                ldc = Commons.ConvertToDecimal(mobjDataAcces.ExecuteScalar("Select  Rate  From tblChargeMaster Where  ChargeDescription = '" + pstrChangeDescruption + "'"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldc;
        }

        private List<SqlParameter> CreateParameterOPDPatient(EntityOPDPatient entOPDPatient)
        {
            List<SqlParameter> lstParam = new List<SqlParameter>();
            Commons.ADDParameter(ref lstParam, "@PatientCode", DbType.String, entOPDPatient.PatientCode);
            Commons.ADDParameter(ref lstParam, "@PatientName", DbType.String, entOPDPatient.PatientName);
            Commons.ADDParameter(ref lstParam, "@Medicine", DbType.String, entOPDPatient.DrugName);
            Commons.ADDParameter(ref lstParam, "@Morning", DbType.Boolean, entOPDPatient.Morning);
            Commons.ADDParameter(ref lstParam, "@AfterNoon", DbType.Boolean, entOPDPatient.AfterNoon);
            Commons.ADDParameter(ref lstParam, "@Night", DbType.Boolean, entOPDPatient.Night);
            Commons.ADDParameter(ref lstParam, "@EntryBy", DbType.String, entOPDPatient.EntryBy);
            Commons.ADDParameter(ref lstParam, "@DiagnosisNo", DbType.String, entOPDPatient.DiagnosisCode);
            return lstParam;
        }

        public int InsertOPDPatientTreatmentDetail(List<EntityOPDPatient> lstentOPDMaster, EntityOPDPatient entOPDMaster)
        {
            int cnt = 0;
            List<string> lstspNames = new List<string>();
            List<List<SqlParameter>> lstParamVals = new List<List<SqlParameter>>();
            List<SqlParameter> lstParam;
            try
            {
                foreach (EntityOPDPatient entOPD in lstentOPDMaster)
                {
                    lstspNames.Add("sp_InsertOPDPatient");
                    lstParamVals.Add(CreateParameterOPDPatient(entOPD));
                }

                lstspNames.Add("sp_InsertOPDPatientDiagnosis");
                lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@PatientCode", DbType.String, entOPDMaster.PatientCode);
                Commons.ADDParameter(ref lstParam, "@PatientName", DbType.String, entOPDMaster.PatientName);
                Commons.ADDParameter(ref lstParam, "@Injections", DbType.Int32, entOPDMaster.TotalInjections);
                Commons.ADDParameter(ref lstParam, "@InjectionCharge", DbType.Decimal, entOPDMaster.InjectionCharge);
                Commons.ADDParameter(ref lstParam, "@DressingCharge", DbType.Decimal, entOPDMaster.DressingCharge);
                Commons.ADDParameter(ref lstParam, "@RevisitCharge", DbType.Decimal, entOPDMaster.RevisitCharge);
                Commons.ADDParameter(ref lstParam, "@ConsultantCharge", DbType.Decimal, entOPDMaster.ConsultantCharge);
                Commons.ADDParameter(ref lstParam, "@TotalOPDBill", DbType.Decimal, entOPDMaster.TotalOPDBill);
                Commons.ADDParameter(ref lstParam, "@PatientVisitType", DbType.String, entOPDMaster.PatientVisitType);
                Commons.ADDParameter(ref lstParam, "@ConsultedBy", DbType.Int32, entOPDMaster.ConsultedBy);
                Commons.ADDParameter(ref lstParam, "@EntryBy", DbType.String, entOPDMaster.EntryBy);
                Commons.ADDParameter(ref lstParam, "@DiagnosisNo", DbType.String, entOPDMaster.DiagnosisCode);
                Commons.ADDParameter(ref lstParam, "@Symptoms", DbType.String, entOPDMaster.Symptoms);
                Commons.ADDParameter(ref lstParam, "@Diagnosis", DbType.String, entOPDMaster.Diagnosis);
                Commons.ADDParameter(ref lstParam, "@XRays", DbType.String, entOPDMaster.XRay);
                Commons.ADDParameter(ref lstParam, "@BloodTests", DbType.String, entOPDMaster.BloodTest);
                lstParamVals.Add(lstParam);

                lstspNames.Add("sp_CloseOPDPatient");
                lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@PatientCode", DbType.String, entOPDMaster.PatientCode);
                Commons.ADDParameter(ref lstParam, "@AppointNo", DbType.Int32, entOPDMaster.AppointNO);
                lstParamVals.Add(lstParam);

                cnt = mobjDataAcces.ExecuteTransaction(lstspNames, lstParamVals);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cnt;
        }

        public DataTable GetPatientByPID(string pstrPatientCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@PatientCode", DbType.String, pstrPatientCode);
                ldt = mobjDataAcces.GetDataTable("sp_SelectPatientDetailByPatientId", lstParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable GetCompanies()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetCompanies");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable GetPatientDetailById(string pstrPatientCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@PatientCode", DbType.String, pstrPatientCode);
                ldt = mobjDataAcces.GetDataTable("sp_GetOPDPatientDetailByCode", lstParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable GetOPDPatientDetailForReport(string pstrPatientCode, string pstrOPDBillNo)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@PatientCode", DbType.String, pstrPatientCode);
                Commons.ADDParameter(ref lstParam, "@OPDBillNo", DbType.String, pstrOPDBillNo);
                ldt = mobjDataAcces.GetDataTable("sp_GetOPDInfoForReport", lstParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable SelectCheckedPatient()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_SelectCheckedPatient");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public int InsertOPDBill(EntityOPDBilling entOPDBill, bool pbCompFlag)
        {
            int cnt = 0;
            List<string> lstspNames = new List<string>();
            List<SqlParameter> lstParam;
            List<List<SqlParameter>> lstParamVals = new List<List<SqlParameter>>();
            try
            {
                lstspNames.Add("sp_PatientOPDBill");
                lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@BillNo", DbType.String, entOPDBill.OPDBillNo);
                Commons.ADDParameter(ref lstParam, "@OPDNo", DbType.String, entOPDBill.OPDNo);
                Commons.ADDParameter(ref lstParam, "@PatientCode", DbType.String, entOPDBill.PatientCode);
                Commons.ADDParameter(ref lstParam, "@PatientName", DbType.String, entOPDBill.PatientName);
                Commons.ADDParameter(ref lstParam, "@ConsultingCharges", DbType.Decimal, entOPDBill.ConsultantCharges);
                Commons.ADDParameter(ref lstParam, "@InjectionCharge", DbType.Decimal, entOPDBill.InjectionCharge);
                Commons.ADDParameter(ref lstParam, "@DressingCharge", DbType.Decimal, entOPDBill.DressingCharge);
                Commons.ADDParameter(ref lstParam, "@RevisitCharge", DbType.Decimal, entOPDBill.RevisitCharge);
                Commons.ADDParameter(ref lstParam, "@TotalFees", DbType.Decimal, entOPDBill.TotalFees);
                Commons.ADDParameter(ref lstParam, "@ReceivedCharges", DbType.Decimal, entOPDBill.ReceivedFees);
                Commons.ADDParameter(ref lstParam, "@BalanceAmnt", DbType.Decimal, entOPDBill.BalanceAmt);
                Commons.ADDParameter(ref lstParam, "@CompanyCode", DbType.String, entOPDBill.CompanyCode);
                Commons.ADDParameter(ref lstParam, "@EntryBy", DbType.String, entOPDBill.EntryBy);
                Commons.ADDParameter(ref lstParam, "@BillStatus", DbType.String, entOPDBill.BillStatus);
                lstParamVals.Add(lstParam);

                if (pbCompFlag)
                {
                    lstspNames.Add("sp_UpdateOPDBillInCompMaster");
                    lstParam = new List<SqlParameter>();
                    Commons.ADDParameter(ref lstParam, "@CompanyCode", DbType.String, entOPDBill.CompanyCode);
                    Commons.ADDParameter(ref lstParam, "@BalanceAmnt", DbType.Decimal, entOPDBill.BalanceAmt);
                    lstParamVals.Add(lstParam);
                }

                lstspNames.Add("sp_UpdateOPDBillStatusInDiagnosisMT");
                lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@OPDNo", DbType.String, entOPDBill.OPDNo);
                Commons.ADDParameter(ref lstParam, "@BillStatus", DbType.String, entOPDBill.BillStatus);
                lstParamVals.Add(lstParam);

                lstspNames.Add("sp_UpdateOPDBillStatusInDiagnosis");
                lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@OPDNo", DbType.String, entOPDBill.OPDNo);
                Commons.ADDParameter(ref lstParam, "@BillStatus", DbType.String, entOPDBill.BillStatus);
                lstParamVals.Add(lstParam);

                cnt = mobjDataAcces.ExecuteTransaction(lstspNames, lstParamVals);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cnt;
        }


        public List<EntityPatientMaster> SearchPatient(string Prefix)
        {

            List<EntityPatientMaster> lst = null;
            try
            {
                lst = (from tbl in GetPatientList()
                       where (tbl.PatientCode.Contains(Prefix)
                       || tbl.FullName.ToUpper().ToString().Contains(Prefix.ToString().ToUpper())
                       || tbl.PatientAddress.ToUpper().ToString().Contains(Prefix.ToUpper().ToString()) || tbl.Age.ToString().ToUpper().Contains(Prefix.ToUpper())
                       || tbl.GenderDesc.ToString().ToUpper().Contains(Prefix.ToUpper()))
                       select tbl).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }

        public List<EntityPatientMaster> GetAllPatients()
        {
            List<EntityPatientMaster> lst = null;
            try
            {
                lst = (from tbl in objData.tblPatientMasters
                       select new EntityPatientMaster
                       {
                           PatientId = tbl.PKId,
                           FullName = tbl.PatientFirstName + " " + tbl.PatientMiddleName + " " + tbl.PatientLastName,
                       }).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }

        public List<sp_GetAllPatientListForRegisteredResult> GetPatientListForRegistered(string prefix)
        {
            prefix.ToUpper();
            List<sp_GetAllPatientListForRegisteredResult> lst = null;
            try
            {
                lst = (from tbl in GetPatientListForRegistered()
                       where tbl.PatientCode.ToUpper().Contains(prefix.ToUpper())
                       || tbl.FullName.ToUpper().Contains(prefix.ToUpper())
                       || tbl.PatientType.ToUpper().Contains(prefix.ToUpper())
                       select tbl).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }
    }
}