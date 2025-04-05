using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EducationalSeminars_4team_Novikova_Nastya
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();

        }

        private void btnLogInAsACustomer_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
            form2.btnAddEvent.Enabled = false;
            form2.dataGridView1.Enabled = false;
            form2.dataGridView1.ReadOnly = true;
            form2.dataGridView1.AllowUserToAddRows = false;
            form2.dataGridView1.AllowUserToDeleteRows = false;

        }
    }
}
