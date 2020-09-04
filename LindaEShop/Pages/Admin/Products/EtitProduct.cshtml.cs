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
    public class EtitProductModel : PageModel
    {
        private IProduct _productService;
        public EtitProductModel(IProduct productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public Product Product { get; set; }
        public void OnGet(int id)
        {
            Product = _productService.GetProductyId(id);

            ViewData["SizeId"] = _productService.GetSizeOfProduct(id);
            ViewData["ColorId"] = _productService.GetColorOfProduct(id);

            ViewData["Color"] = _productService.GetAllColors();
            ViewData["Size"] = _productService.GetAllSize();
        }

		public IActionResult OnPost(IFormFile imgUp, List<int> selectedColor, List<int> selectedSize)
		{
			if (ModelState.IsValid)
			{
                _productService.EditProduct(Product, imgUp, selectedColor, selectedSize);
                return RedirectToPage("index");
			}

            ViewData["SizeId"] = _productService.GetSizeOfProduct(Product.Id);
            ViewData["ColorId"] = _productService.GetColorOfProduct(Product.Id);

            ViewData["Color"] = _productService.GetAllColors();
            ViewData["Size"] = _productService.GetAllSize();

            return Page();
		}
	}
}