using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityInsuranceCom
    /// </summary>
    public class EntityInsuranceCom
    {
        public EntityInsuranceCom()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region "Properties"

        public string InsuranceCode { get; set; }
        public string InsuranceDesc { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string EmailID { get; set; }
        public string ContactNo { get; set; }
        public string PostalCode { get; set; }
        public string FaxNumber { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPhNo { get; set; }
        public string MobileNo { get; set; }
        public string ContactEmail { get; set; }
        public string Notes { get; set; }
        public string EntryBy { get; set; }
        public string ChangeBy { get; set; }
        #endregion
    }
}