using GpProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GpProject.ViewModels
{
    public class UsersPostsViewModel
    {
        public virtual Post Post { get; set; }
    }
    public class UserPostViewModel:UsersPostsViewModel
    {
        public virtual Person Person { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}