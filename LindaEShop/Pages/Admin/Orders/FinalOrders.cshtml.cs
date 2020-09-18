using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LindaEShop.Core.Services.Interfaces;
using LindaEShop.DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LindaEShop.Pages.Admin.Orders
{
	[Authorize(Roles = "1")]
	public class FinalOrdersModel : PageModel
	{
		private IOrder _orderService;
		public FinalOrdersModel(IOrder order)
		{
			_orderService = order;
		}

		public List<Order> Orders { get; set; }
		public void OnGet()
		{
			Orders = _orderService.PakeagingOrdersToAdminPanel();
		}
	}
}
