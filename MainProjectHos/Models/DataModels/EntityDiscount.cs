using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityDiscount
    /// </summary>
    public class EntityDiscount
    {
        public EntityDiscount()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region "Properties"

        public string DiscountCode { get; set; }
        public string DiscountDesc { get; set; }
        public Decimal Discount { get; set; }
        public string EntryBy { get; set; }
        public string ChangeBy { get; set; }
        #endregion
    }
}