using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LindaEShop.Core;
using LindaEShop.Core.DTOs;
using LindaEShop.Core.Security;
using LindaEShop.Core.Services.Interfaces;
using LindaEShop.DataLayer.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LindaEShop.Controllers
{
	public class AccountController : Controller
	{
		IUser _userService;
		public AccountController(IUser user)
		{
			_userService = user;
		}

		[Route("Register")]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[Route("Register")]
		public IActionResult Register(RegisterViewModel register)
		{
			if (ModelState.IsValid)
			{
				if (_userService.IsExistUserNumber(register.Number))
				{
					ModelState.AddModelError("Number", "شماره موبایل تکراری می باشد ! از شماره جدیدی استفاده کنید.");
					return View(register);
				}

				LindaEShop.DataLayer.Entities.User user = new User()
				{
					CreateDate = DateTime.Now,
					IsActive = true,
					ActivCode = new Random().Next(100000, 999999).ToString(),
					Number = register.Number.Trim(),
					Password = PasswordHelper.EncodePasswordMd5(register.Password),
					Name = register.Name,
					RoleId = 2
				};
				_userService.AddUser(user);
				Sms.SendSms(register.Number, "فروشگاه اینترنتی لیندا . ثبت نام شما با موفقیت انجام شد .");
				ViewBag.register = "ok";
				return Redirect("/");
			}
			return View(register);
		}

		[Route("LogIn")]
		public IActionResult LogIn(string ReturnUrl = "/")
		{
			ViewData["ReturnUrl"] = ReturnUrl;
			return View();
		}

		[HttpPost]
		[Route("LogIn")]
		public IActionResult LogIn(LogInViewModel logIn, string ReturnUrl)
		{
			if (ModelState.IsValid)
			{
				var user = _userService.LoginUser(logIn);
				if (user != null)
				{
					if (user.IsActive)
					{
						#region LogIn

						var claims = new List<Claim>()
							{
								new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
								new Claim(ClaimTypes.Name,user.Name),
								new Claim(ClaimTypes.Email,user.Number),
								new Claim(ClaimTypes.Role,user.RoleId.ToString())
							};

						var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
						var principal = new ClaimsPrincipal(identity);

						var properties = new AuthenticationProperties
						{
							IsPersistent = logIn.RememberMe
						};

						HttpContext.SignInAsync(principal, properties);

						#endregion

						return Redirect(ReturnUrl);
					}
					ModelState.AddModelError("Number", "حساب کاربری فعال نیست");
				}
				else
				{
					ModelState.AddModelError("Number", "کاربری یافت نشد");
				}
			}
			return View(logIn);
		}

		[Route("LogOut")]
		public IActionResult LogOut()
		{
			HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return Redirect("/LogIn");
		}

		[Route("ForgotPassword")]
		public IActionResult ForgotPassword()
		{
			return View();
		}

		[HttpPost]
		[Route("ForgotPassword")]
		public IActionResult ForgotPassword(ForgotPasswordViewModel forgot)
		{
			if (!ModelState.IsValid)
				return View(forgot);

			forgot.Number = forgot.Number.Trim();
			var user = _userService.GetUserByNumber(forgot.Number);
			if (user == null)
			{
				ModelState.AddModelError("Number", "کاربری یافت نشد !");
				return View(forgot);
			}
			else
			{
				Sms.SendSms(user.Number, $"فروشگاه اینترنتی لیندا . کاربر گرامی {user.Name} کدفعالسازی شما {user.ActivCode} می باشد .");
				return RedirectToAction("ResetPassword");
			}
		}

		[Route("ResetPassword")]
		public IActionResult ResetPassword()
		{
			return View();
		}

		[HttpPost]
		[Route("ResetPassword")]
		public IActionResult ResetPassword(ResetPasswordViewModel reset)
		{
			if (!ModelState.IsValid)
				return View(reset);

			User user = _userService.GetUserByActiveCode(reset.ActiveCode.Trim());
			if (user == null)
			{
				return NotFound();
			}
			string newPassword = PasswordHelper.EncodePasswordMd5(reset.Password);
			user.Password = newPassword;
			user.ActivCode = new Random().Next(100000, 999999).ToString();
			_userService.UpdateUser(user);

			ViewBag.ResetPassword = "ok";
			return Redirect("/LogIn");
		}
	}
}
