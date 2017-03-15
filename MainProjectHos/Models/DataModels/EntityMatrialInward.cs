using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityMatrialInword
    /// </summary>
    public class EntityMatrialInword
    {
        public EntityMatrialInword()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region "properties"
        public string InwardNo { get; set; }
        public DateTime InwardDate { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string ItemCode { get; set; }
        public string ItemDesc { get; set; }
        public int Group { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public string unit { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal OtherCharges { get; set; }
        public decimal TotalProductAmount { get; set; }
        public int TotalInwardQty { get; set; }
        public decimal TotalInwardAmount { get; set; }
        public string EntryBy { get; set; }

        #endregion
    }
}