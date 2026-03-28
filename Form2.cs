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

namespace BlankDemo
{
    public partial class Form2 : Form
    {
        /// <summary>
        /// 每页条数
        /// </summary>
        public int pageSize { get; set; } = 10;

        /// <summary>
        /// 当前页码
        /// </summary>
        public int pageIndex { get; set; } = 1;

        /// <summary>
        /// 总记录数（自动赋值）
        /// </summary>
        private int totalCount;


        public Form2()
        {
            InitializeComponent();
            if (comboBox1.Items.Count == 0)
            {
                comboBox1.Items.AddRange(new object[] { "10", "20", "30", "50", "100" });
                comboBox1.SelectedIndex = 3;
            }
            LoadData();
        }

        public void LoadData()
        {
            //dataGridView1.DataSource = SqlSugerHepler.Db.Queryable<UserInfo>().ToList();
            ucList1.LoadData<UserInfo>();
        }

        private void ucList1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 更新分页信息显示
        /// </summary>
        public void UpdatePageInfo()
        {
            int totalPage = (totalCount + pageSize - 1) / pageSize;
            label5.Text = $"第 {pageIndex} 页 / 共 {totalPage} 页  总记录数：{totalCount}";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pageIndex = 1;
            ucList1.LoadData<UserInfo>();
            UpdatePageInfo();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pageIndex > 1)
            {
                pageIndex--;
            }
            ucList1.LoadData<UserInfo>();
            UpdatePageInfo();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (pageIndex < totalCount)
            {
                pageIndex++;
            }
            ucList1.LoadData<UserInfo>();
            UpdatePageInfo();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int totalPage = (totalCount + pageSize - 1) / pageSize;
            pageIndex = totalPage;
            UpdatePageInfo();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(comboBox1.Text, out int size))
            {
                pageSize = size;
                pageIndex = 1; // 切换条数自动回到第一页
                ucList1.LoadData<UserInfo>();
                UpdatePageInfo();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox2.Text, out int targetPage) && targetPage > 0)
            {
                int totalPage = (totalCount + pageSize - 1) / (int)pageSize;
                pageIndex = Math.Min(Math.Max(targetPage, 1), totalPage);
            }
            ucList1.LoadData<UserInfo>();
            UpdatePageInfo();
        }

    }
}
