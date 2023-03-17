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
        private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Jesper\source\repos\ErrorReport_Exam_Console\ErrorReport_Exam_Console\ErrorReportServiceDb.mdf;Integrated Security=True";

        #region constructors
        public DataContext()
        {

        }
        public DataContext(DbContextOptions<DataContext> options) : base(options)      //constructor av klassen DataContext som ärver från DbContext klassen i EFC, som instansierar ifrån DbContextOptions. Initierar DbContext med de som skickas in, så vi kan interagera med databasen.
        {

        }

        #endregion


        #region overrides
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)  //config. av databasen,vilken connectionstring den skall till. anv sig av SqlServer.
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(_connectionString);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        #endregion


        #region entities

        public DbSet<CustomerEntity> Customers { get; set; } = null!;
        public DbSet<CommentEntity> Comments { get; set; } = null!;
        public DbSet<ErrorReportEntity> Errorreports { get; set; } = null!;
        public DbSet<WorkerEntity> Workers { get; set; } = null!;


        #endregion
    }
}

