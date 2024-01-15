using dotnet_graphql_api.GraphQL.Queries;
using GraphQL.Types;

namespace dotnet_graphql_api.GraphQL;

public class AppSchema : Schema
{
    public AppSchema(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        Query = serviceProvider.GetRequiredService<CourseQuery>();
    }
}
