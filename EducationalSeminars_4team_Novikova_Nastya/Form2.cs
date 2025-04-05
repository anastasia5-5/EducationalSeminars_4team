using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EducationalSeminars_4team_Novikova_Nastya
{
    public partial class Form2 : Form
    {
        
        private DatabaseContext db;
       
        
        public Form2()
        {
            InitializeComponent();
            LoadEvents();
            
        }

        public void LoadEvents()
        {
            List<Event> events = new List<Event>();
        }

        

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnFilter_Click(object sender, EventArgs e)
        {

        }

        private void btnAddEvent_Click(object sender, EventArgs e)
        {
            List<Event> events = new List<Event>();
            string title = txtTitle.Text;
            string eventTitle = txtTitle.Text;
            if (!string.IsNullOrWhiteSpace(eventTitle))
            {
                DateTime eventDate = dateTimePicker1.Value;
                events.Add(new Event { Title = eventTitle, Date = eventDate });
                MessageBox.Show($"Событие '{eventTitle}' добавлено");
                txtTitle.Clear();
            }
        }

       
        private void btnDelete_Click(object sender, EventArgs e)
        {
            
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
