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
    public partial class CustomerAdd : Form
    {
        private bool _isEdit = false;
        private CustomerInfo _model = null;

        public CustomerAdd(bool isEdit = false, CustomerInfo model = null)
        {
            InitializeComponent();

            _isEdit = isEdit;
            _model = model;

            if (_isEdit && model != null)
            {
                this.Text = "更新客户信息";
                button1.Text = "更新";
                textBox1.Text = _model.CustomerName;
                textBox2.Text = _model.Sex == true ? "男" : "女";
                textBox3.Text = _model.Phone;
                dateTimePicker1.Value = _model.Birthday;
                //label5.Text = "修改时间";
                //dateTimePicker2.Value = DateTime.Now;


            }
            else
            {
                this.Text = "添加客户信息";
                button1.Text = "保存";
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                dateTimePicker1.Value = DateTime.Now;
                //label5.Text = "创建时间";
                //dateTimePicker2.Value = DateTime.Now;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_isEdit)
            {
                CustomerInfo model = new CustomerInfo()
                {
                    CustomerId = _model.CustomerId,
                    CustomerName = textBox1.Text.Trim(),
                    Sex = textBox2.Text.Trim() == "男" ? true : false,
                    Phone = textBox3.Text.Trim(),
                    Birthday = dateTimePicker1.Value,
                    CreateTime = _model.CreateTime,
                    LastUpdateTime = DateTime.Now,
                    CreateUserId = _model.CreateUserId,
                    LastUpdateUserId = _model.LastUpdateUserId,
                    Status = 1
                };

                SqlSugerHepler.Db.Updateable(model)
                    .WhereColumns(it => it.CustomerId)
                    .ExecuteCommand();

            }
            else
            {
                CustomerInfo model = new CustomerInfo()
                {
              
                    CustomerName = textBox1.Text.Trim(),
                    Sex = textBox2.Text.Trim() == "男" ? true : false,
                    Phone = textBox3.Text.Trim(),
                    Birthday = dateTimePicker1.Value,
                    CreateUserId = 2,
                    LastUpdateUserId = 1,
                    CreateTime = DateTime.Now,
                    LastUpdateTime = DateTime.Now,
                    Status = 1
                };

                SqlSugerHepler.Db.Insertable(model).ExecuteCommand();
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
