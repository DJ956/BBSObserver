using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace BBSObserver.Models.History
{
    public class FileHistoryContext : DbContext
    {
        public FileHistoryContext() : base("DefaultConnection")
        {
            Database.SetInitializer<FileHistoryContext>(null);
        }

        public DbSet<FileHistory> FileHistories { get; set; }
    }
}