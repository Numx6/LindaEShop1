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
			var query = _productService.GetAllProductForShop(pageId,12, filter, orderBy, startPrice, endPrice, selectedGroup);
			ViewBag.PageCount = query.Item2;
			ViewBag.selectedGroup = selectedGroup;
			ViewBag.endPrice = endPrice;
			ViewBag.startPrice = startPrice;
			ViewBag.filter = filter;
			ViewBag.orderBy = orderBy;
			ViewBag.pageId = pageId;

			return View(query.Item1);
		}

		public IActionResult ShowProduct(int id)
		{
			var product = _productService.GetProductForShow(id);
			product.Visit++;
			_productService.EditProduct(product);

			if (product != null)
			{
				var showProduct = new BuyProductViewModel()
				{
					Product = product
				};

				ViewData["colors"] = _productService.GetAllColors();
				ViewData["sizes"] = _productService.GetAllSize();

				return View(showProduct);
			}

			return NotFound();
		}

		[HttpPost]
		public IActionResult ShowProduct(BuyProductViewModel showProduct, int productId)
		{
			showProduct.Product = _productService.GetProductForShow(productId);
			ViewData["colors"] = _productService.GetAllColors();
			ViewData["sizes"] = _productService.GetAllSize();

			if (ModelState.IsValid)
			{
				if (showProduct.Product.IsActive == false)
				{
					ModelState.AddModelError("quantityNumber", "محصول غیر فعال می باشد");
					return View(showProduct);
				}
				if (showProduct.Product.Count <= 0)
				{
					ModelState.AddModelError("quantityNumber", "موجود نمی باشد");
					return View(showProduct);
				}
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
				if (showProduct.quantityNumber > showProduct.Product.Count)
				{
					ModelState.AddModelError("quantityNumber", "تعداد بیشتر از موجودی محصول می باشد");
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
