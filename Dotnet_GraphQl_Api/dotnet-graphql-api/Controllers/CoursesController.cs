using dotnet_graphql_api.Data.Models;
using dotnet_graphql_api.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_graphql_api.Controllers;

[ApiController]
[Route("api/courses")]
public class CoursesController(CoursesRepository coursesRepository) : ControllerBase
{
    private readonly CoursesRepository _repository = coursesRepository;

    [HttpGet]
    public IActionResult GetAllCourses()
    {
        try
        {
            var allCourses = _repository.GetAllCourses();
    
            return Ok(allCourses);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult GetCourse(Guid id)
    {
        try
        {
            var course = _repository.GetCourseById(id);

            if(course == null)
            {
                return NotFound();
            }
    
            return Ok(course);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public IActionResult CreateCourse([FromBody] AddCourseDto courseDto)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addCourse = _repository.AddCourse(courseDto);

            return Ok(addCourse);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult UpdateCourse(Guid id, [FromBody] AddCourseDto courseDto)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedCourse = _repository.UpdateCourse(id, courseDto);

            if(updatedCourse == null)
            {
                return NotFound();
            }

            return Ok(updatedCourse);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult DeleteCourse(Guid id)
    {
        try
        {
            var isDeleted = _repository.DeleteCourse(id);

            if(isDeleted)
            {
                return Ok("Course is deleted!");
            }

            return NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
