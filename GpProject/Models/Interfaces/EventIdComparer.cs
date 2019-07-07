using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GpProject.Models.Interfaces
{
    public class EventIdComparer : IEqualityComparer<Event>
    {
        public bool Equals(Event x, Event y)
        {
            if (x.Id==y.Id)
            {
                return true;
            }
            return false;
        }
        public int GetHashCode(Event obj)
        {
            return obj.Id.GetHashCode();
        }
    }
   
    }