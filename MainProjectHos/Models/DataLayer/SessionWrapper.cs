using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataLayer
{
    public static class SessionWrapper
    {
        static SessionWrapper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static string UserName
        {
            get
            {
                string strUserName = string.Empty;
                try
                {
                    strUserName = Convert.ToString(HttpContext.Current.Session["UserName"]);
                }
                catch
                {
                    HttpContext.Current.Session["UserName"] = null;
                }
                return strUserName;
            }
            set
            {
                HttpContext.Current.Session["UserName"] = value;
            }
        }

        public static string UserType
        {
            get
            {
                string strUserType = string.Empty;
                try
                {
                    strUserType = Convert.ToString(HttpContext.Current.Session["UserType"]);
                }
                catch
                {
                    HttpContext.Current.Session["UserType"] = null;
                }
                return strUserType;
            }
            set
            {
                HttpContext.Current.Session["UserType"] = value;
            }
        }

        public static int SiteId
        {
            get
            {
                int intSiteId = 0;
                try
                {
                    intSiteId = Convert.ToInt32(HttpContext.Current.Session["SiteId"]);
                }
                catch
                {
                    HttpContext.Current.Session["SiteId"] = null;
                }
                return intSiteId;
            }
            set
            {
                HttpContext.Current.Session["SiteId"] = value;
            }
        }

        public static string SiteCode
        {
            get
            {
                string strSiteCode = string.Empty;
                try
                {
                    strSiteCode = Convert.ToString(HttpContext.Current.Session["SiteCode"]);
                }
                catch
                {
                    HttpContext.Current.Session["SiteCode"] = null;
                }
                return strSiteCode;
            }
            set
            {
                HttpContext.Current.Session["SiteCode"] = value;
            }
        }

        public static DataTable UserRights
        {
            get
            {
                DataTable ldtUserRights = new DataTable();
                try
                {
                    ldtUserRights = (DataTable)HttpContext.Current.Session["UserRights"];
                }
                catch
                {
                    HttpContext.Current.Session["UserRights"] = null;
                }
                return ldtUserRights;
            }
            set
            {
                HttpContext.Current.Session["UserRights"] = value;
            }
        }

        public static string CurrentBrowser
        {
            get;
            set;
        }
    }

    public enum BrowserType
    {
        IE,
        Mozilla,
        Opera,
        Safari
    }
}