using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EducationalSeminars_4team_Novikova_Nastya
{
    public partial class Authorization : Form
    {
        private const string OrganizerPassword = "administrator555";
        public Authorization()
        {
            InitializeComponent();
            txtBoxPassword.PasswordChar = '•';
        }

        /// <summary>
        /// Обработчик нажатия кнопки входа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEnter_Click(object sender, EventArgs e)
        {
            if(txtBoxPassword.Text == OrganizerPassword)
            {
                EventTable eventTable = new EventTable(isCustomer: false);
                eventTable.Show();
            }
            else
            {
                MessageBox.Show("Неверный пароль администратора", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBoxPassword.Focus();
            }
           
        }

        /// <summary>
        /// Обработчик нажатия кнопки вход как гость
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogInAsACustomer_Click(object sender, EventArgs e)
        {
            EventTable eventTable = new EventTable(isCustomer: true);
            eventTable.Show();
            this.Hide();
        }

        /// <summary>
        /// Метод для проверки заполнены ли текстовые поля логина и пароля
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FullUp(object sender, EventArgs e)
        {
            if (txtBoxLogin == null)
            {
                MessageBox.Show("Введите логин");
            }
            if (txtBoxPassword == null) 
            {
                MessageBox.Show("Введите пароль");
            }
        }

        /// <summary>
        /// Метод для ввода пароля только английскими буквами и цифрами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBoxPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
                return;
            }
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"^[a-zA-Z0-9]$"))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Метод для ввода логина только английскими буквами и цифрами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBoxLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
                return;
            }
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"^[a-zA-Z0-9]$"))
            {
                e.Handled = true;
            }
        }
    }
}
