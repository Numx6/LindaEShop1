using LindaEShop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LindaEShop.ViewComponents
{
	public class ProductGroupComponent:ViewComponent
	{
		private IProduct _productService;
		public ProductGroupComponent(IProduct product)
		{
			_productService = product;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return await Task.FromResult((IViewComponentResult) View("ProductGroup",_productService.GetAllproductGroups()));
		}
	}
}
