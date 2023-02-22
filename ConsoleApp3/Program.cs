
using System;
using Microsoft.EntityFrameworkCore;

namespace DBFinal

{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var context = new UniversityContext())

            {
                DbInitialData(context);
                var departmentId = context.Departments.First().Id;
                ShowDepartmentStudents(context, departmentId); //6 funkcionalumas -  per Department ID atvaizduojami esami studentai

                ShowLectures(context, departmentId); //7 funkcionalumas - per Department ID atvaizduojamos paskaitos

                var studentId = context.Students.First().Id;
                ShowStudentLectures(context, studentId); //8 funkcionalumas - atvaizduoti visas studento paskaitas 

                var lectureId = context.Lectures.Skip(1).First().Id;
                CreateLectureToExistingDepartment(context, "Information Technology", departmentId);

                AddStudentToExistingDepartment(context, "Marius", "Barzda", departmentId);

                AddStudentToExistingLectures(context, studentId, lectureId);

                CreateDepartment(context, "Engineering");

                var department2Id = context.Departments.Skip(1).First().Id;
                ChangeStudentDepartment(context, studentId, department2Id);

                AddLectureToExistingDepartment(context, lectureId, department2Id);

            }
        }


        private static void DbInitialData(UniversityContext context)
        {
            var universityDepartment = context.Departments.FirstOrDefault();
            if (universityDepartment == null)
            {
                var uniDepartment = new Department("Law");
                var uniLecture = new Lecture("Introduction to Law");
                var uniLecture2 = new Lecture("Western Law");

                uniDepartment.Lectures.Add(uniLecture);
                uniDepartment.Lectures.Add(uniLecture2);

                var student = new Student("Neivydas", "Apulskis");
                var student2 = new Student("Vilius", "Mockus");

                uniDepartment.Students.Add(student);
                uniDepartment.Students.Add(student2);
                uniLecture.Students.Add(student);
                uniLecture2.Students.Add(student2);
                context.Departments.Add(uniDepartment);
                context.SaveChanges();
            }
        }

        //1 Sukurti departamenta ir prideti studentus
        private static void CreateDepartment(UniversityContext context, string title)
        {
            var addedDepartment = new Department(title);
            context.Departments.Add(addedDepartment);
            context.SaveChanges();

        }

        private static void AddLectureToExistingDepartment(UniversityContext context, Guid lectureId, Guid departmentId)
        {
            var addedExistingLectureToAnExistingDepartment = context.Lectures.FirstOrDefault(x => x.Id == lectureId);
            if (addedExistingLectureToAnExistingDepartment == null)
            {
                Console.WriteLine("This lecture was not found");
                return;
            }

            var existingDepartmentAddInto = context.Departments.FirstOrDefault(x => x.Id == departmentId);
            if (existingDepartmentAddInto == null)
            {
                Console.WriteLine("Department does not exist.");
                return;
            }

            existingDepartmentAddInto.Lectures.Add(addedExistingLectureToAnExistingDepartment);
            context.SaveChanges();

        }


        // 2/4 Pridėti studentus/paskaitas į jau egzistuojantį departamentą. / Sukurti studentą, jį pridėti prie egzistuojančio departamento ir priskirti jam egzistuojančias paskaitas.

        private static void AddStudentToExistingDepartment(UniversityContext context, string name, string surname, Guid departmentId)
        {
            var addedStudent = new Student(name, surname);
            context.Students.Add(addedStudent);
            var existingDepartment = context.Departments.FirstOrDefault(x => x.Id == departmentId);

            if (existingDepartment == null)
            {
                Console.WriteLine("Department does not exist.");
                return;
            }

            existingDepartment.Students.Add(addedStudent);
            context.SaveChanges();

        }

        private static void AddStudentToExistingLectures(UniversityContext context, Guid studentId, Guid lectureId)
        {
            var existingStudent = context.Students.FirstOrDefault(x => x.Id == studentId);

            if (existingStudent == null)
            {
                Console.WriteLine("Student doest not exist.");
            }

            var existingLecture = context.Lectures.FirstOrDefault(x => x.Id == lectureId);
            if (existingLecture == null)
            {
                Console.WriteLine("Lecture does not exist.");
            }
            existingLecture.Students.Add(existingStudent);
            context.SaveChanges();
        }


        //3. Sukurti paskaitą ir ją priskirti prie departamento.
        private static void CreateLectureToExistingDepartment(UniversityContext context, string subject, Guid departmentId) // Metodas pridėti naują paskaitą į esantį departamentą
        {
            var addedLecture = new Lecture(subject);
            context.Lectures.Add(addedLecture);


            var existingDepartment = context.Departments.FirstOrDefault(x => x.Id == departmentId);
            if (existingDepartment == null)
            {
                Console.WriteLine("Department does not exist.");
                return;
            }

            existingDepartment.Lectures.Add(addedLecture);
            context.SaveChanges();
        }

        // 5. Perkelti studentą į kitą departamentą(bonus points jei pakeičiamos ir jo paskaitos).
        private static void ChangeStudentDepartment(UniversityContext context, Guid studentId, Guid departmentIdToAdd)
        {
            var exchangeStudent = context.Students.FirstOrDefault(x => x.Id == studentId);
            if (exchangeStudent == null)
            {
                Console.WriteLine("Student does not exist.");
                return;
            }

            exchangeStudent.DepartmentId = departmentIdToAdd;
            context.SaveChanges();
        }

        // 6. Atvaizduoti visus departamento studentus.
        private static void ShowDepartmentStudents(UniversityContext context, Guid departmentId) //Parodomi departamento studentai juos radus per departamento ID
        {
            var department = context.Departments.Include("Students").FirstOrDefault(x => x.Id == departmentId);
            if (department == null)
            {
                Console.WriteLine("Department does not exist.");
                return;
            }

            var students = department.Students;
            foreach (var student in students)
            {
                Console.WriteLine("Student " + student.Name + " " + student.Surname);
            }
        }

        // 7. Atvaizduoti visas departamento paskaitas.
        private static void ShowLectures(UniversityContext context, Guid departmentId) //Parodomos departamento paskaitos radus jas per departamento ID
        {
            var department = context.Departments.Include("Lectures").FirstOrDefault(x => x.Id == departmentId);
            if (department == null)
            {
                Console.WriteLine("Department does not exist.");
                return;
            }

            var lectures = department.Lectures;
            foreach (var lecture in lectures)
            {
                Console.WriteLine("Lecture " + lecture.Subject);
            }
        }
        // 8. Atvaizduoti visas paskaitas pagal studentą.
        private static void ShowStudentLectures(UniversityContext context, Guid studentId) // Metodas studento paskaitoms parodyti
        {
            var student = context.Students.Include("Lectures").FirstOrDefault(x => x.Id == studentId);
            if (student == null)
            {
                Console.WriteLine("Student does not exist.");
                return;
            }

            var studentLectures = student.Lectures;
            foreach (var studentLecture in studentLectures)
            {
                Console.WriteLine(value: "Student " + student.Name + " " + student.Surname + " belongs to these lectures: ");
                foreach (var lecture in student.Lectures)
                {
                    Console.WriteLine(lecture.Subject);
                };
            }
        }
    }

}