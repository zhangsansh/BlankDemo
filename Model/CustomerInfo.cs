using SqlSugar;
using System;
namespace BlankDemo.Model
{
	/// <summary>
	/// CustomerInfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class CustomerInfo
	{
		public CustomerInfo()
		{}
		#region Model
		private int _customerid;
		private string _customername;
		private bool _sex;
		private DateTime _birthday;
		private string _phone;
		private int _addressid;
		private int _createuserid;
		private DateTime _createtime= DateTime.Now;
		private int? _lastupdateuserid;
		private DateTime? _lastupdatetime;
		private int _status=0;
        /// <summary>
        /// 客户编码
        /// </summary>
        /// 
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int CustomerId
		{
			set{ _customerid=value;}
			get{return _customerid;}
		}
		/// <summary>
		/// 客户姓名
		/// </summary>
		public string CustomerName
		{
			set{ _customername=value;}
			get{return _customername;}
		}
		/// <summary>
		/// 性别
		/// </summary>
		public bool Sex
		{
			set{ _sex=value;}
			get{return _sex;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Birthday
		{
			set{ _birthday=value;}
			get{return _birthday;}
		}
		/// <summary>
		/// 手机
		/// </summary>
		public string Phone
		{
			set{ _phone=value;}
			get{return _phone;}
		}
		/// <summary>
		/// 客户地址主键（对应AddressInfo表中的主键）
		/// </summary>
		public int AddressId
		{
			set{ _addressid=value;}
			get{return _addressid;}
		}
		/// <summary>
		/// 创建人（对应的UserInfo表中的UserId主键）
		/// </summary>
		public int CreateUserId
		{
			set{ _createuserid=value;}
			get{return _createuserid;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// 最后次修改人（对应UserInfo表中的主键UserId)
		/// </summary>
		public int? LastUpdateUserId
		{
			set{ _lastupdateuserid=value;}
			get{return _lastupdateuserid;}
		}
		/// <summary>
		/// 最后一次修改时间
		/// </summary>
		public DateTime? LastUpdateTime
		{
			set{ _lastupdatetime=value;}
			get{return _lastupdatetime;}
		}
		/// <summary>
		/// 数据状态（0正常，1删除）
		/// </summary>
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		#endregion Model

	}
}

