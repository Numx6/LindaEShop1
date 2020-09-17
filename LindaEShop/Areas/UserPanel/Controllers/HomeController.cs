using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LindaEShop.Core;
using LindaEShop.Core.Security;
using LindaEShop.Core.Services.Interfaces;
using LindaEShop.DataLayer.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace TopLearn.Web.Areas.UserPanel.Controllers
{
	[Area("UserPanel")]
	[Authorize]
	//[PermissionChecker(1)]
	public class HomeController : Controller
	{
		private IOrder _orderService;

		public HomeController(IOrder _order)
		{
			_orderService = _order;
		}

		public IActionResult Index()
		{
			Order order;
			string userName = User.FindFirst(ClaimTypes.Email)?.Value;

			if (_orderService.GetOrderForuserPanel(userName) == null)
			{
				return Redirect("/");
			}
			else
			{
				order = _orderService.GetOrderForuserPanel(userName);
				return View(order);
			}
		}

		public IActionResult FinalyOrder(int id)
		{
			//TO DO پرداخت اینترنتی

			string userNumber = User.FindFirst(ClaimTypes.Email)?.Value;

			if (_orderService.FinalyOrder(userNumber, id))
			{
				//TO DO و کد پیگیری و اینا ارسال به صفحه ای برای نمایش تکمیل شدن خرید
				//TO DO ارسال اس ام اس  شامل کد پیگیری و از اینجور چیزا

				Sms.SendSms(userNumber, "کاربر گرامی خرید شما با موفقیت انجام شد .");

				return null;
			}
			else
			{
				return BadRequest();
			}

		}

		public IActionResult DeleteDetileInvoice(int id)
		{
			_orderService.DeleteDetileInvoice(id);

			return Redirect("/UserPanel");
		}

	}
}