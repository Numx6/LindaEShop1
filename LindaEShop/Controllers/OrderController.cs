using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LindaEShop.Core;
using LindaEShop.Core.DTOs;
using LindaEShop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LindaEShop.Controllers
{
	public class OrderController : Controller
	{
		private IOrder _orderService;
		public OrderController(IOrder order)
		{
			_orderService = order;
		}

		[Authorize]
		public IActionResult BuyProduct(int productId, int sizeId, int colorId, int quantityNumber)
		{
			string userNumber = User.FindFirst(ClaimTypes.Email)?.Value;
			_orderService.AddOrder(productId, userNumber, sizeId, colorId, quantityNumber);
			Sms.SendSms("09372796350", "افزودن به سبد خرید ."+ userNumber);

			return Redirect("/UserPanel");
		}

		[Authorize(Roles = "1")]
		public IActionResult TakingToOrderPackaging(int id)
		{
			_orderService.EditTakingToOrderPackaging(id);

			return Redirect("/Admin/Orders");
		}

		[Authorize(Roles = "1")]
		public IActionResult OrderPackagingToTaking(int id)
		{
			_orderService.EditOrderPackagingToTaking(id);

			return Redirect("/Admin/Orders/FinalOrders");
		}

	}
}
