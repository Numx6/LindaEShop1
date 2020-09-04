using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LindaEShop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LindaEShop.Controllers
{
	public class OrderController : Controller
	{
		private IOrder _orderService;
		public OrderController(IOrder order)
		{
			_orderService = order;
		}
		public IActionResult Index()
		{
			return View();
		}

		[Authorize]
		public IActionResult BuyProduct(int id)
		{
			string userNumber = User.Identity.Name;
			_orderService.AddOrder(id,userNumber);
			return Redirect("/Admin/Products");
		}

	}
}
