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


                if (bugupdate.personDiscovered.id == bugupdate.personAssigned.id)
                {
                        pGContext.ChangeTracker.Clear();
                        pGContext.Attach(bugupdate.personDiscovered);
                        pGContext.Entry(bugupdate.personDiscovered).State = EntityState.Detached;
                        pGContext.Entry(bugupdate.project).State = EntityState.Detached;

                        Console.WriteLine(pGContext.Entry(bugupdate.personDiscovered).State.ToString());
                        Console.WriteLine(pGContext.Entry(bugupdate.personAssigned).State.ToString());


                        Console.WriteLine("Bugupdate assinged is " + bugupdate?.personAssigned?.firstname ?? "null I think");
                        Console.WriteLine("Bugupdate  discovered is " + bugupdate?.personDiscovered?.firstname ?? "null I think");


                        Bug newbug = pGContext.Bugs.Where(b => b.id == bugupdate.id).AsNoTracking().FirstOrDefault() ?? new();

                        newbug.personDiscovered = pGContext.People.Where(p => p.id == bugupdate.personDiscovered.id).AsNoTracking().FirstOrDefault();

                        // Console.WriteLine("newbug is A-" + newbug.personAssigned?.firstname ?? "NA" + " D-" + newbug.personDiscovered?.firstname ?? "NA");
                        Console.WriteLine("newbug is D-" + newbug.personDiscovered.firstname);
                        Console.WriteLine("newbug is A-" + newbug.personAssigned?.firstname);


                        // pGContext.Update(bugupdate);
                        pGContext.Update(newbug);
                        await pGContext.SaveChangesAsync();


                        pGContext.ChangeTracker.Clear();
                        pGContext.Attach(bugupdate.personAssigned);
                        pGContext.Entry(bugupdate.personAssigned).State = EntityState.Detached;
                        pGContext.Entry(bugupdate.project).State = EntityState.Detached;

                        Bug newbug2 = new();
                        newbug2 = pGContext.Bugs.Where(b => b.id == bugupdate.id).AsNoTracking().FirstOrDefault() ?? new();
                        newbug2.personAssigned = pGContext.People.Where(p => p.id == bugupdate.personAssigned.id).AsNoTracking().FirstOrDefault();

                    newbug2.summary = bugupdate.summary;
                    newbug2.description = bugupdate.description;
                    newbug2.lifecycle = bugupdate.lifecycle;
                    newbug2.priority = bugupdate.priority;
                    newbug2.status = bugupdate.status;
                    newbug2.lifecycle = bugupdate.lifecycle;
                        pGContext.Update(newbug2);
                        await pGContext.SaveChangesAsync();
                }
                else
                {

                    pGContext.Update(bugupdate);
                    await pGContext.SaveChangesAsync();
                }
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


        public static string RemovePerson(int personid)
        {
            using (PGContext pGContext = new())
            {

                Console.WriteLine("Attempting to remove person id " + personid);
                Person person = pGContext.People.Where(p => p.id == personid).FirstOrDefault();

                List<Bug> bugsAssigned = pGContext.Bugs.Where(p => p.personAssigned.id == person.id).ToList();


                List<Bug> bugsDiscovered = pGContext.Bugs.Where(p => p.personDiscovered.id == person.id).ToList();




                for (int i = 0; i < bugsDiscovered.Count-1; i++)
                {
                    Console.WriteLine("attempting to update bug " + bugsDiscovered[i].id);
                    bugsDiscovered[i].id = 0;
                    pGContext.Update(bugsAssigned[i]);              
                }


                for (int i = 0; i < bugsAssigned.Count - 1; i++)
                {
                    Console.WriteLine("attempting to update bug " + bugsAssigned[i].id);

                    bugsAssigned[i].id = 0;
                    pGContext.Update(bugsAssigned[i]);
                }

             

                pGContext.SaveChanges();

                //if (bugsAssigned.Count > 0 || bugsDiscovered.Count > 0)
                //{
                //    return "This User is associated with bugs, please remove this user from bugs list before deleteing";
                //}

                pGContext.Remove(person);
                pGContext.SaveChanges();
                return "Complete";
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
