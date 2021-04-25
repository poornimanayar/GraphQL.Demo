using System.Collections.Generic;

namespace GraphQL.Demo.Blazor.App.Models
{
    public class EnrollmentModel
    {
        public int Id { get; set; }

        public string Grade { get; set; }

        public CourseModel Course { get; set; }
    }
}
