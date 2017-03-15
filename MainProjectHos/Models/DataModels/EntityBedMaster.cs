using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityBedMaster
    /// </summary>
    public class EntityBedMaster
    {
        public EntityBedMaster()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region "Properties"
        public int BedId { get; set; }
        public int RoomId { get; set; }
        public int CategoryId { get; set; }
        public int FloorNo { get; set; }
        public string BedNo { get; set; }
        public string RoomNo { get; set; }
        public string FloorName { get; set; }
        public string CategoryDesc { get; set; }

        #endregion
    }
}