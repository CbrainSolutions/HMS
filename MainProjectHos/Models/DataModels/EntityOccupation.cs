using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityOccupation
    /// </summary>
    public class EntityOccupation
    {
        public EntityOccupation()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region "Properties"

        public int PKId { get; set; }
        public string OccupationDesc { get; set; }
        public string EntryBy { get; set; }
        public string ChangeBy { get; set; }

        #endregion
    }
}