using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    public class EntityOTMedicineBillDetails
    {
        public int BillDetailId { get; internal set; }
        public bool? IsDelete { get; internal set; }
        public decimal? Price { get; internal set; }
        public int? Quantity { get; internal set; }
        public int? TabletId { get; internal set; }
        public int? BillNo { get; set; }
        public string MedicineName { get; internal set; }
        public decimal Amount { get; internal set; }
        public int TempId { get; internal set; }
    }
}