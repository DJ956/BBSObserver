using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace BBSObserver.Models.Core
{
    public class BBSCoreInfoContext : DbContext
    {
        public BBSCoreInfoContext() : base("DefaultConnection")
        {
            Database.SetInitializer<BBSCoreInfoContext>(null);
        }

        public DbSet<BBSCoreInfo> BBSCoreInfos { get; set; }
    }
}