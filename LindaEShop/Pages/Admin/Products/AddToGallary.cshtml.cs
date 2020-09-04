using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LindaEShop.Core.Services.Interfaces;
using LindaEShop.DataLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LindaEShop.Pages.Admin.Products
{
	public class AddToGallaryModel : PageModel
	{
		private IProduct _productService;
		public AddToGallaryModel(IProduct productService)
		{
			_productService = productService;
		}

		public void OnGet(int id)
		{
			ViewData["productId"] = id;
			ViewData["gallariImg"] = _productService.GetGallaryProduct(id);
		}

		public IActionResult OnPost(IFormFile imgUp, int id, int deleteId)
		{
			if (imgUp != null)
			{
				_productService.AddGallary(imgUp, id);
				return RedirectToPage("AddToGallary");
			}
			if (deleteId != 0)
			{
				_productService.DeleletFromGallary(deleteId);
			}

			ViewData["gallariImg"] = _productService.GetGallaryProduct(id);

			return Page();
		}
	}
}
