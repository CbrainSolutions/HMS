using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityGender
    /// </summary>
    public class EntityGender
    {
        public EntityGender()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region "Properties"

        public string GenderCode { get; set; }
        public string GenderDesc { get; set; }
        public string EntryBy { get; set; }
        public string ChangeBy { get; set; }
        #endregion
    }
}