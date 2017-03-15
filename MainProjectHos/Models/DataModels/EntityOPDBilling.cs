using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityOPDBilling
    /// </summary>
    public class EntityOPDBilling
    {
        public EntityOPDBilling()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Properties

        public string OPDBillNo { get; set; }
        public string OPDNo { get; set; }
        public string PatientCode { get; set; }
        public string PatientName { get; set; }
        public string PatientType { get; set; }
        public decimal InjectionCharge { get; set; }
        public decimal ConsultantCharges { get; set; }
        public decimal DressingCharge { get; set; }
        public decimal RevisitCharge { get; set; }
        public decimal TotalFees { get; set; }
        public decimal ReceivedFees { get; set; }
        public decimal BalanceAmt { get; set; }
        public string CompanyCode { get; set; }
        public string EntryBy { get; set; }
        public string BillStatus { get; set; }
        #endregion
    }
}