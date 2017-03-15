using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{

    /// <summary>
    /// Summary description for EntityCountry
    /// </summary>
    public class EntityCountry
    {
        public EntityCountry()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region "Properties"

        public string CountryCode { get; set; }
        public string CountryDesc { get; set; }
        public string EntryBy { get; set; }
        public string ChangeBy { get; set; }
        #endregion
    }
}