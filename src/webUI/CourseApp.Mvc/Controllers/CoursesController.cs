using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CourseApp.DataTransferObject.Requests;
using CourseApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CourseApp.Mvc.Controllers
{
    [Authorize(Roles = "Admin,Editor")]
    public class CoursesController : Controller
    {
        private readonly ICourseService courseService;
        private readonly ICategoryService categoryService;

        public CoursesController(ICourseService courseService, ICategoryService categoryService)
        {
            this.courseService = courseService;
            this.categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var courses = courseService.GetCourseDisplayResponses();
            return View(courses);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = getCategoriesForSelectList();
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Create(CreateNewCourseRequest request)
        {
            if (ModelState.IsValid)
            {
                await courseService.CreateCourseAsync(request);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = getCategoriesForSelectList();
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Categories = getCategoriesForSelectList();
            var course = await courseService.GetCourseForUpdate(id);

            return View(course);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, UpdateCourseRequest updateCourseRequest)
        {
            if (await courseService.CourseIsExists(id))
            {
                if (ModelState.IsValid)
                {
                    await courseService.UpdateCourse(updateCourseRequest);
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Categories = getCategoriesForSelectList();
                return View();
            }
            return NotFound();
        }



        private IEnumerable<SelectListItem> getCategoriesForSelectList()
        {
            var categories = categoryService.GetCategoriesForList().Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() }).ToList();
            // categories.Insert(0, new SelectListItem { Text = "Seçiniz", Value = string.Empty });
            return categories;
        }
    }
}

