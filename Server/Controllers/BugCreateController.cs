using BlazorBugOne.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

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
        PGContext pGContext = new();
        //https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/routing?view=aspnetcore-5.0
        
        [HttpPost("MakePost")]
        public IActionResult CreateBlog(Bug bug)
        {
            Console.WriteLine("Post has been called");
            //  return RedirectToAction("Counter");
          //  ActionResult actionResult;
            IActionResult response = Ok(new { result = "TEST" });

            Console.WriteLine(bug.description);
            Console.WriteLine(bug.summary);

            return response;

        }






        [HttpPost("MakePostAgain")]
        public IActionResult CreateBlogAgain(Bug bug)
        {

            Console.WriteLine("Bug incomming ...bug id is " + bug.id);

            //See if this bug already exist
            //   Bug newbug = pGContext.Bugs.Where(b => b.id == bug.id).FirstOrDefault();

            //if it already exist then we need to update the bug not add it.
            //     newbug = bug;

            // if (bug.id == 54)
            // {
            ////     pGContext.Bugs.Attach(bug);
            ////     pGContext.Entry(bug).State = EntityState.Modified;

            //  //   pGContext.SaveChanges();
            //     Console.WriteLine("Trying to update bug");
            //     pGContext.Update(newbug);

            // }else
            // {
            //  //   pGContext.Add(bug);
            // }

           

            Console.WriteLine("Post has been called again");
            //  return RedirectToAction("Counter");
            //  ActionResult actionResult;
            IActionResult response = Ok(new { result = "Success" });

            Console.WriteLine(bug.description);
            Console.WriteLine(bug.summary);
            Console.WriteLine("project name is " + bug.project?.name);
            Console.WriteLine("firstname is " + bug.person?.firstname);

            //   Person person = new Person();

            //  bug.person.firstname = person.firstname;
            //     pGContext.Add(person);

            //   bug.person = person;

            pGContext.Update(bug);
            pGContext.SaveChanges();

            return response;

        }


        //public IActionResult MrPost()
        //{
        //    IActionResult response = Ok(new { result = "TEST" });
        //    return response;
        //}



        [HttpGet("Delete")]
        public string Delete() 
        {

            int bugcount = pGContext.Bugs.Count();
            string description = "";
            //   Bug newbug;
            //bug.description = "I am a delete bug #1";
            ////pGContext.Add()
            //pGContext.Add(bug);
            //pGContext.SaveChanges();



            if (bugcount >= 0)
            {
              //  bug = pGContext.Bugs.Where(b => b.id == bugcount).FirstOrDefault();
                bug = pGContext.Bugs.OrderByDescending(b => b.id).FirstOrDefault();
            }


            if (bug != null)
            {
                description = bug.description + " " + bug.id;
                pGContext.Remove(bug);
                pGContext.SaveChanges();
                return "Data Deleted on bug " + description;
            }else
            {
                return "Bug was null on id " + bugcount;
            }

            //var weatherForecast = new WeatherForecast
            //{
            //    Date = DateTime.Parse("2019-08-01"),
            //    // TemperatureCelsius = 25,
            //    Summary = "Hot"
            //};

            //string jsonString = JsonSerializer.Serialize(weatherForecast);

            //return jsonString;
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
            pGContext.Add(bug);
            pGContext.SaveChanges();

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
            //   showbugs = pGContext.Bugs.Include(bug => bug.person).Include(bug => bug.project).ToList();
            var onebug = pGContext.Bugs.Include(bug => bug.person).Include(bug => bug.project).Include(bug => bug.assignedto).Where(b => b.id == id).FirstOrDefault();
            string oneJson = JsonSerializer.Serialize(onebug);
            Console.WriteLine(oneJson);
            return oneJson;

        }

        [HttpGet("GetBugs")]
        public string GetBugs()
        {

           List<Bug> showbugs = new List<Bug>();


            //          showbugs = pGContext.Bugs.Where(b => b.id > 0).ToList();


            //https://docs.microsoft.com/en-us/ef/core/querying/related-data/#lazy-loading
            //note the using entity core above to get the include option
            showbugs = pGContext.Bugs.Include(bug => bug.person).Include(bug => bug.project).Include(bug => bug.assignedto).ToList();

              

            //     bugs = pGContext.Bugs.Where(b => b.person.firstname == "BOB".ToLower() ).ToList();


            foreach (var item in pGContext.Bugs)
            {
                Console.WriteLine("Person:");
                if (item.person != null)
                {
                    Console.WriteLine("Person is not null!!!!");
                    Console.WriteLine(item.description);

                    Console.WriteLine(item.person.firstname);
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
            PGContext.AddDemoPeople();
            Console.WriteLine("Trying to add demo people...");
        }


    }
}
