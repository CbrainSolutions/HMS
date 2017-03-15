using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityCast
    /// </summary>
    public class EntityCast
    {
        public EntityCast()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region "Properties"

        public string CastCode { get; set; }
        public string CastDesc { get; set; }
        public int Religion { get; set; }
        public string EntryBy { get; set; }
        public string ChangeBy { get; set; }
        #endregion

        public int PKId { get; set; }

        public string ReligionName { get; set; }
    }
}