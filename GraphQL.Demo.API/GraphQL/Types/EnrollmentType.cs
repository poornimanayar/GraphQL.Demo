using GraphQL.DataLoader;
using GraphQL.Demo.API.Models;
using GraphQL.Demo.API.Repositories;
using GraphQL.Types;

namespace GraphQL.Demo.API.GraphQL.Types
{
    public class EnrollmentType : ObjectGraphType<Enrollment>
    {
        public EnrollmentType(IDataLoaderContextAccessor dataLoaderAccessor, CoursesRepository coursesRepository, StudentsRepository studentsRepository)
        {
            Field(x => x.EnrollmentID).Name("Id").Description("The enrollment id");
            Field(x => x.CourseID).Name("CourseId").Description("The course id");
            Field(x => x.StudentID).Name("StudentId").Description("The student id");
            Field<EnrollmentGradeEnum>("Grade", "Grade of the student for the course enrollment");

            Field<CourseType, Course>()
                .Name("Course")
                .Description("The course enrolled for")
                .ResolveAsync(context =>
                {
                    //This is an example of using a DataLoader to batch requests for loading a collection of items by a key.
                    //This is used when a key may be associated with more than one item. LoadAsync() is called by the field resolver for each User.

                    var loader =
                       dataLoaderAccessor.Context.GetOrAddBatchLoader<int, Course>(
                           "GetCourses", coursesRepository.GetCourses);

                    return loader.LoadAsync(context.Source.CourseID);
                });

            Field<StudentType, Student>()
               .Name("Student")
               .Description("The student enrolled")
               .ResolveAsync(context =>
               {
                   //This is an example of using a DataLoader to batch requests for loading a collection of items by a key.
                   //This is used when a key may be associated with more than one item. LoadAsync() is called by the field resolver for each User.

                   var loader =
                      dataLoaderAccessor.Context.GetOrAddBatchLoader<int, Student>(
                          "GetStudents", studentsRepository.GetStudents);

                   return loader.LoadAsync(context.Source.StudentID);
               });
        }
    }

    public class EnrollmentGradeEnum : EnumerationGraphType<Grade>
    {
        public EnrollmentGradeEnum()
        {
            Name = "EnrollmentGrade";
            Description = "The grade of the student for the enrollment";

        }
    }
}
