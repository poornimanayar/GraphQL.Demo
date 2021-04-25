using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.Demo.Blazor.App.FormModels
{
    public class AddStudentModel
    {
       public string FirstName { get; set; }
        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public DateTime EnrollmentDate { get; set; }
    }
}
