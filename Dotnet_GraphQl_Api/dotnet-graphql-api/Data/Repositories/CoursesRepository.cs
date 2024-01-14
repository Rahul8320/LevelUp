using dotnet_graphql_api.Data.Models;

namespace dotnet_graphql_api.Data.Repositories;

public class CoursesRepository
{
    private readonly List<Course> _courses = [];
    private int _lastIndex = 0;

    public List<Course> GetAllCourses()
    {
        return _courses;
    }

    public Course? GetCourseById(int id)
    {
        return _courses.FirstOrDefault(item => item.Id == id);
    }

    public Course AddCourse(AddCourseDto courseDto)
    {
        _lastIndex++;
        var course = new Course
        {
            Id = _lastIndex,
            Name = courseDto.Name,
            Description = courseDto.Description,
            Review  = courseDto.Review,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        _courses.Add(course);

        return course;
    }

    public Course? UpdateCourse(int id, AddCourseDto courseDto)
    {
        var existingCourse = _courses.FirstOrDefault(item => item.Id == id);

        if(existingCourse == null)
        {
            return null;
        }

        existingCourse.Name = courseDto.Name;
        existingCourse.Description = courseDto.Description;
        existingCourse.Review = courseDto.Review;
        existingCourse.UpdatedAt = DateTime.UtcNow;

        return existingCourse;
    }

    public bool DeleteCourse(int id)
    {
        return _courses.RemoveAll(item => item.Id == id) != 0;
    }
}
