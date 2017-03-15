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
    }
}