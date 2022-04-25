using Microsoft.EntityFrameworkCore;
using StudentApp.Api.Context;
using StudentApp.Api.Exceptions;
using StudentApp.Api.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApp.Api.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DataContext _context;

        public StudentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Student> Save(Student student)
        {
            if (student == null)
                throw new StudentException("Student cannot be null");

            if (CheckIfStudentExist(student))
                throw new StudentException("Student Already exist");

            _context.Add(student);
            await _context.SaveChangesAsync();

            return student;
        }

        public async Task<Student> Update(Student student)
        {
            if (student == null)
                throw new StudentException("Student cannot be null");

            if (CheckIfStudentExist(student))
                throw new StudentException("Student Already exist");

            _context.Students.Update(student);

            await _context.SaveChangesAsync();

            return student;
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var studentRecord = await GetStudentById(id);

            if (studentRecord != null)
            {
                _context.Students.Remove(studentRecord);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<Student> GetStudentById(int id)
        {
            return await _context.Students.FirstOrDefaultAsync(x => x.Id == id);
        }

        private bool CheckIfStudentExist(Student student)
        {
            return _context.Students.Any(x => x.IDorPassport == student.IDorPassport && x.Id != student.Id);
        }
    }
}