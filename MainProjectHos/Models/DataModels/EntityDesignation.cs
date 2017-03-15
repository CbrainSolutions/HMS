using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityDesignation
    /// </summary>
    public class EntityDesignation
    {
        public EntityDesignation()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region "Properties"
        public int PKID { get; set; }
        public string DesignationCode { get; set; }
        public string DesignationDesc { get; set; }
        public string EntryBy { get; set; }
        public string ChangeBy { get; set; }
        #endregion
    }
}