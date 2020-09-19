using System;
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
			if (HttpContext.Request.Query["Status"] != "" &&
				HttpContext.Request.Query["Status"].ToString().ToLower() == "ok"
				&& HttpContext.Request.Query["Authority"] != "")
			{
				string authority = HttpContext.Request.Query["Authority"];

				var order = _orderService.GetOrderByOrderId(id);
				string userNumber = User.FindFirst(ClaimTypes.Email)?.Value;
				string Name = User.FindFirst(ClaimTypes.Name)?.Value;

				var payment = new Zarinpal.Payment("054939ee-3bc1-11ea-9822-000c295eb8fc", order.OrderSum);
				var res = payment.Verification(authority).Result; 
				if (res.Status == 100)
				{
					ViewBag.code = res.RefId;
					ViewBag.IsSuccess = true;
					order.IsFinaly = true;
					_orderService.UpdateOrder(order);

					Sms.SendSms(userNumber, $"{Name} عزیز خرید شما با موفقیت انجام شد . کد پیگیری : {res.RefId} ");
				}
			}

			return View();
		}

		[HttpPost]
		public IActionResult SmsChimp(string number)
		{
			number = number.Trim();
			_userService.AddSmsChimp(number);

			return RedirectToAction("Index");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

	}
}
