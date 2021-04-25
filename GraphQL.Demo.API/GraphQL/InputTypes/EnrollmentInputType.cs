using GraphQL.Demo.API.GraphQL.Types;
using GraphQL.Demo.API.Models;
using GraphQL.Types;
using System;

namespace GraphQL.Demo.API.GraphQL
{
    public class EnrollmentInputType : InputObjectGraphType<Enrollment>
    {
        public EnrollmentInputType()
        {
            Name = "EnrollmentInput";
            Description = "The enrollment input type";
            Field(x => x.StudentID, nullable: false).Description("The student id");
            Field(x => x.CourseID, nullable: false).Description("The course id");
            Field<EnrollmentGradeEnum>().Name("Grade").Description("Grade");

        }

        
    }
}
