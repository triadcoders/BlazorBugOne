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
using System.Text.Json;

namespace BlazorBugOne.Server
{
    public class ContextUpdater
    {



       // public static PGContext pGContext;

     


        public static async Task UpdateBug2(Bug bugupdate)
        {
            Console.WriteLine("**** ContextController UpdateBug: " +bugupdate.id + " *****");
            Console.WriteLine("**** ContextController UpdateBug: " +bugupdate.personAssigned ?? "NULL" + " *****");
            Console.WriteLine("**** ContextController UpdateBug: " +bugupdate.personDiscovered ?? "NULL" + " *****");

            // PGContext pGContext;
            using (PGContext pGContext = new())
            {

                if (bugupdate.personAssigned != null & bugupdate.personDiscovered != null)
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
                        
                        newbug2.project = bugupdate.project;

                        pGContext.Update(newbug2);
                        await pGContext.SaveChangesAsync();
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("updating discoverd " + bugupdate.personDiscovered);
                    Console.WriteLine("updating discoverd " + bugupdate.personAssigned);

                    if (bugupdate.personDiscovered == null && bugupdate.personAssigned == null)
                    {
                        Bug newbug2 = new();
                        newbug2 = pGContext.Bugs.Include(b => b.personDiscovered).Include(b => b.personAssigned).Where(b => b.id == bugupdate.id).AsNoTracking().FirstOrDefault() ?? new();
                        // newbug2.personAssigned = pGContext.People.Where(p => p.id == bugupdate.personAssigned.id).AsNoTracking().FirstOrDefault();
                        newbug2.personAssigned = null;
                        newbug2.personDiscovered = null;

                        newbug2.summary = bugupdate.summary;
                        newbug2.description = bugupdate.description;
                        newbug2.lifecycle = bugupdate.lifecycle;
                        newbug2.priority = bugupdate.priority;
                        newbug2.status = bugupdate.status;
                        newbug2.lifecycle = bugupdate.lifecycle;
                        pGContext.Update(newbug2);
                        await pGContext.SaveChangesAsync();
                        Console.WriteLine("THE END!");
                        return;
                        
                    }
                    

                    pGContext.Update(bugupdate);
                    await pGContext.SaveChangesAsync();
                    return;
                }
                Console.WriteLine("Nothing matched on the update so nothing was done");
                
            }
        }


