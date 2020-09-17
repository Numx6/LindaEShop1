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

namespace LindaEShop.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private IProduct _productService;
		private IUser _userService;

		public HomeController(ILogger<HomeController> logger,IProduct product,IUser _user)
		{
			_logger = logger;
			_productService = product;
			_userService = _user;
		}

		public IActionResult Index()
		{
			ViewData["popular"] = _productService.GetAllProductForShop(1, 8, "", "m", 0, 0, 0).Item1;
			return View(_productService.GetAllProductForShop(1,4,"","n",0,0,0).Item1);
		}

		[Route("AboutUs")]
		public IActionResult AboutUs()
		{
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
