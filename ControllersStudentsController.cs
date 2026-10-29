using Microsoft.AspNetCore.Mvc;
using StudentManagementAPI.Data;
using StudentManagementAPI.Models;

namespace StudentManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly StudentRepository repository;

        public StudentsController(StudentRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/students
        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetAllStudents()
        {
            var students = repository.GetAll();

            return Ok(students);
        }

        // GET: api/students/1
        [HttpGet("{id:int}")]
        public ActionResult<Student> GetStudentById(int id)
        {
            var student = repository.GetById(id);

            if (student == null)
            {
                return NotFound(new
                {
                    message = $"Student with ID {id} not found."
                });
            }

            return Ok(student);
        }

        // POST: api/students
        [HttpPost]
        public ActionResult<Student> CreateStudent(Student student)
        {
            if (student.Age < 1 || student.Age > 100)
            {
                return BadRequest(new
                {
                    message = "Age must be between 1 and 100."
                });
            }

            var newStudent = repository.Add(student);

            return CreatedAtAction(
                nameof(GetStudentById),
                new
                {
                    id = newStudent.Id
                },
                newStudent
            );
        }

        // PUT: api/students/1
        [HttpPut("{id:int}")]
        public IActionResult UpdateStudent(
            int id,
            Student student)
        {
            if (student.Age < 1 || student.Age > 100)
            {
                return BadRequest(new
                {
                    message = "Age must be between 1 and 100."
                });
            }

            bool updated = repository.Update(id, student);

            if (!updated)
            {
                return NotFound(new
                {
                    message = $"Student with ID {id} not found."
                });
            }

            return Ok(new
            {
                message = "Student updated successfully."
            });
        }

        // DELETE: api/students/1
        [HttpDelete("{id:int}")]
        public IActionResult DeleteStudent(int id)
        {
            bool deleted = repository.Delete(id);

            if (!deleted)
            {
                return NotFound(new
                {
                    message = $"Student with ID {id} not found."
                });
            }

            return Ok(new
            {
                message = "Student deleted successfully."
            });
        }
    }
}