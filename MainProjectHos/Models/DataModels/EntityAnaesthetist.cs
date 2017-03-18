using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    public class EntityAnaesthetist
    {
        public EntityAnaesthetist()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region "Properties"

        public string AnaesthetistCode { get; set; }
        public string AnaesthetistName { get; set; }
        public string TypeOfAnaesthetist { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public decimal ConsultCharges { get; set; }
        #endregion
    }
}