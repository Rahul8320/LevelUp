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
    [Route("/{id}")]
    public IActionResult GetCourse(string id)
    {
        try
        {
            var allCourses = _repository.GetCourseById(int.Parse(id));
    
            return Ok(allCourses);
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
    [Route("/{id}")]
    public IActionResult UpdateCourse(string id, [FromBody] AddCourseDto courseDto)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedCourse = _repository.UpdateCourse(int.Parse(id), courseDto);

            return Ok(updatedCourse);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult DeleteCourse(string id)
    {
        try
        {
            var isDeleted = _repository.DeleteCourse(int.Parse(id));

            if(isDeleted)
            {
                return Ok(isDeleted);
            }

            return BadRequest("Course not Deleted!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
