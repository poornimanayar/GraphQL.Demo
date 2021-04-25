using GraphQL.DataLoader;
using GraphQL.Demo.API.Models;
using GraphQL.Demo.API.Repositories;
using GraphQL.Types;
using System.Collections.Generic;

namespace GraphQL.Demo.API.GraphQL.Types
{
    public class StudentType : ObjectGraphType<Student>
    {
        public StudentType(IDataLoaderContextAccessor dataLoaderAccessor, EnrollmentRepository enrollmentRepository)
        {
            
            Field(x => x.ID).Name("Id").Description("The ID of the student");

            Field(x => x.FirstMidName).Name("FirstName").Description("The firstname of the student");

            Field(x => x.LastName).Description("The lastname of the student");

            Field(x => x.EmailAddress).Description("The email address of the student");

           Field(x => x.EnrollmentDate).Description("The enrollment date of the student");

            Field<ListGraphType<EnrollmentType>, IEnumerable<Enrollment>>().Name("Enrollments").Description("Enrollment details for the student")
              .ResolveAsync(context =>
              {
                  //This is an example of using a DataLoader to batch requests for loading a collection of items by a key.
                  //This is used when a key may be associated with more than one item. LoadAsync() is called by the field resolver for each User.

                  var loader =
                     dataLoaderAccessor.Context.GetOrAddCollectionBatchLoader<int, Enrollment>(
                         "GetEnrollmentsByStudent", enrollmentRepository.GetEnrollmentForStudents);

                  return loader.LoadAsync(context.Source.ID); //load data for the given key
              });



            ////n+1 issue, fetches a student and then gets the enrollments for the student
            //Field<ListGraphType<EnrollmentType>>("Enrollments", "Enrollment details for the student",
            //  resolve: context => enrollmentRepository.GetByStudentId(context.Source.ID)
            //  );
            //Field(x => x.Enrollments)
            //    .Name("EnrollmentsTest")
            //    .Description("The enrollment date of the student")
            //    .Resolve(context => enrollmentRepository.GetByStudentId(context.Source.ID))
            //    });

        }
    }
}
