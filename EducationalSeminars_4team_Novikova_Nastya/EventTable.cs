using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EducationalSeminars_4team_Novikova_Nastya
{
    /// <summary>
    /// Основной класс с таблицей
    /// </summary>
    public partial class EventTable : Form
    {

        private readonly bool isCustomer;
        private EventDatabase db;

        public EventTable(bool isCustomer)
        {
            InitializeComponent();
            this.isCustomer = isCustomer;
            db = new EventDatabase();
            InitializeDatabase();
            SetUpForm();
            Categories();
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }
        /// <summary>
        /// Метод bнициализирующий и заполняющий выпадающий список предопределенными категориями событий
        /// </summary>
        public void Categories()
        {
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
        /// <summary>
        /// Метод для настройки формы в зависимости от пользователя
        /// </summary>
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

        /// <summary>
        /// Загрузка событий из БД
        /// </summary>
        public void LoadEvents()
        {
            using (var db = new EventDatabase())
            {
                if (db.Database.CanConnect())
                {
                    dataGridView1.DataSource = db.Events.ToList();
                }
            }
        }


        /// <summary>
        /// Обработчик нажатия кнопки фильтр
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFilter_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count <= 1)
            {
                MessageBox.Show("Нет данных для фильтрации", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedCategory = comboBox1.SelectedItem?.ToString();
            var selectedDate = dateTimePicker1.Value.Date;

            if (string.IsNullOrEmpty(selectedCategory))
            {
                MessageBox.Show("Не выбрана категория для фильтрации", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var matchingRows = new List<DataGridViewRow>();
                var categoryFound = false;
                var dateFound = false;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow)
                    {
                        continue;
                    }
                    var categoryCell = row.Cells["Category"];
                    var dateCell = row.Cells["Date"];

                    if (categoryCell.Value == null || dateCell.Value == null)
                    {
                        continue;
                    }
                    string categoryValue = categoryCell.Value.ToString();
                    if (!DateTime.TryParse(dateCell.Value.ToString(), out DateTime dateValue))
                        continue;

                    var currentCategoryMatch = string.Equals(categoryValue, selectedCategory,StringComparison.OrdinalIgnoreCase);
                    var currentDateMatch = dateValue.Date == selectedDate;

                    categoryFound = categoryFound || currentCategoryMatch;
                    dateFound = dateFound || currentDateMatch;

                    if (currentCategoryMatch && currentDateMatch)
                    {
                        matchingRows.Add(row);
                    }
                }

                if (!categoryFound)
                {
                    MessageBox.Show($"Выбранная категория '{selectedCategory}' не найдена в таблице","Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!dateFound)
                {
                    MessageBox.Show($"Выбранная дата '{selectedDate.ToShortDateString()}' не найдена в таблице","Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (matchingRows.Count == 0)
                {
                    MessageBox.Show("Нет данных, соответствующих выбранным критериям фильтрации","Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                dataGridView1.ClearSelection();
                foreach (var row in matchingRows)
                {
                    row.Selected = true;
                }

                if (matchingRows.Count > 0)
                {
                    dataGridView1.FirstDisplayedScrollingRowIndex = matchingRows[0].Index;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при фильтрации: {ex.Message}", "Ошибка",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        /// <summary>
        /// Обработчик кнопки добавления события
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddEvent_Click(object sender, EventArgs e)
        {
            var addEvent = new AddEvent();
            addEvent.EventSaved += AddEvent_EventSaved;
            addEvent.Show();
        }
        private void AddEvent_EventSaved(Event newEvent)
        {

            var currentDataSource = dataGridView1.DataSource as List<Event>;
            if (currentDataSource != null)
            {
                currentDataSource.Add(newEvent);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = currentDataSource;
            }
            else
            {
                LoadEvents();
            }
        }

        /// <summary>
        /// Обработчик кнопки удаления события
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                var rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                if (dataGridView1.Rows[rowIndex].IsNewRow)
                {
                    return;
                }
                var selectedId = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["EventId"].Value);

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

        /// <summary>
        /// Обработчик кнопки редактирования события
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                var rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                if (dataGridView1.Rows[rowIndex].IsNewRow)
                {
                    return;
                }

                var selectedId = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["EventId"].Value);

                using (var db = new EventDatabase())
                {
                    var eventToEdit = db.Events.Find(selectedId);
                    if (eventToEdit != null)
                    {
                        var eventData = new Event
                        {
                            Title = dataGridView1.Rows[rowIndex].Cells["Title"].Value?.ToString(),
                            Description = dataGridView1.Rows[rowIndex].Cells["Description"].Value?.ToString(),
                            Date = Convert.ToDateTime(dataGridView1.Rows[rowIndex].Cells["Date"].Value).Date,
                            Time = dataGridView1.Rows[rowIndex].Cells["Time"].Value?.ToString() ?? "00:00",
                            Category = dataGridView1.Rows[rowIndex].Cells["Category"].Value?.ToString(),
                        };
                        db.Events.Remove(eventToEdit);
                        db.SaveChanges();
                        AddEvent editForm = new AddEvent();
                        if (editForm.ShowDialog() == DialogResult.OK)
                        {
                            LoadEvents();
                        }
                        else
                        {
                            db.Events.Add(eventToEdit);
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

        /// <summary>
        /// Обработчик кнопки сохранения отчета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Excel Files|*.xlsx";
                saveDialog.Title = "Сохранить отчет";
                saveDialog.FileName = "Отчет_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".xlsx";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (ExcelPackage pck = new ExcelPackage())
                        {
                            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Отчет");

                            for (int i = 0; i < dataGridView1.Columns.Count; i++)
                            {
                                ws.Cells[1, i + 1].Value = dataGridView1.Columns[i].HeaderText;
                            }

                            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                            {
                                if (dataGridView1.Rows[i].IsNewRow)
                                {
                                    continue;
                                }
                                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                                {
                                    var cellValue = dataGridView1.Rows[i].Cells[j].Value;
                                    ws.Cells[i + 2, j + 1].Value = cellValue?.ToString() ?? "";
                                }
                            }
                            ws.Cells[ws.Dimension.Address].AutoFitColumns();
                            pck.SaveAs(new System.IO.FileInfo(saveDialog.FileName));

                            MessageBox.Show("Экспорт завершен успешно!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при экспорте: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dataGridView1.Rows[e.RowIndex].IsNewRow)
            {
                return;
            }
            try
            {
                var selectedId = (int)dataGridView1.Rows[e.RowIndex].Cells["EventId"].Value;

                using (var detailForm = new EventInformation())
                {
                    detailForm.DisplayEventDetails(selectedId);
                    detailForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии деталей события: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
