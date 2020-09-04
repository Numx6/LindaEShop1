using LindaEShop.Core.DTOs;
using LindaEShop.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LindaEShop.Core.Services.Interfaces
{
	public interface IUser
	{
		public User GetUserByNumber(string number);
		public List<User> GetAllUsers();


		#region Login-Register-resat
		public bool IsExistUserNumber(string number);
		public int AddUser(User user);
		public User LoginUser(LogInViewModel logIn);
		public User GetUserByActiveCode(string activeCode);
		public void UpdateUser(User user);

		#endregion


	}
}
