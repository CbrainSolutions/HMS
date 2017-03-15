using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityInitials
    /// </summary>
    public class EntityInitials
    {
        public EntityInitials()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region "Properties"

        public string InitialCode { get; set; }
        public string InitialDesc { get; set; }
        public string EntryBy { get; set; }
        public string ChangeBy { get; set; }
        #endregion
    }
}