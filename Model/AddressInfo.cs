using SqlSugar;
using System;
namespace BlankDemo.Model
{
	/// <summary>
	/// AddressInfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class AddressInfo
	{
		public AddressInfo()
		{}
		#region Model
		private int _addressid;
		private string _provincename;
		private string _city;
		private string _area;
		private string _detailaddress;
		private int _createuserid;
		private DateTime _createtime= DateTime.Now;
		private int? _lastupdateuserid;
		private DateTime? _lastupdatetime;
		private int _status=0;
        /// <summary>
        /// 地址编号
        /// </summary>
        /// 
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int AddressId
		{
			set{ _addressid=value;}
			get{return _addressid;}
		}
		/// <summary>
		/// 省份
		/// </summary>
		public string ProvinceName
		{
			set{ _provincename=value;}
			get{return _provincename;}
		}
		/// <summary>
		/// 城市
		/// </summary>
		public string City
		{
			set{ _city=value;}
			get{return _city;}
		}
		/// <summary>
		/// 区域
		/// </summary>
		public string Area
		{
			set{ _area=value;}
			get{return _area;}
		}
		/// <summary>
		/// 详细地址
		/// </summary>
		public string DetailAddress
		{
			set{ _detailaddress=value;}
			get{return _detailaddress;}
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

