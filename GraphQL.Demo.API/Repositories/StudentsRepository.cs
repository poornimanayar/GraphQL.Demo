using GraphQL.Demo.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.Demo.API.Repositories
{
    public class StudentsRepository
    {
        private readonly SchoolContext schoolContext;

        public StudentsRepository(SchoolContext schoolContext)
        {
            this.schoolContext = schoolContext;
        }

        public Task<List<Student>> GetAll()
        {
            return schoolContext.Students.Include(c => c.Enrollments).ToListAsync();
        }

        public async Task<Student> GetById(int id)
        {
            return await schoolContext.Students.FindAsync(id);
        }

        public async Task<IDictionary<int, Student>> GetStudents(IEnumerable<int> studentIds)
        {
           return await schoolContext.Students.Where(s => studentIds.Contains(s.ID)).AsNoTracking().ToDictionaryAsync(s => s.ID);
        }

        public async Task<Student> CreateStudent(Student student)
        {
            schoolContext.Students.Add(student);
            await schoolContext.SaveChangesAsync();
            return student;
        }


    }
}
