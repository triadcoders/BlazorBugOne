using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BlazorBugOne.Shared
{
   public class Bug
    {

        [Key]
        public int id { get; set; }
        public string summary { get; set; }
        public string description { get; set; }

        public Person person { get; set; }
        public Person assignedto { get; set; }
        public DateTime created { get; set; }

        public Project project { get; set; }

     //   public enum priority { Low, Medium,  High, Urgent }  can do but later

        public string priority { get; set; }
        public string status { get; set; }

        public DateTime targetdate { get; set; }

        public DateTime resolutiondate { get; set; }

        public string progressreport { get; set; }

        public string resolutionsummary { get; set; }
        public string lifecycle { get; set; }

    }
}
