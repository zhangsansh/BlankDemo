using System;
namespace BlankDemo.Model
{
	/// <summary>
	/// StudentInfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class StudentInfo
	{
		public StudentInfo()
		{}
		#region Model
		private int _studentid;
		private string _studentname;
		private int? _score;
		/// <summary>
		/// 
		/// </summary>
		public int StudentId
		{
			set{ _studentid=value;}
			get{return _studentid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string StudentName
		{
			set{ _studentname=value;}
			get{return _studentname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Score
		{
			set{ _score=value;}
			get{return _score;}
		}
		#endregion Model

	}
}

