//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using FacultySystem.Models;
//using FacultySystem.ViewModels;
//namespace FacultySystem.Controllers
//{
//    public class CoursesController : Controller
//    {
//        private SystemDbContext db = new SystemDbContext();

//        // GET: Courses
//        public ActionResult Index()
//        {
//            return View(db.Courses.ToList());
//        }

//        // GET: Courses/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Course course = db.Courses.Find(id);
//            if (course == null)
//            {
//                return HttpNotFound();
//            }
//            return View(course);
//        }

//        // GET: Courses/Create
//        public ActionResult Create()
//        {
//            var DepartmentsDb = db.Departments.ToList();
//            var ViewModel = new CourseDeptViewModel
//            {
//                Course = new Course(),
//                Departments = DepartmentsDb
//            };
//            return View("CourseForm", ViewModel);
//        }

//        // POST: Courses/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Save([Bind(Include = "Id,Name,Description,Duration,IsActive,StartDate,EndDate,DropDateDeadLine,DepartmentId")] Course course)
//        {
//            Console.WriteLine(course.Department);
//            if (!ModelState.IsValid)
//            {
//                var ViewModel = new CourseDeptViewModel
//                {
//                    Course = course,
//                    Departments = db.Departments.ToList()
//                };
//                return View("CourseForm",ViewModel);
//            }
//            if(course.Id==0)
//                db.Courses.Add(course);
//            else
//            {
//                db.Entry(course).State = EntityState.Modified;
//            }
        
//            db.SaveChanges();
//             return RedirectToAction("Index", "Courses");
//        }

//        // GET: Courses/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Course course = db.Courses.Find(id);
//            var ViewModel = new CourseDeptViewModel
//            {
//                Course = course,
//                Departments = db.Departments.ToList()
//             };

//            if (course == null)
//            {
//                return HttpNotFound();
//            }
//            return View("CourseForm",ViewModel);
//        }

//        // POST: Courses/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        //[HttpPost]
//        //[ValidateAntiForgeryToken]
//        //public ActionResult Edit([Bind(Include = "Id,Name,Description,Duration,IsActive,StartDate,EndDate,DropDateDeadLine,Department")] Course course)
//        //{
//        //    Console.WriteLine(course.Department);

//        //    if (ModelState.IsValid)
//        //    {
//        //        db.Entry(course).State = EntityState.Modified;
//        //        db.SaveChanges();
//        //        return RedirectToAction("Index");
//        //    }
//        //    return View(course);
//        //}

//        // GET: Courses/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Course course = db.Courses.Find(id);
//            if (course == null)
//            {
//                return HttpNotFound();
//            }
//            return View(course);
//        }

//        // POST: Courses/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            Course course = db.Courses.Find(id);
//            db.Courses.Remove(course);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
