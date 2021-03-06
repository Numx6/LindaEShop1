﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LindaEShop.Models;
using Microsoft.AspNetCore.Authorization;
using LindaEShop.Core.Services.Interfaces;
using LindaEShop.Core;
using System.Security.Claims;

namespace LindaEShop.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private IProduct _productService;
		private IUser _userService;
		private IOrder _orderService;

		public HomeController(ILogger<HomeController> logger, IProduct product, IUser _user, IOrder _order)
		{
			_logger = logger;
			_productService = product;
			_userService = _user;
			_orderService = _order;
		}

		public IActionResult Index()
		{
			ViewData["popular"] = _productService.GetAllProductForShop(1, 8, "", "m", 0, 0, 0).Item1;
			return View(_productService.GetAllProductForShop(1, 4, "", "n", 0, 0, 0).Item1);
		}

		[Route("AboutUs")]
		public IActionResult AboutUs()
		{
			return View();
		}

		[Route("OnlinePayment/{id}")]
		public IActionResult OnlinePayment(int id)
		{
			string userNumber = User.FindFirst(ClaimTypes.Email)?.Value;
			string roleId = User.FindFirst(ClaimTypes.Role)?.Value;
			var order = _orderService.GetOrderByOrderId(id);

			#region admin pyment

			if (roleId == "1")
			{
				foreach (var item in order.OrderDetails)
				{
					_productService.UpdateCountProduct(item.ProductId, item.Count);
				}

				ViewBag.IsSuccess = true;
				order.IsFinaly = true;
				order.FinalyDate = DateTime.Now;
				_orderService.UpdateOrder(order);
				Sms.SendSms("09372796350", " خرید ادمین ." + userNumber);

				return View();
			}

			#endregion

			#region user payment

			if (HttpContext.Request.Query["Status"] != "" &&
				HttpContext.Request.Query["Status"].ToString().ToLower() == "ok"
				&& HttpContext.Request.Query["Authority"] != "")
			{
				string authority = HttpContext.Request.Query["Authority"];
				string Name = User.FindFirst(ClaimTypes.Name)?.Value;

				var payment = new Zarinpal.Payment("054939ee-3bc1-11ea-9822-000c295eb8fc", order.OrderSum + 15000);
				var res = payment.Verification(authority).Result;

				if (res.Status == 100)
				{
					foreach (var item in order.OrderDetails)
					{
						_productService.UpdateCountProduct(item.ProductId, item.Count);
					}

					ViewBag.code = res.RefId;
					ViewBag.IsSuccess = true;
					order.IsFinaly = true;
					order.FinalyDate = DateTime.Now;
					_orderService.UpdateOrder(order);
					Sms.SendSms(userNumber, $"{Name} عزیز خرید شما با موفقیت انجام شد . کد پیگیری : {res.RefId} ");
					Sms.SendSms("09372796350", "پرداخت فاکتور ." + userNumber);

					return View();
				}

				return View();
			}

			#endregion

			return View();
		}

		[HttpPost]
		public IActionResult SmsChimp(string number)
		{
			if (!string.IsNullOrEmpty(number))
			{
				number = number.Trim();
				_userService.AddSmsChimp(number);
			}

			return RedirectToAction("Index");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

	}
}
