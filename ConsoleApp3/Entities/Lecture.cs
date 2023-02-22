using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBFinal
{
    public class Lecture
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public List<Department> Departments { get; set; }
        public List<Student> Students { get; set; }

        public Lecture(string subject)
        {
            Id = Guid.NewGuid();
            Subject = subject;
            Departments = new List<Department>();
            Students = new List<Student>();
        }
    }
}
