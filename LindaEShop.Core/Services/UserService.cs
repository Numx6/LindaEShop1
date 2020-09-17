using LindaEShop.Core.DTOs;
using LindaEShop.Core.Security;
using LindaEShop.Core.Services.Interfaces;
using LindaEShop.DataLayer;
using LindaEShop.DataLayer.Context;
using LindaEShop.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LindaEShop.Core.Services
{
	public class UserService : IUser
	{
		private LindaContext _context;

		public UserService(LindaContext context)
		{
			_context = context;
		}

		public User GetUserByNumber(string number)
		{
			return _context.Users.SingleOrDefault(u => u.Number == number);
		}

		public List<User> GetAllUsers()
		{
			return _context.Users.ToList();
		}

		public User LoginUser(LogInViewModel logIn)
		{
			logIn.Password = PasswordHelper.EncodePasswordMd5(logIn.Password);
			return _context.Users.Include(r=>r.Role).SingleOrDefault(u => u.Number == logIn.Number.Trim() && u.Password == logIn.Password);	
		}

		public bool IsExistUserNumber(string number)
		{
			return _context.Users.Any(u => u.Number == number.Trim());
		}

		public int AddUser(User user)
		{
			_context.Users.Add(user);
			_context.SaveChanges();
			return user.UserId;
		}

		public User GetUserByActiveCode(string activeCode)
		{
			return _context.Users.SingleOrDefault(u => u.ActivCode == activeCode);
		}

		public void UpdateUser(User user)
		{
			_context.Users.Update(user);
			_context.SaveChanges();
		}

		public int GetUserIdByUserName(string userName)
		{
			return _context.Users.FirstOrDefault(u=>u.Number==userName).UserId;
		}

		public void AddSmsChimp(string number)
		{
			_context.SmsChimps.Add(new SmsChimp() { 
			 Number=number.Trim(),
			  CreatDate=DateTime.Now
			});

			_context.SaveChanges();
		}

		public int AddUserAddress(UserAddress userAddress,string userName)
		{
			int userId = GetUserIdByUserName(userName);

			userAddress.CreatDate = DateTime.Now;
			userAddress.UserId = userId;

			_context.UserAddresses.Add(userAddress);
			_context.SaveChanges();

			return userAddress.UserId;
		}

		public List<UserAddress> GetAllUserAddress()
		{
			return _context.UserAddresses.OrderByDescending(c => c.CreatDate).ToList();
		}
	}
}
