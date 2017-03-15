using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityItem
    /// </summary>
    public class EntityItem
    {
        public EntityItem()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region "properties"
        public string ItemCode { get; set; }
        public string ItemDesc { get; set; }
        public int ReorderLevel { get; set; }
        public int ReorderMaxLevel { get; set; }
        public string UnitCode { get; set; }
        public string SupplierCode { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal Rate { get; set; }
        public bool DisContinued { get; set; }
        public DateTime DisContFrom { get; set; }
        public string DisContRemark { get; set; }
        public int GroupId { get; set; }
        public DateTime ManifacturingDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string EntryBy { get; set; }
        public string ChangeBy { get; set; }
        public int IsCheked { get; set; }
        #endregion
    }
}