using System;
using System.Windows.Forms;

namespace TEST
{
    internal static class Program
    {
        static void Main()
        {
            // �������������� ���������� � ��������� �����
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}
