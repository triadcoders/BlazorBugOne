using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorBugOne.Server;
using BlazorBugOne.Shared;
using Microsoft.EntityFrameworkCore;
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
    public class ContextUpdater
    {



       // public static PGContext pGContext;

     


        public static async Task UpdateBug(Bug bugupdate)
        {
            // PGContext pGContext;
            using (PGContext pGContext = new())
            {
               // pGContext.Dispose();
             

             //   bugupdate = pGContext.Bugs.Include(bug => bug.personDiscovered).Where(d => d.id == bugupdate.id).FirstOrDefault(); //Include(bug => bug.personAssigned).Where(b => b.id == bugupdate.id).FirstOrDefault();
                pGContext.Update(bugupdate);
                await pGContext.SaveChangesAsync();

                    //bugupdate = pGContext.Bugs
                    //.Include(bug => bug.personAssigned)
                    //.Where(b => b.personAssigned.id == bugupdate.personAssigned.id)
                    //.Include(bug => bug.personDiscovered)
                    //.Where(bug => bug.personDiscovered.id == bugupdate.personDiscovered.id)
                    //.FirstOrDefault();


                

                //    .Include(a => a.personDiscovered.id == bugupdate.personDiscovered))


                Console.WriteLine("Bugupdate assinged is " + bugupdate?.personAssigned?.firstname ?? "null I think");
                Console.WriteLine("Bugupdate  discovered is " + bugupdate?.personDiscovered?.firstname ?? "null I think");

                

                pGContext.Update(bugupdate);
                await pGContext.SaveChangesAsync();
            }
        }


        public static async Task UpdatePerson(Person personupdate)
        {
            using (PGContext pGContext = new())
            {
                pGContext.Update(personupdate);
                await pGContext.SaveChangesAsync();
            }
        }


        public static List<Person> GetPeople()
        {
            using (PGContext pGContext = new())
            {
                //people = ContextUpdater.pGContext.People.ToList();
                List<Person> people = pGContext.People.ToList();
                return people;
            }
        }

        //            var onebug = ContextUpdater.pGContext.Bugs.Include(bug => bug.personDiscovered).Include(bug => bug.project).Include(bug => bug.personAssigned).Where(b => b.id == id).FirstOrDefault();


        public static Bug GetBug(int id)
        {
            using (PGContext pGContext = new())
            {
                //people = ContextUpdater.pGContext.People.ToList();
              Bug bug = pGContext.Bugs.Include(bug => bug.personDiscovered).Include(bug => bug.project).Include(bug => bug.personAssigned).Where(b => b.id == id).FirstOrDefault();
                return bug;
            }
        }


        //            showbugs = ContextUpdater.pGContext.Bugs.Include(bug => bug.personAssigned).Include(bug => bug.project).Include(bug => bug.personAssigned).ToList();
        public static List<Bug> GetBugs()
        {
            using (PGContext pGContext = new())
            {
                //people = ContextUpdater.pGContext.People.ToList();
                List<Bug> bugs = pGContext.Bugs.Include(bug => bug.personAssigned).Include(bug => bug.project).Include(bug => bug.personAssigned).ToList();
                return bugs;
            }
        }

        public static int GetBugCount()
        {
            using (PGContext pGContext = new())
            {
                //people = ContextUpdater.pGContext.People.ToList();
                List<Bug> bugs = pGContext.Bugs.Include(bug => bug.personAssigned).Include(bug => bug.project).Include(bug => bug.personAssigned).ToList();
                return bugs.Count();
            }
        }


        public static Bug GetBugOrderByDesending()
        {
            using (PGContext pGContext = new())
            {
                //people = ContextUpdater.pGContext.People.ToList();
              Bug  bug = pGContext.Bugs.OrderByDescending(b => b.id).FirstOrDefault();
                return bug;
            }
        }


        public static int GetPersonCountbyId(int personid)
        {
            using (PGContext pGContext = new())
            {
                int mycount = pGContext.Bugs.Where(b => b.personDiscovered.id == personid).Count();
                return mycount;
            }
        }


        public static void RemovePerson(Person person)
        {
            using (PGContext pGContext = new())
            {
                pGContext.Remove(person);
                pGContext.SaveChanges();
            }
        }




        public static void RemoveBug(Bug bug)
        {
            using (PGContext pGContext = new())
            {
                 pGContext.Remove(bug);
                pGContext.SaveChanges();
            }
        }




        //            var oneperson = ContextUpdater.pGContext.People.Where(p => p.id == id).First();

        public static Person GetPerson(int id)
        {
            using (PGContext pGContext = new())
            {
                //people = ContextUpdater.pGContext.People.ToList();
                   Person person = pGContext.People.Where(p => p.id == id).First();
                return person;
            }
        }
    }
}
