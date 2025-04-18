using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EducationalSeminars_4team_Novikova_Nastya
{
    /// <summary>
    /// Класс для формы авторизации
    /// </summary>
    public partial class Authorization : Form
    {
        private const string OrganizerPassword = "administrator555";
        private const string OrganizerLogin = "nastya";
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
            var isLoginCorrect = (txtBoxLogin.Text == OrganizerLogin);
            var isPasswordCorrect = (txtBoxPassword.Text == OrganizerPassword);

            if (txtBoxPassword.Text == OrganizerPassword && txtBoxLogin.Text == OrganizerLogin)
            {
                var eventTable = new EventTable(isCustomer: false);
                eventTable.Show();
            }
            else 
            {
                if (!isLoginCorrect && !isPasswordCorrect)
                {
                    MessageBox.Show("Неверный логин и пароль", "Ошибка",MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!isLoginCorrect)
                {
                    MessageBox.Show("Неверный логин", "Ошибка",MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Неверный пароль", "Ошибка",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtBoxPassword.Focus();
                }
            }
            
        }

        /// <summary>
        /// Обработчик нажатия кнопки вход как гость
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogInAsACustomer_Click(object sender, EventArgs e)
        {
            FullUp();
            var eventTable = new EventTable(isCustomer: true);
            eventTable.Show();
            this.Hide();
        }

        /// <summary>
        /// Метод для проверки заполнены ли текстовые поля логина и пароля
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FullUp()
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
            FullUp();
            KeyPress(sender,e);
        }

        private void KeyPress(object sender, KeyPressEventArgs e)
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
            KeyPress(sender, e);
        }
    }
}
