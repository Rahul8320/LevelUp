using dotnet_graphql_api.Data.Repositories;
using dotnet_graphql_api.GraphQL.Types;
using GraphQL;
using GraphQL.Types;

namespace dotnet_graphql_api.GraphQL.Queries;

public class CourseQuery : ObjectGraphType
{
    public CourseQuery(CoursesRepository repository)
    {
        Field<ListGraphType<CourseType>>("allCourses").Resolve(context => repository.GetAllCourses());

        Field<CourseType>("course")
            .Argument<GuidGraphType>("id")
            .Resolve(context => repository.GetCourseById(context.GetArgument<Guid>("id")));
    }
}
