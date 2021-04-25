using GraphQL.Demo.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.Demo.API.Repositories
{
    public class CoursesRepository
    {
        private readonly SchoolContext schoolContext;

        public CoursesRepository(SchoolContext schoolContext)
        {
            this.schoolContext = schoolContext;
        }

        public async Task<List<Course>> GetAll()
        {
            return await schoolContext.Courses.Include(c => c.Enrollments).ToListAsync();
        }

        public async Task<Course> GetById(int id)
        {
            return await schoolContext.Courses.FindAsync(id);
        }                 

        public async Task<IDictionary<int, Course>> GetCourses(IEnumerable<int> courseIds)
        {
            return await schoolContext.Courses.Where(s => courseIds.Contains(s.CourseID)).AsNoTracking().ToDictionaryAsync(c => c.CourseID);
        }

        public async Task<Course> CreateCourse(Course course)
        {
            schoolContext.Courses.Add(course);
            await schoolContext.SaveChangesAsync();
            return course;
        }
    }
}
