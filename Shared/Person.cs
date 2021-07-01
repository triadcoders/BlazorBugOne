using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BlazorBugOne.Shared
{
    public class Person
    {
        [Key]
        public int id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string usertype { get; set; }

        //  public Func<string, string, string> FullName { get; set; }

        // delegate string FullName(string first, string last);




    }

    public static class DemoData
        {

       public static List<Person> GenerateDemoData()
        {
            List<Person> demopeople = new();

            for (int i = 0; i < 5; i++)
            {
                demopeople.Add( CreatePerson("Bob", "Smith"));
                demopeople.Add(CreatePerson("Kevin", "Bacon"));
                demopeople.Add(CreatePerson("George", "Lucas"));
                demopeople.Add(CreatePerson("Luke", "Skywalker"));
                demopeople.Add(CreatePerson("Fred", "Sanford"));
                demopeople.Add(CreatePerson("Sally", "Fields"));
            }
            return demopeople;
        }


        static Person CreatePerson(string first, string last)
        {
            Person person = new() { firstname = first, lastname = last };
            return person;
        }

    }








}

        

   

   

