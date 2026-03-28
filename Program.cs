using BlankDemo.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlankDemo
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            FormLogin form1 = new FormLogin();
            if (form1.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new FormMain());
            }

        }
    }
}
