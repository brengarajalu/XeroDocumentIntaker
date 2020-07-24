using System;
using Microsoft.EntityFrameworkCore;

namespace XeroDocumentIntaker.Models
{

    public class ReportDBContext : DbContext
    {
    
        public DbSet<Report> Report { get; set; }
        public DbSet<ReportDetail> ReportDetail { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=sqlitedemo.db");
    }
}
