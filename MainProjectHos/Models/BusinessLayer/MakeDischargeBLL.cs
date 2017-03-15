using MainProjectHos.Models.DataLayer;
using MainProjectHos.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.BusinessLayer
{
    public class MakeDischargeBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();

        public MakeDischargeBLL()
        {
            objData = new CriticareHospitalDataContext();
        }

        public CriticareHospitalDataContext objData { get; set; }

        public int Save(EntityMakeDischarge entDischarge)
        {
            tblDischarge tbl = new tblDischarge()
            {
                PatientId = entDischarge.PatientId,
                TypeOfDischarge = entDischarge.TypeOfDischarge,
                Diagnosis = entDischarge.Diagnosis,
                NameOfSurgery = entDischarge.Surgery,
                HistoryClinical = entDischarge.HistoryClinical,
                XRay = entDischarge.XRay,
                USG = entDischarge.USG,
                Others = entDischarge.Others,
                OperativeNotes = entDischarge.OperativeNotes,
                TreatmentInHospitalisation = entDischarge.TreatmentInHospitalisation,
                TreatmentOnDischarge = entDischarge.TreatmentOnDischarge,
                FollowUp = entDischarge.FollowUp,
                DischargeReceiptDate = entDischarge.DischargeReceiptDate,
                HB = entDischarge.HB,
                TLC = entDischarge.TLC,
                PLC = entDischarge.PLC,
                BUL = entDischarge.BUL,
                Creat = entDischarge.Creat,
                SE = entDischarge.SE,
                BSL = entDischarge.BSL,
                PP = entDischarge.PP,
                R = entDischarge.R,
                Urine = entDischarge.Urine,
                HIV = entDischarge.HIV,
                HBSAG = entDischarge.HBSAG,
                IsDelete = false
            };

            objData.tblDischarges.InsertOnSubmit(tbl);
            objData.SubmitChanges();
            return 1;
        }

        public List<EntityMakeDischarge> GetDischargeInvoiceList()
        {
            List<EntityMakeDischarge> lst = new List<EntityMakeDischarge>();

            lst = (from tbl in objData.tblDischarges
                   join tblAdmit in objData.tblPatientAdmitDetails
                   on tbl.PatientId equals tblAdmit.AdmitId
                   join tblPat in objData.tblPatientMasters
                   on tblAdmit.PatientId equals tblPat.PKId
                   where tbl.IsDelete == false
                   select new EntityMakeDischarge { MRN = tblPat.PatientCode, DichargeId = tbl.DichargeId, PatName = tblPat.PatientFirstName + ' ' + tblPat.PatientMiddleName + ' ' + tblPat.PatientLastName, DischargeReceiptDate = tbl.DischargeReceiptDate, PatientId = tbl.PatientId }).ToList();

            return lst;
        }


        public EntityMakeDischarge GetDischargeDetails(int DischargeId)
        {
            EntityMakeDischarge entDis = new EntityMakeDischarge();

            entDis = (from tbl in objData.tblDischarges
                      where tbl.IsDelete == false
                      && tbl.DichargeId == DischargeId
                      select new EntityMakeDischarge
                      {
                          TypeOfDischarge = tbl.TypeOfDischarge,
                          Diagnosis = tbl.Diagnosis,
                          Surgery = tbl.NameOfSurgery,
                          HistoryClinical = tbl.HistoryClinical,
                          XRay = tbl.XRay,
                          USG = tbl.USG,
                          Others = tbl.Others,
                          OperativeNotes = tbl.OperativeNotes,
                          TreatmentInHospitalisation = tbl.TreatmentInHospitalisation,
                          TreatmentOnDischarge = tbl.TreatmentOnDischarge,
                          FollowUp = tbl.FollowUp,
                          DischargeReceiptDate = tbl.DischargeReceiptDate,
                          HB = tbl.HB,
                          TLC = tbl.TLC,
                          PLC = tbl.PLC,
                          BUL = tbl.BUL,
                          Creat = tbl.Creat,
                          SE = tbl.SE,
                          BSL = tbl.BSL,
                          PP = tbl.PP,
                          R = tbl.R,
                          Urine = tbl.Urine,
                          HIV = tbl.HIV,
                          HBSAG = tbl.HBSAG,
                      }).FirstOrDefault();

            return entDis;
        }



        public int Update(EntityMakeDischarge entDischarge, int DischargeID)
        {
            tblDischarge obj = new tblDischarge();

            obj = (from tbl in objData.tblDischarges
                   where tbl.DichargeId.Equals(DischargeID)
                   && tbl.IsDelete == false
                   select tbl).FirstOrDefault();

            obj.TypeOfDischarge = entDischarge.TypeOfDischarge;
            obj.Diagnosis = entDischarge.Diagnosis;
            obj.NameOfSurgery = entDischarge.Surgery;
            obj.HistoryClinical = entDischarge.HistoryClinical;
            obj.XRay = entDischarge.XRay;
            obj.USG = entDischarge.USG;
            obj.Others = entDischarge.Others;
            obj.OperativeNotes = entDischarge.OperativeNotes;
            obj.TreatmentInHospitalisation = entDischarge.TreatmentInHospitalisation;
            obj.TreatmentOnDischarge = entDischarge.TreatmentOnDischarge;
            obj.FollowUp = entDischarge.FollowUp;
            obj.DischargeReceiptDate = entDischarge.DischargeReceiptDate;
            obj.HB = entDischarge.HB;
            obj.TLC = entDischarge.TLC;
            obj.PLC = entDischarge.PLC;
            obj.BUL = entDischarge.BUL;
            obj.Creat = entDischarge.Creat;
            obj.SE = entDischarge.SE;
            obj.BSL = entDischarge.BSL;
            obj.PP = entDischarge.PP;
            obj.R = entDischarge.R;
            obj.Urine = entDischarge.Urine;
            obj.HIV = entDischarge.HIV;
            obj.HBSAG = entDischarge.HBSAG;
            objData.SubmitChanges();
            return 1;
        }

        public DateTime? GetDischargeDetailsByPatientId(int AdmitId)
        {
            DateTime? dt = null;
            try
            {
                tblDischarge objtblDischarge = (from tbl in objData.tblDischarges
                                                where tbl.IsDelete == false
                                                && tbl.PatientId == AdmitId
                                                select tbl).FirstOrDefault();
                if (objtblDischarge != null)
                {
                    dt = objtblDischarge.DischargeReceiptDate;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
    }
}