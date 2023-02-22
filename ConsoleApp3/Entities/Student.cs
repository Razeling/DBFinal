using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBFinal
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<Lecture> Lectures { get; set; }


        [ForeignKey("Department")]
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }
        public Student(string name, string surname)
        {
            Id = Guid.NewGuid();
            Name = name;
            Surname = surname;
            Lectures = new List<Lecture>();
        }
    }
}
