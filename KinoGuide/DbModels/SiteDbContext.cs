using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace KinoGuide.DbModels
{
    public class SiteDbContext: DbContext
    {
        public SiteDbContext(): base("KinoGuideDb")
        {

        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Film> Films { get; set; }
    }
}