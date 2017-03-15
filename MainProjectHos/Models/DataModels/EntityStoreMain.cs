using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{

    /// <summary>
    /// Summary description for EntityStoreMain
    /// </summary>
    public class EntityStoreMain
    {
        public EntityStoreMain()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region "properties"
        public string ItemCode { get; set; }
        public string ItemDesc { get; set; }
        public int Qty { get; set; }
        public string SupplierName { get; set; }
        #endregion
    }
}