using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseApp.DataTransferObject.Responses;
using CourseApp.Mvc.Extensions;
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
            var courseCollection = getCourseCollectionFromSession();
            return View(courseCollection);
        }

        public IActionResult AddCourse(int id)
        {
            CourseDisplayResponse selectedCourse = courseService.GetCourse(id);
            //Selected kursu sepete eklenecek kursa dönüştürdük.
            var courseItem = new CourseItem { Course = selectedCourse, Quantity = 1 };
            CourseCollection courseCollection = getCourseCollectionFromSession();
            //Kurs koleksiyonunu sessiondan aldık boş mu dolu mu bilmiyoruz
            courseCollection.AddNewCourse(courseItem);
            saveToSession(courseCollection); //Tekrar sessiona kaydettik.

            return Json(new { message = $"{selectedCourse.Name} Sepete eklendi" });
        }


        private CourseCollection getCourseCollectionFromSession()
        {
            return HttpContext.Session.GetJson<CourseCollection>("basket") ?? new CourseCollection();
        }


        private void saveToSession(CourseCollection courseCollection)
        {

            HttpContext.Session.SetJson("basket", courseCollection);

        }

    }
}

