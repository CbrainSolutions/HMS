using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityCity
    /// </summary>
    public class EntityCity
    {
        public EntityCity()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region "Properties"

        public int CityCode { get; set; }
        public string CityDesc { get; set; }
        public int State { get; set; }
        public string EntryBy { get; set; }
        public string ChangeBy { get; set; }
        #endregion
    }
}