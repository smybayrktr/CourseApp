using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseApp.DataTransferObject.Responses;
using CourseApp.Mvc.Models;
using CourseApp.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CourseApp.Mvc.Controllers
{
    public class ShoppingController : Controller
    {
        private readonly ICourseService courseService;

        public ShoppingController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddCourse(int id)
        {
            CourseDisplayResponse selectedCourse = courseService.GetCourse(id);
            
            return Json(new { message = $"{selectedCourse.Name} Sepete eklendi" });
        }


        
    }
}

