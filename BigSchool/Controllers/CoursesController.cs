using BigSchool.Models;
using BigSchool.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace BigSchool.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public CoursesController()
        {
            _dbContext = new ApplicationDbContext();
        }
        // GET: Courses
        [Authorize]
        public ActionResult Create()
        {
            var ViewModel = new CourseViewModel
            {
                Categories = _dbContext.Categories.ToList()
            };
            return View(ViewModel);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseViewModel ViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewModel.Categories = _dbContext.Categories.ToList();
                return View("Create", ViewModel);
            }
            var courses = new Course
            {
                LecturerId = User.Identity.GetUserId(),
                DateTime = ViewModel.GetDateTime(),
                CategoryId = ViewModel.Category,
                Place = ViewModel.Place
            };
            _dbContext.Courses.Add(courses);
            _dbContext.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}