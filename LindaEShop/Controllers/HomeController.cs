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

		public HomeController(ILogger<HomeController> logger,IProduct product)
		{
			_logger = logger;
			_productService = product;
		}

		public IActionResult Index()
		{
			return View(_productService.GetAllProductForShop(1,4,"","n",0,0,0).Item1);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

	}
}
