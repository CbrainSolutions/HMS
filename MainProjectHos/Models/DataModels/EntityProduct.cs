using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels

{
    /// <summary>
    /// Summary description for EntityCompany
    /// </summary>
    public class EntityProduct
    {
        public EntityProduct()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region "properties"

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string UOM { get; set; }
        public string SubUOM { get; set; }
        public decimal Price { get; set; }

        #endregion
    }
}