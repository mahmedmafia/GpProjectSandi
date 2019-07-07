using GpProject.Models;
using GpProject.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
namespace GpProject.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> UserManager { get; set; }
        public Person CurrentUser()
        {
            var person = new Person
            {
                Id = 0,
            };
            if (User.Identity.IsAuthenticated)
            {
                var currentuserId = User.Identity.GetUserId();
                ApplicationUser currentuser = db.Users.FirstOrDefault(x => x.Id == currentuserId);
                person = db.People.FirstOrDefault(p => p.Id == currentuser.AppUser.Id);

            }
            return person;

        }
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Home()
        {
            // VIew Alll Users Posts and His Posts

            return View();
        }
        public ActionResult MyProfile()
        {
            //View Details
            //View Friends
            //View His Posts
         
            return View();
        }

        [HttpPost]
        public JsonResult AddPost(Post Post)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var post= Post;
            post.EventId = 5;
            post.DatePosted = DateTime.Now;
            db.Posts.Add(post);
            db.SaveChanges();
            //var person = 
            post.Person= db.People.FirstOrDefault(m => m.Id == Post.PersonId);
            return Json(post, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddComment(Comment comment)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var Comment = comment;
            Comment.DateCommented = DateTime.Now;
            db.Comments.Add(comment);
            db.SaveChanges();
            Comment.Person = db.People.FirstOrDefault(m => m.Id == Comment.PersonId);
            return Json(Comment, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult Posts()
        {

            //View Details
            //View Friends
            //View His Posts  
            var Posts = db.Posts.Include(p=>p.Person).Include(e=> e.Comments).OrderByDescending(h=>h.DatePosted).ToList();
            var currentuser = CurrentUser();

            var viewmodel = new UserPostViewModel {
                Person = currentuser,
                Posts=Posts,
                Post=new Post ()
            };
            return View(viewmodel);
        }

    }
}