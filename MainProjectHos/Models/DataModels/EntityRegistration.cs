using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityRegistration
    /// </summary>
    public class EntityRegistration
    {
        public EntityRegistration()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region Properties

        public string UserName { get; set; }
        public string DeptName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string UserType { get; set; }
        public string EmpCode { get; set; }

        #endregion
    }
}