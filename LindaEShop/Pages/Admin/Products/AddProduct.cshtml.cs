using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LindaEShop.Core.Services.Interfaces;
using LindaEShop.DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LindaEShop.Pages.Admin.Products
{
	[Authorize(Roles = "1")]
	public class AddProductModel : PageModel
	{
		private IProduct _productService;
		public AddProductModel(IProduct productService)
		{
			_productService = productService;
		}

		[BindProperty]
		public Product Product { get; set; }
		public void OnGet()
		{
			var groups = _productService.GetAllProductGroupsSelectList();
			ViewData["Groups"] = new SelectList(groups, "Value", "Text");

			ViewData["Color"] = _productService.GetAllColors();
			ViewData["Size"] = _productService.GetAllSize();
		}

		public IActionResult OnPost(IFormFile imgUp, List<int> selectedColor, List<int> selectedSize)
		{
			if (ModelState.IsValid)
			{
				Product.ProductCode = Product.ProductCode.Trim();
				if (_productService.CodeProductIsExist(Product.ProductCode))
				{
					ModelState.AddModelError("ProductCode", "کد محصول تکراری می باشد . کد کحصول جدید انتخاب کنید");
					return Page();
				}
				else
				{
					_productService.AddProduct(Product, imgUp, selectedColor, selectedSize);
					return RedirectToPage("index");
				}
			}

			var groups = _productService.GetAllProductGroupsSelectList();
			ViewData["Groups"] = new SelectList(groups, "Value", "Text");

			ViewData["Color"] = _productService.GetAllColors();
			ViewData["Size"] = _productService.GetAllSize();

			return Page();
		}
	}
}
