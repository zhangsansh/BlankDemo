using BlankDemo.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlankDemo.Forms
{
    public partial class FormLogin : Form
    {


        public FormLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text.Trim()) && !string.IsNullOrEmpty(textBox2.Text.Trim()))
            {
                UserInfo user = SqlSugerHepler.Db.Queryable<UserInfo>()
                    .Where(it => it.Account == textBox1.Text.Trim() && it.Password == textBox2.Text.Trim())
                    .First();
                LoginInfo.CurrentUser = user;
                if (user != null)
                {
                    DialogResult = DialogResult.OK;
                    MessageBox.Show("登录成功！");
                }
                else
                {
                    MessageBox.Show("账号或密码错误");
                }

            }
            else
            {
                MessageBox.Show("账号或者密码错误");
            }
        }
    }
}
