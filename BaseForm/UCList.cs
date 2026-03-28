using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlankDemo.BaseForm
{
    public partial class UCList : UserControl
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

        public UCList()
        {
            InitializeComponent();

            
        }


        public void LoadData<T>()
        {
            // 1. 构建查询对象（关键：先创建，再逐步拼接条件）
            dataGridView1.DataSource = SqlSugerHepler.Db.Queryable<T>().ToPageList(pageIndex, pageSize);
        }

        public void LoadData<T>(int pageIndex,int pageSize)
        {
            // 1. 构建查询对象（关键：先创建，再逐步拼接条件）
            dataGridView1.DataSource = SqlSugerHepler.Db.Queryable<T>().ToPageList(pageIndex, pageSize);
        }


        //public void LoadData<T>(Expression<Func<T, DateTime>> timeField)
        //{
        //    if(dateTimePicker1.Value != DateTime.Now && dateTimePicker2.Value != DateTime.Now)
        //    {
        //        DateTime startTime = dateTimePicker1.Value.Date;
        //        DateTime endTime = dateTimePicker2.Value.AddDays(1).AddSeconds(-1);

        //        dataGridView1.DataSource = SqlSugerHepler.Db.Queryable<T>().Where(it => SqlFunc.Between(timeField, startTime, endTime))
        //                             .ToList();
        //    }
        //    if (!string.IsNullOrEmpty(textBox1.Text))
        //    {
        //        dataGridView1.DataSource = SqlSugerHepler.Db.Queryable<T>().Where()
        //                            .ToList();
        //    }
        //}

        /// <summary>
        /// 通用多条件查询：时间范围 + 文本框模糊查询
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="timeField">时间字段表达式：t => t.CreateTime</param>
        /// <param name="searchField">模糊查询字段表达式：t => t.Name</param>
        public void LoadData<T>(
            Expression<Func<T, DateTime>> timeField,
            Expression<Func<T, string>> searchField)
        {
            // 1. 构建查询对象（关键：先创建，再逐步拼接条件）
            var query = SqlSugerHepler.Db.Queryable<T>();

            // ==============================================
            // 条件1：时间范围（只要用户选了时间就生效，不判断 == Now）
            // ==============================================
            DateTime startTime = dateTimePicker1.Value.Date;
            DateTime endTime = dateTimePicker2.Value.Date.AddDays(1).AddSeconds(-1); // 23:59:59

            //if (dateTimePicker1.Value != DateTime.Now && dateTimePicker2.Value != DateTime.Now)
            //{
            //    // 时间永远生效，去掉你原来错误的判断
            //    query = query.Where(it => SqlFunc.Between(timeField, startTime, endTime));
            //}
            // =========================================
            // 时间条件（修复版 —— 不再用外部表达式传参）
            // =========================================
            if (dateTimePicker1.Value != DateTime.Now && dateTimePicker2.Value != DateTime.Now)
            {
                query = query.Where(
                    Expression.Lambda<Func<T, bool>>(
                        Expression.GreaterThanOrEqual(timeField.Body, Expression.Constant(startTime)),
                        timeField.Parameters[0]
                    )
                );
                query = query.Where(
                    Expression.Lambda<Func<T, bool>>(
                        Expression.LessThanOrEqual(timeField.Body, Expression.Constant(endTime)),
                        timeField.Parameters[0]
                    )
                );
            }


            // ==============================================
            // 条件2：文本框模糊查询（有内容才生效）
            // ==============================================
            string keyword = textBox1.Text.Trim();
            if (!string.IsNullOrEmpty(keyword))
            {
                // 拼接完整表达式，解决所有报错
                var parameter = Expression.Parameter(typeof(T));
                var left = Expression.Invoke(searchField, parameter);
                var right = Expression.Constant(keyword);
                var contains = Expression.Call(left, "Contains", Type.EmptyTypes, right);
                var lambda = Expression.Lambda<Func<T, bool>>(contains, parameter);
                // ✅ 正确：使用 SqlFunc.Contains 支持表达式树
                query = query.Where(lambda);
            }

            // ==============================================
            // 最终执行查询并绑定
            // ==============================================
            dataGridView1.DataSource = query.ToPageList(pageIndex,pageSize);
        }
    }
}
