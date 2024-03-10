using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0903_list
{
    public partial class Form3 : Form
    {
        private Group selectedGroup;
        public int SelectedStudentIndex { get; private set; }


        public Form3(Group group)
        {
            InitializeComponent();
            selectedGroup = group;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Додаємо нового студента при натисканні кнопки "Додати"
            string studentSurname = textBox1.Text;
            int studentGrade;

            if (!string.IsNullOrEmpty(studentSurname) && int.TryParse(textBox2.Text, out studentGrade))
            {
                Student newStudent = new Student(studentSurname, studentGrade);
                selectedGroup.AddStudent(newStudent); // Add the new student to the selected group
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Будь ласка, заповніть всі поля коректно.");
            }
        }


        public Student GetStudent()
        {
            // Повертаємо нового студента, якого ввів користувач
            string studentSurname = textBox1.Text;
            int studentGrade = int.Parse(textBox2.Text);
            return new Student(studentSurname, studentGrade);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}