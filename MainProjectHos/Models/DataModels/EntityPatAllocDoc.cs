using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityMix
    /// </summary>
    public class EntityPatAllocDoc
    {
        public EntityPatAllocDoc()
        {

        }

        public int PKId { get; set; }
        public string PatientName { get; set; }
        public string Pat_Type { get; set; }
        public int Emp_Id { get; set; }
        public string EmpName { get; set; }
        public string ShiftName { get; set; }
        public DateTime AppointDate { get; set; }
        public decimal Charges { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}