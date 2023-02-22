using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBFinal
{
    public class Department
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Lecture> Lectures { get; set; }
        public List<Student> Students { get; set; }


        public Department(string name)
        {
            Name = name;
            Id = Guid.NewGuid();
            Students = new List<Student>();
            Lectures = new List<Lecture>();
        }
    }
}
