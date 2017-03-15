using MainProjectHos.Models.DataLayer;
using MainProjectHos.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.BusinessLayer
{
    public class OutstandingBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public OutstandingBLL()
        {
            objData = new CriticareHospitalDataContext();

        }
        public CriticareHospitalDataContext objData { get; set; }

        public List<STP_OutstandingReportResult> SearchOutstanding(DateTime fromdate, DateTime todate)
        {
            try
            {
                return (objData.STP_OutstandingReport(fromdate, todate)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<STP_SupplierOutstandingReportResult> SearchSupplierOutstanding(DateTime fromdate, DateTime todate)
        {
            try
            {
                return (objData.STP_SupplierOutstandingReport(fromdate, todate)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}