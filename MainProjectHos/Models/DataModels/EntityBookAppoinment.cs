using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityBookAppoinment
    /// </summary>
    public class EntityBookAppoinment
    {
        public EntityBookAppoinment()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region "Properties"

        public string PatientCode { get; set; }
        public string FullName { get; set; }
        public int ConsultingDr { get; set; }
        public int OPDRoom { get; set; }
        public string VisitType { get; set; }
        public string Shift { get; set; }
        public int DepartmentId { get; set; }

        #endregion

    }
}