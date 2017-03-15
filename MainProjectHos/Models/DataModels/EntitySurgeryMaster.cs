using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityReligion
    /// </summary>
    public class EntitySurgeryMaster
    {
        public EntitySurgeryMaster()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region "Properties"

        public int PKId { get; set; }
        public string NameOfSurgery { get; set; }
        public string OperationalProcedure { get; set; }
        #endregion
    }
}