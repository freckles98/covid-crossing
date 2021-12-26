using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CovidCrossingServer
{
    /// <summary>
    /// EntityFrameworkCore context for Leaderboard database
    /// </summary>
    public class LeaderboardContext : DbContext
    {
        public DbSet<Record> Records { get; set; }

        /// <summary>
        /// Read the leaderboard database from the SQLite database.
        /// </summary>
        /// <param name="options"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=leaderboard.db");
    }

    /// <summary>
    /// Represents an individual player record. The GUID is used to uniquely identify clients.
    /// </summary>
    public class Record
    {
        [Key]
        public Guid Guid { get; set; }
        public string Nickname { get; set; }
        public int HighScore { get; set; }
    }
}
