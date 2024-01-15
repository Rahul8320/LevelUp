using dotnet_graphql_api.Data.Models;
using GraphQL.Types;

namespace dotnet_graphql_api.GraphQL.Types;

public class CourseType : ObjectGraphType<Course>
{
    public CourseType()
    {
        Field(x => x.Id, type: typeof(GuidGraphType)).Description("Id property from the Course object");
        Field(x => x.Description, type: typeof(StringGraphType)).Description("Description property from the Course object");
        Field(x => x.Name, type: typeof(StringGraphType)).Description("Name property from the Course object");
        Field(x => x.Review, type: typeof(IntGraphType)).Description("Review property from the Course object");
        Field(x => x.CreatedAt, type: typeof(DateTimeGraphType)).Description("CreatedAt property from the Course object");
        Field(x => x.UpdatedAt, type: typeof(DateTimeGraphType)).Description("UpdatedAt property from the Course object");
    }
}
