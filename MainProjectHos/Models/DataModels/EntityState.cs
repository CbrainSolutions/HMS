using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityState
    /// </summary>
    public class EntityState
    {
        public EntityState()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region "Properties"

        public string StateCode { get; set; }
        public string StateDesc { get; set; }
        public int Country { get; set; }
        public string EntryBy { get; set; }
        public string ChangeBy { get; set; }
        #endregion
    }
}