using MainProjectHos.Models.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.BusinessLayer
{
    public class DatewiseCollectionBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public DatewiseCollectionBLL()
        {
            objData = new CriticareHospitalDataContext();
        }
        public CriticareHospitalDataContext objData { get; set; }
        public List<STP_DatewiseCollectionResult> SearchDatewiseCollection(DateTime fromdate, DateTime todate)
        {
            try
            {
                return (objData.STP_DatewiseCollection(fromdate, todate)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}