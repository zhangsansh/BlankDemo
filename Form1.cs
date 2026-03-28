using BlankDemo.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlankDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if(!string.IsNullOrEmpty(textBox1.Text.Trim()) && !string.IsNullOrEmpty(textBox2.Text.Trim()))
            {
                UserInfo user = SqlSugerHepler.Db.Queryable<UserInfo>()
                        .Where(it => it.Account == textBox1.Text.Trim() && it.Password == textBox2.Text.Trim()) // 你的字段名
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
                MessageBox.Show("账号和密码不能为空！");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
