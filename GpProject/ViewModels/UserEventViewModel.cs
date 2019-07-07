using GpProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GpProject.ViewModels
{
    public class UsersEventsViewModel:UserJoinEventViewModel
    {

        public virtual ICollection<Person> People { get; set; }
        public virtual ICollection<Event> Events { get; set; }

    }
    public class UserJoinEventViewModel
    {
        public virtual JoinedEvents UserJoin{get;set;}
    }
    public class UserEventViewModel :UsersEventsViewModel
    {
        public virtual Person Person { get; set; }
        public virtual Event Event { get; set; }
    }
    public class EventPostsViewModel:UserEventViewModel
    {
        public virtual ICollection<Post> Posts { get; set; }

    }

  
}
