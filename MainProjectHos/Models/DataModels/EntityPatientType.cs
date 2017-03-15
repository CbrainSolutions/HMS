using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityPatientType
    /// </summary>
    public class EntityPatientType
    {
        public EntityPatientType()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region "Properties"

        public string PatientCode { get; set; }
        public string PatientDesc { get; set; }
        public string EntryBy { get; set; }
        public string ChangeBy { get; set; }
        #endregion
    }
}