using LindaEShop.Core.DTOs;
using LindaEShop.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LindaEShop.Core.Services.Interfaces
{
	public interface IUser
	{

		#region Get
		public User GetUserByNumber(string number);

		public int GetUserIdByUserName(string userName);

		public List<User> GetAllUsers();

		#endregion

		#region Login-Register-resat
		public bool IsExistUserNumber(string number);

		public int AddUser(User user);

		public User LoginUser(LogInViewModel logIn);

		public User GetUserByActiveCode(string activeCode);

		public void UpdateUser(User user);

		#endregion

		#region SmsChimp

		public void AddSmsChimp(string number);

		#endregion

	}
}
