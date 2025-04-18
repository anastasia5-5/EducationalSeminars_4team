using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EducationalSeminars_4team_Novikova_Nastya
{
    /// <summary>
    /// Класс формы добавления события
    /// </summary>
    public partial class AddEvent : Form
    {
        /// <summary>
        /// Событие, возникающее при успешном сохранении нового события
        /// </summary>
        public event Action<Event> EventSaved;
        public AddEvent()
        {
            InitializeComponent();
            SetupCategories();
           
         }
        private void SetupCategories()
        {
            var categories = new List<string>
            {
                "Творчество",
                "Гуманитарные науки",
                "Естественные науки",
                "Технические науки"
            };
            comboBoxCategory.DataSource = categories;
            comboBoxCategory.SelectedIndex = 0;
        }
        

        /// <summary>
        /// Обработчик нажатия кнопки сохранить событие
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveEvent_Click(object sender, EventArgs e)
        {
            if (!DateTime.TryParse(dateTimePicker1.Text, out var eventDate))
            {
                MessageBox.Show("Некорректная дата");
            }

            if (string.IsNullOrWhiteSpace(txtBoxWriteTitle.Text))
            {
                MessageBox.Show("Название события обязательно!");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxWriteTime.Text) || !IsValidTimeFormat(textBoxWriteTime.Text))
            {
                MessageBox.Show("Введите время в формате HH:MM (например, 09:00 )");
                return;
            }

            try
            {
                var newEvent = new Event
                {
                    Title = txtBoxWriteTitle.Text,
                    Date = eventDate,
                    Time = textBoxWriteTime.Text,
                    Category = comboBoxCategory.Text,
                    Description = txtBoxWriteDiscription.Text,
                    Participants = txtBoxWriteParticipants.Text,
                };

                using (var db = new EventDatabase())
                {  
                    db.Events.Add(newEvent);
                    db.SaveChanges();
                    EventSaved?.Invoke(newEvent);
                }
                MessageBox.Show("Событие успешно сохранено!");
                this.Close();
            }
            catch 
            {
                 MessageBox.Show($"Ошибка при сохранении");
            }
        }
        private bool IsValidTimeFormat(string time)
        {
            return Regex.IsMatch(time, @"^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$");
        }

    }
}
