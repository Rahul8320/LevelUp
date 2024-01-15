using dotnet_graphql_api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_graphql_api.Data.Repositories;

public class CoursesRepository(CourseDbContext context)
{

    private readonly CourseDbContext _context = context;

    public List<Course> GetAllCourses()
    {
        return [.. _context.Courses];
    }

    public Course? GetCourseById(Guid id)
    {
        return _context.Courses.FirstOrDefault(item => item.Id == id);
    }

    public Course AddCourse(AddCourseDto courseDto)
    {
        var course = new Course
        {
            Id = Guid.NewGuid(),
            Name = courseDto.Name,
            Description = courseDto.Description,
            Review  = courseDto.Review,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        _context.Courses.Add(course);
        _context.SaveChanges();

        return course;
    }

    public Course? UpdateCourse(Guid id, AddCourseDto courseDto)
    {
        var existingCourse = _context.Courses.FirstOrDefault(item => item.Id == id);

        if(existingCourse == null)
        {
            return null;
        }

        existingCourse.Name = courseDto.Name;
        existingCourse.Description = courseDto.Description;
        existingCourse.Review = courseDto.Review;
        existingCourse.UpdatedAt = DateTime.UtcNow;

        _context.Entry(existingCourse).State = EntityState.Modified;
        _context.SaveChanges();

        return existingCourse;
    }

    public bool DeleteCourse(Guid id)
    {
        var course = _context.Courses.Find(id);
        if(course == null)
        {
            return false;
        }
        _context.Courses.Remove(course);
        _context.SaveChanges();

        return true;
    }
}
