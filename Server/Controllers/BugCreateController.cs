using BlazorBugOne.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorBugOne.Server;
using Microsoft.EntityFrameworkCore;

namespace BlazorBugOne.Server.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    [Route("BugManage/")]
    public class BugCreateController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        Bug bug = new();
    ///    PGContext pGContext = new();
        //https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/routing?view=aspnetcore-5.0
        
        [HttpPost("MakePost")]
        public IActionResult CreateBlog(Bug bug)
        {
            Console.WriteLine("********************");
            Console.WriteLine("********************");
            Console.WriteLine("Post has been called");
            //  return RedirectToAction("Counter");
          //  ActionResult actionResult;
            IActionResult response = Ok(new { result = "TEST" });

            Console.WriteLine(bug.description);
            Console.WriteLine(bug.summary);
            Console.WriteLine("The bug person is " + bug.personAssigned.firstname);
            Console.WriteLine("The bug assingee is " + bug.personAssigned.firstname);
      
            return response;

        }


        [HttpPost("AddPerson")]
        public async Task<IActionResult> AddUser(Person person)
        {


            Console.WriteLine("Post has been called to create a person");
            Console.WriteLine("The Person id is " + person.id);
            //  return RedirectToAction("Counter");
            //  ActionResult actionResult;
            IActionResult response = Ok(new { result = "TEST" });

            //     Console.WriteLine(bug.description);
            //Console.WriteLine(bug.summary);

            //pGContext.Add(person);
            //pGContext.Update(person);
            // pGContext.SaveChanges();

           await ContextUpdater.UpdatePerson(person);



            return response;

        }



        [HttpPost("DeleteBug")]
        public async Task<IActionResult> DeleteBug(Bug bug)
        {
            Console.WriteLine("Post has been called to delete a bug " + bug.id);
            IActionResult response = Ok(new { result = "TEST" });
       //     await ContextUpdater.UpdateBug(bug);
             ContextUpdater.RemoveBug(bug);
            return response;

        }




        [HttpPost("MakePostAgain")]
        public async Task<IActionResult> CreateBlogAgain(Bug bug)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("*** Entering MakePostAgain on BugCreateController ****");


            Console.WriteLine("***START MakePostAgain in BugCreateController****");
            Console.WriteLine("Bug summary is " + bug.summary);
            Console.WriteLine("Bug incomming ...bug id is " + bug.id);        
            Console.WriteLine("Post has been called again");
            IActionResult response = Ok(new { result = "Success" });

            Console.WriteLine("Person Assigned is is " + bug.personAssigned?.firstname);
            Console.WriteLine("Bug Assinged id is " + bug.personAssigned?.id ?? "null");
            Console.WriteLine("Bug discovered id is " + bug.personDiscovered?.id ?? "null");

            Console.WriteLine("*****END of MakePostAgain in BugCreateController***");
            await ContextUpdater.UpdateBug(bug);          
            return response;

        }




        [HttpGet("Delete")]
        public string Delete() 
        {

            int bugcount = ContextUpdater.GetBugCount(); //remember
            string description = "";

            if (bugcount >= 0)
            {
                bug = ContextUpdater.GetBugOrderByDesending();
            }


            if (bug != null)
            {
                description = bug.description + " " + bug.id;
                
              //  ContextUpdater.pGContext.Remove(bug); //remember
                ContextUpdater.RemoveBug(bug);
                //ContextUpdater.pGContext.SaveChanges();


                return "Data Deleted on bug " + description;
            }else
            {
                return "Bug was null on id " + bugcount;
            }
            

        }

        string myparam = "nada";

     //   List<string> thevars = new();

        [HttpGet("[action]")]
          public string Create(string myparam, string myparam2) //Would not work with the Get() Method once I added a subroute or added a delete method.  Had to create a new method... happed to use the same name as route.
        {
            foreach (var item in RouteData.Values)
            {
               // thevars.Add(item.Value.ToString());
               // myparam = myparam + " " + item.ToString();
            } 


            bug.description = "I am a bug #1";
            //pGContext.Add()

            ContextUpdater.UpdateBug(bug);  //remember  updating instead of adding...is ok?
            //ContextUpdater.pGContext.Add(bug);
            //ContextUpdater.pGContext.SaveChanges();

            var weatherForecast = new WeatherForecast
            {
                Date = DateTime.Parse("2019-08-01"),
                // TemperatureCelsius = 25,
                Summary = "Hot"
            };

            string jsonString = JsonSerializer.Serialize(weatherForecast);
            string returnstring = "";
           


            //foreach (var item in myparam)
            //{
            //    returnstring = returnstring + " " + item;
            //}


            return "Data Saved Create " + myparam + " " + myparam2;
            //return jsonString;
        }



        // GET api/user/firstname/lastname/address
        // [HttpGet("{firstName}/{lastName}/{address}")]

        [HttpGet("GetBug/{id}")]
        public string GetBug(int id)
        {
            // id = 56;
            //  var onebug = pGContext.Bugs.Where(b => b.id == id).FirstOrDefault();
            //   showbugs = pGContext.Bugs.Include(bug => bug.personAssigned).Include(bug => bug.project).ToList();
            
            //remember
        //    var onebug = ContextUpdater.pGContext.Bugs.Include(bug => bug.personDiscovered).Include(bug => bug.project).Include(bug => bug.personAssigned).Where(b => b.id == id).FirstOrDefault();

            var onebug = ContextUpdater.GetBug(id);
            
            string oneJson = JsonSerializer.Serialize(onebug);
            Console.WriteLine(oneJson);
            return oneJson;

        }



        [HttpGet("GetPerson/{id}")]
        public string GetPerson(int id)
        {
            // id = 56;
            //  var onebug = pGContext.Bugs.Where(b => b.id == id).FirstOrDefault();
            //   showbugs = pGContext.Bugs.Include(bug => bug.personAssigned).Include(bug => bug.project).ToList();
            
            
            //var oneperson = ContextUpdater.pGContext.People.Where(p => p.id == id).First(); remember

            var oneperson = ContextUpdater.GetPerson(id);


            string oneJson = JsonSerializer.Serialize(oneperson);
            Console.WriteLine(oneJson);
            return oneJson;

        }


        [HttpGet("GetPeople")]
        public string GetPeople()
        {
            List<Person> people = new List<Person>();
            //https://docs.microsoft.com/en-us/ef/core/querying/related-data/#lazy-loading
            //note the using entity core above to get the include option
         
            
            //  people = ContextUpdater.pGContext.People.ToList(); //remember

            people = ContextUpdater.GetPeople();


            string jsonString = JsonSerializer.Serialize(people);
            Console.WriteLine(jsonString);
            return jsonString;
        }





     //   public IActionResult DeletePerson(Person person)

        [HttpGet("DeletePerson/{id}")]
        public string DeletePerson(int id)
        {
            //      IActionResult response;
            Console.WriteLine("Attempting to Remove person using contextupdater at id " + id);

            

            string result = ContextUpdater.RemovePerson(id);


           var response = Ok(new { result = "Can you see this?" });

            return result;
            //    Console.WriteLine("Post has been called to delete a person " + person.id);
            //    int mycount = ContextUpdater.GetPersonCountbyId(person.id);


            //    if (mycount > 0)
            //    {
               //      response = Ok(new { result = "DeletedPerson" });
            //         Console.WriteLine("Sorry, This user has {mycount} bugs assigned to them. \r\n Please assign to another user first.");
            //    }else
            //    {
            //         response = Ok(new { result = "{ 'fruit':'Apple','size':'Large','color': 'Red'}" });
            //        Console.WriteLine("");
            //        Console.WriteLine("");
            //        Console.WriteLine("");
            //        Console.WriteLine("");
            //        Console.WriteLine("");
            //        Console.WriteLine("");


            //        Console.WriteLine("mycount is " + mycount);
            //        // ContextUpdater.pGContext.Remove(person);


            //        Console.WriteLine("Attempting to remove " + person.firstname + " using the ContextUpdater");
            //        ContextUpdater.RemovePerson(person);

            //   ContextUpdater.pGContext.SaveChanges();
            //return response;
            //}




          //  return Content("<xml>This is poorly formatted xml.</xml>", "text/xml");


          //  return response;

        }






        [HttpGet("GetBugs")]
        public string GetBugs()
        {

           List<Bug> showbugs = new List<Bug>();


            //          showbugs = pGContext.Bugs.Where(b => b.id > 0).ToList();


            //https://docs.microsoft.com/en-us/ef/core/querying/related-data/#lazy-loading
            //note the using entity core above to get the include option
          
            //remember
            //  showbugs = ContextUpdater.pGContext.Bugs.Include(bug => bug.personAssigned).Include(bug => bug.project).Include(bug => bug.personAssigned).ToList();

            showbugs = ContextUpdater.GetBugs();
              

            //     bugs = pGContext.Bugs.Where(b => b.person.firstname == "BOB".ToLower() ).ToList();


         //   foreach (var item in ContextUpdater.pGContext.Bugs)
            foreach (var item in showbugs)
            {
                Console.WriteLine("Person:");
                if (item.personDiscovered != null)
                {
                    Console.WriteLine("Person is not null!!!!");
                    Console.WriteLine(item.description);

                    Console.WriteLine(item.personDiscovered.firstname);
                    Console.WriteLine("Priority is " + item.priority);
                }else
                {
                    Console.WriteLine(item.description);

                    Console.WriteLine("NULL");
                }
            }


            //foreach (var item in showbugs)
            //{
            //    Console.WriteLine(item.id);

            //    if (item.id == 40)
            //    {
            //        Console.WriteLine("Firstname is " + item.person.firstname);
            //    }

            //}
            Console.WriteLine(" ");


            //foreach (var item in bugs)
            //{
            //    if (item != null)
            //    {
            //        if (item.person.firstname != null)
            //        {
            //            Console.WriteLine("Name is " + item.person.firstname);
            //        }
            //    }
            //}

            string jsonString = JsonSerializer.Serialize(showbugs);
            Console.WriteLine(jsonString);



            return jsonString;

        }

        [HttpGet("DemoData")]
        public void CreateDemoData()
        {
          //  PGContext.AddDemoPeople();
         //   Console.WriteLine("Trying to add demo people...");
        }


    }
}
