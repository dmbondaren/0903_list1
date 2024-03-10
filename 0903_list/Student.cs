using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0903_list
{
    public class Student
    {
        public string Surname { get; set; }
        public int Grade { get; set; }

        public Student(string surname, int grade)
        {
            Surname = surname;
            Grade = grade;
        }
        public override string ToString()
        {
            // Customize this method to display all the student details
            return $"Surname: {Surname}, Grade: {Grade}";
        }
    }
}
