using GraphQL.Demo.API.Models;
using GraphQL.Types;

namespace GraphQL.Demo.API.GraphQL
{
    public class CourseInputType : InputObjectGraphType<Course>
    {
        public CourseInputType()
        {
            Name = "CourseInput";
            Description = "The course input type";
            Field(x => x.CourseID, nullable: false).Name("id").Description("The course title");
            Field(x => x.Title, nullable: false).Description("The course title");
            Field(x => x.Credits, nullable: true).Description("Course credits");
        }
    }
}
