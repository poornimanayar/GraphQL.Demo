using GraphQL.Demo.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.Demo.API.Repositories
{
    public class EnrollmentRepository
    {
        private readonly SchoolContext schoolContext;

        public EnrollmentRepository(SchoolContext schoolContext)
        {
            this.schoolContext = schoolContext;
        }

        public Task<List<Enrollment>> GetByStudentId(int id)
        {
            return schoolContext.Enrollments.Where(e => e.StudentID == id).ToListAsync();
        }

        public async Task<List<Enrollment>> GetAllEnrollments()
        {
            return await schoolContext.Enrollments.ToListAsync();
        }

        public async Task<List<Enrollment>> GetAllEnrollment()
        {
            return await schoolContext.Enrollments.ToListAsync();
        }



        //ILookup can have multiple values against a single key, for every student we can have multiple enrollments
        public async Task<ILookup<int, Enrollment>> GetEnrollmentForStudents(IEnumerable<int> studentIds)
        {
            var enrollments = await schoolContext.Enrollments.Where(s => studentIds.Contains(s.StudentID)).ToListAsync();
            return enrollments.ToLookup(e => e.StudentID);

        }

        public async Task<ILookup<int, Enrollment>> GetEnrollmentForCourses(IEnumerable<int> courseIds)
        {
            var enrollments = await schoolContext.Enrollments.Where(c => courseIds.Contains(c.CourseID)).ToListAsync();
            return enrollments.ToLookup(e => e.CourseID);
        }

        public async Task<Enrollment> CreateEnrollment(Enrollment enrollment)
        {
            schoolContext.Enrollments.Add(enrollment);
            await schoolContext.SaveChangesAsync();
            return enrollment;
        }
    }
}
