using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore;
using BlazorBugOne.Shared;

namespace BlazorBugOne.Server
    {
    public class PGContext : DbContext
    {

        public DbSet<Bug> Bugs { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            
            string myconnnection = (@"Server=localhost;Port=5432;Database=BlazorBugOne;User Id=mradmin;Password=password");
            base.OnConfiguring(optionsBuilder);

            //  optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DBTESTCORE2B");
            optionsBuilder.UseNpgsql(myconnnection);
        }

    }
}
