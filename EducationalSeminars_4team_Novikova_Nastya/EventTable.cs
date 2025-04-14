﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EducationalSeminars_4team_Novikova_Nastya
{
    public partial class EventTable : Form
    {

        private readonly bool isCustomer;
        private readonly EventDatabase db;
        
        public EventTable(bool isCustomer)
        {
            InitializeComponent();
            this.isCustomer = isCustomer;
            db = new EventDatabase();
            InitializeDatabase();
            SetUpForm();
            var categories = new List<string>
            {
                "Творчество",
                "Гуманитарные науки",
                "Естественные науки",
                "Технические науки"
            };
            comboBox1.DataSource = categories;
            comboBox1.SelectedIndex = 0;
        }

        private void InitializeDatabase()
        {
            db.Database.EnsureCreated();
            LoadEvents();
        }
        private void SetUpForm()
        {
            if (isCustomer)
            {
                btnAddEvent.Enabled = false;
                btnEditEvent.Enabled = false;
                btnDeleteEvent.Enabled = false;
                btnSaveEvent.Enabled = false;
            }
        }
        public void LoadEvents()
        {
            using (var db = new EventDatabase())
            {
                dataGridView1.DataSource = db.Events.ToList();
            }
        }

        

        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 1) // 1 - потому что есть пустая новая строка
            {
                MessageBox.Show("Нет данных для фильтрации");
                return;
            }

            string selectedCategory = comboBox1.SelectedItem != null ? comboBox1.SelectedItem.ToString() : null;
            DateTime selectedDate = dateTimePicker1.Value.Date;
            dataGridView1.SuspendLayout();

            try
            {

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewRow row = dataGridView1.Rows[i];

                    if (row.IsNewRow) continue;

                    object categoryValueObj = row.Cells["Category"].Value;
                    object dateValueObj = row.Cells["Date"].Value;

                    if (categoryValueObj == null || dateValueObj == null)
                    {
                        row.Visible = false;
                        continue;
                    }

                    string categoryValue = categoryValueObj.ToString();
                    DateTime dateValue;

                    try
                    {
                        dateValue = Convert.ToDateTime(dateValueObj).Date;
                    }
                    catch
                    {
                        row.Visible = false;
                        continue;
                    }

                    bool categoryMatches = true;
                    if (!string.IsNullOrEmpty(selectedCategory))
                    {
                        categoryMatches = string.Equals(categoryValue, selectedCategory, StringComparison.OrdinalIgnoreCase);
                    }

                    bool dateMatches = (dateValue == selectedDate);
                    row.Visible = categoryMatches && dateMatches;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при фильтрации: " + ex.Message);
            }
            finally
            {
                dataGridView1.ResumeLayout();
            }
        }

        
        private void btnAddEvent_Click(object sender, EventArgs e)
        {
            AddEvent addEvent = new AddEvent();
            addEvent.Show();
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                if (dataGridView1.Rows[rowIndex].IsNewRow) return;
                var selectedId = (int)dataGridView1.Rows[rowIndex].Cells["ID"].Value;

                if (MessageBox.Show("Удалить выбранное событие?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (var db = new EventDatabase())
                    {
                        var eventToDelete = db.Events.Find(selectedId);
                        if (eventToDelete != null)
                        {
                            db.Events.Remove(eventToDelete);
                            db.SaveChanges();
                            LoadEvents();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Выберите событие для удаления");
                }
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                if (dataGridView1.Rows[rowIndex].IsNewRow) return;

                var selectedId = (int)dataGridView1.Rows[rowIndex].Cells["ID"].Value;

                using (var db = new EventDatabase())
                {
                    var eventToEdit = db.Events.Find(selectedId);
                    if (eventToEdit != null)
                    {
                       
                        AddEvent editForm = new AddEvent();
                        if (editForm.ShowDialog() == DialogResult.OK)
                        {
                            
                            db.SaveChanges();
                            LoadEvents();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите событие для редактирования");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView1.SelectedRows[0];

                var eventToSave = new Event
                {
                    ID = Convert.ToInt32(selectedRow.Cells["ID"].Value),
                    Title = selectedRow.Cells["Title"].Value.ToString(),
                    Date = Convert.ToDateTime(selectedRow.Cells["Date"].Value)
                };

                
                if (string.IsNullOrWhiteSpace(eventToSave.Title))
                {
                    MessageBox.Show("Название события не может быть пустым.");
                    return;
                }

                try
                {
                    SaveEvent(eventToSave);
                    LoadEvents(); 
                    MessageBox.Show("Событие успешно сохранено.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении события: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите событие для сохранения.");
            }

        }
        public void SaveEvent(Event eventToSave)
        {
            using (var db = new EventDatabase())
            {
                var existingEvent = db.Events.Find(eventToSave.ID);
                if (existingEvent != null)
                {
                    existingEvent.Title = eventToSave.Title;
                    existingEvent.Date = eventToSave.Date;

                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("Событие не найдено.");
                }
            }
        }


    }
}
