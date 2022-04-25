using StudentApp.Api.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentApp.Api.Repository
{
    public interface IStudentRepository
    {
        Task<Student> Save(Student student);

        Task<Student> Update(Student student);

        Task<IEnumerable<Student>> GetStudents();

        Task<bool> Delete(int id);

        Task<Student> GetStudentById(int id);
    }
}