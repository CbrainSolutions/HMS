using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityDignosis
    /// </summary>
    public class EntityDignosis
    {
        public EntityDignosis()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region "Properties"

        public string DignosisCode { get; set; }
        public string DignosisDesc { get; set; }
        public string EntryBy { get; set; }
        public string ChangeBy { get; set; }
        #endregion
    }
}