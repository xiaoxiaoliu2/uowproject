using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using uowpublic.Services;
namespace uowpublic.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CourseController(ICourseService courseService) =>
        _courseService = courseService;


    [HttpGet]
    public async Task<List<Models.Course>> Get() =>
        await _courseService.GetAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Models.Course>> Get(int id)
    {
        var course = await _courseService.GetAsync(id);

        if (course is null)
        {
            return NotFound();
        }

        return course;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Models.Course newCourse)
    {
        await _courseService.CreateAsync(newCourse);

        return CreatedAtAction(nameof(Get), new { id = newCourse.Id }, newCourse);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Models.Course updatedCourse)
    {
        var course = await _courseService.GetAsync(id);

        if (course is null)
        {
            return NotFound();
        }

        updatedCourse.Id = course.Id;

        await _courseService.UpdateAsync(id, updatedCourse);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var course = await _courseService.GetAsync(id);

        if (course is null)
        {
            return NotFound();
        }

        await _courseService.RemoveAsync(id);

        return NoContent();
    }
}