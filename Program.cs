using System;
using System.Windows.Forms;

namespace TEST
{
    internal static class Program
    {
        static void Main()
        {
            // »нициализируем приложение и запускаем форму
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}
