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

        public List<STP_DatewiseConsultDoctorResult> SearchDatewiseConsultDoctor(DateTime fromdate, DateTime todate, int deptCatId, int deptDocId)
        {
            try
            {
                return (objData.STP_DatewiseConsultDoctor(fromdate, todate, deptCatId, deptDocId)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<STP_DatewiseConsultDoctorCatResult> SearchDatewiseConsultDoctorCat(DateTime fromdate, DateTime todate, int deptCatId)
        {
            try
            {
                return (objData.STP_DatewiseConsultDoctorCat(fromdate, todate, deptCatId)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<STP_DatewiseConsultDoctorDocResult> SearchDatewiseConsultDoctorDoc(DateTime fromdate, DateTime todate, int deptDocId)
        {
            try
            {
                return (objData.STP_DatewiseConsultDoctorDoc(fromdate, todate, deptDocId)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}