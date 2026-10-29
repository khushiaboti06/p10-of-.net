using StudentManagementAPI.Models;

namespace StudentManagementAPI.Data
{
    public class StudentRepository
    {
        private readonly List<Student> students = new List<Student>
        {
            new Student
            {
                Id = 1,
                Name = "Khushi Aboti",
                Email = "khushi@example.com",
                Course = "B.Tech",
                Age = 21
            },

            new Student
            {
                Id = 2,
                Name = "Jaimini Patel",
                Email = "jaimini@example.com",
                Course = "BCA",
                Age = 20
            }
        };

        private int nextId = 3;

        // Get all students
        public List<Student> GetAll()
        {
            return students.ToList();
        }

        // Get student by ID
        public Student? GetById(int id)
        {
            return students.FirstOrDefault(s => s.Id == id);
        }

        // Add student
        public Student Add(Student student)
        {
            student.Id = nextId++;

            students.Add(student);

            return student;
        }

        // Update student
        public bool Update(int id, Student updatedStudent)
        {
            var student = GetById(id);

            if (student == null)
            {
                return false;
            }

            student.Name = updatedStudent.Name;
            student.Email = updatedStudent.Email;
            student.Course = updatedStudent.Course;
            student.Age = updatedStudent.Age;

            return true;
        }

        // Delete student
        public bool Delete(int id)
        {
            var student = GetById(id);

            if (student == null)
            {
                return false;
            }

            students.Remove(student);

            return true;
        }
    }
}