using MainProjectHos.Models.DataModels;
using MainProjectHos.Models.DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.BusinessLayer
{
    public class StoreMainBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public StoreMainBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataTable GetData()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetShortItemList");
            }
            catch (Exception ex)
            {

                Commons.FileLog("StoreMainBLL - GetData()", ex);
            }
            return ldt;

        }


    }

}