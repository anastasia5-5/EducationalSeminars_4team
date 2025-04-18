using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Microsoft.IO.RecyclableMemoryStreamManager;

namespace EducationalSeminars_4team_Novikova_Nastya
{
    /// <summary>
    /// Класс для формы с деталями события
    /// </summary>
    public partial class EventInformation : Form
    {
        public EventInformation()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Метод для показа информации в таблице
        /// </summary>
        /// <param name="EventId"></param>
        public void DisplayEventDetails(int EventId)
        {
            using (var db = new EventDatabase())
            {
                var eventDetails = db.Events
                                   .FirstOrDefault(e => e.EventId == EventId);
                if (eventDetails != null)
                {
                    var table = new DataTable();

                    table.Columns.Add("название", typeof(string));
                    table.Columns.Add("дата", typeof(string));
                    table.Columns.Add("время", typeof(string));
                    table.Columns.Add("категория", typeof(string));
                    table.Columns.Add("описание", typeof(string));
                    table.Columns.Add("участники", typeof(string));

                    var row = table.NewRow();
                    row["название"] = eventDetails.Title;
                    row["дата"] = eventDetails.Date.ToShortDateString();
                    row["время"] = eventDetails.Time;
                    row["категория"] = eventDetails.Category; ; 
                    row["описание"] = eventDetails.Description; 
                    row["участники"] = eventDetails.Participants;

                    table.Rows.Add(row);

                    dataGridViewInformation.DataSource = table;
                }

                else
                {
                    dataGridViewInformation.DataSource = null;
                }
            }

        }
    } 
}
