using System;
using System.Data.Entity;
using System.Data.SQLite;
using System.Windows.Forms;

namespace EducationalSeminars_4team_Novikova_Nastya
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //SQLiteConnection.CreateFile("EventDatabase");
            
              
            Application.Run(new Authorization());
        }
    }
}
