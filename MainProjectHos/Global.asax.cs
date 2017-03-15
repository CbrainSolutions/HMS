using MainProjectHos.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace MainProjectHos
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }

        static List<EntityLogin> _UserData = new List<EntityLogin>();

        public static List<EntityLogin> UsersData
        {
            get
            {
                return _UserData;
            }
            set
            {
                _UserData = value;
            }
        }

    }

    
}