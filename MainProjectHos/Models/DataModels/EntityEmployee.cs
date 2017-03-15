using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{

    /// <summary>
    /// Summary description for EntityEmployee
    /// </summary>
    public class EntityEmployee
    {
        public EntityEmployee()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Properties
        public int PKId { get; set; }
        public string EmpCode { get; set; }
        public string EmpFirstName { get; set; }
        public string EmpLastName { get; set; }
        public string EmpMiddleName { get; set; }
        public int DeptId { get; set; }

        public int DesignationId { get; set; }

        public DateTime EmpDOB { get; set; }
        public DateTime EmpDOJ { get; set; }
        public string EmpAddress { get; set; }
        public string EmpPassword { get; set; }
        public string EmpConfirmPass { get; set; }
        public bool DisContinued { get; set; }
        public DateTime DisContFrom { get; set; }
        public string DisContRemark { get; set; }
        public string UserType { get; set; }
        public string EntryBy { get; set; }
        public string ChangeBy { get; set; }
        public string EmpName { get; set; }
        public decimal BasicSal { get; set; }
        public string BankName { get; set; }
        public string BankACNo { get; set; }
        public string PFNo { get; set; }
        public string PanNo { get; set; }
        public string Education { get; internal set; }
        public string RegistrationNo { get; internal set; }

        #endregion
    }
}