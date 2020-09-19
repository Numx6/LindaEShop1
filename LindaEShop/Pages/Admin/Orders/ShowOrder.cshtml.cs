using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using LindaEShop.Core.DTOs;
using LindaEShop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LindaEShop.Pages.Admin.Orders
{
	[Authorize(Roles = "1")]
	public class ShowOrderModel : PageModel
	{
		private IOrder _orderService;
		public ShowOrderModel(IOrder order)
		{
			_orderService = order;
		}

		public ShowOrderForUserPanelViewModel showOrder { get; set; }
		public void OnGet(int id)
		{
			showOrder = _orderService.ShowOrderForUserPanel(id);
		}
	}
}
