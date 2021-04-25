using System.ComponentModel;

namespace GraphQL.Demo.API.Models
{
    public enum Grade
    {
        [Description("Grade A")]
        A=0,

        [Description("Grade B")]
        B=1,

        [Description("Grade C")]
        C=2,

        [Description("Grade D")]
        D=3,

        [Description("Grade F")]
        F=4
    }

    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public Grade? Grade { get; set; }
        public Course Course { get; set; }
        public Student Student { get; set; }
    }
}