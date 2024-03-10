using _0903_list;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Group
{
    public string Name { get; set; }
    public string Specialty { get; set; }
    public List<Student> Students { get; set; }

    public Group(string name, string specialty)
    {
        Name = name;
        Specialty = specialty;
        Students = new List<Student>();
    }

    public int NumberOfStudents
    {
        get { return Students.Count; }
    }

    public string DisplayGroupInfo()
    {
        string studentNames = string.Join(", ", Students.Select(student => student.Surname));
        return $"{Name} {Specialty} [{studentNames}]";
    }
    public Student GetStudentByIndex(int index)
    {
        if (index >= 0 && index < Students.Count)
        {
            return Students[index];
        }
        return null; // Handle invalid index
    }
    public void AddStudent(Student student)
    {
        Students.Add(student);
    }
}