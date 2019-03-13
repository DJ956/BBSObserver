using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace BBSObserver.Models.History
{
    public class HistoryContext : DbContext
    {
        public HistoryContext() : base("DefaultConnection")
        {
            Database.SetInitializer<HistoryContext>(null);
        }

        public DbSet<History> Histories { get; set; }
    }
}