using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using LindaEShop.Core.DTOs;
using LindaEShop.Core.Services.Interfaces;
using LindaEShop.DataLayer.Entities;
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
			var product = _productService.GetProductForShow(id);
			var showProduct = new BuyProductViewModel()
			{
				Product = product
			};

			if (product == null)
			{
				return NotFound();
			}

			ViewData["colors"] = _productService.GetAllColors();
			ViewData["sizes"] = _productService.GetAllSize();

			return View(showProduct);
		}

		[HttpPost]
		public IActionResult ShowProduct(BuyProductViewModel showProduct, int productId)
		{
			showProduct.Product = _productService.GetProductForShow(productId);
			ViewData["colors"] = _productService.GetAllColors();
			ViewData["sizes"] = _productService.GetAllSize();

			if (ModelState.IsValid)
			{
				if (showProduct.SizeId == 0)
				{
					ModelState.AddModelError("SizeId", "انتخاب سایز اجباری است !");
					return View(showProduct);
				}
				if (showProduct.ColorId == 0)
				{
					ModelState.AddModelError("ColorId", "انتخاب رنگ اجباری  است !");
					return View(showProduct);
				}
				if (showProduct.quantityNumber < 1)
				{
					ModelState.AddModelError("quantityNumber", "کمترین تعداد سفارش 1 عدد می باشد !");
					return View(showProduct);
				}
				return RedirectToAction("BuyProduct", "Order", new
				{
					colorId = showProduct.ColorId,
					sizeId = showProduct.SizeId,
					productId = showProduct.Product.Id,
					quantityNumber=showProduct.quantityNumber
				});
			}

			return View(showProduct);
		}
	}
}
