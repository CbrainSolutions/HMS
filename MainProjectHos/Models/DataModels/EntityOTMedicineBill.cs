using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    public class EntityOTMedicineBill
    {
        public int? AdmitId { get; internal set; }
        public object BillNo { get; internal set; }
        public object Bill_Date { get; internal set; }
        public object PatientName { get; internal set; }
        public object TotalAmount { get; internal set; }
    }
}