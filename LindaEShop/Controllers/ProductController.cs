using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LindaEShop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LindaEShop.Controllers
{
	public class ProductController : Controller
	{
		private IProduct _productService;
		public ProductController(IProduct product)
		{
			_productService = product;
		}
		public IActionResult Index(int pageId = 1, string filter = ""
			, string orderBy = "n", int startPrice = 0
			, int endPrice = 0, int selectedGroup = 0)
		{
			var query = _productService.GetAllProductForShop(pageId, 4, filter, orderBy, startPrice, endPrice, selectedGroup);
			ViewBag.PageCount = query.Item2;
			return View(query.Item1);
		}

		public IActionResult ShowProduct(int id)
		{
			return View();
		}
	}
}
