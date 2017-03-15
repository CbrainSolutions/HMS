using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityConsultant
    /// </summary>
    public class EntityConsultant
    {
        public EntityConsultant()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region "Properties"
        public string ConsultantCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Address { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DOJ { get; set; }
        public bool DisContinued { get; set; }
        public DateTime DisContFrom { get; set; }
        public string DisContRemark { get; set; }
        public string EntryBy { get; set; }
        public string ChaneBy { get; set; }
        public int WardNo { get; set; }
        public decimal Fees { get; set; }
        #endregion
    }
}