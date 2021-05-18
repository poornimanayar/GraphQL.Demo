using GraphQL.Demo.API.GraphQL.Types;
using GraphQL.Demo.API.Models;
using GraphQL.Demo.API.Models.Messaging;
using GraphQL.Demo.API.Repositories;
using GraphQL.Types;

namespace GraphQL.Demo.API.GraphQL
{
    //operation type in GraphQL
    public class ContosoUniversityMutation : ObjectGraphType
    {
        public ContosoUniversityMutation(CoursesRepository courseRepository, StudentsRepository studentsRepository, EnrollmentRepository enrollmentRepository, CourseMessageService courseMessageService)
        {
            // specify fields and resolve
            FieldAsync<CourseType>( //type of the field
                "createCourse", "Create a course",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<CourseInputType>> { Name = "course" }),//arguments matched by name

                resolve: async context =>
                {
                    var course = context.GetArgument<Course>("course");                   
                    await courseRepository.CreateCourse(course);
                    courseMessageService.AddCourseAddedMessage(course);//subscription
                    return course;
                });

            FieldAsync<StudentType>(
               "createStudent",
               arguments: new QueryArguments(
                   new QueryArgument<NonNullGraphType<StudentInputType>> { Name = "student" }),

               resolve: async context =>
               {
                   var student = context.GetArgument<Student>("student");
                   return await studentsRepository.CreateStudent(student);
               });

            // can create update and delete fields 
        }
    }
}