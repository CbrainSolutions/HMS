using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    public class EnitiyRoomMaster
    {
        public EnitiyRoomMaster()
        {

        }
        #region Properties
        public int RoomId { get; set; }
        public int CategoryId { get; set; }
        public string RoomNo { get; set; }
        public int FloorNo { get; set; }
        #endregion



        public string CategoryName { get; set; }

        public string FloorName { get; set; }
    }

    public class EntityDocument
    {
        public EntityDocument()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string DocumentNAme { get; set; }
        public byte[] File { get; set; }
        public string PatientName { get; set; }
        public int PatientId { get; set; }
        public DateTime UploadDate { get; set; }
        public int PKId { get; set; }
    }
}