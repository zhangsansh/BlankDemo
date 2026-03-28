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
    public partial class FormMain : Form
    {



        public FormMain()
        {
            InitializeComponent();

            toolStripLabel1.Text = $"当前用户：【{LoginInfo.CurrentUser.Account}】";
            toolStripLabel2.Text = $"系统时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}";

        }

        public void OpenForm(Form form, bool isAdd = false)
        {
            foreach (var item in MdiChildren)
            {
                if(item.GetType() == form.GetType())
                {
                    item.Activate();
                    return ;
                }
            }

            if (!isAdd)
            {
                form.Show();
                form.MdiParent = this;
                form.WindowState = FormWindowState.Maximized;
            }

        }
        
        private void FormMain_Load(object sender, EventArgs e)
        {
            DataTable dt = SqlSugerHepler.Db.Queryable<UserInfo>()
                         .OrderByDescending(it => it.UserId)
                         .ToDataTable();
        }

        private void 用户列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           OpenForm(new UserList());
        }

        private void 用户添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new UserAdd(),false);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OpenForm(new UserList());
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            OpenForm(new UserAdd(), false);
        }

        private void toolStripLabel4_Click(object sender, EventArgs e)
        {

        }

        private void toolStripLabel5_Click(object sender, EventArgs e)
        {

        }

        private void 客户列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new CustomerList());
        }

        private void 客户添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new CustomerAdd(),false);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            OpenForm(new CustomerList());
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            OpenForm(new CustomerAdd(), false);
        }


        private void 产品列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new ProductList());
        }

        private void 产品添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new ProductAdd(),false);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            OpenForm(new ProductList());
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            OpenForm(new ProductAdd(), false);
        }

        private void 注销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void 退出系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
