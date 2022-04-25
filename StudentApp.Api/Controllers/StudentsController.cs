using Microsoft.AspNetCore.Mvc;
using StudentApp.Api.Model;
using StudentApp.Api.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentsController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await _studentRepository.GetStudents();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _studentRepository.GetStudentById(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        [HttpPut]
        public async Task<IActionResult> PutStudent(Student student)
        {
            try
            {
                await _studentRepository.Update(student);

                return CreatedAtAction("GetStudent", new { id = student.Id }, student);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            try
            {
                await _studentRepository.Save(student);

                return CreatedAtAction("GetStudent", new { id = student.Id }, student);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteStudent(int id)
        {
            return await _studentRepository.Delete(id);
        }
    }
}