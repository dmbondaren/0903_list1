using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0903_list
{
    public partial class Form1 : Form
    {
        private List<Group> groups;

        public Form1()
        {
            InitializeComponent();
            groups = new List<Group>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Відкриваємо Form2 для додавання нової групи
            Form2 form2 = new Form2();
            DialogResult result = form2.ShowDialog();

            if (result == DialogResult.OK)
            {
                Group newGroup = form2.GetGroup();
                groups.Add(newGroup);
                listBox1.Items.Add(newGroup.DisplayGroupInfo());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int selectedIndex = listBox1.SelectedIndex;

            if (selectedIndex != -1)
            {
                Group selectedGroup = groups[selectedIndex];

                // Open Form3 for adding a student to the selected group
                Form3 form3 = new Form3(selectedGroup);
                DialogResult result = form3.ShowDialog();

                if (result == DialogResult.OK)
                {
                    // Refresh the ListBox with the updated group information
                    listBox1.Items[selectedIndex] = selectedGroup.DisplayGroupInfo();
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть групу.");
            }
        }



        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = listBox1.SelectedIndex;

            if (selectedIndex != -1)
            {
                Group selectedGroup = groups[selectedIndex];

                // Unsubscribe temporarily
                listBox1.SelectedIndexChanged -= listBox1_SelectedIndexChanged;

                // Update the ListBox item
                listBox1.Items[selectedIndex] = selectedGroup.DisplayGroupInfo();

                // Resubscribe to the event
                listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int selectedIndex = listBox1.SelectedIndex;

            if (selectedIndex != -1)
            {
                Group selectedGroup = groups[selectedIndex];

                // Check if the input index is valid
                if (int.TryParse(textBox1.Text, out int studentIndex) && studentIndex >= 0 && studentIndex < selectedGroup.NumberOfStudents)
                {
                    Student selectedStudent = selectedGroup.GetStudentByIndex(studentIndex);

                    // Display student details in a MessageBox (you can customize this part)
                    MessageBox.Show($"Student Details:\n{selectedStudent}");
                }
                else
                {
                    MessageBox.Show("Invalid student index. Please enter a valid index.");
                }
            }
            else
            {
                MessageBox.Show("Please select a group.");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int selectedIndex = listBox1.SelectedIndex;

            if (selectedIndex != -1)
            {
                Group selectedGroup = groups[selectedIndex];

                // Open Form4 for editing the selected group
                Form4 form4 = new Form4(selectedGroup);
                DialogResult result = form4.ShowDialog();

                if (result == DialogResult.OK)
                {
                    // Refresh the ListBox with the updated group information
                    listBox1.Items[selectedIndex] = selectedGroup.DisplayGroupInfo();
                }
            }
            else
            {
                MessageBox.Show("Please select a group.");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int selectedIndex = listBox1.SelectedIndex;

            if (selectedIndex != -1)
            {
                Group selectedGroup = groups[selectedIndex];

                // Open Form4 for editing the selected student
                Form4 form4 = new Form4(selectedGroup.GetStudentByIndex(selectedIndex)); // Use selectedIndex here
                DialogResult result = form4.ShowDialog();

                if (result == DialogResult.OK)
                {
                    // Refresh the ListBox with the updated group information
                    listBox1.Items[selectedIndex] = selectedGroup.DisplayGroupInfo();
                }
            }
            else
            {
                MessageBox.Show("Please select a group.");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Get input values from TextBoxes
            string studentSurname = textBox3.Text;
            int currentGroupIndex, targetGroupIndex;

            if (!int.TryParse(textBox2.Text, out currentGroupIndex) || currentGroupIndex < 0 || currentGroupIndex >= groups.Count)
            {
                MessageBox.Show("Invalid current group index. Please enter a valid index.");
                return;
            }

            if (!int.TryParse(textBox4.Text, out targetGroupIndex) || targetGroupIndex < 0 || targetGroupIndex >= groups.Count)
            {
                MessageBox.Show("Invalid target group index. Please enter a valid index.");
                return;
            }

            // Find the current and target groups
            Group currentGroup = groups[currentGroupIndex];
            Group targetGroup = groups[targetGroupIndex];

            // Find the student by surname in the current group
            Student studentToMove = currentGroup.Students.FirstOrDefault(student => student.Surname == studentSurname);

            if (studentToMove != null)
            {
                // Remove the student from the current group
                currentGroup.Students.Remove(studentToMove);

                // Add the student to the target group
                targetGroup.AddStudent(studentToMove);

                // Refresh the ListBox with the updated group information
                listBox1.Items[currentGroupIndex] = currentGroup.DisplayGroupInfo();
                listBox1.Items[targetGroupIndex] = targetGroup.DisplayGroupInfo();
            }
            else
            {
                MessageBox.Show($"Student with surname '{studentSurname}' not found in the current group.");
            }
        }




        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Search for students with a specified grade
            if (int.TryParse(textBox5.Text, out int searchGrade))
            {
                List<Student> matchingStudents = groups
                    .SelectMany(group => group.Students)
                    .Where(student => student.Grade == searchGrade)
                    .ToList();

                DisplaySearchResults(matchingStudents);
            }
            else
            {
                MessageBox.Show("Invalid grade. Please enter a valid numeric grade.");
            }
        }

        // Helper method to display search results
        private void DisplaySearchResults(List<Student> matchingStudents)
        {
            if (matchingStudents.Count > 0)
            {
                StringBuilder resultMessage = new StringBuilder("Matching Students:\n");

                foreach (Student student in matchingStudents)
                {
                    resultMessage.AppendLine($"{student.Surname} - Group: {GetStudentGroup(student)}");
                }

                MessageBox.Show(resultMessage.ToString());
            }
            else
            {
                MessageBox.Show("No students found matching the specified criteria.");
            }
        }

        // Helper method to get the group of a student
        private string GetStudentGroup(Student student)
        {
            foreach (Group group in groups)
            {
                if (group.Students.Contains(student))
                {
                    return $"{group.Name} {group.Specialty}";
                }
            }

            return "Group not found";
        }


        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            string searchSurname = textBox6.Text;

            if (!string.IsNullOrEmpty(searchSurname))
            {
                List<Student> matchingStudents = groups
                    .SelectMany(group => group.Students)
                    .Where(student => student.Surname.Equals(searchSurname, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                DisplaySearchResults(matchingStudents);
            }
            else
            {
                MessageBox.Show("Please enter a valid surname for the search.");
            }
        }


        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
    }
}