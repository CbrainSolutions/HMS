using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    public class EntityNurse
    {
        public EntityNurse()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region "Properties"

        public string NurseCode { get; set; }
        public string NurseName { get; set; }
        public int DeptId { get; set; }
        public string DepartmentName { get; set; }
        #endregion
    }
}