using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace BBSObserver.Models.Lecture
{
    /// <summary>
    /// 受講者リスト
    /// </summary>
    public class ParticipantContext  : DbContext
    {
        public ParticipantContext() : base("DefaultConnection")
        {
            Database.SetInitializer<ParticipantContext>(null);
        }

        public DbSet<Participant> Participants { get; set; }
    }
}