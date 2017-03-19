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
    public class TdsReportBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public TdsReportBLL()
        {
            objData = new CriticareHospitalDataContext();

        }
        public CriticareHospitalDataContext objData { get; set; }

        public List<STP_TdsReportResult> SearchTds(DateTime fromdate, DateTime todate)
        {
            try
            {
                return (objData.STP_TdsReport(fromdate, todate)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<STP_DatewisInsuranceClaimReportResult> SearchDatewiseInsurance(DateTime fromdate, DateTime todate)
        {
            try
            {
                return (objData.STP_DatewisInsuranceClaimReport(fromdate, todate)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<STP_DatewiseCompanyClaimReportResult> SearchDatewiseCompany(DateTime fromdate, DateTime todate)
        {
            try
            {
                return (objData.STP_DatewiseCompanyClaimReport(fromdate, todate)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}