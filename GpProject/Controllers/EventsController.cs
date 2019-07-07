using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GpProject.Models;
using GpProject.Models.Interfaces;
using GpProject.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GpProject.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> UserManager { get; set; }

        public ICollection<Event> AllEvents()
        {
            var @event = db.Events.Include(x => x.Owner).OrderByDescending(m => m.DateCreated).ToList();
            return (@event);
        }
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

        // GET: Events
        [AllowAnonymous]
        public ActionResult Index()
        { 
        
            var person = CurrentUser();
            var Events = AllEvents();
            var viewModel = new UserEventViewModel
            {
                Events = Events,
                Person = person,
                UserJoin = new JoinedEvents
                {
                    JoinedPerson = person,
                }
            };
            return View(viewModel);
        }
        public ActionResult MyEvents()
        {
            var currentuser = CurrentUser();
            var allEvents = AllEvents();
            var Events = allEvents.Where(m => m.OwnerId == currentuser.Id).ToList();
            var viewModel = new UserEventViewModel
            {
                Events = Events,
                Person = currentuser,
                UserJoin = new JoinedEvents
                {
                    JoinedPerson = currentuser,
                }
            };
            return View("Index", viewModel);
        }
        public ActionResult JoinedEvents()
        {
            var currentuser = CurrentUser();
            var Events = AllEvents();
            var JoinedEvents = new JoinedEvents();
            //int?[] productModelIds = { 19, 26, 118 };
            //var products = AWEntities.Products.
            //    Where(p => productModelIds.Contains(p.ProductModelID));
            var joins = JoinedEvents.EventsJoined(currentuser);
            Events.ToList();
            joins.ToList();
            var Ev=Events.Intersect(joins,new EventIdComparer()).ToList();
            
            var viewModel = new UserEventViewModel
            {
                Person=currentuser,
                Events=Ev,
                 UserJoin = new JoinedEvents
                 {
                     JoinedPerson = currentuser,
                 }
            };
            return View("Index", viewModel);

        }
        [HttpPost]
        public ActionResult JoinEvent(int Id)
        {
            var UserToJoin = CurrentUser();
            var EventToBeJoined = AllEvents().FirstOrDefault(e => e.Id == Id);
            if (EventToBeJoined == null)
            {
                return HttpNotFound();
                //return Content("Event Not found");
            }
            //validatio
            if (EventToBeJoined.Owner != UserToJoin)
            {
                

                var JoinedEvents = new JoinedEvents
                {
                    JoinedPerson = UserToJoin,
                    Event = EventToBeJoined,
                };
                var common = db.JoinedEvents.Where(p => p.JoinedPerson.Id == UserToJoin.Id).Where(e => e.Event.Id == EventToBeJoined.Id).ToList();
                var result=JoinedEvents.IsUserJoined(UserToJoin,EventToBeJoined);
                if (result)
                {
                    db.JoinedEvents.Add(JoinedEvents);
                }
                else 
                {
                    foreach (var commonJoin in common)
                    {
                    db.JoinedEvents.Remove(commonJoin);
                    }
                }
                db.SaveChanges();

                //return Content("Joined");

            }


            return new EmptyResult();


        }
      

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            var currentuser = CurrentUser();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event currentevent = db.Events.Include(m=>m.Posts).Include(p=>p.Owner).FirstOrDefault(m=>m.Id==id);
            var eventposts = db.Posts.Include(c => c.Comments).Include(p=>p.Person).Where(c=>c.EventId==currentevent.Id).ToList();
            var eventpostsviewmodel = new EventPostsViewModel {
                Posts = eventposts,
                Event = currentevent,
                Person = currentuser,
                UserJoin = new JoinedEvents
                {
                    JoinedPerson = currentuser
                },
            };
            if (currentevent== null)
            {
                return HttpNotFound();
            }
            return View(eventpostsviewmodel);
        }
        public ActionResult JoinedUsers (int id)
        {
            var Events = AllEvents();
            var Event=Events.FirstOrDefault(e => e.Id == id);
            var joinedUsers = new JoinedEvents();
            var Persons=joinedUsers.PersonsJoined(Event);

            return PartialView(Persons);
        }
        public ActionResult EventPosts(int id)
        {
            var currentuser = CurrentUser();
            Event currentevent = db.Events.Include(m => m.Posts).Include(p => p.Owner).FirstOrDefault(m => m.Id == id);
            //var eventposts = db.Posts.Include(c => c.Comments).Include(p => p.Person).Where(c => c.EventId == currentevent.Id).OrderByDescending(m=>m.DatePosted).ToList();
            var eventposts = new List<Post>
           {
               new Post{DatePosted=new DateTime(2015,6,21),Person=currentuser,Content="its my birthday bitches 2015",EventId=5},

               new Post{DatePosted=new DateTime(2016,6,21),Person=currentuser,Content="its my birthday bitches2016",EventId=5},
               new Post{DatePosted=new DateTime(2017,6,21),Person=currentuser,Content="its my birthday bitches2017",EventId=5},
               new Post{DatePosted=new DateTime(2018,6,21),Person=currentuser,Content="its my birthday bitches2018",EventId=7},
               new Post{DatePosted=new DateTime(2019,6,21),Person=currentuser,Content="its my birthday bitches 2019",EventId=7},

           };
            eventposts=eventposts.Where(c => c.EventId == currentevent.Id).OrderByDescending(m=>m.DatePosted).ToList();
            var eventpostsviewmodel = new EventPostsViewModel
            {
                Posts = eventposts,
            };
            return PartialView("EventPosts", eventpostsviewmodel);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            var currentUser = CurrentUser();
            var viewmodel = new UserEventViewModel
            {
                Person = currentUser,
                Event = new Event()
                
            };

            return View("EventForm", viewmodel);

        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Event @event)
         {
            var currentuser = CurrentUser();
            var Event = @event;
            Event.OwnerId = currentuser.Id;
            Event.DateCreated = DateTime.Now;

            if (ModelState.IsValid)
            {
                if (Event.Id == 0)
                {
                    db.Events.Add(Event);
                }
                else
                {
                    db.Entry(Event).State = EntityState.Modified;

                }

            }
            else
            {
                var userEventViewModel = new UserEventViewModel
                {
                    Person = currentuser,
                    Event=Event
                   
                };
                return View("EventForm", userEventViewModel);

            }

            db.SaveChanges();
            return RedirectToAction("Index", "Events");
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Event @event = db.Events.Find(id);
            var currentUser = CurrentUser();
            var viewmodel = new UserEventViewModel
            {
                Person = currentUser,
                Event=@event
            };

            if (@event == null)
            {
                return HttpNotFound();
            }
            return View("EventForm",viewmodel);

        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Description,DateCreated,DateStart,Active,Location")] Event @event)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(@event).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(@event);
        //}

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
