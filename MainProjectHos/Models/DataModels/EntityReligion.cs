using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{

    /// <summary>
    /// Summary description for EntityReligion
    /// </summary>
    public class EntityReligion
    {
        public EntityReligion()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region "Properties"

        public string ReligionCode { get; set; }
        public string ReligionDesc { get; set; }
        public string EntryBy { get; set; }
        public string ChangeBy { get; set; }
        #endregion
    }
}