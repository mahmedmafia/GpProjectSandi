﻿using GpProject.Models;
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
        

        //Adding Post To Home
        [HttpPost]
        public JsonResult AddPost(HomePost Post)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var post= Post;
            post.DatePosted = DateTime.Now;
            db.Posts.Add(post);
            db.SaveChanges();
            post.Person= db.People.FirstOrDefault(m => m.Id == Post.PersonId);
            return Json(post, JsonRequestBehavior.AllowGet);
        }


        //Adding Comment To Home
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



        [HttpPost]
        public ActionResult DeleteComment(int id)
        {
            var comment = db.Comments.FirstOrDefault(com => com.Id == id);
            if (comment != null)
            {
                db.Comments.Remove(comment);
                db.SaveChanges();
            }
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult DeletePost(int id)
        {

            var comments = db.Comments.Where(com => com.PostId == id).ToList();
            if (comments.Count != 0)
            {
                foreach (var comment in comments)
                {
                    db.Comments.Remove(comment);

                }
            }

            var post = db.Posts.FirstOrDefault(pos => pos.Id == id);
            if (post != null)
            {
                db.Posts.Remove(post);
                db.SaveChanges();
            }
            return new EmptyResult();
        }

        public ActionResult EditPost(int id,string val)
        {
            var post = db.Posts.FirstOrDefault(pos => pos.Id == id);
            post.Content = val;
            db.Entry(post).State = EntityState.Modified;
            db.SaveChanges();

            return new EmptyResult();
        }
        public ActionResult EditComment(int id, string val)
        {
            var comment = db.Comments.FirstOrDefault(pos => pos.Id == id);
            comment.Content = val;
            db.Entry(comment).State = EntityState.Modified;
            db.SaveChanges();

            return new EmptyResult();
        }

        //The main Home Page
        [Authorize]
        public ActionResult Posts()
        {

            //View Details
            //View Friends
            //View His Posts  
            var Posts = db.Posts.OfType<HomePost>().Where(t=>t.TypeId==PostType.Profile).Include(p=>p.Person).Include(e=> e.Comments).OrderByDescending(h=>h.DatePosted).ToList();
            var currentuser = CurrentUser();

            var viewmodel = new UserPostViewModel {
                Person = currentuser,
                Posts=Posts,
                Post=new HomePost ()
            };
            return View(viewmodel);
        }

    }
}