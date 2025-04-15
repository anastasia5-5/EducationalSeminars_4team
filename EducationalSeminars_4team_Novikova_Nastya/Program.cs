using System;
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
           
            using(EventDatabase db = new EventDatabase())
            {
                db.Database.EnsureCreated();
            }
              
            Application.Run(new Authorization());
        }
    }
}
