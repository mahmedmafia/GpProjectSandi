using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GpProject.Models
{
    public class PostType
    {
        public int Id { get; set; }
        public String Name { get; set; }


       
        public static readonly  int Profile = 1;
        public static readonly int Event = 2;
        public static readonly int Question = 3;

      
    }
}