        public static async Task UpdateBug(Bug bugupdate)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("**** THE NEW BUG UPDATER ***");
            using (PGContext pGContext = new())
            {
                bool personAassignedIsNull = false;
                bool personDiscoveredIsNull = false;

                if (bugupdate.personAssigned == null) personAassignedIsNull = true;
                if (bugupdate.personDiscovered == null) personDiscoveredIsNull = true;

                Bug tempbug = new();
                if (bugupdate.personAssigned == null) { Console.WriteLine("The person Assigned is null"); }
                if (bugupdate.personDiscovered == null) { Console.WriteLine("The person Discovered is null"); }

                Console.WriteLine("The current state of bugupdate is" + pGContext.Entry(bugupdate).State);

                pGContext.Entry(bugupdate).State = EntityState.Modified;
                Console.WriteLine("The current state of bugupdate is" + pGContext.Entry(bugupdate).State);
                if (bugupdate.personAssigned == null) { Console.WriteLine("The person Assinged is null"); }
                if (bugupdate.personDiscovered == null) { Console.WriteLine("The person Discovered is null"); }


                Console.WriteLine("I am tempbug before finding myself my id is " + tempbug.id);
                
              //  tempbug = pGContext.Bugs.Include(b => b.personAssigned).Include(b=> b.personDiscovered).Where(b => bugupdate.id == b.id).FirstOrDefault();
                tempbug = pGContext.Bugs.Include(b => b.personAssigned).Include(b=> b.personDiscovered).Include(b=>b.project).Where(b => bugupdate.id == b.id).FirstOrDefault();
                if (tempbug != null) 
                {
                     Console.WriteLine("I am tempbug after trying to find myself " + tempbug.id);
                }else 
                {
                    Console.WriteLine("Temp bug is now null after trying to find itself");                
                };

                Person newperson = new();

                if (tempbug != null)
                {
                    Console.WriteLine("I tempbug am not null");
                    if (!personAassignedIsNull) //Have to make below null
                    {
                        Console.WriteLine("Trying to set personDiscovered on tempbug");
                        tempbug.personAssigned = pGContext.People.Where(p => p.id == bugupdate.personAssigned.id).FirstOrDefault();

                        if (tempbug == null)
                        {
                            Console.WriteLine("Why is tempbug now null???");
                        }
                    
                        
                    }
                    else { Console.WriteLine("the personassignedisnull bool is " + personAassignedIsNull); }

                    if (!personDiscoveredIsNull) //Have to make below null
                    {
                        Console.WriteLine("Attempting to find and existing personAssinged that is on bugupdate.personassigned.id and assign it to tempbug..line 181");
                     
                        tempbug.personDiscovered = pGContext.People.Where(p => p.id == bugupdate.personDiscovered.id).FirstOrDefault();

                    }
                    else { Console.WriteLine("The persondiscovrdisnull bool is " + personDiscoveredIsNull); }



                    //if (tempbug == null)
                    //{
                    //    tempbug = new(); Console.WriteLine("Tempbug was null so I created a new one");
                    //}


                    if (tempbug.personAssigned != null)
                        Console.WriteLine("Tempbug personassigned is " + tempbug.personAssigned?.id ?? "null");

                    if (tempbug.personDiscovered != null)
                        Console.WriteLine("Tempbug Discovered is " + tempbug.personDiscovered?.id ?? "null");


                    Console.WriteLine("Tempbug tracking is " + pGContext.Entry(tempbug).State ?? "null");


                    if (personAassignedIsNull)
                    {
                        Console.WriteLine("Trying to set personassigned to null");
                        tempbug.personAssigned = pGContext.People.FirstOrDefault();

                        pGContext.Update(tempbug);
                        await pGContext.SaveChangesAsync();

                        tempbug.personAssigned = null;

                        pGContext.Update(tempbug);
                        await pGContext.SaveChangesAsync();

                    }

                    if (personDiscoveredIsNull)
                    {
                        Console.WriteLine("Trying to set persondiscovered to null");
                        Person firstperson = pGContext.People.FirstOrDefault();
                        tempbug.personDiscovered = firstperson;

                        pGContext.Update(tempbug);
                        await pGContext.SaveChangesAsync();

                        tempbug.personDiscovered = null;

                         pGContext.Update(tempbug);
                        await pGContext.SaveChangesAsync();

                    }

                    Console.WriteLine("The project name is on tempbug " + tempbug.project.name);
                    Console.WriteLine("Attempting to update tempbug " + tempbug.id);


                    // tempbug.project = pGContext.Projects.Where(p => p.id == bugupdate.project.id).FirstOrDefault();

                    //  Console.WriteLine("The project2 name is on bugupdate  " + bugupdate.project.name);
                    //Console.WriteLine("The project2 name is on tempbug " + tempbug.project.name);

                    pGContext.ChangeTracker.Clear();
                    pGContext.Update(tempbug);
                    await pGContext.SaveChangesAsync();
                }
                else
                {
                    //TODO:  if tempbug is null do something here

                    pGContext.ChangeTracker.Clear();

                    Console.WriteLine("I am going to update bugupdate now");
                    pGContext.Update(bugupdate);
                    string jsonString = JsonSerializer.Serialize(bugupdate);

                    Console.WriteLine();
                    Console.WriteLine(jsonString);
                    Console.WriteLine(  );
                    await pGContext.SaveChangesAsync();

                }



                //   if (bugupdate.personDiscovered == null) tempbug.personDiscovered = null;
                //   tempbug.personDiscovered = null;
                // tempbug.personAssigned = null;


                //pGContext.Update(tempbug);
               // await pGContext.SaveChangesAsync();
                ///EntityState.Modified;


                Console.WriteLine("*** END IF NEW BUG UPDATER ***");
                return;

            }
        }

        public static void DetachBug(Bug bug)
        {
            using (PGContext pGContext = new())
            {
                pGContext.Entry(bug).State = EntityState.Detached;

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
