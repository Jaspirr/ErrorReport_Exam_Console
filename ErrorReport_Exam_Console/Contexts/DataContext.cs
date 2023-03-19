using ErrorReport_Exam_Console.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorReport_Exam_Console.Contexts
{
    internal class DataContext : DbContext
    {
        private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Jesper\Documents\sql_db_ConsoleErrorbase.mdf;Integrated Security=True;Connect Timeout=30";
        public DbSet<CustomerEntity> Customers { get; set; } = null!;
        public DbSet<ErrorReportEntity> ErrorReports { get; set; } = null!;
        public DbSet<CommentEntity> Comments { get; set; } = null!;
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ErrorReportEntity>()
                .HasOne<CustomerEntity>(i => i.Customer)
                .WithMany(c => c.ErrorReports)
                .HasForeignKey(i => i.CustomerId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CommentEntity>()
                .HasOne<ErrorReportEntity>(c => c.ErrorReport)
                .WithMany(i => i.Comments)
                .HasForeignKey(c => c.ErrorReportId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

