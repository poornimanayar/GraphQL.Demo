using GraphQL.DataLoader;
using GraphQL.Demo.API.Models;
using GraphQL.Demo.API.Repositories;
using GraphQL.Types;
using System.Collections.Generic;

namespace GraphQL.Demo.API.GraphQL.Types
{
    public class CourseType : ObjectGraphType<Course>
    {
        public CourseType(IDataLoaderContextAccessor dataLoaderAccessor, EnrollmentRepository enrollmentRepository)
        {
            Field(c => c.CourseID).Name("Id").Description("The course id");

            Field(c => c.Title).Description("The course title");

            Field(c => c.Credits).Description("The course credits");

            Field(typeof(StringGraphType),"testfield", "test field for deprecation",null, resolve:context => {
                return "test";
            }, "test reason for deprecation");

            Field<ListGraphType<EnrollmentType>, IEnumerable<Enrollment>>().Name("Enrollments").Description("Enrollment details for the course")
             .ResolveAsync(context =>
             {
                  //This is an example of using a DataLoader to batch requests for loading a collection of items by a key.
                  //This is used when a key may be associated with more than one item. LoadAsync() is called by the field resolver for each User.

                  var loader =
                    dataLoaderAccessor.Context.GetOrAddCollectionBatchLoader<int, Enrollment>(
                        "GetEnrollmentsByCourse", enrollmentRepository.GetEnrollmentForCourses);

                 return loader.LoadAsync(context.Source.CourseID); //load data for the given key
              });
        }
    }
}
