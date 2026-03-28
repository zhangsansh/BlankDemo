using BlankDemo.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlankDemo.Forms
{
    public partial class UserAdd : Form
    {
        private bool _isEdit = false;
        private UserInfo _model = null;

        // 正则常量（只编译一次，提升性能）
        private readonly Regex _regAccount = new Regex(@"^[A-Za-z0-9_]{6,12}$");
        private readonly Regex _regPassword = new Regex(@"^[a-zA-Z0-9]{6,12}$");

        public UserAdd(bool isEdit = false, UserInfo model = null)
        {
            InitializeComponent();
            _isEdit = isEdit;
            _model = model;
            InitForm(); // 抽成方法，代码更清晰
        }

        /// <summary>
        /// 初始化窗体（新增/编辑）
        /// </summary>
        private void InitForm()
        {
            if (_isEdit && _model != null)
            {
                this.Text = "更新用户信息";
                button1.Text = "保存";
                //textBox1.Text = _model.UserId.ToString();
                textBox2.Text = _model.Account;
                textBox3.Text = _model.Password;
                label4.Text = "修改时间";
                dateTimePicker1.Value = (DateTime)_model.LastUpdateTime;

                // 编辑时用户ID不允许修改
                //textBox1.ReadOnly = true;
            }
            else
            {
                this.Text = "添加用户";
                button1.Text = "添加";
                //textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                label4.Text = "创建时间";
                dateTimePicker1.Value = DateTime.Now;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 1. 前端校验（必须开启）
            if (!ValidateInput())
            {
                return;
            }

            try
            {
                if (_isEdit)
                {
                    // 编辑 → 使用 Update
                    UpdateUser();
                }
                else
                {
                    // 新增 → 使用 Insert
                    AddUser();
                }

                MessageBox.Show("操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"操作失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 输入校验
        /// </summary>
        private bool ValidateInput()
        {
            // 清空背景色
            textBox2.BackColor = Color.White;
            textBox3.BackColor = Color.White;

            // 账号校验
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                textBox2.BackColor = Color.LightCoral;
                MessageBox.Show("账号不能为空！");
                textBox2.Focus();
                return false;
            }
            if (!_regAccount.IsMatch(textBox2.Text))
            {
                textBox2.BackColor = Color.LightCoral;
                MessageBox.Show("账号只能包含字母、数字、下划线，长度6-12位！");
                textBox2.Focus();
                return false;
            }

            // 密码校验
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                textBox3.BackColor = Color.LightCoral;
                MessageBox.Show("密码不能为空！");
                textBox3.Focus();
                return false;
            }
            if (!_regPassword.IsMatch(textBox3.Text))
            {
                textBox3.BackColor = Color.LightCoral;
                MessageBox.Show("密码只能包含字母、数字，长度6-12位！");
                textBox3.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        private void AddUser()
        {
            var model = new UserInfo()
            {
                Account = textBox2.Text.Trim(),
                Password = textBox3.Text.Trim(),
                Status = _model.Status,
                LastUpdateUserId = 1,
                CreateUserId = 2,
                CreateTime = DateTime.Now,
                LastUpdateTime = DateTime.Now // 建议统一加上
            };

            // 新增必须用 Insertable
            SqlSugerHepler.Db.Insertable(model).ExecuteCommand();
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        private void UpdateUser()
        {
            var model = new UserInfo()
            {
                UserId = _model.UserId,
                Account = textBox2.Text.Trim(),
                Password = textBox3.Text.Trim(),
                Status = _model.Status,
                LastUpdateUserId = 1,
                CreateUserId = 2,
                LastUpdateTime = DateTime.Now
            };

            SqlSugerHepler.Db.Updateable(model)
                  .WhereColumns(it => it.UserId)
                  .ExecuteCommand();
        }
    }
}