using dotnet_graphql_api.Data.Models;
using dotnet_graphql_api.Data.Repositories;

namespace dotnet_graphql_api.GraphQL;

public class CourseQuery(CoursesRepository repository)
{
    private readonly CoursesRepository _repository = repository;

    public List<Course> GetCourses() => _repository.GetAllCourses();

    public Course? GetCourse(Guid id) => _repository.GetCourseById(id);
}
