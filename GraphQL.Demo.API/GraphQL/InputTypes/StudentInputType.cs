using GraphQL.Demo.API.Models;
using GraphQL.Types;
using System;

namespace GraphQL.Demo.API.GraphQL
{
    public class StudentInputType : InputObjectGraphType<Student>
    {
        public StudentInputType()
        {
            Name = "StudentInput";
            Description = "The student input type";
            Field(x => x.FirstMidName, nullable:false).Name("firstName").Description("First name of the student");
            Field(x => x.LastName, nullable: false).Description("Last name of the student");
            Field(x => x.EmailAddress, nullable: false).Description("Email address of the student");
            Field(x => x.EnrollmentDate, nullable: false).Description("Enrollment date of the student");

        }

        
    }
}
