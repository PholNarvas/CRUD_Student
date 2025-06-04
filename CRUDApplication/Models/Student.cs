
using System.ComponentModel.DataAnnotations.Schema;

public class Student
    {
        public int StudentsID { get; set; }
        public string FName { get; set; }
        public string? MName { get; set; }
        public string LName { get; set; }
        public int DepartmentID { get; set; }
        public int ClassID { get; set; }
        public string Email { get; set; }

        public string? DepartmentName { get; set; }
        public string? SectionName { get; set; }
    }

