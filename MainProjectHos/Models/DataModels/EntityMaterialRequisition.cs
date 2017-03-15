using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityMaterialRequisition
    /// </summary>
    public class EntityMaterialRequisition
    {
        public EntityMaterialRequisition()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region "properties"
        public string RequisitionCode { get; set; }
        public string RequisitionBy { get; set; }
        public int Group { get; set; }
        public string ItemCode { get; set; }
        public string Item { get; set; }
        public string ItemDesc { get; set; }
        public decimal Qty { get; set; }
        public string Unit { get; set; }
        public char RequisitionStatus { get; set; }
        public string EntryBy { get; set; }
        public string ChangeBy { get; set; }

        #endregion

    }
}