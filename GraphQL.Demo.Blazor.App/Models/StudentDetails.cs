using System;
using System.Collections.Generic;

namespace GraphQL.Demo.Blazor.App.Models
{
    public class StudentDetails
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public List<EnrollmentModel> Enrollments { get; set; }
    }       
}
