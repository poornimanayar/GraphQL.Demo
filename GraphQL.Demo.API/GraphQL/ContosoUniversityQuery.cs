using GraphQL.Demo.API.GraphQL.Types;
using GraphQL.Demo.API.Repositories;
using GraphQL.Types;

namespace GraphQL.Demo.API.GraphQL
{
    public class ContosoUniversityQuery : ObjectGraphType
    {
        //operation type in GraphQL
        public ContosoUniversityQuery(StudentsRepository studentsRepository, CoursesRepository coursesRepository)
        {
            // specify fields and resolve
            Field<ListGraphType<CourseType>>( //type of the field
                "courses", "Get all courses", //name and description shows up in schema docs and name used for querying.
                resolve: context => coursesRepository.GetAll() //returns a task which does not need waiting GraphQL.Net takes care of that. Returns a list of Student models and GraphQL.Net takes care of the conversion again
                );           

            Field<CourseType>(
                "course", "Get details about a given course",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> //argument matched by name
                {
                    Name = "id",
                    Description = "The id of the student"
                }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    return coursesRepository.GetById(id);
                }
            );

            Field<ListGraphType<StudentType>>(
                "students","Get all students",                
                resolve: context => studentsRepository.GetAll()
            );

            Field<StudentType>(
                "student","Get details about a given student",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>>
                {
                    Name = "id",
                    Description = "The id of the student"
                }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    return studentsRepository.GetById(id);
                }
            );           

        }
    }
}
