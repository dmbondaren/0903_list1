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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Додаємо нову групу при натисканні кнопки "Додати"
            string groupName = textBox1.Text;
            string groupSpecialty = textBox2.Text;

            if (!string.IsNullOrEmpty(groupName) && !string.IsNullOrEmpty(groupSpecialty))
            {
                Group newGroup = new Group(groupName, groupSpecialty);
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Будь ласка, заповніть всі поля.");
            }
        }
        public Group GetGroup()
        {
            // Повертаємо нову групу, яку ввів користувач
            string groupName = textBox1.Text;
            string groupSpecialty = textBox2.Text;
            return new Group(groupName, groupSpecialty);
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
