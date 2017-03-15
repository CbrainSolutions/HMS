using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for CategoryBLL
    /// </summary>
    public class EntityRoomCategory
    {
        public EntityRoomCategory()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string CategoryCode { get; set; }
        public string CategoryDesc { get; set; }
        public decimal Rate { get; set; }
        public string EntryBy { get; set; }
        public string ChangeBy { get; set; }
        public bool IsICU { get; set; }
        public int PKId { get; set; }
        public bool IsOT { get; set; }
    }
}