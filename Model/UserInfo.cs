using SqlSugar;
using System;
namespace BlankDemo.Model
{
	/// <summary>
	/// UserInfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class UserInfo
	{
		public UserInfo()
		{}
		#region Model
		private int _userid;
		private string _account;
		private string _password;
		private int _createuserid;
		private DateTime _createtime= DateTime.Now;
		private int? _lastupdateuserid;
		private DateTime? _lastupdatetime;
		private int _status=0;
        /// <summary>
        /// 用户编号
        /// </summary>
        
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int UserId
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 账号
		/// </summary>
		public string Account
		{
			set{ _account=value;}
			get{return _account;}
		}
		/// <summary>
		/// 密码
		/// </summary>
		public string Password
		{
			set{ _password=value;}
			get{return _password;}
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

