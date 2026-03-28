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
    public partial class ProductList : Form
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
        public ProductList()
        {
            InitializeComponent();
            if (comboBox1.Items.Count == 0)
            {
                comboBox1.Items.AddRange(new object[] { "10", "20", "30", "50", "100" });
                comboBox1.SelectedIndex = 3;
            }
            LoadData();
            // ======================
            // 所有控件 手动代码绑定
            // ======================
            this.button1.Click += new EventHandler(button1_Click);
            this.button2.Click += new EventHandler(button2_Click);
            this.button3.Click += new EventHandler(button3_Click);
            this.button4.Click += new EventHandler(button4_Click);
            this.button5.Click += new EventHandler(button5_Click);
            this.button6.Click += new EventHandler(button6_Click);
            this.button7.Click += new EventHandler(button7_Click);
            this.button8.Click += new EventHandler(button8_Click);
            this.button9.Click += new EventHandler(button9_Click);
            this.comboBox1.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);
        }

        /// <summary>
        /// 加载数据（核心修复）
        /// </summary>
        public void LoadData()
        {
            // 1. 构建查询对象
            var query = SqlSugerHepler.Db.Queryable<AddressInfo>();

            // =========================================
            // 条件1：时间范围（永远生效）
            // =========================================
            //2026/3/28 14:42
            DateTime defaultDate = new DateTime(2026, 3, 1, 0, 0, 0);

            DateTime startTime = dateTimePicker1.Value.Date;
            DateTime endTime = dateTimePicker2.Value.Date; // 当天 23:59:59
            if (startTime != endTime)
            {
                query = query.Where(it => it.CreateTime >= startTime && it.CreateTime <= endTime);
            }
            // =========================================
            // 条件2：账号精确查询
            // =========================================
            string keyword = textBox1.Text.Trim();
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(it => it.DetailAddress.Contains(keyword));
            }

            // =========================================
            // 分页查询 + 获取总条数（关键修复）
            // =========================================
            dataGridView1.DataSource = query
                .Where(it => it.Status == 1)
                .OrderByDescending(it => it.AddressId)
                .ToPageList(pageIndex, pageSize, ref totalCount); // ref 自动赋值 totalCount

            UpdatePageInfo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        /// <summary>
        /// 更新分页信息
        /// </summary>
        public void UpdatePageInfo()
        {
            int totalPage = (totalCount + pageSize - 1) / pageSize;
            label5.Text = $"第 {pageIndex} 页 / 共 {totalPage} 页  总记录数：{totalCount}";
        }

        // 查询
        private void button2_Click(object sender, EventArgs e)
        {
            pageIndex = 1;
            LoadData();
        }

        // 上一页
        private void button3_Click(object sender, EventArgs e)
        {
            if (pageIndex > 1)
                pageIndex--;

            LoadData();
        }

        // 下一页（修复判断逻辑）
        private void button4_Click(object sender, EventArgs e)
        {
            int totalPage = (totalCount + pageSize - 1) / pageSize;
            if (pageIndex < totalPage)
                pageIndex++;

            LoadData();
        }

        // 最后一页
        private void button5_Click(object sender, EventArgs e)
        {
            int totalPage = (totalCount + pageSize - 1) / pageSize;
            pageIndex = totalPage;
            LoadData();
        }

        // 切换每页条数
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(comboBox1.Text, out int size))
            {
                pageSize = size;
                pageIndex = 1;
                LoadData();
            }
        }

        // 跳页
        private void button6_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox2.Text, out int targetPage) && targetPage > 0)
            {
                int totalPage = (totalCount + pageSize - 1) / pageSize;
                pageIndex = Math.Min(Math.Max(targetPage, 1), totalPage);
            }
            LoadData();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选择要删除的数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show($"确定要删除选中的 {dataGridView1.SelectedRows.Count} 个数据吗？",
                "确认批量删除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    // 收集所有选中的用户ID
                    List<int> idList = new List<int>();
                    // 遍历所有选中的行并删除
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        if (row.IsNewRow) continue;
                        int AddressId = Convert.ToInt32(row.Cells["AddressId"].Value);
                        idList.Add(AddressId);
                    }

                    // ====================== 【核心：批量更新字段】 ======================
                    // 这里可以改任意字段！示例：更新状态、姓名、性别、时间、开关等
                    int count = SqlSugerHepler.Db.Updateable<AddressInfo>()
                        .SetColumns(it => new AddressInfo
                        {
                            // 在这里写你要更新的字段（可多个）
                            Status = 0,              // 例：状态改为2
                                                     // 例：禁用账号
                                                     // Age = 30,
                                                     // UpdateTime = DateTime.Now
                        })
                        .Where(it => idList.Contains(it.AddressId)) // 只更新选中的ID
                        .ExecuteCommand();


                    // 刷新数据列表
                    LoadData();
                    //MessageBox.Show("批量删除成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"批量删除失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int id = int.Parse(dataGridView1.SelectedRows[0].Cells["AddressId"].Value.ToString());
            AddressInfo info = SqlSugerHepler.Db.Queryable<AddressInfo>().Where(it => it.AddressId == id).First();
            ProductAdd productAdd = new ProductAdd(true, info);
            if (productAdd.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            //int id = int.Parse(dataGridView1.SelectedRows[0].Cells["AddressId"].Value.ToString());
            //AddressInfo info = SqlSugerHepler.Db.Queryable<AddressInfo>().Where(it => it.AddressId == id).First();
            AddressInfo info = new AddressInfo();
            ProductAdd productAdd = new ProductAdd(false, info);
            if (productAdd.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }
    }
}
