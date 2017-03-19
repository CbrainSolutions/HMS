using MainProjectHos.Models.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.BusinessLayer
{
    public class BedConsumtionBLL
    {
        //clsDataAccess mobjDataAcces = new clsDataAccess();

        public BedConsumtionBLL()
        {
            objData = new CriticareHospitalDataContext();
        }

        public CriticareHospitalDataContext objData { get; set; }


        public List<STP_BedConsumptionResult> SearchBedConsumption(DateTime fromdate, DateTime todate)
        {
            try
            {
                return (objData.STP_BedConsumption(fromdate, todate)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<STP_DeptCategorywisePatientDetailsResult> DeptCategorywisePatientDetails(DateTime fromdate, DateTime todate, int deptId)
        {
            try
            {
                return (objData.STP_DeptCategorywisePatientDetails(fromdate, todate, deptId)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<STP_DepartmentwisePatientDetailsResult> SearchDepartmentwise(DateTime fromdate, DateTime todate, int deptId, int deptDoctorId, string patientType)
        {
            try
            {
                return (objData.STP_DepartmentwisePatientDetails(fromdate, todate, deptId, deptDoctorId, patientType)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<STP_DeptCatDoctorwisePatientDetailsResult> DeptCatDoctorwisePatientDetails(DateTime fromdate, DateTime todate, int deptId, int deptDoctorId)
        {
            try
            {
                return (objData.STP_DeptCatDoctorwisePatientDetails(fromdate, todate, deptId, deptDoctorId)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<STP_PatientTypewisePatientDetailsResult> SearchDepartmentwise(DateTime fromdate, DateTime todate, string patientType)
        {
            try
            {
                return (objData.STP_PatientTypewisePatientDetails(fromdate, todate, patientType)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<STP_WardwisePatientDetailsResult> SearchWardwise(DateTime fromdate, DateTime todate, string wardname)
        {
            try
            {
                return (objData.STP_WardwisePatientDetails(fromdate, todate, wardname)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<STP_DailyNursingChartResult> SearchDailyChart(DateTime fromdate, DateTime todate)
        {
            try
            {
                return (objData.STP_DailyNursingChart(fromdate, todate)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<STP_DailyDepartmentwiseNursingChartResult> SearchDepartmentwiseDailyChart(DateTime fromdate, DateTime todate, string dept)
        {
            try
            {
                return (objData.STP_DailyDepartmentwiseNursingChart(fromdate, todate, dept)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<STP_ServiceswiseReportAllResult> NewAllServices(DateTime fromdate, DateTime todate)
        {
            try
            {
                return (objData.STP_ServiceswiseReportAll(fromdate, todate)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<STP_ServiceswiseReportUserResult> SearchServicewiseReportUser(DateTime fromdate, DateTime todate, string EmpName)
        {
            try
            {
                return (objData.STP_ServiceswiseReportUser(fromdate, todate, EmpName)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<STP_ServiceswiseReportUserSerResult> SearchServicewiseReportUserSer(DateTime fromdate, DateTime todate, string EmpName, string Services)
        {
            try
            {
                return (objData.STP_ServiceswiseReportUserSer(fromdate, todate, EmpName, Services)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<STP_ServiceswiseReportResult> SearchServicewiseReport(DateTime fromdate, DateTime todate, string services)
        {
            try
            {
                return (objData.STP_ServiceswiseReport(fromdate, todate, services)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<STP_DischargePatientListReportResult> NewAllDischargePatient(DateTime fromdate, DateTime todate)
        {
            try
            {
                return (objData.STP_DischargePatientListReport(fromdate, todate)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<STP_IPDRegistrationBookReportResult> IPDRegistrationBookPatient(DateTime fromdate, DateTime todate)
        {
            try
            {
                return (objData.STP_IPDRegistrationBookReport(fromdate, todate)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<STP_AdmitedPatientListReportResult> AdmitedPatientListReport(DateTime fromdate, DateTime todate)
        {
            try
            {
                return (objData.STP_AdmitedPatientListReport(fromdate, todate)).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<STP_IPDOPDDailyCollectionReportResult> SearchIPDOPDDailyCollReport(DateTime fromdate, DateTime todate, string patientType)
        {
            try
            {
                return (objData.STP_IPDOPDDailyCollectionReport(fromdate, todate, patientType)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<STP_IPDOPDDailyCollectionCompanyReportResult> SearchIPDOPDDailyCollCompanyReport(DateTime fromdate, DateTime todate)
        {
            try
            {
                return (objData.STP_IPDOPDDailyCollectionCompanyReport(fromdate, todate)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<STP_IPDOPDDailyCollectionInsuranceReportResult> SearchIPDOPDDailyCollInsuranceReport(DateTime fromdate, DateTime todate)
        {
            try
            {
                return (objData.STP_IPDOPDDailyCollectionInsuranceReport(fromdate, todate)).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<STP_IPDOPDDailyCollectionReportUserwiseResult> SearchIPDOPdCollUserwise(DateTime fromDate, DateTime toDate, string empName)
        {
            try
            {
                return (objData.STP_IPDOPDDailyCollectionReportUserwise(fromDate, toDate, empName)).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<STP_IPDOPDDailyCollectionReportUserCatResult> SearchIPDOPDCollUserCategorywise(DateTime fromDate, DateTime toDate, string empName, string patientType)
        {
            try
            {
                return (objData.STP_IPDOPDDailyCollectionReportUserCat(fromDate, toDate, empName, patientType)).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<STP_IPDOPDDailyCollectionReportUserCatResult> SearchIPDOPDCollOtherUser(DateTime fromDate, DateTime toDate, string empName, string patientType)
        {
            try
            {
                return (objData.STP_IPDOPDDailyCollectionReportUserCat(fromDate, toDate, empName, patientType)).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<STP_IPDOPDDailyCollectionReportDepartmentwiseResult> SearchIPDOPDCollDepartmentwise(DateTime fromDate, DateTime toDate, string deptName)
        {
            try
            {
                return (objData.STP_IPDOPDDailyCollectionReportDepartmentwise(fromDate, toDate, deptName)).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<STP_IPDOPDDailyCollectionReportDoctorwiseResult> SearchIPDOPDCollDoctorwise(DateTime fromDate, DateTime toDate, string deptName, string doctoName)
        {
            try
            {
                return (objData.STP_IPDOPDDailyCollectionReportDoctorwise(fromDate, toDate, deptName, doctoName)).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<STP_IPDOPDDailyCollectionReportDeptPatCatResult> SearchIPDOPDCollDoctDeptwise(DateTime fromDate, DateTime toDate, string deptName, string doctorName, string patientType)
        {
            try
            {
                return (objData.STP_IPDOPDDailyCollectionReportDeptPatCat(fromDate, toDate, deptName, doctorName, patientType)).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<STP_IPDOPDDailyCollectionReportpattyUserwiseResult> SearchIPDOPDCollpattyuser(DateTime fromDate, DateTime toDate, string UserName, string patientType)
        {
            try
            {
                return (objData.STP_IPDOPDDailyCollectionReportpattyUserwise(fromDate, toDate, UserName, patientType)).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public decimal GetIPDRegFeeInvoiceDetails(DateTime fromDate, DateTime toDate)
        {
            List<tblPatientInvoiceDetail> lstTrans = new List<tblPatientInvoiceDetail>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.IsIPD == true
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tblPaDet in objData.tblPatientInvoiceDetails
                                      join tbl in objData.tblPatientInvoices
                                      on tblPaDet.BillNo equals tbl.BillNo
                                      join tblCharge in objData.tblChargeCategories
                                      on tblPaDet.OtherChargesId equals tblCharge.ChargesId
                                      where tbl.PatientId == item.AdmitId
                                      && tbl.BillDate >= fromDate && tbl.BillDate <= toDate
                                      && tbl.IsDelete == false && tblCharge.ChargeCategoryName == "Registration Fees"
                                      && tbl.BillType != "Estimated"
                                      select tblPaDet);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.ChargePerDay));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetOPDRegFeeInvoiceDetails(DateTime fromDate, DateTime toDate)
        {
            List<tblPatientInvoiceDetail> lstTrans = new List<tblPatientInvoiceDetail>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.IsOPD == true
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tblPaDet in objData.tblPatientInvoiceDetails
                                      join tbl in objData.tblPatientInvoices
                                      on tblPaDet.BillNo equals tbl.BillNo
                                      join tblCharge in objData.tblChargeCategories
                                      on tblPaDet.OtherChargesId equals tblCharge.ChargesId
                                      where tbl.PatientId == item.AdmitId
                                      && tbl.BillDate >= fromDate && tbl.BillDate <= toDate
                                      && tbl.IsDelete == false && tblCharge.ChargeCategoryName == "Registration Fees"
                                      && tbl.BillType != "Estimated"
                                      select tblPaDet);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.ChargePerDay));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetIPDDiscountInvoiceDetails(DateTime fromDate, DateTime toDate)
        {
            List<tblPatientInvoice> lstTrans = new List<tblPatientInvoice>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.IsIPD == true && tbl.IsDischarge == true
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblPatientInvoices
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.BillDate >= fromDate && tbl.BillDate <= toDate
                                      && tbl.IsDelete == false && tbl.BillType != "Estimated"
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.FixedDiscount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetOPDDiscountInvoiceDetails(DateTime fromDate, DateTime toDate)
        {
            List<tblPatientInvoice> lstTrans = new List<tblPatientInvoice>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.IsOPD == true
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblPatientInvoices
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.BillDate >= fromDate && tbl.BillDate <= toDate
                                      && tbl.IsDelete == false && tbl.BillType != "Estimated"
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.FixedDiscount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetDDEP(DateTime fromDate, DateTime toDate, string EmpName, string patientType)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.PatientType == patientType
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Invoice") && tbl.IsCash == true
                                          && tbl.PreparedByName == EmpName
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetDDEP1(DateTime fromDate, DateTime toDate, string EmpName, string patientType)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal receipt = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.PatientType == patientType
                                                        select tbl).ToList();

                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Receipt")
                                          && tbl.PreparedByName == EmpName
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                receipt = Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return receipt;
        }

        public decimal GetDDEP2(DateTime fromDate, DateTime toDate, string EmpName, string patientType)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal receipt = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.PatientType == patientType
                                                        select tbl).ToList();

                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Receipt")
                                          && tbl.PreparedByName == EmpName
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                receipt = Convert.ToDecimal(lstTrans.Sum(p => p.AdvanceAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return receipt;
        }

        public decimal GetDDEPk1(DateTime fromDate, DateTime toDate, string DeptName, string doctName, string patientType)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        join tbl1 in objData.tblOperationCategories
                                                        on tbl.DeptCategory equals tbl1.CategoryId
                                                        join tbl3 in objData.tblEmployees
                                                        on tbl.DeptDoctorId equals tbl3.PKId
                                                        where tbl.IsDelete == false
                                                        && tbl1.CategoryName == DeptName
                                                        && tbl.PatientType == patientType
                                                        && (tbl3.EmpFirstName + ' ' + tbl3.EmpMiddleName + ' ' + tbl3.EmpLastName) == doctName
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Invoice") && tbl.IsCash == true
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetDDEPk2(DateTime fromDate, DateTime toDate, string DeptName, string doctName, string patientType)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal receipt = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        join tbl1 in objData.tblOperationCategories
                                                        on tbl.DeptCategory equals tbl1.CategoryId
                                                        join tbl3 in objData.tblEmployees
                                                        on tbl.DeptDoctorId equals tbl3.PKId
                                                        where tbl.IsDelete == false
                                                        && tbl1.CategoryName == DeptName
                                                        && tbl.PatientType == patientType
                                                        && (tbl3.EmpFirstName + ' ' + tbl3.EmpMiddleName + ' ' + tbl3.EmpLastName) == doctName
                                                        select tbl).ToList();

                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Receipt")
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                receipt = Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return receipt;
        }

        public decimal GetDDEPk3(DateTime fromDate, DateTime toDate, string DeptName, string doctName, string patientType)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal receipt = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        join tbl1 in objData.tblOperationCategories
                                                        on tbl.DeptCategory equals tbl1.CategoryId
                                                        join tbl3 in objData.tblEmployees
                                                        on tbl.DeptDoctorId equals tbl3.PKId
                                                        where tbl.IsDelete == false
                                                        && tbl1.CategoryName == DeptName
                                                        && tbl.PatientType == patientType
                                                        && (tbl3.EmpFirstName + ' ' + tbl3.EmpMiddleName + ' ' + tbl3.EmpLastName) == doctName
                                                        select tbl).ToList();

                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Receipt")
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                receipt = Convert.ToDecimal(lstTrans.Sum(p => p.AdvanceAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return receipt;
        }


        public decimal GetDDEPj1(DateTime fromDate, DateTime toDate, string DeptName, string doctName)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        join tbl1 in objData.tblOperationCategories
                                                        on tbl.DeptCategory equals tbl1.CategoryId
                                                        join tbl3 in objData.tblEmployees
                                                        on tbl.DeptDoctorId equals tbl3.PKId
                                                        where tbl.IsDelete == false
                                                        && tbl1.CategoryName == DeptName
                                                        && (tbl3.EmpFirstName + ' ' + tbl3.EmpMiddleName + ' ' + tbl3.EmpLastName) == doctName
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Invoice") && tbl.IsCash == true
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetDDEPj2(DateTime fromDate, DateTime toDate, string DeptName, string doctName)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal receipt = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        join tbl1 in objData.tblOperationCategories
                                                        on tbl.DeptCategory equals tbl1.CategoryId
                                                        join tbl3 in objData.tblEmployees
                                                        on tbl.DeptDoctorId equals tbl3.PKId
                                                        where tbl.IsDelete == false
                                                        && tbl1.CategoryName == DeptName
                                                        && (tbl3.EmpFirstName + ' ' + tbl3.EmpMiddleName + ' ' + tbl3.EmpLastName) == doctName
                                                        select tbl).ToList();

                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Receipt")
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                receipt = Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return receipt;
        }

        public decimal GetDDEPj3(DateTime fromDate, DateTime toDate, string DeptName, string doctName)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal receipt = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        join tbl1 in objData.tblOperationCategories
                                                        on tbl.DeptCategory equals tbl1.CategoryId
                                                        join tbl3 in objData.tblEmployees
                                                        on tbl.DeptDoctorId equals tbl3.PKId
                                                        where tbl.IsDelete == false
                                                        && tbl1.CategoryName == DeptName
                                                        && (tbl3.EmpFirstName + ' ' + tbl3.EmpMiddleName + ' ' + tbl3.EmpLastName) == doctName
                                                        select tbl).ToList();

                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Receipt")
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                receipt = Convert.ToDecimal(lstTrans.Sum(p => p.AdvanceAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return receipt;
        }

        public decimal GetDDEPi1(DateTime fromDate, DateTime toDate, string DeptName)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        join tbl1 in objData.tblOperationCategories
                                                        on tbl.DeptCategory equals tbl1.CategoryId
                                                        where tbl.IsDelete == false
                                                        && tbl1.CategoryName == DeptName
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Invoice") && tbl.IsCash == true
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetDDEPi2(DateTime fromDate, DateTime toDate, string DeptName)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal receipt = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        join tbl1 in objData.tblOperationCategories
                                                        on tbl.DeptCategory equals tbl1.CategoryId
                                                        where tbl.IsDelete == false
                                                        && tbl1.CategoryName == DeptName
                                                        select tbl).ToList();

                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Receipt")
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                receipt = Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return receipt;
        }

        public decimal GetDDEPi3(DateTime fromDate, DateTime toDate, string DeptName)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal receipt = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        join tbl1 in objData.tblOperationCategories
                                                        on tbl.DeptCategory equals tbl1.CategoryId
                                                        where tbl.IsDelete == false
                                                        && tbl1.CategoryName == DeptName
                                                        select tbl).ToList();

                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Receipt")
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                receipt = Convert.ToDecimal(lstTrans.Sum(p => p.AdvanceAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return receipt;
        }


        public decimal GetDDEPg1(DateTime fromDate, DateTime toDate, string EmpName, string patientType)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.PatientType == patientType
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Invoice") && tbl.IsCash == true
                                          && tbl.PreparedByName == EmpName
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetDDEPg2(DateTime fromDate, DateTime toDate, string EmpName, string patientType)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal receipt = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.PatientType == patientType
                                                        select tbl).ToList();

                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Receipt")
                                          && tbl.PreparedByName == EmpName
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                receipt = Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return receipt;
        }

        public decimal GetDDEPg3(DateTime fromDate, DateTime toDate, string EmpName, string patientType)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal receipt = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.PatientType == patientType
                                                        select tbl).ToList();

                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Receipt")
                                          && tbl.PreparedByName == EmpName
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                receipt = Convert.ToDecimal(lstTrans.Sum(p => p.AdvanceAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return receipt;
        }

        public decimal GetDDEPf1(DateTime fromDate, DateTime toDate, string EmpName)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Invoice") && tbl.IsCash == true
                                          && tbl.PreparedByName == EmpName
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetDDEPf2(DateTime fromDate, DateTime toDate, string EmpName)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal receipt = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        select tbl).ToList();

                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Receipt")
                                          && tbl.PreparedByName == EmpName
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                receipt = Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return receipt;
        }

        public decimal GetDDEPf3(DateTime fromDate, DateTime toDate, string EmpName)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal receipt = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        select tbl).ToList();

                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Receipt")
                                          && tbl.PreparedByName == EmpName
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                receipt = Convert.ToDecimal(lstTrans.Sum(p => p.AdvanceAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return receipt;
        }

        public decimal GetDDEPe1(DateTime fromDate, DateTime toDate, string patientType)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.PatientType == patientType
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Invoice") && tbl.IsCash == true
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetDDEPe2(DateTime fromDate, DateTime toDate, string patientType)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal receipt = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.PatientType == patientType
                                                        select tbl).ToList();

                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Receipt")
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                receipt = Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return receipt;
        }

        public decimal GetDDEPe3(DateTime fromDate, DateTime toDate, string patientType)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal receipt = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.PatientType == patientType
                                                        select tbl).ToList();

                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Receipt")
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                receipt = Convert.ToDecimal(lstTrans.Sum(p => p.AdvanceAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return receipt;
        }

        public decimal GetDDEPd1(DateTime fromDate, DateTime toDate, string patientType)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.PatientType == patientType
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Invoice") && tbl.IsCash == true
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetDDEPd2(DateTime fromDate, DateTime toDate, string patientType)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal receipt = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.PatientType == patientType
                                                        select tbl).ToList();

                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Receipt")
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                receipt = Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount) + lstTrans.Sum(p => p.AdvanceAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return receipt;
        }

        public decimal GetDDEPd3(DateTime fromDate, DateTime toDate, string patientType)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal receipt = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.PatientType == patientType
                                                        select tbl).ToList();

                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Receipt")
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                receipt = Convert.ToDecimal(lstTrans.Sum(p => p.AdvanceAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return receipt;
        }

        public decimal GetDDEPc1(DateTime fromDate, DateTime toDate, string patientType)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.PatientType == patientType
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Invoice") && tbl.IsCash == true
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetDDEPc2(DateTime fromDate, DateTime toDate, string patientType)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal receipt = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.PatientType == patientType
                                                        select tbl).ToList();

                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Receipt")
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                receipt = Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return receipt;
        }

        public decimal GetDDEPc3(DateTime fromDate, DateTime toDate, string patientType)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal receipt = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.PatientType == patientType
                                                        select tbl).ToList();

                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Receipt")
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                receipt = Convert.ToDecimal(lstTrans.Sum(p => p.AdvanceAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return receipt;
        }

        public decimal GetDDEPb1(DateTime fromDate, DateTime toDate, string EmpName)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Invoice") && tbl.IsCash == true
                                          && tbl.PreparedByName == EmpName
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetDDEPb2(DateTime fromDate, DateTime toDate, string EmpName)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal receipt = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        select tbl).ToList();

                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Receipt")
                                          && tbl.PreparedByName == EmpName
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                receipt = Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return receipt;
        }

        public decimal GetDDEb3(DateTime fromDate, DateTime toDate, string EmpName)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal receipt = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        select tbl).ToList();

                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Receipt")
                                          && tbl.PreparedByName == EmpName
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                receipt = Convert.ToDecimal(lstTrans.Sum(p => p.AdvanceAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return receipt;
        }

        public decimal GetCashInvoiceDetails(DateTime fromDate, DateTime toDate)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.IsIPD == true && tbl.IsDischarge == true
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Invoice") && tbl.IsCash == true
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetRecCashDetails(DateTime fromDate, DateTime toDate)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.IsIPD == true && tbl.IsDischarge == true
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Receipt") && tbl.IsCash == true
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetRecChequeDetails(DateTime fromDate, DateTime toDate)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.IsIPD == true && tbl.IsDischarge == true
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Receipt") && tbl.ISCheque == true
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetRecCardDetails(DateTime fromDate, DateTime toDate)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.IsIPD == true && tbl.IsDischarge == true
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Receipt") && tbl.IsCard == true
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetRecRTGSDetails(DateTime fromDate, DateTime toDate)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.IsIPD == true && tbl.IsDischarge == true
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Receipt") && tbl.IsRTGS == true
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }


        public decimal GetOPDCashInvoiceDetails(DateTime fromDate, DateTime toDate)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.IsOPD == true
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Invoice") && tbl.IsCash == true
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetOPDRecCashDetails(DateTime fromDate, DateTime toDate)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.IsOPD == true
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Receipt") && tbl.IsCash == true
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetOPDRecChequeDetails(DateTime fromDate, DateTime toDate)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.IsOPD == true
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Receipt") && tbl.ISCheque == true
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetOPDRecCardDetails(DateTime fromDate, DateTime toDate)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.IsOPD == true
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Receipt") && tbl.IsCard == true
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetOPDRecRTGSDetails(DateTime fromDate, DateTime toDate)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.IsOPD == true
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Receipt") && tbl.IsRTGS == true
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetIPDAdvanceRecCashDetails(DateTime fromDate, DateTime toDate)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.IsIPD == true
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Receipt") && tbl.IsCash == true
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.AdvanceAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetIPDAdvanceRecChequeDetails(DateTime fromDate, DateTime toDate)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.IsIPD == true
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Receipt") && tbl.ISCheque == true
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.AdvanceAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetIPDAdvanceRecCardDetails(DateTime fromDate, DateTime toDate)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.IsIPD == true
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Receipt") && tbl.IsCard == true
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.AdvanceAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetIPDAdvanceRecRTGSDetails(DateTime fromDate, DateTime toDate)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.IsIPD == true
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Receipt") && tbl.IsRTGS == true
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.AdvanceAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetIPDCashRefundDetails(DateTime fromDate, DateTime toDate)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.IsIPD == true
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Refund") && tbl.IsCash == true
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.BillAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetIPDChequeRefundDetails(DateTime fromDate, DateTime toDate)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.IsIPD == true
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Refund") && tbl.ISCheque == true
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.BillAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetIPDCardRefundDetails(DateTime fromDate, DateTime toDate)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.IsIPD == true
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Refund") && tbl.IsCard == true
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.BillAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetIPDRTGSRefundDetails(DateTime fromDate, DateTime toDate)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.IsIPD == true
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Refund") && tbl.IsRTGS == true
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.BillAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetOPDCashRefundDetails(DateTime fromDate, DateTime toDate)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.IsOPD == true
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Refund") && tbl.IsCash == true
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.BillAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetOPDChequeRefundDetails(DateTime fromDate, DateTime toDate)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.IsOPD == true
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Refund") && tbl.ISCheque == true
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.BillAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetOPDCardRefundDetails(DateTime fromDate, DateTime toDate)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.IsOPD == true
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Refund") && tbl.IsCard == true
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.BillAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetOPDRTGSRefundDetails(DateTime fromDate, DateTime toDate)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.IsOPD == true
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Refund") && tbl.IsRTGS == true
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.BillAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetCreditAmtDetails(DateTime fromDate, DateTime toDate)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                      && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.BillAmount)) - (Convert.ToDecimal(lstTrans.Sum(p => p.AdvanceAmount)) + Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount)));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetOPDRefundPayDetails(DateTime fromDate, DateTime toDate)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        && tbl.IsOPD == true
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                      && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.BillAmount)) - (Convert.ToDecimal(lstTrans.Sum(p => p.AdvanceAmount)) + Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount)));
                if (FinalAmount >= 0)
                {
                    FinalAmount = 0;
                }
                else
                {
                    FinalAmount = (Convert.ToDecimal(lstTrans.Sum(p => p.AdvanceAmount)) + Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount))) - Convert.ToDecimal(lstTrans.Sum(p => p.BillAmount));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetInsuranceRTGSClaimDetails(DateTime fromDate, DateTime toDate)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Claim") && tbl.IsRTGS == true
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetInsuranceCashClaimDetails(DateTime fromDate, DateTime toDate)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Claim") && tbl.IsCash == true
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetInsuranceChequeClaimDetails(DateTime fromDate, DateTime toDate)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Claim") && tbl.ISCheque == true
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetInsuranceCardClaimDetails(DateTime fromDate, DateTime toDate)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("Claim") && tbl.IsCard == true
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetCompanyRTGSClaimDetails(DateTime fromDate, DateTime toDate)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("CompanyClaim") && tbl.IsRTGS == true
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetCompanyCashClaimDetails(DateTime fromDate, DateTime toDate)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("CompanyClaim") && tbl.IsCash == true
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetCompanyChequeClaimDetails(DateTime fromDate, DateTime toDate)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("CompanyClaim") && tbl.ISCheque == true
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }

        public decimal GetCompanyCardClaimDetails(DateTime fromDate, DateTime toDate)
        {
            List<tblCustomerTransaction> lstTrans = new List<tblCustomerTransaction>();
            decimal FinalAmount = 0;
            try
            {
                List<tblPatientAdmitDetail> lstAdmit = (from tbl in objData.tblPatientAdmitDetails
                                                        where tbl.IsDelete == false
                                                        select tbl).ToList();
                foreach (tblPatientAdmitDetail item in lstAdmit)
                {
                    lstTrans.AddRange(from tbl in objData.tblCustomerTransactions
                                      where tbl.PatientId == item.AdmitId
                                          && tbl.TransactionType.Equals("CompanyClaim") && tbl.IsCard == true
                                          && tbl.ReceiptDate >= fromDate && tbl.ReceiptDate <= toDate
                                      && tbl.IsDelete == false
                                      select tbl);
                }
                FinalAmount = Convert.ToDecimal(lstTrans.Sum(p => p.PayAmount));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FinalAmount;
        }
    }
}