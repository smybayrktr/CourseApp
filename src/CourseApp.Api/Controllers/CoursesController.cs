using CourseApp.DataTransferObject.Requests;
using CourseApp.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace CourseApp.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _courseService;
    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet]
    public IActionResult GetCourses()
    {
        var courses = _courseService.GetCourseDisplayResponses();
        return Ok(courses);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetCourse(int id)
    {
        var course = _courseService.GetCourse(id);
        if (course == null)
        {
            return NotFound();
        }
        return Ok(course);
    }
    [HttpGet("[action]")]
    public async Task<IActionResult> SearchCourseByName(string name)
    {
        var courses = await _courseService.SearchByName(name);
        return Ok(courses);
    }

    [HttpGet("[action]")]
    public IActionResult GetCoursesByCategory(int id)
    {
        var courses = _courseService.GetCoursesByCategory(id);
        return Ok(courses);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateNewCourseRequest request)
    {
        if (ModelState.IsValid)
        {
            var lastCourseId = await _courseService.CreateCourseAndReturnIdAsync(request);
            return CreatedAtAction(nameof(GetCourse), routeValues: new { id = lastCourseId }, null);

        }
        return BadRequest(ModelState);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateCourseRequest updateCourseRequest)
    {
        var isExists = await _courseService.CourseIsExists(id);
        if (isExists)
        {
            if (ModelState.IsValid)
            {
                await _courseService.UpdateCourse(updateCourseRequest);
                return Ok();
            }

            return BadRequest(ModelState);
        }
        return NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (await _courseService.CourseIsExists(id))
        {
            await _courseService.DeleteAsync(id);
            return Ok();
        }
        return NotFound();
    }



}

