using System;
using System.Windows.Forms;

namespace EducationalSeminars_4team_Novikova_Nastya
{
    public partial class AddEvent : Form
    {
       
        public AddEvent()
        {
            InitializeComponent();
            SetupCategories();
           
         }
        private void SetupCategories()
        {
            comboBoxCategory.Items.AddRange(new[]
            {
            "Творчество",
            "Гуманитарные науки",
            "Естественные науки",
            "Технические науки"
            });
            comboBoxCategory.SelectedIndex = 0;
        }

        /// <summary>
        /// Обработчик нажатия кнопки сохранить событие
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveEvent_Click(object sender, EventArgs e)
        {
            if (DateTime.TryParse(dateTimePicker1.Text, out DateTime eventDate))
            {
                MessageBox.Show("Некорректная дата");
            }

            if (string.IsNullOrWhiteSpace(txtBoxWriteTitle.Text))
            {
                MessageBox.Show("Название события обязательно!");
                return;
            }

            try
            {
                var newEvent = new Event
                {
                    Title = txtBoxWriteTitle.Text,
                    Date = eventDate,
                    Time = txtBoxWriteTime.Text,
                    Category = comboBoxCategory.Text,
                    Description = txtBoxWriteDiscription.Text,
                    Participants = txtBoxWriteParticipants.Text,
                };

                using (var db = new EventDatabase())
                {
                    db.Events.Add(newEvent);
                    db.SaveChanges();
                }
                MessageBox.Show("Событие успешно сохранено!");
                this.Close();
            }
            catch (Exception ex)
            {
                 MessageBox.Show($"Ошибка при сохранении: {ex.Message}");
            }
        }
        //надо удалить эту строчку
    }
}
