using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace BBSObserver.Models.Lecture
{
    public class LectureContext : DbContext
    {
        public LectureContext() : base("DefaultConnection")
        {
            Database.SetInitializer < LectureContext > (null);
        }

        public DbSet<Lecture> Lectures { get; set; }

        /// <summary>
        /// IDから講義名を取得する
        /// </summary>
        /// <param name="id">講義ID</param>
        /// <returns>講義名</returns>
        public string GetNameById(int id)
        {
            var result = from x in Lectures
                         where x.LectureId == id
                         select x;

            return result.First().Name;
        }
    }
}