using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GpProject.Models
{
    public class Types
    {
        public int Id { get; set; }
        public String TypeName { get; set; }


        public class TypeId
        {
            public const int Home = 1;
            public const int Event = 2;
            public const int Question = 3;

        }
    }
}