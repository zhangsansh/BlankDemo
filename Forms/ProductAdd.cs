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
    public partial class ProductAdd : Form
    {
        private bool _isEdit = false;
        private AddressInfo _model = null;

        public ProductAdd(bool isEdit = false, AddressInfo model = null)
        {
            InitializeComponent();

            _isEdit = isEdit;
            _model = model;

           

            if (_isEdit && model != null)
            {
                this.Text = "更新地址信息";
                button1.Text = "更新";
                textBox1.Text = _model.ProvinceName;
                textBox2.Text = _model.City;
                textBox3.Text = _model.Area;
                textBox4.Text = _model.DetailAddress;
            }
            else
            {
                this.Text = "添加地址信息";
                button1.Text = "保存";
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_isEdit)
            {
                AddressInfo model = new AddressInfo() 
                {
                    AddressId = _model.AddressId,
                    ProvinceName = textBox1.Text.Trim(),
                    City = textBox2.Text.Trim(),
                    Area = textBox3.Text.Trim(),
                    DetailAddress = textBox4.Text.Trim(),
                    Status = 1,
                    CreateTime = _model.CreateTime,
                    CreateUserId = _model.CreateUserId,
                    LastUpdateUserId = _model.LastUpdateUserId,
                    LastUpdateTime = DateTime.Now,
                };

                SqlSugerHepler.Db.Updateable(model)
                    .WhereColumns(it => it.AddressId)
                    .ExecuteCommand();
            }
            else
            {
                AddressInfo model = new AddressInfo()
                {
                    //AddressId = _model.AddressId,
                    ProvinceName = textBox1.Text.Trim(),
                    City = textBox2.Text.Trim(),
                    Area = textBox3.Text.Trim(),
                    DetailAddress = textBox4.Text.Trim(),
                    CreateTime = DateTime.Now,
                    LastUpdateTime = DateTime.Now,
                    CreateUserId = _model.CreateUserId,
                    LastUpdateUserId = _model.LastUpdateUserId,
                    Status = 1,
                };

                SqlSugerHepler.Db.Insertable(model).ExecuteCommand();
            }

            this.DialogResult = DialogResult.OK;
            this.Close();

        }
    }
}
