using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{

    /// <summary>
    /// Summary description for EntityCompany
    /// </summary>
    public class EntityCompany
    {
        public EntityCompany()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region "properties"

        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public string VATCSTNo { get; set; }
        public string ExciseNo { get; set; }
        public string Email { get; set; }
        public string ServiceTaxNo { get; set; }
        public string EntryBy { get; set; }
        public string ChangeBy { get; set; }

        #endregion
    }
}