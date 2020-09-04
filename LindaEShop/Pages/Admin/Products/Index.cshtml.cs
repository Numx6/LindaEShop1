using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LindaEShop.Core.DTOs;
using LindaEShop.Core.Services.Interfaces;
using LindaEShop.DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LindaEShop.Pages.Admin.Products
{
    public class IndexModel : PageModel
    {
        private IProduct _productService;
		public IndexModel(IProduct productService)
		{
            _productService = productService;
		}

        public IndexProductAdminPanelViewModel IndexProduct { get; set; }
        public void OnGet(int pageId = 1, string codeFilter = "", string nameFilter = "")
        {
            IndexProduct = _productService.GetAllProduct(pageId,codeFilter,nameFilter);
        }
    }
}
