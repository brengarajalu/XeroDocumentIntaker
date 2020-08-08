using System;
using Microsoft.EntityFrameworkCore;
using XeroDocumentIntaker.Models;

namespace XeroDocumentIntaker.DataAccess
{

    /// <summary>
    /// Manages CRUD interactions with database
    /// </summary>
    public class ReportDBContext : DbContext
    {
        public DbSet<Report> Report { get; set; }
        public DbSet<ReportDetail> ReportDetail { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Report>()
                .HasOne(b => b.ReportDetails)
                .WithOne();
        }
        /// <summary>
        /// Creates SQL Lite database on start
        /// </summary>
        /// <param name="options"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=sqlitedemo.db");
    }
}
