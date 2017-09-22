using KinoGuide.DbModels;
using KinoGuide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace KinoGuide
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // For Test
            System.Data.Entity.Database.SetInitializer(new SiteDbInitializer());
            SiteDbContext db = new SiteDbContext();
            db.Database.Initialize(true);

            AutofacConfig.Register();

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }


    }
}
