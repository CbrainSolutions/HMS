using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityUnit
    /// </summary>
    public class EntityMedicine
    {
        public EntityMedicine()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public int MedicineId { get; set; }

        public string MedicineName { get; set; }
    }
}