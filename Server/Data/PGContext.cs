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

            
            string myconnnection = (@"Server=localhost;Port=5432;Database=BlazorBugOne;User Id=mradmin;Password=passthemradmin");
            base.OnConfiguring(optionsBuilder);

            //  optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DBTESTCORE2B");
            optionsBuilder.UseNpgsql(myconnnection);

            
        }

       public static void AddDemoPeople()
        {
            //PGContext pGContext = new();

            //List<Person> people = DemoData.GenerateDemoData();


            //var peoplegroup = pGContext.People.Where(p => p.firstname == "Bob" && p.lastname == "Smith");

            //Console.WriteLine("mynum is " + peoplegroup.Count());
            //if (peoplegroup.Count()  == 0)
            //{
            //    /// people.Where(p => );
            //    // people.Select(p => Console.WriteLine(p.firstname + " " + p.lastname));
            //    foreach (var person in people)
            //    {
            //        pGContext.Remove(person);
            //    }
            //    Console.WriteLine("Demo People should have been added");
            //    pGContext.SaveChanges();
            //}
            //else
            //{
            //    Console.WriteLine("Demo People not added, probably already there");
            //}




        }


    }
}
